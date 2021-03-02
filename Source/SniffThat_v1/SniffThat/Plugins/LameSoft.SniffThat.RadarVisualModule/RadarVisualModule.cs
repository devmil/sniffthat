//==========================================================================================
//
//		LameSoft.SniffThat.Plugins.RadarVisualModule.RadarVisualModule
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
using LameSoft.Mobile.Gps.Visual;
using LameSoft.Mobile.Gps;
using System.Globalization;
using System.Windows.Forms;

namespace LameSoft.SniffThat.Plugins.RadarVisualModule
{
    public class RadarVisualModule : IVisualPlugin
    {
        public RadarVisualModule()
        {
            RadarVisualModuleResources.Culture = CultureInfo.CurrentCulture;
        }

        private RadarControl _RadarControl = new RadarControl();

        #region IVisualPlugin Members

        public System.Windows.Forms.Control ModuleControl
        {
            get 
            {
                return _RadarControl;
            }
        }

        private bool _Running;

        public void Start()
        {
            _Running = true;
        }

        public void Stop()
        {
            _Running = false;
            if (_Context != null)
            {
                _Context.StoreSettingsValue("Protected", _RadarControl.Protected.ToString());
                _Context.StoreSettingsValue("MaxDistance", _RadarControl.MaxDistance.ToString());
            }
        }

        public void GpsDataChanged(LameSoft.Mobile.Gps.GpsPosition position, LameSoft.Mobile.Gps.GpsMovement mov, IList<LameSoft.Mobile.Gps.GpsSatellite> sats)
        {
            if (_Running)
            {
                _RadarControl.SetCenter(position);
                _RadarControl.SetDegrees(mov.DirectionDegrees);
            }
        }

        public void AccessPointListChanged(IList<AccessPointEntry> entries)
        {
            if (!_Running)
                return;

            _RadarControl.SetAccessPoints(entries);
        }

        public int PreferredPosition
        {
            get 
            {
                return 2;
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
                try
                {
                    _RadarControl.Protected = Convert.ToBoolean(_Context.GetSettingsValue("Protected"));
                }
                catch
                { }
                try
                {
                    _RadarControl.MaxDistance = Convert.ToDouble(_Context.GetSettingsValue("MaxDistance"));
                }
                catch
                { }
            }
        }

        public string PluginName
        {
            get { return RadarVisualModuleResources.RadarName; }
        }

        public string PluginDescription
        {
            get 
            {
                return RadarVisualModuleResources.RadarDescription;
            }
        }

        public string PluginAuthor
        {
            get { return "LameSoft"; }
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
