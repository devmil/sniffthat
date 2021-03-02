//==========================================================================================
//
//		LameSoft.SniffThat.Common.IModuleContext
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
using LameSoft.Mobile.GpsInfoResolver;

namespace LameSoft.SniffThat.Common
{
    /// <summary>
    /// Module Context for all Modules
    /// </summary>
    public interface IModuleContext
    {
        /// <summary>
        /// Sets the progress percent.
        /// </summary>
        /// <value>The progress percent.</value>
        double ProgressPercent
        {
            set;
        }

        /// <summary>
        /// Shows the error.
        /// </summary>
        /// <param name="error">The error.</param>
        void ShowError(string error);

        /// <summary>
        /// Shows the status.
        /// </summary>
        /// <param name="status">The status.</param>
        void ShowStatus(string status);

        /// <summary>
        /// Shows the message.
        /// </summary>
        /// <param name="message">The message.</param>
        void ShowMessage(string message);

        /// <summary>
        /// Stores the settings value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        void StoreSettingsValue(string key, string value);

        /// <summary>
        /// Gets the settings value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        string GetSettingsValue(string key);

        /// <summary>
        /// Gets the GPS info.
        /// </summary>
        /// <value>The GPS info.</value>
        GpsInfo GpsInfo
        {
            get;
        }

        /// <summary>
        /// Gets the Plugin of the Context.
        /// </summary>
        /// <value>The Plugin.</value>
        IPlugin Plugin
        {
            get;
        }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:IModuleContext"/> is active.
		/// </summary>
		/// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
        bool Active
        {
            get;
            set;
        }
    }
}
