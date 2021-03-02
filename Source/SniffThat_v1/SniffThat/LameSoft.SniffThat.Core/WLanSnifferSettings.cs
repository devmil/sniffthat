//==========================================================================================
//
//		LameSoft.SniffThat.Core.WLanSnifferSettings
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
using OpenNETCF.Net;
using LameSoft.SniffThat.Common;
using System.Collections;
using System.Data;
using LameSoft.Mobile.GpsInfoResolver;

namespace LameSoft.SniffThat.Core
{
	/// <summary>
	/// Settings for the WLan Sniffer
	/// </summary>
    public class WLanSnifferSettings
    {
        private string _ComPort = "";

        /// <summary>
        /// Gets or sets the COM port.
        /// </summary>
        /// <value>The COM port.</value>
        public string ComPort
        {
            get { return _ComPort; }
            set { _ComPort = value; }
        }

        private int _BaudRate;

        /// <summary>
        /// Gets or sets the baud rate.
        /// </summary>
        /// <value>The baud rate.</value>
        public int BaudRate
        {
            get { return _BaudRate; }
            set { _BaudRate = value; }
        }

        private String _Adapter = "";

        /// <summary>
        /// Gets or sets the adapter.
        /// </summary>
        /// <value>The adapter.</value>
        public String Adapter
        {
            get { return _Adapter; }
            set { _Adapter = value; }
        }

        private int _Interval = 500;

        /// <summary>
        /// Gets or sets the interval.
        /// </summary>
        /// <value>The interval.</value>
        public int Interval
        {
            get { return _Interval; }
            set { _Interval = value; }
        }

        /// <summary>
        /// Gets the adapter.
        /// </summary>
        /// <returns></returns>
        public Adapter GetAdapter()
        {
            AdapterCollection ac = Networking.GetAdapters();
            foreach (Adapter a in ac)
                if (a.Name.Equals(_Adapter))
                    return a;

            return null;
        }

        private IList<ExportContext> _ExportPlugins;

        /// <summary>
        /// Gets or sets the export plugins.
        /// </summary>
        /// <value>The export plugins.</value>
        public IList<ExportContext> ExportPlugins
        {
            get { return _ExportPlugins; }
            set { _ExportPlugins = value; }
        }

        private IList<VisualContext> _VisualPlugins;

        /// <summary>
        /// Gets or sets the visual plugins.
        /// </summary>
        /// <value>The visual plugins.</value>
        public IList<VisualContext> VisualPlugins
        {
            get { return _VisualPlugins; }
            set { _VisualPlugins = value; }
        }

        private string _LastVisualPlugin;

        /// <summary>
        /// Gets or sets the last visual plugin.
        /// </summary>
        /// <value>The last visual plugin.</value>
        public string LastVisualPlugin
        {
            get { return _LastVisualPlugin; }
            set { _LastVisualPlugin = value; }
        }

        private DataSet _PluginSettings;

        /// <summary>
        /// Gets or sets the plugin settings.
        /// </summary>
        /// <value>The plugin settings.</value>
        public DataSet PluginSettings
        {
            get { return _PluginSettings; }
            set { _PluginSettings = value; }
        }

        private string _GpsInfoFile;

        /// <summary>
        /// Gets or sets the GPS info file.
        /// </summary>
        /// <value>The GPS info file.</value>
        public string GpsInfoFile
        {
            get { return _GpsInfoFile; }
            set { _GpsInfoFile = value; }
        }

        private int _GpsInfoRadius;

        /// <summary>
        /// Gets or sets the GPS info radius.
        /// </summary>
        /// <value>The GPS info radius.</value>
        public int GpsInfoRadius
        {
            get { return _GpsInfoRadius; }
            set { _GpsInfoRadius = value; }
        }

        private GpsInfo _GpsInfo;

        /// <summary>
        /// Gets or sets the GPS info.
        /// </summary>
        /// <value>The GPS info.</value>
        public GpsInfo GpsInfo
        {
            get { return _GpsInfo; }
            set { _GpsInfo = value; }
        }

        private bool playSoundOnOpenWLan;

        /// <summary>
        /// Gets or sets a value indicating weather to play a sound when an open WLan was found or not
        /// </summary>
        public bool PlaySoundOnOpenWLan
        {
            get { return playSoundOnOpenWLan; }
            set { playSoundOnOpenWLan = value; }
        }

        private string openWLanSoundFile;

        /// <summary>
        /// Gets or sets the filename of the soundfile to play when an open WLan was found
        /// </summary>
        public string OpenWLanSoundFile
        {
            get { return openWLanSoundFile; }
            set { openWLanSoundFile = value; }
        }
    }
}
