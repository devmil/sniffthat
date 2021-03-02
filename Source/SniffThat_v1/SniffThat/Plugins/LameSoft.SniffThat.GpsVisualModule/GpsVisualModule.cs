//==========================================================================================
//
//		LameSoft.SniffThat.Plugins.GpsVisualModule.GpsVisualModule
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

namespace LameSoft.SniffThat.Plugins.GpsVisualModule
{
    public class GpsVisualModule : IVisualPlugin
    {
        private GpsVisualizer _GpsVisualizer;

        public GpsVisualModule()
        {
            GpsVisualResources.Culture = CultureInfo.CurrentCulture;
            _GpsVisualizer = new GpsVisualizer();
        }

        #region IVisualPlugin Members

        public Control ModuleControl
        {
            get 
            {
                return _GpsVisualizer;
            }
        }

        public void Start()
        {
            _GpsVisualizer.Start();
        }

        public void Stop()
        {
            _GpsVisualizer.Stop();
        }

        public void GpsDataChanged(GpsPosition position, GpsMovement mov, IList<GpsSatellite> sats)
        {
            _GpsVisualizer.SetGps(position, mov, sats);
        }

        public void AccessPointListChanged(IList<AccessPointEntry> entries)
        {
            //nothing to do in this visual module
        }

        public int PreferredPosition
        {
            get
            {
                return 3;
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
                _GpsVisualizer.SetGpsInfo(_Context.GpsInfo);
            }
        }

        public string PluginName
        {
            get 
            {
                return GpsVisualResources.GpsVisualName;
            }
        }

        public string PluginDescription
        {
            get 
            {
                return GpsVisualResources.GpsVisualDescription;
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
