//==========================================================================================
//
//		LameSoft.SniffThat.Core.VisualContext
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
//      - 06.02.2007 (Michael Lamers, info@lamesoft.de): 
//          * file added
//
//==========================================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace LameSoft.Mobile.WirelessLan
{
    /// <summary>
    /// Entity for WLan History Entries
    /// </summary>
    public class WLanHistoryEntry
    {
        private double _Strength;
        private DateTime _DateTime;
        private bool _Available;

        /// <summary>
        /// Initializes a new instance of the <see cref="WLanHistoryEntry"/> class.
        /// </summary>
        /// <param name="strength">The strength.</param>
        /// <param name="dateTime">The date time.</param>
        /// <param name="available">if set to <c>true</c> [available].</param>
        public WLanHistoryEntry(double strength, DateTime dateTime, bool available)
        {
            _Strength = strength;
            _DateTime = dateTime;
            _Available = available;
        }

        /// <summary>
        /// Gets or sets the strength.
        /// </summary>
        /// <value>The strength.</value>
        public double Strength
        {
            get { return _Strength; }
            set { _Strength = value; }
        }

        /// <summary>
        /// Gets or sets the date time.
        /// </summary>
        /// <value>The date time.</value>
        public DateTime DateTime
        {
            get { return _DateTime; }
            set { _DateTime = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="WLanHistoryEntry"/> is available.
        /// </summary>
        /// <value><c>true</c> if available; otherwise, <c>false</c>.</value>
        public bool Available
        {
            get
            {
                return _Available;
            }
            set
            {
                _Available = value;
            }
        }
    }
}
