//==========================================================================================
//
//		LameSoft.SniffThat.Common.IVisualPlugin
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
//      - 06.02.2007 (Michael Lamers, info@lamesoft.de): 
//          * removed fast update Method
//
//==========================================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using LameSoft.Mobile.Gps;

namespace LameSoft.SniffThat.Common
{
    /// <summary>
    /// Plugin interace for all Visual Plugins
    /// </summary>
    public interface IVisualPlugin : IPlugin
    {
        /// <summary>
        /// Gets the module control.
        /// </summary>
        /// <value>The module control.</value>
        Control ModuleControl
        {
            get;
        }

        /// <summary>
        /// Gets the preferred position.
        /// </summary>
        /// <value>The preferred position.</value>
        int PreferredPosition
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
        /// GPSs data changed.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="mov">The mov.</param>
        /// <param name="sats">The sats.</param>
        void GpsDataChanged(GpsPosition position, GpsMovement mov, IList<GpsSatellite> sats);

        /// <summary>
        /// Accessespoint list changed.
        /// </summary>
        /// <param name="entries">The entries.</param>
        void AccessPointListChanged(IList<AccessPointEntry> entries);
    }
}
