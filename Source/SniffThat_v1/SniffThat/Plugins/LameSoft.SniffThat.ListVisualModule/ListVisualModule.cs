//==========================================================================================
//
//		LameSoft.SniffThat.Plugins.ListVisualModule.ListVisualModule
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
//      - 06.02.2007 (Michael Lamers, info@lamesoft.de): 
//          * removed fast update Method
//
//==========================================================================================
using System;
using System.Collections.Generic;
using System.Text;
using LameSoft.SniffThat.Common;
using System.Windows.Forms;
using LameSoft.Mobile.Gps;
using System.Globalization;
using System.Xml.Serialization;
using System.IO;
using LameSoft.Mobile.Utils;

namespace LameSoft.SniffThat.Plugins.ListVisualModule
{
    public class ListVisualModule : IVisualPlugin
    {
        private AccessPointList _AccessPointList;

        private ListSettings _ListSettings;

        public ListVisualModule()
        {
            ListResources.Culture = CultureInfo.CurrentCulture;
            _AccessPointList = new AccessPointList();
            _ListSettings = new ListSettings(_AccessPointList);
        }

        #region IVisualPlugin Members

        public Control ModuleControl
        {
            get 
            {
                return _AccessPointList;
            }
        }

        public void Start()
        {
            _AccessPointList.Start();
        }

        public void Stop()
        {
            _AccessPointList.Stop();
            SaveSettings();
        }

        public void GpsDataChanged(GpsPosition position, GpsMovement mov, IList<GpsSatellite> sats)
        {
            //Nothing to do in this visual module
        }

        public void AccessPointListChanged(IList<AccessPointEntry> entries)
        {
            _AccessPointList.SetEntriesDataSource(entries);
        }

        public int PreferredPosition
        {
            get 
            {
                return 1;
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
                _ListSettings.ColumnDefinitions = (List<ColumnDefinition>)DeSerializeDefinitions(_Context.GetSettingsValue("ColumnDefinitions"));
                if (_Context.GetSettingsValue("SortInvert") != null)
                {
                    try
                    {
                        _AccessPointList.SortInvert = Boolean.Parse(_Context.GetSettingsValue("SortInvert"));
                    }
                    catch
                    {}
                }
                _AccessPointList.SelectedSortField = _Context.GetSettingsValue("SortField");
            }
        }

        private List<ColumnDefinition> DeSerializeDefinitions(string serializedList)
        {
            object result = ObjectSerializer.DeSerializeFromString(serializedList, typeof(List<ColumnDefinition>));

            if (result == null)
                return (List<ColumnDefinition>)_AccessPointList.CreateDefinitions();

            return (List<ColumnDefinition>)result;
        }

        private string SerializeDefinitions(List<ColumnDefinition> definitions)
        {
            string result = ObjectSerializer.SerializeToString(definitions);

            if (result == null)
                return "";

            return result;
        }

        public string PluginName
        {
            get 
            {
                return ListResources.ListName;
            }
        }

        public string PluginDescription
        {
            get 
            {
                return ListResources.ListDescription;
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
                return _ListSettings;
            }
        }
        #endregion

        private void SaveSettings()
        {
            _Context.StoreSettingsValue("ColumnDefinitions", SerializeDefinitions((List<ColumnDefinition>)_ListSettings.ColumnDefinitions));
            _Context.StoreSettingsValue("SortField", _AccessPointList.SelectedSortField);
            _Context.StoreSettingsValue("SortInvert", _AccessPointList.SortInvert.ToString());
        }
    }
}
