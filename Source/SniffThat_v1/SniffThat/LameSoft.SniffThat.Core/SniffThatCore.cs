//==========================================================================================
//
//		LameSoft.SniffThat.Core.SniffThatCore
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
using LameSoft.XMLSettings;
using LameSoft.Mobile.Gps;
using LameSoft.Mobile.Utils;
using LameSoft.Mobile.WirelessLan;
using System.Globalization;
using System.Drawing;
using LameSoft.SniffThat.Common;
using System.Xml.Serialization;
using System.Xml;
using LameSoft.Mobile.GpsInfoResolver;
using OpenNETCF.Net;
using System.IO;
using System.Reflection;
using System.Data;
using LameSoft.SniffThat.Core.Events;
using OpenNETCF;
using System.Threading;

namespace LameSoft.SniffThat.Core
{
    /// <summary>
    /// SniffThat Core functions
    /// </summary>
    public class SniffThatCore
    {
        #region Events

        /// <summary>
        /// Gets raised when the Load State changed
        /// </summary>
        public event LoadStateEventHandler LoadStateChanged;

        /// <summary>
        /// Gets raised when the Status changed
        /// </summary>
        public event StatusChangedEventHandler StatusChanged;

        /// <summary>
        /// Gets raised when the active state of wireless or gps changed
        /// </summary>
        public event ActiveChangedEventHandler ActiveChanged;

        /// <summary>
        /// Gets raised when a message has to be shown
        /// </summary>
        public event ShowMessageEventHandler ShowMessage;

        #endregion

        #region Private Fields

        private IWLanScanner _Scanner;

        private WLanSnifferSettings _Settings;

        private StandbyPreventer _StandbyPreventer = new StandbyPreventer();

        private IGpsScanner _GpsScanner;

        private Settings _SettingsFile;

        private IList<AccessPointEntry> _Entries = new List<AccessPointEntry>();
        private GpsInfo _GpsInfo;

        /// <summary>
        /// Gets the name of the settings file.
        /// </summary>
        /// <value>The name of the settings file.</value>
        private string SettingsFileName
        {
            get
            {
                string myDocumentsPath = Environment2.GetFolderPath(OpenNETCF.Environment2.SpecialFolder.Personal);
                return Path.Combine(myDocumentsPath, "SniffThatSettings.xml");
            }
        }

        #endregion

        #region Event Methods

        internal void OnLoadStateChanged(LoadState loadState, Bitmap activeBmp, Bitmap inactiveBmp)
        {
            if (LoadStateChanged != null)
                LoadStateChanged(this, new LoadStateEventArgs(loadState, activeBmp, inactiveBmp));
        }

        internal void OnLoadStateChanged(string text)
        {
            if (LoadStateChanged != null)
                LoadStateChanged(this, new LoadStateEventArgs(text));
        }

        internal void OnStatusChanged(string text)
        {
            if (StatusChanged != null)
                StatusChanged(this, new StatusChangedEventArgs(text));
        }

        internal void OnStatusChanged(Status status, string text)
        {
            if (StatusChanged != null)
                StatusChanged(this, new StatusChangedEventArgs(status, text));
        }

        internal void OnActiveChanged(Element element, bool active)
        {
            if (ActiveChanged != null)
                ActiveChanged(this, new ActiveChangedEventArgs(element, active));
        }

        internal void OnShowMessage(MessageType messageType)
        {
            if (ShowMessage != null)
                ShowMessage(this, new ShowMessageEventArgs(messageType));
        }

        internal void OnShowMessage(MessageType messageType, string messageText)
        {
            if (ShowMessage != null)
                ShowMessage(this, new ShowMessageEventArgs(messageType, messageText));
        }

        #endregion

        #region public Properties

        /// <summary>
        /// Gets the logo.
        /// </summary>
        /// <value>The logo.</value>
        public static Bitmap Logo
        {
            get
            {
                return Properties.Resources._4;
            }
        }

