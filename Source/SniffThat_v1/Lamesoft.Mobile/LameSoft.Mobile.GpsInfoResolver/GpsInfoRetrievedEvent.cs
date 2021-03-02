//==========================================================================================
//
//		LameSoft.Mobile.GpsInfoResolver.GpsInfoRetrievedEvent
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

namespace LameSoft.Mobile.GpsInfoResolver
{
    /// <summary>
    /// Event Handler for a retrieved Gps Info
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="args">Arguments</param>
    public delegate void GpsInfoRetrievedEventHandler(object sender, GpsInfoRetrievedEventArgs args);

    /// <summary>
    /// Event Args for the GpsInfoRetrievedEventHandler delegate
    /// </summary>
    public class GpsInfoRetrievedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:GpsInfoRetrievedEventArgs"/> class.
        /// </summary>
        /// <param name="gpsInfo">The GPS info.</param>
        /// <param name="position">The position.</param>
        /// <param name="distance">The distance.</param>
        public GpsInfoRetrievedEventArgs(string gpsInfo, GpsPositionJob position, double distance)
        {
            _GpsInfo = gpsInfo;
            _Position = position;
            _Distance = distance;
        }

        private string _GpsInfo;

        /// <summary>
        /// Gets or sets the GPS info.
        /// </summary>
        /// <value>The GPS info.</value>
        public string GpsInfo
        {
            get { return _GpsInfo; }
            set { _GpsInfo = value; }
        }

        private double _Distance;

        /// <summary>
        /// Gets the distance.
        /// </summary>
        /// <value>The distance.</value>
        public double Distance
        {
            get { return _Distance; }
        }

        private GpsPositionJob _Position;

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>The position.</value>
        public GpsPositionJob Position
        {
            get { return _Position; }
            set { _Position = value; }
        }
    }
}
