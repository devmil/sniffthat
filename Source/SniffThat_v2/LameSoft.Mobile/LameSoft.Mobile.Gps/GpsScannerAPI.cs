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
//      - 17.01.2007 (Michael Lamers, info@lamesoft.de): 
//          * added header
//
//------------------------------------------------------------------------------------------ 
// Remarks: This piece of code is under developement and doesn't work properly
//==========================================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace LameSoft.Mobile.Gps
{
    /// <summary>
    /// Gps Scanner using the MS API
    /// </summary>
    public class GpsScannerAPI : IGpsScanner
    {
        private WinGpsAPI.Gps _Gps;

        public GpsScannerAPI()
        {
            _Gps = new LameSoft.Mobile.Gps.WinGpsAPI.Gps();
            _Gps.LocationChanged += new LameSoft.Mobile.Gps.WinGpsAPI.LocationChangedEventHandler(_Gps_LocationChanged);
        }

        void _Gps_LocationChanged(object sender, LameSoft.Mobile.Gps.WinGpsAPI.LocationChangedEventArgs args)
        {
            if (GpsInfo != null)
                GpsInfo(this, new GpsInfoEventArgs(GetSatellites(args), GetMovement(args), GetPosition(args), GetFix(args), GetNumberOfSatellites(args)));
        }

        private int GetNumberOfSatellites(LameSoft.Mobile.Gps.WinGpsAPI.LocationChangedEventArgs args)
        {
            return args.Position.SatelliteCount;
        }

        private GpsFix GetFix(LameSoft.Mobile.Gps.WinGpsAPI.LocationChangedEventArgs args)
        {
            switch (args.Position.fixType)
            {
                case LameSoft.Mobile.Gps.WinGpsAPI.FixType.XyD:
                    return GpsFix.Fix2D;
                case LameSoft.Mobile.Gps.WinGpsAPI.FixType.XyzD:
                    return GpsFix.Fix3D;
                default:
                    return GpsFix.FixNone;
            }
        }

        private GpsPosition GetPosition(LameSoft.Mobile.Gps.WinGpsAPI.LocationChangedEventArgs args)
        {
            return new GpsPosition(args.Position.Longitude, args.Position.Latitude, args.Position.SeaLevelAltitude);
        }

        private GpsMovement GetMovement(LameSoft.Mobile.Gps.WinGpsAPI.LocationChangedEventArgs args)
        {
            return new GpsMovement(args.Position.Speed, args.Position.Heading);
        }

        private IList<GpsSatellite> GetSatellites(LameSoft.Mobile.Gps.WinGpsAPI.LocationChangedEventArgs args)
        {
            LameSoft.Mobile.Gps.WinGpsAPI.Satellite[] satellitesInView = args.Position.GetSatellitesInView();
            List<GpsSatellite> result = new List<GpsSatellite>(args.Position.SatelliteCount);
            foreach (LameSoft.Mobile.Gps.WinGpsAPI.Satellite sat in args.Position.GetSatellitesInSolution())
            {
                bool active = Array.IndexOf<LameSoft.Mobile.Gps.WinGpsAPI.Satellite>(satellitesInView, sat) > 0;
                result.Add(new GpsSatellite(sat.Id.ToString(), (byte)sat.Elevation, (short)sat.Azimuth, (byte)sat.SignalStrength, active));
            }

            return result;
        }

        #region IGpsScanner Member

        /// <summary>
        /// Gets or sets the refresh rate.
        /// </summary>
        /// <value>The refresh rate.</value>
        public int RefreshRate
        {
            get
            {
                return 0;
            }
            set
            {
                //NOOP
            }
        }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>The port.</value>
        public string Port
        {
            get
            {
                return "Not available";
            }
            set
            {
                //NOOP: MS API doesn't need a port
            }
        }

        /// <summary>
        /// Gets or sets the baud.
        /// </summary>
        /// <value>The baud.</value>
        public int Baud
        {
            get
            {
                return 0;
            }
            set
            {
                //NOOP: MS API doesn't need a baud rate
            }
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>The state.</value>
        public GpsScannerState State
        {
            get 
            {
                return _Gps.Opened ? GpsScannerState.Running : GpsScannerState.Stopped;
            }
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            _Gps.Open();
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            _Gps.Close();
        }

        /// <summary>
        /// Gps Info Event
        /// </summary>
        public event GpsInfoEventHandler GpsInfo;

        #endregion
    }
}
