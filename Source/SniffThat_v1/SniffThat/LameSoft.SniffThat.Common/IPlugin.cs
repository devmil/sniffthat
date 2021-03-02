//==========================================================================================
//
//		LameSoft.SniffThat.Common.IPlugin
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
using System.Windows.Forms;

namespace LameSoft.SniffThat.Common
{
    /// <summary>
    /// Base Plugin Interface
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Sets the context.
        /// </summary>
        /// <value>The context.</value>
        IModuleContext Context
        {
            set;
        }

        /// <summary>
        /// Gets the name of the plugin.
        /// </summary>
        /// <value>The name of the plugin.</value>
        string PluginName
        {
            get;
        }

        /// <summary>
        /// Gets the plugin description.
        /// </summary>
        /// <value>The plugin description.</value>
        string PluginDescription
        {
            get;
        }

        /// <summary>
        /// Gets the plugin author.
        /// </summary>
        /// <value>The plugin author.</value>
        string PluginAuthor
        {
            get;
        }

        /// <summary>
        /// Gets the global settings control.
        /// </summary>
        /// <value>The global settings control.</value>
        Control GlobalSettingsControl
        {
            get;
        }
    }
}
