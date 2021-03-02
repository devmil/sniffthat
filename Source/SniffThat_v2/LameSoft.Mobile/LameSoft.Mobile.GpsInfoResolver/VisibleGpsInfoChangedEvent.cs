//==========================================================================================
//
//		LameSoft.Mobile.GpsInfoResolver.VisibleGpsInfoChangedEvent
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
    /// Delegate for Events that concern the List of visible Gps Info Items
    /// </summary>
    /// <param name="sender">The sender</param>
    /// <param name="args">The arguments</param>
    /// <returns></returns>
    public delegate void VisibleGpsInfoChangedEventHandler(object sender, VisibleGpsInfoChangedEventArgs args);

    /// <summary>
    /// Event Args for <see cref="T:VisibleGpsInfoChangedEventHandler"/>
    /// </summary>
    public class VisibleGpsInfoChangedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:VisibleGpsInfoChangedEventArgs"/> class.
        /// </summary>
        /// <param name="positionjob">The positionjob.</param>
        /// <param name="entries">The entries.</param>
        public VisibleGpsInfoChangedEventArgs(GpsPositionJob positionjob, IList<GpsInfoEntry> entries)
        {
            _GpsInfos = entries;
            _GpsPositionJob = positionjob;
        }

        private GpsPositionJob _GpsPositionJob;

        /// <summary>
        /// Gets or sets the GPS position job.
        /// </summary>
        /// <value>The GPS position job.</value>
        public GpsPositionJob GpsPositionJob
        {
            get { return _GpsPositionJob; }
            set { _GpsPositionJob = value; }
        }

        private IList<GpsInfoEntry> _GpsInfos;

        /// <summary>
        /// Gets or sets the GPS infos.
        /// </summary>
        /// <value>The GPS infos.</value>
        public IList<GpsInfoEntry> GpsInfos
        {
            get { return _GpsInfos; }
            set { _GpsInfos = value; }
        }
    }
}
