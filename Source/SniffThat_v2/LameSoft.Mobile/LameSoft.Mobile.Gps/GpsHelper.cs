//==========================================================================================
//
//		LameSoft.Mobile.Gps.GpsHelper
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
    public class GpsHelper
    {
        private const double C_Earth_Radius_km = 6380;

        /// <summary>
        /// Gets the distance.
        /// </summary>
        /// <param name="lat1">The lat1.</param>
        /// <param name="lat2">The lat2.</param>
        /// <param name="long1">The long1.</param>
        /// <param name="long2">The long2.</param>
        /// <returns></returns>
        public static double GetDistance(double lat1, double lat2, double long1, double long2)
        {
            return Math.Abs(GetSignedRealDistance(lat1, lat2, long1, long2));
        }

        /// <summary>
        /// Gets the signed real distance.
        /// </summary>
        /// <param name="lat1">The lat1.</param>
        /// <param name="lat2">The lat2.</param>
        /// <param name="long1">The long1.</param>
        /// <param name="long2">The long2.</param>
        /// <returns></returns>
        public static double GetSignedRealDistance(double lat1, double lat2, double long1, double long2)
        {
            return Math.Acos(Math.Cos(lat1) * Math.Cos(long1) * Math.Cos(lat2) * Math.Cos(long2) + Math.Cos(lat1) * Math.Sin(long1) * Math.Cos(lat2) * Math.Sin(long2) + Math.Sin(lat1) * Math.Sin(lat2)) / 180 * Math.PI * C_Earth_Radius_km;
        }
    }
}
