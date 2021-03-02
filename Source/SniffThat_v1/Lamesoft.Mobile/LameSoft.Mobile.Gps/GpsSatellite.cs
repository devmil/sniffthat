//==========================================================================================
//
//		LameSoft.Mobile.Gps.GpsSatellite
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

namespace LameSoft.Mobile.Gps
{
    public class GpsSatellite
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:GpsSatellite"/> class.
        /// </summary>
        /// <param name="prn">The PRN.</param>
        /// <param name="elevation">The elevation.</param>
        /// <param name="azimuth">The azimuth.</param>
        /// <param name="snr">The SNR.</param>
        /// <param name="active">if set to <c>true</c> [active].</param>
        public GpsSatellite(string prn, byte elevation, short azimuth, byte snr, bool active)
        {
            _PRN = prn;
            _Elevation = elevation;
            _Azimuth = azimuth;
            _SNR = snr;
            _Active = active;
        }

        private string _PRN;

        /// <summary>
        /// Pseudo-Random Number ID
        /// </summary>
        public string PRN
        {
            get { return _PRN; }
            set { _PRN = value; }
        }

        private byte _Elevation;

        /// <summary>
        /// Elevation above horizon in degrees (0-90)
        /// </summary>
        public byte Elevation
        {
            get { return _Elevation; }
            set { _Elevation = value; }
        }

        private short _Azimuth;

        /// <summary>
        /// Azimuth	in degrees (0-359)
        /// </summary>
        public short Azimuth
        {
            get { return _Azimuth; }
            set { _Azimuth = value; }
        }

        private byte _SNR;

        /// <summary>
        /// Signal-to-noise ratio in dBHZ (0-99)
        /// </summary>
        public byte SNR
        {
            get { return _SNR; }
            set { _SNR = value; }
        }

        private bool _Active;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:GpsSatellite"/> is active.
        /// </summary>
        /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
    }
}
