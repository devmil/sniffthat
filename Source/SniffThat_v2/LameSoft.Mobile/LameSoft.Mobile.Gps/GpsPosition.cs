//==========================================================================================
//
//		LameSoft.Mobile.Gps.GpsPosition
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
    public class GpsPosition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:GpsPosition"/> class.
        /// </summary>
        /// <param name="longitude">The longitude.</param>
        /// <param name="latitude">The latitude.</param>
        public GpsPosition(double longitude, double latitude)
            : this(longitude, latitude, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GpsPosition"/> class.
        /// </summary>
        /// <param name="longitude">The longitude.</param>
        /// <param name="latitude">The latitude.</param>
        /// <param name="altitude">The altitude.</param>
        public GpsPosition(double longitude, double latitude, double altitude)
        {
            _Latitude = latitude;
            _Longitude = longitude;
            _Altitude = altitude;
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

        private double _Altitude;

        /// <summary>
        /// Gets or sets the altitude.
        /// </summary>
        /// <value>The altitude.</value>
        public double Altitude
        {
            get { return _Altitude; }
            set { _Altitude = value; }
        }

        public override bool Equals(object obj)
        {
            if(!(obj is GpsPosition))
                return false;

            if(obj == this)
                return true;

            GpsPosition pos = (GpsPosition)obj;

            return this._Latitude.Equals(pos.Latitude) && this._Longitude.Equals(pos.Longitude);
        }

        public override int GetHashCode()
        {
            return (31 * _Latitude.GetHashCode()) + _Longitude.GetHashCode();
        }
    }
}
