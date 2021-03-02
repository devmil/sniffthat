//==========================================================================================
//
//		LameSoft.Mobile.Gps.GpsMovement
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
    public class GpsMovement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:GpsMovement"/> class.
        /// </summary>
        /// <param name="speed">The speed.</param>
        /// <param name="directionDegrees">The direction degrees.</param>
        public GpsMovement(double speed, double directionDegrees)
        {
            _Speed = speed;
            _DirectionDegrees = directionDegrees;
        }

        private double _Speed;

        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        /// <value>The speed.</value>
        public double Speed
        {
            get { return _Speed; }
        }

        private double _DirectionDegrees;

        /// <summary>
        /// Gets or sets the direction degrees.
        /// </summary>
        /// <value>The direction degrees.</value>
        public double DirectionDegrees
        {
            get { return _DirectionDegrees; }
        }
    }
}
