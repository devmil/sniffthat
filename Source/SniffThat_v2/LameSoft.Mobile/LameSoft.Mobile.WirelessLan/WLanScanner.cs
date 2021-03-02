//==========================================================================================
//
//		LameSoft.Mobile.WirelessLan.WLanScanner
//		Copyright (c) 2006-2007, LameSoft (www.lamesoft.de)
//
//      This Code is provided under the LGPL. For more Information see 
//      http://www.gnu.org/licenses/lgpl.html
//
//      This library is free software; you can redistribute it and/or
//      modify it under the terms of the GNU Lesser General Public
//      License as published by the Free Software Foundation; either
//      version 2.1 of the License, or (at your option) any later version.
//
//      This library is distributed in the hope that it will be useful,
//      but WITHOUT ANY WARRANTY; without even the implied warranty of
//      MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
//      Lesser General Public License for more details.
//
//      You should have received a copy of the GNU Lesser General Public
//      License along with this library; if not, write to the Free Software
//      Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA
//
//      You are not allowed to remove or change this header!
//
//      If you change the source code in this file, you have to add a changelog entry.
//
//--------------- Changelog ----------------------------------------------------------------  
//
//      - 27.12.2006 (Michael Lamers, info@lamesoft.de): 
//          * added header
//      - 08.02.2006 (Michael Lamers, info@lamesoft.de): 
//          * when stopping: Clear the Access Point List and send a notify
//
//==========================================================================================
using System;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.Net;
using System.Collections;
using System.Threading;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace LameSoft.Mobile.WirelessLan
{
    /// <summary>
    /// WLan Scanner to scan for visible Access Points
    /// </summary>
    public class WLanScanner : IWLanScanner
    {
        private Adapter _Adapter;

        private string _AdapterName;

        /// <summary>
        /// Gets the visible access points.
        /// </summary>
        /// <value>The visible access points.</value>
        public IEnumerable<AccessPoint> VisibleAccessPoints
        {
            get
            {
                return _AccessPoints;
            }
        }

        /// <summary>
        /// Gets or sets the name of the adapter.
        /// </summary>
        /// <value>The name of the adapter.</value>
        public string AdapterName
        {
            get
            {
                return _AdapterName;
            }
            set
            {
                _AdapterName = value;
                if (_Running && (_Adapter == null || _Adapter.Name != value))
                    Start();
            }
        }

        /// <summary>
        /// Gets or sets the interval.
        /// </summary>
        /// <value>The interval.</value>
        public int Interval
        {
            get
            {
                return _Interval;
            }
            set
            {
                _Interval = value;
            }
        }

        /// <summary>
        /// Gets or sets the fast interval.
        /// </summary>
        /// <value>The fast interval.</value>
        public int FastInterval
        {
            get
            {
                return _FastInterval;
            }
            set
            {
                _FastInterval = value;
            }
        }

        private IList<AccessPoint> _AccessPoints;

        public event VisibleAccessPointCollectionChangedEventHandler VisibleAccessPointCollectionChanged;

        /// <summary>
        /// Gets fired if something logable occurs
        /// </summary>
        public event LogEvent Log;

        private int _Interval = 0;
        private int _FastInterval = 200;

        private Timer _Timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:WLanScanner"/> class.
        /// </summary>
        public WLanScanner()
        {
            _AccessPoints = new List<AccessPoint>();

            _Timer = new Timer(new TimerCallback(Scan), this, Timeout.Infinite, Timeout.Infinite);
        }

        private bool _Running = false;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:WLanScanner"/> is running.
        /// </summary>
        /// <value><c>true</c> if running; otherwise, <c>false</c>.</value>
        public bool Running
        {
            get
            {
                lock (this)
                    return _Running;
            }
            set
            {
                lock (this)
                    _Running = value;
            }
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            bool found = false;
            foreach (Adapter adapter in Networking.GetAdapters())
            {
                if (adapter.Name == _AdapterName)
                {
                    found = true;
                    _Adapter = adapter;
                }
            }

            if (!found)
            {
                Stop();
                return;
            }
            Running = true;
            _Timer.Change(0, _Interval);
            SendLog("Starting Scanner");
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            _AccessPointCollection = null;
            Running = false;
            _Timer.Change(Timeout.Infinite, Timeout.Infinite);
            SendLog("Stopping Scanner");
            
            lock (this)
            {
                _AccessPoints.Clear();
            }

            SendChanged();
        }

        private void SendChanged()
        {
            lock (this)
            {
                if (VisibleAccessPointCollectionChanged != null)
                    VisibleAccessPointCollectionChanged(this, new VisibleAccessPointCollectionChangedEventArgs());
            }
        }

        private AccessPointCollection _AccessPointCollection = null;

        private bool _InScan = false;

        private void Scan(object o)
        {
            if (InternalScan(o) && Running)
                SendChanged();
        }

        private bool InternalScan(object lockObject)
        {
            bool result = false;
            if (!Monitor.TryEnter(this))
                return result;

            bool changed = false;

            try
            {
                try
                {
                    if ((_AccessPointCollection == null) || !_AccessPointCollection.AssociatedAdapter.Equals(_Adapter))
                    {
                        _AccessPointCollection = _Adapter.NearbyAccessPoints;
                    }
                    else
                    {
                        _AccessPointCollection.Refresh();
                    }
                }
                catch (Exception)
                {
                    _AccessPointCollection.Refresh();
                }

                List<AccessPoint> newAccessPoints = new List<AccessPoint>();

                lock (lockObject)
                {
                    foreach (OpenNETCF.Net.AccessPoint ap in _AccessPointCollection)
                    {
                        if (ap.InfrastructureMode == OpenNETCF.Net.InfrastructureMode.Infrastructure)
                        {
                            AccessPoint newAp = WrapAccessPoint(ap);
                            newAccessPoints.Add(newAp);
                            if (ApNewOrChanged(_AccessPoints, newAp))
                                changed = true;
                        }
                    }

                    for (int i = _AccessPoints.Count - 1; i >= 0; i--)
                    {
                        AccessPoint ap = _AccessPoints[i];
                        if (!ContainsAccessPoint(newAccessPoints, ap))
                        {
                            changed = true;
                            break;
                        }
                    }

                    if (changed)
                    {
                        _AccessPoints = newAccessPoints;
                        result = true;
                    }
                }
            }
            catch (Exception)
            {
                result = false;
            }

            Monitor.Exit(this);

            return result;
        }

        private AccessPoint WrapAccessPoint(OpenNETCF.Net.AccessPoint ap)
        {
            return new AccessPoint(ap.MacAddress, ap.Name, (Mode)ap.InfrastructureMode, ap.Privacy, ap.SignalStrengthInDecibels, (Strength)ap.SignalStrength.Strength, ap.SupportedRates);
        }

        private bool ContainsAccessPoint(IList<AccessPoint> accesspoints, AccessPoint accessPoint)
        {
            foreach (AccessPoint ap in accesspoints)
            {
                string macAddrAP = Convert.ToBase64String(ap.MacAddress);
                string macAddrAccessPoint = Convert.ToBase64String(accessPoint.MacAddress);
                if ((ap.Name.Equals(accessPoint.Name)) && (macAddrAP.Equals(macAddrAccessPoint)))
                    return true;
            }
            SendLog("SSID nicht in der Liste: " + accessPoint.Name);
            return false;
        }

        private bool ApNewOrChanged(IList<AccessPoint> accesspoints, AccessPoint accesspoint)
        {
            foreach (AccessPoint ap in accesspoints)
            {
                string macAddrAP = Convert.ToBase64String(ap.MacAddress);
                string macAddrAccessPoint = Convert.ToBase64String(accesspoint.MacAddress);
                if (ap.Name.Equals(accesspoint.Name) && macAddrAP.Equals(macAddrAccessPoint))
                {
                    SendLog("Checking AP \"" + ap.Name + "\", MAC: " + macAddrAP);
                    if (!ap.Strength.Equals(accesspoint.Strength))
                    {
                        SendLog("SignalStrength: " + ap.Strength.ToString() + ", " + accesspoint.Strength.ToString());
                        return true;
                    }
                    if (ap.Privacy != accesspoint.Privacy)
                    {
                        SendLog("Privacy: " + ap.Privacy.ToString() + ", " + accesspoint.Privacy.ToString());
                        return true;
                    }
                    if (ap.SignalStrengthInDecibels != accesspoint.SignalStrengthInDecibels)
                    {
                        SendLog("SignalStrengthInDecibels: " + ap.SignalStrengthInDecibels.ToString() + ", " + accesspoint.SignalStrengthInDecibels.ToString());
                        return true;
                    }
                    string supRateAP = Convert.ToBase64String(ap.SupportedRates);
                    string supRateAccessPoint = Convert.ToBase64String(accesspoint.SupportedRates);
                    if (!supRateAP.Equals(supRateAccessPoint))
                    {
                        SendLog("SupportedRates: " + supRateAP + ", " + supRateAccessPoint);
                        return true;
                    }
                    if (!ap.Mode.Equals(accesspoint.Mode))
                    {
                        SendLog("InfrastructureMode: " + ap.Mode.ToString() + ", " + accesspoint.Mode.ToString());
                        return true;
                    }

                    return false;
                }
            }
            return true;
        }

        private void SendLog(string log)
        {
            if (Log != null)
                Log(log);
        }

        /// <summary>
        /// Stores the access data.
        /// </summary>
        /// <param name="ssid">The ssid.</param>
        /// <param name="key">The key.</param>
        /// <param name="keyIndex">Index of the key.</param>
        /// <returns></returns>
        public bool StoreAccessData(string ssid, string key, int keyIndex)
        {
            bool result = false;

            try
            {
                EAPParameters parameters = new EAPParameters();
                result = _Adapter.SetWirelessSettingsAddEx(ssid, true, key, keyIndex, AuthenticationMode.Ndis802_11AuthModeAutoSwitch, WEPStatus.Ndis802_11EncryptionDisabled, parameters);
            }
            catch
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Connects the specified ssid.
        /// </summary>
        /// <param name="ssid">The ssid.</param>
        /// <returns></returns>
        public bool Connect(string ssid)
        {
            bool result = false;

            try
            {
                result = _Adapter.SetWirelessSettings(ssid);
                _Adapter.RebindAdapter();
            }
            catch
            {
                result = false;
            }

            return false;
        }
    }
}
