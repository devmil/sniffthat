//==========================================================================================
//
//		LameSoft.Mobile.WirelessLan.IWLanScanner
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

namespace LameSoft.Mobile.WirelessLan
{
    /// <summary>
    /// Event Handler for Log events
    /// </summary>
    /// <param name="log"></param>
    public delegate void LogEvent(string log);

    /// <summary>
    /// Interface for WLan Scanner
    /// </summary>
    public interface IWLanScanner
    {
        /// <summary>
        /// Gets fired if the List of visible AccessPoints has changed
        /// </summary>
        event VisibleAccessPointCollectionChangedEventHandler VisibleAccessPointCollectionChanged;

        /// <summary>
        /// Gets fired if something logable occurs
        /// </summary>
        event LogEvent Log;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IWLanScanner"/> is running.
        /// </summary>
        /// <value><c>true</c> if running; otherwise, <c>false</c>.</value>
        bool Running
        {
            get;
            set;
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
        /// Gets the visible access points.
        /// </summary>
        /// <value>The visible access points.</value>
        IEnumerable<AccessPoint> VisibleAccessPoints
        {
            get;
        }

        /// <summary>
        /// Gets or sets the name of the adapter.
        /// </summary>
        /// <value>The name of the adapter.</value>
        string AdapterName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the interval.
        /// </summary>
        /// <value>The interval.</value>
        int Interval
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the fast interval.
        /// </summary>
        /// <value>The fast interval.</value>
        int FastInterval
        {
            get;
            set;
        }

        /// <summary>
        /// Stores the access data.
        /// </summary>
        /// <param name="ssid">The ssid.</param>
        /// <param name="key">The key.</param>
        /// <param name="keyIndex">Index of the key.</param>
        /// <returns></returns>
        bool StoreAccessData(string ssid, string key, int keyIndex);

        /// <summary>
        /// Connects the specified ssid.
        /// </summary>
        /// <param name="ssid">The ssid.</param>
        /// <returns></returns>
        bool Connect(string ssid);
    }
}
