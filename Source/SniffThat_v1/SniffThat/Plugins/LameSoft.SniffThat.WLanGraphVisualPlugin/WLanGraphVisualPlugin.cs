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
//          * added resources
//      - 08.02.2007 (Michael Lamers, info@lamesoft.de): 
//          * saving the last active access points
//
//==========================================================================================
using System;
using System.Collections.Generic;
using System.Text;
using LameSoft.SniffThat.Common;
using System.Windows.Forms;
using LameSoft.Mobile.Gps;
using System.Threading;

namespace LameSoft.SniffThat.WLanGraphVisualPlugin
{
    public class WLanGraphVisualPlugin : IVisualPlugin
    {
        private WLanGraphControl _WLanGraphControl = null;
        private bool _Running = false;
        private bool _Initialized = true;

        private string _SelectedEntryMAC1 = null;
        private string _SelectedEntryMAC2 = null;

        private static readonly string C_SETTINGSKEY_AP1 = "AccessPoint1";
        private static readonly string C_SETTINGSKEY_AP2 = "AccessPoint2";

        #region IVisualPlugin Members

        /// <summary>
        /// Gets the module control.
        /// </summary>
        /// <value>The module control.</value>
        public Control ModuleControl
        {
            get
            {
                if (_WLanGraphControl == null)
                    _WLanGraphControl = new WLanGraphControl();

                return _WLanGraphControl;
            }
        }

        /// <summary>
        /// Gets the preferred position.
        /// </summary>
        /// <value>The preferred position.</value>
        public int PreferredPosition
        {
            get
            {
                return 2;
            }
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            lock (this)
                _Running = true;
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            lock (this)
                _Running = false;
            SaveSettings();
        }

        /// <summary>
        /// GPSs data changed.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="mov">The mov.</param>
        /// <param name="sats">The sats.</param>
        public void GpsDataChanged(GpsPosition position, GpsMovement mov, IList<GpsSatellite> sats)
        {
            //NOOP
        }

        /// <summary>
        /// Accessespoint list changed.
        /// </summary>
        /// <param name="entries">The entries.</param>
        public void AccessPointListChanged(IList<AccessPointEntry> entries)
        {
            lock (this)
                if (!_Running)
                    return;
            //Sync with UI Thread
            if (_WLanGraphControl != null)
                _WLanGraphControl.Invoke(new ThreadStart(
                    delegate()
                    {
                        _WLanGraphControl.SetData(entries);
                        lock (this)
                        {
                            //TODO: apply the settings...

                            //this is causing troubles!
                            //if (!_Initialized)
                            //{
                            //    _Initialized = true;
                            //    _WLanGraphControl.ActiveAPE1 = GetAPByMAC(entries, _SelectedEntryMAC1);
                            //    _WLanGraphControl.ActiveAPE2 = GetAPByMAC(entries, _SelectedEntryMAC2);
                            //}
                        }
                    }));
        }

        private AccessPointEntry GetAPByMAC(IList<AccessPointEntry> entries, string mac)
        {
            if (String.IsNullOrEmpty(mac))
                return null;

            foreach (AccessPointEntry entry in entries)
                if (entry.MacAddressString == mac)
                    return entry;
            
            return null;
        }

        #endregion

        #region IPlugin Members

        private IModuleContext _Context;

        /// <summary>
        /// Sets the context.
        /// </summary>
        /// <value>The context.</value>
        public IModuleContext Context
        {
            set
            {
                _Context = value;
                LoadSettings();
            }
        }

        /// <summary>
        /// Gets the name of the plugin.
        /// </summary>
        /// <value>The name of the plugin.</value>
        public string PluginName
        {
            get
            {
                return "WLan Graph";
            }
        }

        /// <summary>
        /// Gets the plugin description.
        /// </summary>
        /// <value>The plugin description.</value>
        public string PluginDescription
        {
            get
            {
                return WLanGraph.WLanGraphDescription;
            }
        }

        /// <summary>
        /// Gets the plugin author.
        /// </summary>
        /// <value>The plugin author.</value>
        public string PluginAuthor
        {
            get
            {
                return "LameSoft";
            }
        }

        /// <summary>
        /// Gets the global settings control.
        /// </summary>
        /// <value>The global settings control.</value>
        public System.Windows.Forms.Control GlobalSettingsControl
        {
            get
            {
                return null;
            }
        }

        #endregion

        private void LoadSettings()
        {
            lock (this)
            {
                _SelectedEntryMAC1 = _Context.GetSettingsValue(C_SETTINGSKEY_AP1);
                _SelectedEntryMAC2 = _Context.GetSettingsValue(C_SETTINGSKEY_AP2);
                _Initialized = false;
            }
        }

        private void SaveSettings()
        {
            string mac1 = _WLanGraphControl.ActiveAPE1 != null ? _WLanGraphControl.ActiveAPE1.MacAddressString : String.Empty;
            string mac2 = _WLanGraphControl.ActiveAPE2 != null ? _WLanGraphControl.ActiveAPE2.MacAddressString : String.Empty;
            _Context.StoreSettingsValue(C_SETTINGSKEY_AP1, mac1);
            _Context.StoreSettingsValue(C_SETTINGSKEY_AP2, mac2);
        }
    }
}
