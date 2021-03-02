//==========================================================================================
//
//		LameSoft.Mobile.WirelessLan.VisibleAccessPointCollectionChanged
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
using OpenNETCF.Net;

namespace LameSoft.Mobile.WirelessLan
{
    /// <summary>
    /// Event Handler for a changed list of AccessPoints
    /// </summary>
    /// <param name="sender">sender</param>
    /// <param name="args">arguments</param>
    public delegate void VisibleAccessPointCollectionChangedEventHandler(object sender, VisibleAccessPointCollectionChangedEventArgs args);

    /// <summary>
    /// Event Arguments for VisibleAccessPointCollectionChangedEventHandler
    /// </summary>
    public class VisibleAccessPointCollectionChangedEventArgs : EventArgs
    {
    }
}
