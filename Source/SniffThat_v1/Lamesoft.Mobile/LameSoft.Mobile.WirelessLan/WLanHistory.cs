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
//      - 07.02.2007 (Michael Lamers, info@lamesoft.de): 
//          * possibility to add an entry with specifying the DateTime value
//
//==========================================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace LameSoft.Mobile.WirelessLan
{
    /// <summary>
    /// Class for managing WLan History Entries
    /// </summary>
    public class WLanHistory
    {
        private List<WLanHistoryEntry> _Entries;
        private int _MaxEntries;
        private TimeSpan _GridSize;
        private string _SSID;

        /// <summary>
        /// Initializes a new instance of the <see cref="WLanHistory"/> class.
        /// </summary>
        /// <param name="ssid">The ssid.</param>
        public WLanHistory(string ssid)
            : this(10000, 200, ssid)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WLanHistory"/> class.
        /// </summary>
        /// <param name="maxEntries">maximum number of entries.</param>
        /// <param name="gridSize">Size of the grid in milliseconds.</param>
        /// <param name="ssid">The ssid.</param>
        public WLanHistory(int maxEntries, int gridSize, string ssid)
        {
            _MaxEntries = maxEntries;
            _GridSize = TimeSpan.FromMilliseconds(gridSize);
            _Entries = new List<WLanHistoryEntry>(maxEntries + 1);
            _SSID = ssid;
        }

        /// <summary>
        /// Adds the specified strength.
        /// </summary>
        /// <param name="strength">The strength.</param>
        /// <param name="available">if set to <c>true</c> [available].</param>
        /// <param name="force">if set to <c>true</c> [force].</param>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public bool Add(double strength, bool available, bool force, DateTime dateTime)
        {
            lock (this) //sync
            {
                //If there are entries and the last added entry is not long enough in the past => return
                if (!force && (_Entries.Count > 0) && (dateTime - _Entries[_Entries.Count - 1].DateTime <= _GridSize))
                    return false;

                //add a new entry
                _Entries.Add(new WLanHistoryEntry(strength, dateTime, available));

                //sort the entries
                _Entries.Sort(new Comparison<WLanHistoryEntry>(
                    delegate(WLanHistoryEntry x1, WLanHistoryEntry x2)
                    {
                        return x1.DateTime.CompareTo(x2.DateTime);
                    }));

                //remove the first entry, if there are too many
                if (_Entries.Count > _MaxEntries)
                    _Entries.RemoveAt(0);

                return true;
            }
        }

        /// <summary>
        /// Adds the specified strength.
        /// </summary>
        /// <param name="strength">The strength.</param>
        /// <param name="available">if set to <c>true</c> [available].</param>
        /// <param name="force">if set to <c>true</c> [force].</param>
        /// <returns></returns>
        public bool Add(double strength, bool available, bool force)
        {
            return Add(strength, available, force, DateTime.Now);
        }

        /// <summary>
        /// Gets the entries.
        /// </summary>
        /// <value>The entries.</value>
        public IList<WLanHistoryEntry> Entries
        {
            get
            {
                return _Entries.AsReadOnly();
            }
        }

        /// <summary>
        /// Gets the SSID.
        /// </summary>
        /// <value>The SSID.</value>
        public string SSID
        {
            get
            {
                return _SSID;
            }
        }
    }
}
