//==========================================================================================
//
//		LameSoft.SniffThat.Core.ExportContext
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
using LameSoft.SniffThat.Common;
using System.Data;
using LameSoft.Mobile.GpsInfoResolver;
using LameSoft.SniffThat.Core.Events;

namespace LameSoft.SniffThat.Core
{
	/// <summary>
	/// Context for Export Plugins
	/// </summary>
    public class ExportContext : IModuleContext
    {
        private IExportPlugin _ExportPlugin;

        /// <summary>
        /// Gets the export plugin.
        /// </summary>
        /// <value>The export plugin.</value>
        public IExportPlugin ExportPlugin
        {
            get { return _ExportPlugin; }
        }

		/// <summary>
		/// Gets raised when an error occurs
		/// </summary>
        public event MessageEventHandler OnShowError;

		/// <summary>
		/// Gets raised when a message has to be shown
		/// </summary>
        public event MessageEventHandler OnShowMessage;

		/// <summary>
		/// Gets raised when the status changed
		/// </summary>
        public event MessageEventHandler OnShowStatus;

        private DataTable _PluginSettings;

		/// <summary>
		/// Gets raised on progress
		/// </summary>
        public event ProgressEventHandler Progress;

        private WLanSnifferSettings _GlobalSettings;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ExportContext"/> class.
		/// </summary>
		/// <param name="exportplugin">The exportplugin.</param>
		/// <param name="pluginSettings">The plugin settings.</param>
		/// <param name="globalsettings">The globalsettings.</param>
        public ExportContext(IExportPlugin exportplugin, DataTable pluginSettings, WLanSnifferSettings globalsettings)
        {
            _PluginSettings = pluginSettings;
            _ExportPlugin = exportplugin;
            _GlobalSettings = globalsettings;
            _ExportPlugin.Context = this;

            try
            {
                string val = this.GetSettingsValue("__PLUGINACTIVE");
                if (val != null)
                    _Active = Convert.ToBoolean(val);
                else
                    _Active = true; //No Settings Present => active
            }
            catch (Exception)
            {
                _Active = true;
            }
        }

        #region IModuleContext Members

		/// <summary>
		/// Sets the progress percent.
		/// </summary>
		/// <value>The progress percent.</value>
        public double ProgressPercent
        {
            set 
            {
                if (Progress != null)
                    Progress(this, new ProgressEventArgs(value));
            }
        }

		/// <summary>
		/// Shows the error.
		/// </summary>
		/// <param name="error">The error.</param>
        public void ShowError(string error)
        {
            if (OnShowError != null)
                OnShowError(this, error);
        }

		/// <summary>
		/// Shows the status.
		/// </summary>
		/// <param name="status">The status.</param>
        public void ShowStatus(string status)
        {
            if (OnShowStatus != null)
                OnShowStatus(this, status);
        }

		/// <summary>
		/// Shows the message.
		/// </summary>
		/// <param name="message">The message.</param>
        public void ShowMessage(string message)
        {
            if (OnShowMessage != null)
                OnShowMessage(this, message);
        }

		/// <summary>
		/// Stores the settings value.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
        public void StoreSettingsValue(string key, string value)
        {
            DataRow[] rows = _PluginSettings.Select("Key = '"+ key +"'");

            if (rows.Length > 0)
                _PluginSettings.Rows.Remove(rows[0]);

            _PluginSettings.Rows.Add(key, value);
        }

		/// <summary>
		/// Gets the settings value.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns></returns>
        public string GetSettingsValue(string key)
        {
            DataRow[] rows = _PluginSettings.Select("Key = '" + key + "'");

            if (rows.Length > 0)
                return (string)rows[0].ItemArray[1];
            return null;
        }

		/// <summary>
		/// Gets the GPS info.
		/// </summary>
		/// <value>The GPS info.</value>
        public GpsInfo GpsInfo
        {
            get
            {
                return _GlobalSettings.GpsInfo;
            }
        }

		/// <summary>
		/// Gets the Plugin of the Context.
		/// </summary>
		/// <value>The Plugin.</value>
        public IPlugin Plugin
        {
            get
            {
                return _ExportPlugin;
            }
        }

        private bool _Active = true;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:IModuleContext"/> is active.
		/// </summary>
		/// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
        public bool Active
        {
            get
            {
                return _Active;
            }
            set
            {
                _Active = value;
                this.StoreSettingsValue("__PLUGINACTIVE", _Active.ToString());
            }
        }
        #endregion

		/// <summary>
		/// Gibt einen <see cref="T:System.String"></see> zurück, der den aktuellen <see cref="T:System.Object"></see> darstellt.
		/// </summary>
		/// <returns>
		/// Ein <see cref="T:System.String"></see>, der den aktuellen <see cref="T:System.Object"></see> darstellt.
		/// </returns>
        public override string ToString()
        {
            return _ExportPlugin.PluginName;
        }
    }
}
