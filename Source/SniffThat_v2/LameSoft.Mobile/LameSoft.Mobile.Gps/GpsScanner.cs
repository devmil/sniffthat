//==========================================================================================
//
//		LameSoft.Mobile.Gps.GpsScanner
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
//
//==========================================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LameSoft.Mobile.Gps
{
    public class GpsScanner : IGpsScanner
    {
        private GPS _Gps;

        private Timer _Timer;

        private int _RefreshRate;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GpsScanner"/> class.
        /// </summary>
        public GpsScanner()
            : this(Timeout.Infinite, "", 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GpsScanner"/> class.
        /// </summary>
        /// <param name="refreshrate">The refreshrate in ms.</param>
        /// <param name="port">The port as string (e.g. "COM1:").</param>
        /// <param name="baud">The baud as integer, e.g. 4800.</param>
        public GpsScanner(int refreshrate, string port, int baud)
        {
            _RefreshRate = refreshrate;
            Port = port;
            Baud = baud;

            _Timer = new Timer(new TimerCallback(OnTimer), this, Timeout.Infinite, Timeout.Infinite);

            _Gps = new GPS();
            _Gps.Movement += new GPS.MovementEventHandler(_Gps_Movement);
            _Gps.Position += new GPS.PositionEventHandler(_Gps_Position);
        }

        void _Gps_Position(object sender, Position pos)
        {
            _LastPosition = new GpsPosition((double)pos.Longitude_Decimal, (double)pos.Latitude_Decimal, (double)pos.Altitude);
        }

        void _Gps_Movement(object sender, Movement mov)
        {
            _LastMovement = new GpsMovement((double)mov.SpeedKph, 90-(double)(mov.Track));
        }

        private GpsMovement _LastMovement = null;

        private GpsPosition _LastPosition = null;

        private string _Port;

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>The port.</value>
        public string Port
        {
            get { return _Port; }
            set { _Port = value; }
        }

        /// <summary>
        /// Gets or sets the _ refresh rate.
        /// </summary>
        /// <value>The _ refresh rate.</value>
        public int RefreshRate
        {
            get
            {
                return _RefreshRate;
            }
            set
            {
                _RefreshRate = value;
            }
        }

        private int _Baud;

        /// <summary>
        /// Gets or sets the baud.
        /// </summary>
        /// <value>The baud.</value>
        public int Baud
        {
            get { return _Baud; }
            set { _Baud = value; }
        }

        private GpsScannerState _State;

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>The state.</value>
        public GpsScannerState State
        {
            get { return _State; }
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            try
            {
                _State = GpsScannerState.Running;
                _OldBaudRate = 0;

                if ((_Gps.State != States.Running) && (_Gps.State != States.Opening))
                {
                    _OldBaudRate = _Baud;
                    _OldComPort = _Port;
                    _Gps.BaudRate = _Baud;
                    _Gps.ComPort = _Port;
                    _Gps.Start();
                }
            }
            catch (Exception)
            {
                _State = GpsScannerState.Stopped;
                throw new Exception("Error opening Com Port");
            }

            _Timer.Change(0, _RefreshRate);
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            _Timer.Change(Timeout.Infinite, Timeout.Infinite);
            _State = GpsScannerState.Stopped;
            _Gps.Stop();
            if (GpsInfo != null)
                GpsInfo(this, new GpsInfoEventArgs(null, null, null, GpsFix.FixNone, 0));
        }

        private bool _RefreshingGpsData = false;
        private string _OldComPort = "";
        private int _OldBaudRate = 0;

        public event GpsInfoEventHandler GpsInfo;

        /// <summary>
        /// Called when [timer].
        /// </summary>
        /// <param name="target">The target.</param>
        private void OnTimer(object target)
        {
            lock (this)
            {
                if (_RefreshingGpsData)
                    return;
                _RefreshingGpsData = true;
            }

            bool restart = false;

            if (_OldComPort != null)
            {
                if (!_OldComPort.Equals(_Port))
                {
                    restart = true;
                }
            }

            if ((_OldBaudRate == 0) || (_OldBaudRate != _Baud))
            {
                restart = true;
            }

            try
            {
                //restart = restart || !_Gps.IsPortOpen;

                if (restart)
                {
                    _OldComPort = _Port;
                    _OldBaudRate = _Baud;

                    try
                    {
                        if ((_Gps.State == States.Running) || (_Gps.State == States.Opening))
                            _Gps.Stop();
                    }
                    catch (Exception)
                    { }

                    _Gps.BaudRate = _Baud;
                    _Gps.ComPort = _Port;
                    _Gps.Start();
                }

            }
            catch (Exception)
            {
            }

            try
            {
                GpsFix fix = GpsFix.FixNone;


                if (_Gps.State == States.Running)
                {
                    _State = GpsScannerState.Running;

                    if (_Gps.FixIndicator == Fix_Indicator.Mode2D)
                        fix = GpsFix.Fix2D;
                    else if (_Gps.FixIndicator == Fix_Indicator.Mode3D)
                        fix = GpsFix.Fix3D;

                    int numofsats = (int)_Gps.SatInView;

                    IList<GpsSatellite> sats = null;
                    if (_Gps.Satellites != null)
                    {
                        sats = new List<GpsSatellite>();
                        foreach (Satellite s in _Gps.Satellites)
                        {
                            sats.Add(new GpsSatellite(s.ID.ToString(), (byte)s.Elevation, (short)s.Azimuth, (byte)s.SNR, s.Active));
                        }
                    }


                    if (GpsInfo != null)
                        GpsInfo(this, new GpsInfoEventArgs(sats, _LastMovement, _LastPosition, fix, numofsats));
                }
                else if(_Gps.State == States.Stopped)
                {
                    _State = GpsScannerState.Stopped;
                    if (GpsInfo != null)
                        GpsInfo(this, new GpsInfoEventArgs(null, null, null, GpsFix.FixNone, 0));
                }
            }
            catch (Exception)
            { }

            lock (this)
            {
                _RefreshingGpsData = false;
            }
        }
    }
}
