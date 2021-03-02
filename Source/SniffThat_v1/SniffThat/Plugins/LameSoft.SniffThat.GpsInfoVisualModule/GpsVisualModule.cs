//==========================================================================================
//
//		LameSoft.SniffThat.Plugins.GpsInfoVisualModule.GpsVisualModule
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
using LameSoft.Mobile.GpsInfoResolver;
using LameSoft.Mobile.Gps;
using System.Globalization;
using System.Windows.Forms;

namespace LameSoft.SniffThat.Plugins.GpsInfoVisualModule
{
    public class GpsVisualModule : IVisualPlugin
    {
        private GpsInfoControl _GpsInfoControl = null;

        public GpsVisualModule()
        {
            GpsInfoResources.Culture = CultureInfo.CurrentCulture;
            _GpsInfoControl = new GpsInfoControl();
            _GpsInfoControl.SetGpsInfos(new List<GpsInfoEntry>());
        }

        #region IVisualPlugin Members

        public System.Windows.Forms.Control ModuleControl
        {
            get 
            {
                return _GpsInfoControl; 
            }
        }

        private bool _Running;

        public void Start()
        {
            _Running = true;
            _ActiveRequest = null;
        }

        public void Stop()
        {
            _Running = false;
        }

        public void GpsDataChanged(GpsPosition position, GpsMovement mov, IList<GpsSatellite> sats)
        {
            if (_ActiveRequest == null)
            {
                if ((_Context != null) && _Running && (position != null))
                {
                    _ActiveRequest = new GpsPositionJob((double)position.Longitude, (double)position.Latitude);
                    _Context.GpsInfo.AddWork(_ActiveRequest);
                }
            }
        }

        public void AccessPointListChanged(IList<AccessPointEntry> entries)
        {
            //nothing to do here
        }

        public int PreferredPosition
        {
            get
            {
                return 4;
            }
        }

        #endregion

        #region IPlugin Members

        private IModuleContext _Context = null;

        public IModuleContext Context
        {
            set 
            {
                _Context = value;
                _Context.GpsInfo.GpsInfoRetrieved += new GpsInfoRetrievedEventHandler(GpsInfo_GpsInfoRetrieved);
                _Context.GpsInfo.VisibleGpsInfosChanged += new VisibleGpsInfoChangedEventHandler(GpsInfo_VisibleGpsInfosChanged);
            }
        }

        private GpsPositionJob _ActiveRequest = null;

        void GpsInfo_VisibleGpsInfosChanged(object sender, VisibleGpsInfoChangedEventArgs args)
        {
            if (!_Running)
                return;
            if (_ActiveRequest == null)
                return;
            if (_ActiveRequest.Equals(args.GpsPositionJob))
            {
                _GpsInfoControl.SetGpsInfos((List<GpsInfoEntry>)args.GpsInfos);
                //gets called last => Set _ActiveRequest back
                _ActiveRequest = null;
            }
        }

        void GpsInfo_GpsInfoRetrieved(object sender, GpsInfoRetrievedEventArgs args)
        {
            if (!_Running)
                return;
            if (_ActiveRequest == null)
                return;
            if (_ActiveRequest.Equals(args.Position))
            {
                string info = "";
                if (args.GpsInfo != null)
                {
                    info = args.GpsInfo;

                    if (args.Distance > -1)
                        info += " (" + args.Distance.ToString("0.00") + " km)";
                }
                _GpsInfoControl.SetCurrentGpsInfo( info );
            }
        }

        public string PluginName
        {
            get { return GpsInfoResources.GpsInfoName; }
        }

        public string PluginDescription
        {
            get { return GpsInfoResources.GpsInfoDescription; }
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
