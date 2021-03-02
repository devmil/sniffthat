//==========================================================================================
//
//		LameSoft.SniffThat.Common.AccessPointEntry
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
//          * added "StrengthAtSavedPosition"
//
//==========================================================================================
using System;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.Net;
using System.Globalization;
using System.Xml.Serialization;
using LameSoft.Mobile.WirelessLan;

namespace LameSoft.SniffThat.Common
{
    /// <summary>
    /// Represents an AccessPoint Entry
    /// </summary>
    public class AccessPointEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:AccessPointEntry"/> class.
        /// </summary>
        private AccessPointEntry()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:AccessPointEntry"/> class.
        /// </summary>
        /// <param name="ssid">The ssid.</param>
        /// <param name="mac">The mac.</param>
        /// <param name="nr">number of the entry</param>
        public AccessPointEntry(string ssid, byte[] mac, int nr)
        {
            _SSID = ssid;
            _MacAddress = mac;
            _Nr = nr;
        }

        private string _SSID;

        /// <summary>
        /// Gets or sets the SSID.
        /// </summary>
        /// <value>The SSID.</value>
        public string SSID
        {
            get { return _SSID; }
            set { _SSID = value; }
        }

        private byte[] _MacAddress;

        /// <summary>
        /// Gets or sets the mac address.
        /// </summary>
        /// <value>The mac address.</value>
        public byte[] MacAddress
        {
            get { return _MacAddress; }
            set { _MacAddress = value; }
        }

        private bool _Protected;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:AccessPointEntry"/> is protected.
        /// </summary>
        /// <value><c>true</c> if protected; otherwise, <c>false</c>.</value>
        public bool Protected
        {
            get { return _Protected; }
            set { _Protected = value; }
        }

        private int _Privacy;

        /// <summary>
        /// Gets or sets the privacy.
        /// </summary>
        /// <value>The privacy.</value>
        public int Privacy
        {
            get { return _Privacy; }
            set { _Privacy = value; }
        }

        private double _Longitude;

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>The longitude.</value>
        public double Longitude
        {
            get { return _Longitude; }
            set { _Longitude = value; }
        }

        private double _Latitude;

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        public double Latitude
        {
            get { return _Latitude; }
            set { _Latitude = value; }
        }

        private double _StrengthAtSavedPosition = 0;

        /// <summary>
        /// Gets or sets the strength at the saved position.
        /// </summary>
        /// <value>The strength at saved position.</value>
        public double StrengthAtSavedPosition
        {
            get { return _StrengthAtSavedPosition; }
            set { _StrengthAtSavedPosition = value; }
        }

        private string _Strength;

        /// <summary>
        /// Gets or sets the strength.
        /// </summary>
        /// <value>The strength.</value>
        public string Strength
        {
            get { return _Strength; }
            set { _Strength = value; }
        }

        private double _StrengthDB;

