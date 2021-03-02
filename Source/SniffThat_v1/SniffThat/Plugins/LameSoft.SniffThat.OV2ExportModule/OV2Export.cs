//==========================================================================================
//
//		LameSoft.SniffThat.Plugins.OV2ExportModule.OV2Export
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
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace LameSoft.SniffThat.Plugins.OV2ExportModule
{
    public class OV2Export : IExportPlugin
    {
        private SettingsPanel _SettingsPanel;

        public OV2Export()
        {
            OV2Resources.Culture = CultureInfo.CurrentCulture;
            _SettingsPanel = new SettingsPanel();
        }

        #region IExportPlugin Members

        public System.Windows.Forms.Control ExportSettingsControl
        {
            get 
            {
                return _SettingsPanel;
            }
        }

        public bool Export(IList<AccessPointEntry> entries)
        {
            try
            {
                _Context.StoreSettingsValue("ExportFileName", _SettingsPanel.ExportFileName);

                int counter = 0;

                using (FileStream fs = File.OpenWrite(_SettingsPanel.ExportFileName))
                {

                    foreach (AccessPointEntry entry in entries)
                    {
                        if ((entry.Latitude != 0) || (entry.Longitude != 0))
                        {
                            counter++;

                            byte type = 2;

                            byte[] ssidBytes = Encoding.ASCII.GetBytes(entry.SSID);

                            int lat = (int)Math.Round(entry.Latitude * 100000);
                            int lon = (int)Math.Round(entry.Longitude * 100000);

                            byte[] length = BitConverter.GetBytes(1 + 4 + 4 + 4 + ssidBytes.Length + 1);
                            byte[] latBytes = BitConverter.GetBytes(lat);
                            byte[] lonBytes = BitConverter.GetBytes(lon);

                            fs.WriteByte(type);
                            fs.Write(length, 0, length.Length);
                            fs.Write(lonBytes, 0, lonBytes.Length);
                            fs.Write(latBytes, 0, latBytes.Length);
                            fs.Write(ssidBytes, 0, ssidBytes.Length);
                            fs.WriteByte((byte)0);
                        }

                    }

                    fs.Flush();
                    fs.Close();
                }

                _Context.ShowMessage(String.Format(OV2Resources.ExportMessage, counter, _SettingsPanel.ExportFileName));

                return true;
            }
            catch (Exception ex)
            {
                _Context.ShowError(ex.Message);
                return false;
            }

        }

        #endregion

        #region IPlugin Members

        private IModuleContext _Context;

        public IModuleContext Context
        {
            set
            {
                _Context = value;
                _SettingsPanel.ExportFileName = _Context.GetSettingsValue("ExportFileName");
            }
        }

        public string PluginName
        {
            get 
            {
                return OV2Resources.PluginName;
            }
        }

        public string PluginDescription
        {
            get 
            {
                return OV2Resources.PluginDescription;
            }
        }

        public string PluginAuthor
        {
            get 
            {
                return "LameSoft";
            }
        }

        public Control GlobalSettingsControl
        {
            get
            {
                return null;
            }
        }

        #endregion
    }
}