        /// <summary>
        /// Gets the plugin template.
        /// </summary>
        /// <value>The plugin template.</value>
        public static Stream PluginTemplate
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetManifestResourceStream("LameSoft.SniffThat.Core.PluginTemplate.htm");
            }
        }

        /// <summary>
        /// Gets the GPS info DB version.
        /// </summary>
        /// <value>The GPS info DB version.</value>
        public string GpsInfoDBVersion
        {
            get
            {
                return _GpsInfo.DBVersion;
            }
        }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public WLanSnifferSettings Settings
        {
            get
            {
                return _Settings;
            }
        }

        /// <summary>
        /// Gets the entries.
        /// </summary>
        /// <value>The entries.</value>
        public IList<AccessPointEntry> Entries
        {
            get
            {
                return _Entries;
            }
        }

        #endregion

        #region ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SniffThatCore"/> class.
        /// </summary>
        public SniffThatCore(IWLanScanner wlanScanner, IGpsScanner gpsScanner)
        {
            _SettingsFile = new Settings(SettingsFileName);
            _Settings = new WLanSnifferSettings();

            OnLoadStateChanged(LoadState.LoadSettings, Properties.Resources.LoadSettings_Active, Properties.Resources.LoadSettings_Inactive);
            LoadSettings();

            _GpsInfo = new GpsInfo(_Settings.GpsInfoFile, _Settings.GpsInfoRadius);
            _GpsInfo.GpsInfoRetrieved += new GpsInfoRetrievedEventHandler(_GpsInfo_GpsInfoRetrieved);

            _Settings.GpsInfo = _GpsInfo;

            _GpsScanner = gpsScanner;
            _Scanner = wlanScanner;
        }
        #endregion

        #region Plugin handling
        /// <summary>
        /// Starts a visual plugin.
        /// </summary>
        /// <param name="plugin">The plugin to start.</param>
        public void StartVisualPlugin(IVisualPlugin plugin)
        {
            plugin.Start();
            plugin.ModuleControl.Focus();
            plugin.AccessPointListChanged(_Entries);
        }

        /// <summary>
        /// Fills the list.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="T:LameSoft.Mobile.WirelessLan.VisibleAccessPointCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void UpdateVisuals(object sender, VisibleAccessPointCollectionChangedEventArgs args)
        {
            lock (_Entries)
            {
                IWLanScanner castedSender = (IWLanScanner)sender;

                UpdateEntries(!castedSender.Running);

                SetEntriesToVisuals(!castedSender.Running);
            }
        }

        /// <summary>
        /// Sets the entries to visuals.
        /// </summary>
        /// <param name="force">if set to <c>true</c> [force].</param>
        private void SetEntriesToVisuals(bool force)
        {
            foreach (VisualContext vc in _Settings.VisualPlugins)
            {
                if (vc.Active)
                    vc.SetEntries(_Entries);
            }
        }

        private delegate void SetGpsDataToVisualsEventHandler(GpsPosition pos, GpsMovement mov, IList<GpsSatellite> sats);

        /// <summary>
        /// Sets the GPS data to visuals.
        /// </summary>
        /// <param name="pos">The pos.</param>
        /// <param name="mov">The mov.</param>
        /// <param name="sats">The sats.</param>
        private void SetGpsDataToVisuals(GpsPosition pos, GpsMovement mov, IList<GpsSatellite> sats)
        {
            foreach (VisualContext vc in _Settings.VisualPlugins)
            {
                if (vc.Active)
                    vc.SetGpsData(pos, mov, sats);
            }
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            _Entries.Clear();

            SetEntriesToVisuals(true);
        }

        private Type[] FindModules(Type intface)
        {
            string executable = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath;

            List<Type> mods = new List<Type>();

            AddModules(mods, Path.GetDirectoryName(executable), intface);

            return mods.ToArray();
        }

        /// <summary>
        /// Adds the modules.
        /// </summary>
        /// <param name="modules">The modules.</param>
        /// <param name="path">The path.</param>
        /// <param name="intface">The intface.</param>
        private void AddModules(IList<Type> modules, string path, Type intface)
        {
            //Files in the directory
            string[] fs = Directory.GetFiles(path, "*.dll");

            foreach (string file in fs)
            {
                try
                {
                    Assembly asm = Assembly.LoadFrom(file);
                    Type[] ts = asm.GetTypes();

                    foreach (Type t in ts)
                        foreach (Type intf in t.GetInterfaces())
                            if (intf.Equals(intface))
                            {
                                modules.Add(t);
                                break;
                            }
                }
                catch (Exception)
                { }
            }

            foreach (string subdir in Directory.GetDirectories(path))
            {
                AddModules(modules, subdir, intface);
            }
        }

        /// <summary>
        /// Inits the plugins.
        /// </summary>
        public void InitPlugins()
        {
            _Settings.ExportPlugins = new List<ExportContext>();

            OnLoadStateChanged(LoadState.SearchPlugins, Properties.Resources.SearchPlugins_Active, Properties.Resources.SearchPlugins_Inactive);

            Type[] plugins = FindModules(typeof(IExportPlugin));

            OnLoadStateChanged(LoadState.None, Properties.Resources.ExportPlugins_Active, Properties.Resources.ExportPlugins_Inactive);

            foreach (Type exportpType in plugins)
            {
                try
                {
                    ConstructorInfo ci = exportpType.GetConstructor(new Type[0]);

                    IExportPlugin exp = (IExportPlugin)ci.Invoke(new object[0]);

                    OnLoadStateChanged(exp.PluginName);

                    string settingsKey = exportpType.FullName;

                    if (!_Settings.PluginSettings.Tables.Contains(settingsKey))
                    {
                        DataTable dt = new DataTable(settingsKey);
                        dt.Columns.Add("Key", typeof(string));
                        dt.Columns.Add("Value", typeof(string));
                        _Settings.PluginSettings.Tables.Add(dt);
                    }

                    ExportContext expc = new ExportContext(exp, _Settings.PluginSettings.Tables[settingsKey], _Settings);
                    expc.OnShowError += new MessageEventHandler(expc_OnShowError);
                    expc.OnShowMessage += new MessageEventHandler(expc_OnShowMessage);
                    expc.OnShowStatus += new MessageEventHandler(expc_OnShowStatus);

                    _Settings.ExportPlugins.Add(expc);
                }
                catch (Exception)
                {
                }
            }

            OnLoadStateChanged(LoadState.None, Properties.Resources.VisualPlugins_Active, Properties.Resources.VisualPlugins_Inactive);

            _Settings.VisualPlugins = new List<VisualContext>();

            plugins = FindModules(typeof(IVisualPlugin));

            foreach (Type visualtype in plugins)
            {
                try
                {
                    ConstructorInfo ci = visualtype.GetConstructor(new Type[0]);

                    IVisualPlugin vis = (IVisualPlugin)ci.Invoke(new object[0]);

                    OnLoadStateChanged(vis.PluginName);

                    string settingsKey = visualtype.FullName;

                    if (!_Settings.PluginSettings.Tables.Contains(settingsKey))
                    {
                        DataTable dt = new DataTable(settingsKey);
                        dt.Columns.Add("Key", typeof(string));
                        dt.Columns.Add("Value", typeof(string));
                        _Settings.PluginSettings.Tables.Add(dt);
                    }

                    VisualContext visc = new VisualContext(vis, _Settings.PluginSettings.Tables[settingsKey], _Settings);
                    visc.OnShowError += new MessageEventHandler(visc_OnShowError);
                    visc.OnShowMessage += new MessageEventHandler(visc_OnShowMessage);
                    visc.OnShowStatus += new MessageEventHandler(visc_OnShowStatus);
                    _Settings.VisualPlugins.Add(visc);

                }
                catch (Exception ex)
                {
                    OnShowMessage(MessageType.NoCaption, "Error creating Module \"" + visualtype.FullName + "\":\r\n" + ex.Message);
                }

            }
        }

        /// <summary>
        /// Gets the sorted visual plugins.
        /// </summary>
        /// <value>The sorted visual plugins.</value>
        public IList<VisualContext> SortedVisualPlugins
        {
            get
            {
                ((List<VisualContext>)_Settings.VisualPlugins).Sort(new VisualContextComparer());
                return _Settings.VisualPlugins;
            }
        }

        class VisualContextComparer : IComparer<VisualContext>
        {

            #region IComparer<VisualContext> Members

            public int Compare(VisualContext x, VisualContext y)
            {
                return x.VisualPlugin.PreferredPosition - y.VisualPlugin.PreferredPosition;
            }

            #endregion
        }

        void visc_OnShowStatus(object sender, string message)
        {
            OnStatusChanged(message);
        }

        void visc_OnShowMessage(object sender, string message)
        {
            OnShowMessage(MessageType.VisualMessage, message);
        }

        void visc_OnShowError(object sender, string message)
        {
            OnShowMessage(MessageType.VisualError, message);
        }

        void expc_OnShowStatus(object sender, string message)
        {
            OnStatusChanged(message);
        }

        void expc_OnShowMessage(object sender, string message)
        {
            OnShowMessage(MessageType.ExportMessage, message);
        }

        void expc_OnShowError(object sender, string message)
        {
            OnShowMessage(MessageType.ExportError, message);
        }
        #endregion

        #region Scanner Events

        /// <summary>
        /// Handles the GpsInfo event of the _GpsScanner control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="T:LameSoft.Mobile.Gps.GpsInfoEventArgs"/> instance containing the event data.</param>
        void _GpsScanner_GpsInfo(object sender, GpsInfoEventArgs args)
        {
            try
            {
                _LastPosition = args.Position;

                GpsScanner scanner = (GpsScanner)sender;

                GpsPosition pos = args.Position;
                GpsMovement mov = args.Movement;
                IList<GpsSatellite> sats = args.Satellites;

                if (args.GpsFix == GpsFix.FixNone)
                {
                    pos = null;
                    mov = null;
                }

                SetGpsDataToVisuals(pos, mov, args.Satellites);

                OnActiveChanged(Element.Gps, scanner.State == GpsScannerState.Running);

                if (scanner.State == GpsScannerState.Running)
                {
                    string fix = "No Fix";
                    if (args.GpsFix == GpsFix.Fix2D)
                        fix = "Fix 2D";
                    else if (args.GpsFix == GpsFix.Fix3D)
                        fix = "Fix 3D";

                    OnStatusChanged(Status.GpsStatus, ": " + args.NumOfSats + " Satelites, " + fix);
                }
                else
                {
                    OnStatusChanged(Status.GpsStopped, "");
                }
            }
            catch (Exception)
            { }
        }

        private void GpsInfoRcvd(object sender, GpsInfoRetrievedEventArgs args)
        {
            int index = 0;
            lock (_Entries)
            {
                foreach (AccessPointEntry ape in _Entries)
                {
                    if (args.Position.Latitude.Equals(ape.Latitude) && args.Position.Longitude.Equals(ape.Longitude))
                    {
                        ape.GpsInfo = (args.GpsInfo == null) ? "" : args.GpsInfo;
                        ape.GpsInfoDistance = args.Distance;
                    }
                    index++;
                }

                SetEntriesToVisuals(false);
            }
        }

        void _GpsInfo_GpsInfoRetrieved(object sender, GpsInfoRetrievedEventArgs args)
        {
            GpsInfoRcvd(sender, args);
            //this.Invoke(new GpsInfoRetrievedEventHandler(GpsInfoRcvd), sender, args);
        }

        /// <summary>
        /// Updates the entries.
        /// </summary>
        /// <param name="force">if set to <c>true</c> [force].</param>
        private void UpdateEntries(bool force)
        {
            IList<AccessPointEntry> updated = new List<AccessPointEntry>();
            if (_Scanner != null && _Scanner.VisibleAccessPoints != null)
            {
                foreach (LameSoft.Mobile.WirelessLan.AccessPoint ap in _Scanner.VisibleAccessPoints)
                {
                    AccessPointEntry tmpEntry = new AccessPointEntry(ap.Name, ap.MacAddress, -1);
                    if (_Entries.Contains(tmpEntry))
                    {
                        int index = _Entries.IndexOf(tmpEntry);
                        FillEntry(_Entries[index], ap, index + 1);
                    }
                    else //new access point
                    {
                        FillEntry(tmpEntry, ap, _Entries.Count + 1);
                        tmpEntry.FirstSeen = DateTime.Now;
                        _Entries.Add(tmpEntry);
                        if (tmpEntry.IsOpen && _Settings.PlaySoundOnOpenWLan && File.Exists(_Settings.OpenWLanSoundFile))
                        {
                            try
                            {
                                OpenNETCF.Media.SoundPlayer soundPlayer = new OpenNETCF.Media.SoundPlayer();
                                soundPlayer.SoundLocation = _Settings.OpenWLanSoundFile;
                                soundPlayer.Play();
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    updated.Add(tmpEntry);
                }
            }

            int idx = 0;

            foreach (AccessPointEntry ape in _Entries)
            {
                if (!updated.Contains(ape))
                {
                    if ((ape.Strength != "N/A") || (ape.Visible))
                    {
                        ape.Strength = "N/A";
                        ape.Visible = false;
                    }
                    UpdateWLanHistory(ape, false, force);
                }
                else
                    UpdateWLanHistory(ape, true, force);
                idx++;
            }
        }

        private void UpdateWLanHistory(AccessPointEntry ape, bool available, bool force)
        {
            ape.WLanHistory.Add(ape.StrengthDB, available, force);
        }

        GpsPosition _LastPosition = null;

        /// <summary>
        /// Fills the entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <param name="accesspoint">The accesspoint.</param>
        /// <param name="nr">the number of the entry</param>
        private void FillEntry(AccessPointEntry entry, LameSoft.Mobile.WirelessLan.AccessPoint accesspoint, int nr)
        {
            entry.Protected = accesspoint.Privacy > 0;
            entry.Strength = accesspoint.Strength.ToString();
            entry.StrengthDB = accesspoint.SignalStrengthInDecibels;
            entry.LastSeen = DateTime.Now;
            entry.Privacy = accesspoint.Privacy;
            if ((((entry.Longitude == 0) && (entry.Latitude == 0)) || (entry.StrengthAtSavedPosition < accesspoint.SignalStrengthInDecibels)) && (_LastPosition != null))
            {
                entry.Latitude = _LastPosition.Latitude;
                entry.Longitude = _LastPosition.Longitude;
                entry.StrengthAtSavedPosition = accesspoint.SignalStrengthInDecibels;
                _GpsInfo.AddWork(new GpsPositionJob(entry.Longitude, entry.Latitude));
            }
            entry.Visible = true;
            entry.SupportedRates.Clear();
            foreach (byte b in accesspoint.SupportedRates)
            {
                int b2 = b & 0x7F;// w/o high Bit
                double rate = b2 * .5;

                entry.SupportedRates.Add(rate);
            }

            entry.Nr = nr;

            //if (!entry.InternetAccessibleSet)
            //{
            //    ThreadPool.QueueUserWorkItem(
            //        delegate(object target)
            //        {
            //            try
            //            {
            //                entry.InternetAccessible = AccessPointConnectionChecker.IsInternetAccessible(_Settings.GetAdapter(), accesspoint);
            //                SetEntriesToVisuals();
            //            }
            //            catch (Exception ex)
            //            {
            //                ex.ToString();
            //            }
            //        });
            //}
        }

        /// <summary>
        /// Handles the VisibleAccessPointCollectionChanged event of the _Scanner control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="T:LameSoft.Mobile.WirelessLan.VisibleAccessPointCollectionChangedEventArgs"/> instance containing the event data.</param>
        public void _Scanner_VisibleAccessPointCollectionChanged(object sender, VisibleAccessPointCollectionChangedEventArgs args)
        {
            UpdateVisuals(sender, args);
        }


        #endregion

        #region Settings

        /// <summary>
        /// Loads the settings.
        /// </summary>
        private void LoadSettings()
        {
            _Settings.Adapter = _SettingsFile["AdapterName"];
            _Settings.ComPort = _SettingsFile["GPSComPort"];
            _Settings.Interval = _SettingsFile.Integer["ScanInterval"];
            _Settings.LastVisualPlugin = _SettingsFile["LastVisualPlugin"];
            _Settings.GpsInfoFile = _SettingsFile["GpsInfoFile"];
            _Settings.GpsInfoRadius = _SettingsFile.Integer["GpsInfoRadius"];
            _Settings.PlaySoundOnOpenWLan = _SettingsFile.Boolean["PlaySound"];
            _Settings.OpenWLanSoundFile = _SettingsFile["SoundFile"];
            try
            {
                _Settings.BaudRate = _SettingsFile.Integer["GPSBaudRate"];
            }
            catch (Exception)
            {
                _Settings.BaudRate = 4800;
            }

            try
            {
                string val = _SettingsFile["PluginSettings"];

                byte[] bs = Convert.FromBase64String(val);

                XmlSerializer ser = new XmlSerializer(typeof(DataSet));

                MemoryStream ms = new MemoryStream(bs);

                Object o = ser.Deserialize(ms);

                _Settings.PluginSettings = (DataSet)o;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                _Settings.PluginSettings = new DataSet();
            }
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        private void SaveSettings()
        {
            _SettingsFile["AdapterName"] = _Settings.Adapter;
            _SettingsFile["GPSComPort"] = _Settings.ComPort;
            _SettingsFile.Integer["ScanInterval"] = _Settings.Interval;
            _SettingsFile["GPSBaudRate"] = _Settings.BaudRate.ToString();
            _SettingsFile["LastVisualPlugin"] = _Settings.LastVisualPlugin;

            _SettingsFile["GpsInfoFile"] = _Settings.GpsInfoFile;
            _SettingsFile.Integer["GpsInfoRadius"] = _Settings.GpsInfoRadius;
            _SettingsFile.Boolean["PlaySound"] = _Settings.PlaySoundOnOpenWLan;
            _SettingsFile["SoundFile"] = _Settings.OpenWLanSoundFile;

            try
            {
                MemoryStream ms = new MemoryStream();

                XmlSerializer ser = new XmlSerializer(typeof(DataSet));

                ser.Serialize(ms, _Settings.PluginSettings);
                ms.Flush();

                byte[] bs = ms.ToArray();

                _SettingsFile["PluginSettings"] = Convert.ToBase64String(bs);
            }
            catch (Exception)
            {
                _SettingsFile["PluginSettings"] = "";
            }

            if (File.Exists(SettingsFileName))
            {
                FileInfo fi = new FileInfo(SettingsFileName);
                fi.Attributes = fi.Attributes & ~FileAttributes.ReadOnly;
                fi.Delete();
            }

            _SettingsFile.WriteSettings();
        }
        #endregion

        #region Start and Stop Scanning

        private string _LastAdapter = "";

        private int _LastInterval = 0;

        /// <summary>
        /// Sets the scan.
        /// </summary>
        /// <param name="scan">if set to <c>true</c> [scan].</param>
        /// <param name="showErrors">if set to <c>true</c> [show errors].</param>
        public void SetScan(bool scan, bool showErrors)
        {
            if (scan)
            {
                try
                {
                    _Scanner.Stop();
                    OnActiveChanged(Element.Scan, false);
                }
                catch (Exception)
                { }

                _LastAdapter = _Settings.Adapter;
                _LastInterval = _Settings.Interval;
                Adapter a = _Settings.GetAdapter();
                if (a != null)
                {
                    _Scanner.AdapterName = a.Name;
                    _Scanner.Interval = _Settings.Interval;
                    _Scanner.VisibleAccessPointCollectionChanged += new VisibleAccessPointCollectionChangedEventHandler(_Scanner_VisibleAccessPointCollectionChanged);
                    _Scanner.Start();
                    OnActiveChanged(Element.Scan, true);
                }
                else if (showErrors)
                {
                    OnShowMessage(MessageType.WirelessAdapterNotFound);
                }
            }
            else
            {
                if (_Scanner != null)
                    _Scanner.Stop();
                OnActiveChanged(Element.Scan, false);
            }
        }

        /// <summary>
        /// Sets the GPS.
        /// </summary>
        /// <param name="gps">if set to <c>true</c> [GPS].</param>
        /// <param name="showErrors">if set to <c>true</c> [show errors].</param>
        public void SetGps(bool gps, bool showErrors)
        {
            if (gps)
            {
                try
                {
                    _GpsScanner.Port = _Settings.ComPort;
                    _GpsScanner.Baud = _Settings.BaudRate;
                    _GpsScanner.RefreshRate = _Settings.Interval;
                    _GpsScanner.Start();
                }
                catch (Exception ex)
                {
                    if (showErrors)
                        OnShowMessage(MessageType.GpsOpenError, ex.Message);
                }
            }
            else
            {
                try
                {
                    _GpsScanner.Stop();
                }
                catch (Exception ex)
                {
                    if (showErrors)
                        OnShowMessage(MessageType.GpsCloseError, ex.Message);
                }
            }
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            LoadLastAccessPoints();
            SetGps(true, false);
            SetScan(true, false);
        }
        #endregion

        #region Core Methods

        /// <summary>
        /// Refreshes this instance.
        /// </summary>
        public void Refresh()
        {
            if (!_LastAdapter.Equals(_Settings.Adapter) || (_LastInterval != _Settings.Interval))
                SetScan(true, false);
            SetGps(true, false);
            _GpsInfo.Start(_Settings.GpsInfoFile, _Settings.GpsInfoRadius);

        }

        /// <summary>
        /// Inits this instance.
        /// </summary>
        public void Init()
        {
            OnLoadStateChanged(LoadState.StartApplication, Properties.Resources.StartApplication_Active, Properties.Resources.StartApplication_Inactive);

            SetEntriesToVisuals(true);

            _GpsScanner.GpsInfo += new GpsInfoEventHandler(_GpsScanner_GpsInfo);

            _StandbyPreventer.Start();
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            try
            {
                _GpsScanner.Stop();
            }
            catch (Exception)
            {
            }

            try
            {
                _GpsInfo.Stop();
            }
            catch (Exception)
            {
            }

            _StandbyPreventer.Stop();

            foreach (VisualContext vc in _Settings.VisualPlugins)
                (vc.Plugin as IVisualPlugin).Stop();

            SaveSettings();

            SaveLastAccessPoints();
        }

        /// <summary>
        /// Gets a value indicating if the plugin is active
        /// </summary>
        /// <param name="plugin">The plugin.</param>
        /// <returns></returns>
        public bool PluginActive(IPlugin plugin)
        {
            foreach (VisualContext vc in _Settings.VisualPlugins)
                if (vc.Plugin == plugin)
                    return vc.Active;

            foreach (ExportContext ec in _Settings.ExportPlugins)
                if (ec.Plugin == plugin)
                    return ec.Active;

            return false;
        }

        #endregion

        #region LastAccessPoints

        private void SaveLastAccessPoints()
        {
            try
            {
                SaveEntries(LastAccessPointFilePath, _Entries);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private string LastAccessPointFilePath
        {
            get
            {
                string myDocumentsPath = Environment2.GetFolderPath(OpenNETCF.Environment2.SpecialFolder.Personal);
                return Path.Combine(myDocumentsPath, "SniffThatLastAccessPoints.xml");
            }
        }

        private void LoadLastAccessPoints()
        {
            try
            {
                List<AccessPointEntry> newAccessPoints = (List<AccessPointEntry>)LoadEntries(LastAccessPointFilePath);

                int index = _Entries.Count + 1;

                (_Entries as List<AccessPointEntry>).AddRange(newAccessPoints);

                foreach (AccessPointEntry entry in newAccessPoints)
                {
                    entry.Nr = index;
                    index++;
                }

                UpdateVisuals(this, new VisibleAccessPointCollectionChangedEventArgs());
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        #endregion

        #region Entries

        /// <summary>
        /// Saves the entries.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public void SaveEntries(string filename)
        {
            SaveEntries(filename, _Entries);
        }

        /// <summary>
        /// Saves the entries.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="entries">The entries to save</param>
        public void SaveEntries(string filename, IList<AccessPointEntry> entries)
        {
            XmlSerializer xmlSer = new XmlSerializer(typeof(List<AccessPointEntry>));

            XmlTextWriter xmltw = new XmlTextWriter(filename, Encoding.Unicode);

            xmlSer.Serialize(xmltw, entries);

            xmltw.Flush();
            xmltw.Close();
        }

        /// <summary>
        /// Loads the entries.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        public IList<AccessPointEntry> LoadEntries(string filename)
        {
            XmlSerializer xmlSer = new XmlSerializer(typeof(List<AccessPointEntry>));

            XmlTextReader xmltr = new XmlTextReader(filename);

            Object result = null;

            result = xmlSer.Deserialize(xmltr);

            xmltr.Close();

            return UpdateNumbers((List<AccessPointEntry>)result);
        }

        private IList<AccessPointEntry> UpdateNumbers(List<AccessPointEntry> list)
        {
            for (int i = 0; i < list.Count; i++)
                list[i].Nr = i + 1;

            return list;
        }

        /// <summary>
        /// Loads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void Load(string fileName)
        {
            _Entries = LoadEntries(fileName);

            int index = 0;

            foreach (AccessPointEntry entry in _Entries)
            {
                index++;
            }

            UpdateVisuals(this, new VisibleAccessPointCollectionChangedEventArgs());
        }

        /// <summary>
        /// Merges the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void Merge(string fileName)
        {
            IList<AccessPointEntry> entries = LoadEntries(fileName);

            int index = _Entries.Count;

            foreach (AccessPointEntry ape in entries)
            {
                if (!_Entries.Contains(ape))
                {
                    ape.Nr = index + 1;
                    _Entries.Add(ape);
                    index++;
                }
            }

            UpdateVisuals(this, new VisibleAccessPointCollectionChangedEventArgs());

        }

        /// <summary>
        /// Refreshes the GPS info.
        /// </summary>
        public void RefreshGpsInfo()
        {
            _GpsInfo.CheckBuffer = false;
            foreach (AccessPointEntry ape in _Entries)
            {
                if ((ape.Longitude > 0) && (ape.Latitude > 0))
                    _GpsInfo.AddWork(new GpsPositionJob(ape.Longitude, ape.Latitude));
            }
            _GpsInfo.CheckBuffer = true;
        }
        #endregion
    }
}
