//==========================================================================================
//
//		LameSoft.Mobile.Gps.GpsInfoEventHandler
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
    public delegate void GpsInfoEventHandler(object sender, GpsInfoEventArgs args);

    public class GpsInfoEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:GpsInfoEventArgs"/> class.
        /// </summary>
        /// <param name="satellites">The satellites.</param>
        /// <param name="movement">The movement.</param>
        /// <param name="position">The position.</param>
        /// <param name="fix">The fix.</param>
        public GpsInfoEventArgs(IList<GpsSatellite> satellites, GpsMovement movement, GpsPosition position, GpsFix fix, int numofsatsInView)
        {
            _Satellites = satellites;
            _Movement = movement;
            _Position = position;
            _GpsFix = fix;
            _NumOfSats = numofsatsInView;
        }

        private IList<GpsSatellite> _Satellites;

        /// <summary>
        /// Gets or sets the satellites.
        /// </summary>
        /// <value>The satellites.</value>
        public IList<GpsSatellite> Satellites
        {
            get { return _Satellites; }
            set { _Satellites = value; }
        }

        private GpsMovement _Movement;

        /// <summary>
        /// Gets or sets the movement.
        /// </summary>
        /// <value>The movement.</value>
        public GpsMovement Movement
        {
            get { return _Movement; }
            set { _Movement = value; }
        }

        private GpsPosition _Position;

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>The position.</value>
        public GpsPosition Position
        {
            get { return _Position; }
            set { _Position = value; }
        }

        private GpsFix _GpsFix;

        /// <summary>
        /// Gets or sets the GPS fix.
        /// </summary>
        /// <value>The GPS fix.</value>
        public GpsFix GpsFix
        {
            get { return _GpsFix; }
            set { _GpsFix = value; }
        }

        private int _NumOfSats;

        /// <summary>
        /// Gets or sets the num of sats.
        /// </summary>
        /// <value>The num of sats.</value>
        public int NumOfSats
        {
            get { return _NumOfSats; }
            set { _NumOfSats = value; }
        }
    }
}
