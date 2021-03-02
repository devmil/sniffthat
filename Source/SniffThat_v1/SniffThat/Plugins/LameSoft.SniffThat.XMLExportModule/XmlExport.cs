//==========================================================================================
//
//		LameSoft.SniffThat.Plugins.XMLExportModule.XmlExport
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
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Xml;
using System.Drawing;
using System.Globalization;

namespace LameSoft.SniffThat.Plugins.XMLExportModule
{
    public class XmlExport : IExportPlugin
    {
        private SettingsPanel _SettingsPanel;

        public XmlExport()
        {
            XmlExportResoures.Culture = CultureInfo.CurrentCulture;
            _SettingsPanel = new SettingsPanel();
        }

        #region IExportPlugin Members

        public Control ExportSettingsControl
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

                XmlSerializer ser = new XmlSerializer(entries.GetType());

                XmlTextWriter xmltw = new XmlTextWriter(_SettingsPanel.ExportFileName, Encoding.UTF8);

                ser.Serialize(xmltw, entries);

                xmltw.Flush();

                xmltw.Close();

                _Context.ShowMessage(String.Format(XmlExportResoures.ExportMessage, entries.Count, _SettingsPanel.ExportFileName));

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
                return XmlExportResoures.XmlExportName;
            }
        }

        public string PluginDescription
        {
            get 
            {
                return XmlExportResoures.XmlExportDescription;
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