        /// <summary>
        /// Gets or sets the strength DB.
        /// </summary>
        /// <value>The strength DB.</value>
        public double StrengthDB
        {
            get { return _StrengthDB; }
            set
            {
                _StrengthDB = value;
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"></see> is equal to the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object"></see> to compare with the current <see cref="T:System.Object"></see>.</param>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"></see> is equal to the current <see cref="T:System.Object"></see>; otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            if (!(obj is AccessPointEntry))
                return false;

            AccessPointEntry ape = (AccessPointEntry)obj;

            return ape.MacAddressString.Equals(MacAddressString);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. <see cref="M:System.Object.GetHashCode"></see> is suitable for use in hashing algorithms and data structures like a hash table.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"></see>.
        /// </returns>
        public override int GetHashCode()
        {
            return (base.GetHashCode() * 31 + MacAddressString.GetHashCode());
        }

        /// <summary>
        /// Gets the mac address string.
        /// </summary>
        /// <value>The mac address string.</value>
        [XmlIgnore]
        public string MacAddressString
        {
            get
            {
                string mac = HexEncoding.ToString(_MacAddress);

                StringBuilder result = new StringBuilder();

                for (int i = 0; i < mac.Length - 1; i += 2)
                {
                    if (i > 0)
                        result.Append(":");
                    result.Append(mac.Substring(i, 2));
                }

                return result.ToString();
            }
        }

        private DateTime _FirstSeen;

        /// <summary>
        /// Gets or sets the first seen.
        /// </summary>
        /// <value>The first seen.</value>
        public DateTime FirstSeen
        {
            get { return _FirstSeen; }
            set { _FirstSeen = value; }
        }

        /// <summary>
        /// Gets the first seen string.
        /// </summary>
        /// <value>The first seen string.</value>
        public string FirstSeenString
        {
            get
            {
                return _FirstSeen.ToString("dd.MM.yyyy HH:mm:ss", CultureInfo.CreateSpecificCulture("de-DE"));
            }
        }

        private DateTime _LastSeen;

        /// <summary>
        /// Gets or sets the last seen.
        /// </summary>
        /// <value>The last seen.</value>
        public DateTime LastSeen
        {
            get { return _LastSeen; }
            set { _LastSeen = value; }
        }

        /// <summary>
        /// Gets the last seen string.
        /// </summary>
        /// <value>The last seen string.</value>
        public string LastSeenString
        {
            get
            {
                return _LastSeen.ToString("dd.MM.yyyy HH:mm:ss", CultureInfo.CreateSpecificCulture("de-DE"));
            }
        }

        private bool _Visible;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:AccessPointEntry"/> is visible.
        /// </summary>
        /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
        public bool Visible
        {
            get { return _Visible; }
            set { _Visible = value; }
        }

        private string _GpsInfo = "";

        /// <summary>
        /// Gets or sets the GPS info.
        /// </summary>
        /// <value>The GPS info.</value>
        public string GpsInfo
        {
            get { return _GpsInfo; }
            set { _GpsInfo = value; }
        }

        private double _GpsInfoDistance = -1;

        /// <summary>
        /// Gets or sets the GPS info distance.
        /// </summary>
        /// <value>The GPS info distance.</value>
        public double GpsInfoDistance
        {
            get { return _GpsInfoDistance; }
            set { _GpsInfoDistance = value; }
        }

        /// <summary>
        /// Gets the GPS info distance string.
        /// </summary>
        /// <value>The GPS info distance string.</value>
        public string GpsInfoDistanceString
        {
            get
            {
                return _GpsInfoDistance > -1 ? _GpsInfoDistance.ToString("0.00") + " km" : "";
            }
        }

        private List<double> _SupportedRates = new List<double>();

        /// <summary>
        /// Gets the supported rates.
        /// </summary>
        /// <value>The supported rates.</value>
        public List<double> SupportedRates
        {
            get { return _SupportedRates; }
            set { _SupportedRates = value; }
        }

        /// <summary>
        /// Gets the supported rates string.
        /// </summary>
        /// <value>The supported rates string.</value>
        public string SupportedRatesString
        {
            get
            {
                StringBuilder result = new StringBuilder();

                foreach (double rate in _SupportedRates)
                {
                    if (result.Length > 0)
                        result.Append(", ");
                    result.Append(rate.ToString());
                }

                return result.ToString();
            }
        }

        private int _Nr;

        /// <summary>
        /// Number of the Access Point in the List
        /// </summary>
        public int Nr
        {
            get { return _Nr; }
            set { _Nr = value; }
        }

        /// <summary>
        /// Gets a value indicating weather this access point is open
        /// </summary>
        public bool IsOpen
        {
            get
            {
                return !String.IsNullOrEmpty(_SSID) && !_Protected;
            }
        }

        private WLanHistory _WLanHistory;

        /// <summary>
        /// Gets the Wlan history.
        /// </summary>
        /// <value>The Wlan history.</value>
        [XmlIgnore]
        public WLanHistory WLanHistory
        {
            get 
            {
                if (_WLanHistory == null)
                    _WLanHistory = new WLanHistory(2000, 200, _SSID);
                return _WLanHistory; 
            }
        }
    }
}
