//==========================================================================================
//
//		LameSoft.Mobile.GpsInfoResolver.GpsInfoEntry
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
using System.Data;

namespace LameSoft.Mobile.GpsInfoResolver
{
    /// <summary>
    /// Represents a Gps Info Entry
    /// </summary>
    public class GpsInfoEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:GpsInfoEntry"/> class.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="dbversion">The dbversion.</param>
        public GpsInfoEntry(DataRow row, string dbversion)
        {
            FillFields(row, dbversion);
            _OriginalRow = row;
        }

        private void FillFields(DataRow row, string dbversion)
        {
            if (dbversion.StartsWith("1.0.0"))
            {
                _ID = Convert.ToInt32(row["ID"]);
                _Info = Convert.ToString(row["City"]);
                _Latitude = Convert.ToDouble(row["Latitude"]);
                _Longitude = Convert.ToDouble(row["Longitude"]);
            }
            else
            {
                _ID = -1;
                _Info = "";
                _Latitude = 0;
                _Longitude = 0;
            }
        }

        private DataRow _OriginalRow;

        /// <summary>
        /// Gets or sets the original row.
        /// </summary>
        /// <value>The original row.</value>
        public DataRow OriginalRow
        {
            get { return _OriginalRow; }
        }

        private int _ID;

        /// <summary>
        /// Gets the ID.
        /// </summary>
        /// <value>The ID.</value>
        public int ID
        {
            get { return _ID; }
        }

        private string _Info;

        /// <summary>
        /// Gets or sets the info.
        /// </summary>
        /// <value>The info.</value>
        public string Info
        {
            get { return _Info; }
            set { _Info = value; }
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

        private double _Distance;

        /// <summary>
        /// Gets or sets the distance.
        /// </summary>
        /// <value>The distance.</value>
        public double Distance
        {
            get { return _Distance; }
            set { _Distance = value; }
        }

        /// <summary>
        /// Gets the distance string.
        /// </summary>
        /// <value>The distance string.</value>
        public string DistanceString
        {
            get
            {
                return _Distance.ToString("0.00") + " km";
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
            if (!(obj is GpsInfoEntry))
                return false;

            if (obj == this)
                return true;

            GpsInfoEntry objC = (GpsInfoEntry)obj;

            return objC._Distance.Equals(_Distance)
                && (((objC._Info == null) && (_Info == null)) || objC._Info.Equals(_Info))
                && objC._Latitude.Equals(_Latitude)
                && objC._Longitude.Equals(_Longitude);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. <see cref="M:System.Object.GetHashCode"></see> is suitable for use in hashing algorithms and data structures like a hash table.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"></see>.
        /// </returns>
        public override int GetHashCode()
        {
            return (((_Distance.GetHashCode() * 31 + ((_Info == null) ? 0 : _Info.GetHashCode())) * 31 + _Longitude.GetHashCode()) * 31 + _Latitude.GetHashCode()) * 31;
        }
    }
}
