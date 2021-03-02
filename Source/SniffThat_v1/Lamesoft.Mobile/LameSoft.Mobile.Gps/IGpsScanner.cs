//==========================================================================================
//
//		LameSoft.Mobile.Gps.IGpsScanner
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
//==========================================================================================using System;
using System.Collections.Generic;
using System.Text;

namespace LameSoft.Mobile.Gps
{
    /// <summary>
    /// Interface for Gps Scanner
    /// </summary>
    public interface IGpsScanner
    {
        /// <summary>
        /// Gets or sets the _ refresh rate.
        /// </summary>
        /// <value>The _ refresh rate.</value>
        int RefreshRate
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>The port.</value>
        string Port
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the baud.
        /// </summary>
        /// <value>The baud.</value>
        int Baud
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>The state.</value>
        GpsScannerState State
        {
            get;
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops this instance.
        /// </summary>
        void Stop();

        /// <summary>
        /// Gps Info Event. Gets fired whenever new GpsInfo is available
        /// </summary>
        event GpsInfoEventHandler GpsInfo;
    }
}
