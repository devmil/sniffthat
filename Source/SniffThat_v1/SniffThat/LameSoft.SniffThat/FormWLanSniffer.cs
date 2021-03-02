//==========================================================================================
//
//		LameSoft.SniffThat.FormWLanSniffer
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Collections;
using System.Xml.Serialization;
using System.Xml;
using LameSoft.SniffThat.Common;
using System.Threading;
using System.Globalization;
using LameSoft.SniffThat.Core;
using LameSoft.Mobile.Utils;
using LameSoft.SniffThat.Core.Events;
using LameSoft.Mobile.Gps;
using LameSoft.Mobile.WirelessLan;

namespace LameSoft.SniffThat
{
    public partial class FormWLanSniffer : Form
    {
		private List<TabPage> _AvailableTabPages = new List<TabPage>();

		internal const string C_SPLASHKEY = "SniffThatSplash";

		private SniffThatCore _Core;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:FormWLanSniffer"/> class.
        /// </summary>
        public FormWLanSniffer()
        {
			InitializeComponent();

			SniffThat.Culture = CultureInfo.CurrentCulture;

			SetLocaleToMenus();

            //Cursor.Current = Cursors.WaitCursor;

			_Core = new SniffThatCore(new WLanScanner(), new GpsScanner());
			_Core.LoadStateChanged += new LoadStateEventHandler(_Core_LoadStateChanged);
			_Core.StatusChanged += new StatusChangedEventHandler(_Core_StatusChanged);
			_Core.ActiveChanged += new ActiveChangedEventHandler(_Core_ActiveChanged);
			_Core.ShowMessage += new ShowMessageEventHandler(_Core_ShowMessage);

			InitPlugins();

			_Core.Init();

            FillAbout();
            Cursor.Current = Cursors.Default;
        }

		void _Core_ShowMessage(object sender, ShowMessageEventArgs args)
		{
			switch (args.MessageType)
			{
				case MessageType.WirelessAdapterNotFound:
					MessageBox.Show(SniffThat.WirelessAdapterNotFoundError, SniffThat.WirelessAdapterNotFoundErrorHead);
					break;
				case MessageType.GpsOpenError:
					MessageBox.Show(args.MessageText, SniffThat.GpsOpenError);
					break;
				case MessageType.GpsCloseError:
					MessageBox.Show(args.MessageText, SniffThat.GpsCloseError);
					break;
				case MessageType.NoCaption:
					MessageBox.Show(args.MessageText);
					break;
				case MessageType.VisualError:
					MessageBox.Show(args.MessageText, SniffThat.VisualError);
					break;
				case MessageType.VisualMessage:
					MessageBox.Show(args.MessageText, SniffThat.VisualMessage);
					break;
				case MessageType.ExportError:
					MessageBox.Show(args.MessageText, SniffThat.ExportError);
					break;
				case MessageType.ExportMessage:
					MessageBox.Show(args.MessageText, SniffThat.ExportMessage);
					break;
			}
		}

		void _Core_ActiveChanged(object sender, ActiveChangedEventArgs args)
		{
			this.Invoke(new ThreadStart(
				delegate()
				{
					switch (args.Element)
					{
						case Element.Gps:
							miGps.Checked = args.Active;
							break;
						case Element.Scan:
							miScan.Checked = args.Active;
							break;
					}
				}));
		}

		void _Core_StatusChanged(object sender, StatusChangedEventArgs args)
		{
			string sbarText = "";
			if(args.Status != Status.None)
			{
				switch (args.Status)
				{
					case Status.GpsStatus:
						sbarText += SniffThat.GpsState;
						break;
					case Status.GpsStopped:
						sbarText = SniffThat.GpsStopped;
						break;
				}
			}

			sbarText += args.Text;

			sBar.Invoke(new SetStringEventHandler(SetGPSState), sbarText);
		}

		private void SetGPSState(string text)
		{
			if (!sBar.Text.Equals(text))
				sBar.Text = text;
		}

		void _Core_LoadStateChanged(object sender, LoadStateEventArgs args)
		{
            string msg = "";
            switch (args.LoadState)
            {
                case LoadState.LoadSettings:
                    msg = SniffThat.LoadSettings;
                    break;
                case LoadState.StartApplication:
                    msg = SniffThat.StartApplication;
                    break;
                case LoadState.SearchPlugins:
                    msg = SniffThat.SearchPlugins;
                    break;
            }
            
            this.Invoke(new ThreadStart(
				delegate()
				{
					if (args.LoadState != LoadState.TextOnly)
						SplashScreen.SetState(C_SPLASHKEY, msg, args.ActiveBitmap, args.InactiveBitmap);
					else
						SplashScreen.SetState(C_SPLASHKEY, args.Text);

					Application.DoEvents();
				}));
		}

		private void SetLocaleToMenus()
        {
            miVisuals.Text = SniffThat.miVisuals;
            miAccessPoints.Text = SniffThat.miAccessPoints;
            miLoad.Text = SniffThat.miLoad;
            miMerge.Text = SniffThat.miMerge;
            miSave.Text = SniffThat.miSave;
            miClear.Text = SniffThat.miClear;
            miExport.Text = SniffThat.miExport;
            miRefreshGpsInfo.Text = SniffThat.miRefreshGpsInfo;
            miMenu.Text = SniffThat.miMenu;
            miExit.Text = SniffThat.miExit;

            miSettings.Text = SniffThat.miSettings;
            miScan.Text = SniffThat.miScan;
            miGps.Text = SniffThat.miGps;

            tpAbout.Text = SniffThat.tpAbout;
        }

        private void FillAbout()
        {
            try
            {
                lHead.Text = "SniffThat v" + Assembly.GetExecutingAssembly().GetName().Version.ToString(2);

                pictureBox1.Image = SniffThatCore.Logo;

                if (_Core.GpsInfoDBVersion != null)
                    lDBVersion.Text = SniffThat.GpsInfoDBVersion + ": " + _Core.GpsInfoDBVersion;
                else
                    lDBVersion.Text = SniffThat.IllegalDBFile;
            }
            catch
            { }
        }

        private delegate void SetmiCheckedEventHandler(MenuItem item, bool chk);

        private void SetmiChecked(MenuItem item, bool chk)
        {
            item.Checked = chk;
        }

        private delegate void SetStringEventHandler(string text);

        /// <summary>
        /// Handles the Click event of the menuItem2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void menuItem2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the miScan control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void miScan_Click(object sender, EventArgs e)
        {
            _Core.SetScan(!miScan.Checked, true);
        }

        /// <summary>
        /// Handles the Click event of the miClear control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void miClear_Click(object sender, EventArgs e)
        {
			_Core.Clear();
        }

        /// <summary>
        /// Handles the Closing event of the FormWLanSniffer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void FormWLanSniffer_Closing(object sender, CancelEventArgs e)
        {
			_Core.Settings.LastVisualPlugin = tcVisuals.TabPages[tcVisuals.SelectedIndex].Text;

			_Core.Stop();

			//foreach (TabPage tp in _AvailableTabPages)
			//    try
			//    {
			//        if (tp.Tag != null)
			//            ((IVisualPlugin)tp.Tag).Stop();
			//    }
			//    catch (Exception)
			//    { }
        }

        /// <summary>
        /// Handles the Click event of the miSettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void miSettings_Click(object sender, EventArgs e)
        {
            FormSettings frm = new FormSettings(_Core.Settings);
            frm.ShowDialog();
            if(frm.DialogResult == DialogResult.OK) //Settings saved
            {
				_Core.Refresh();
                FillAbout();

                RefreshTabs();
            }
        }

        private void RefreshTabs()
        {
            tcVisuals.TabPages.Clear();

            foreach (TabPage tp in _AvailableTabPages)
            {
                if (!(tp.Tag is IPlugin) || _Core.PluginActive(tp.Tag as IPlugin))
                {
                    tcVisuals.TabPages.Add(tp);
                    if (_Core.Settings.LastVisualPlugin.Equals(tp.Text))
                        tcVisuals.SelectedIndex = tcVisuals.TabPages.IndexOf(tp);
                }
            }


            miVisuals.MenuItems.Clear();

            foreach (TabPage tp in tcVisuals.TabPages)
            {
                MenuItem mi = new MenuItem();
                mi.Text = tp.Text;
                mi.Click += new EventHandler(mi_Click);
                miVisuals.MenuItems.Add(mi);
            }

        }

        /// <summary>
        /// Handles the Load event of the FormWLanSniffer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FormWLanSniffer_Load(object sender, EventArgs e)
        {
			_Core.Start();
            SplashScreen.CloseSplash(C_SPLASHKEY);
        }

        private void miSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
					_Core.SaveEntries(saveFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(SniffThat.ListSaveError + ":\r\n" + ex.Message, SniffThat.ListSaveErrorHead);
                }
            }
        }

        private void miLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
					_Core.Load(openFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(SniffThat.ListLoadError + ":\r\n" + ex.Message, SniffThat.ListLoadErrorHead);
                }
            }
        }

        private void miMerge_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
				_Core.Merge(openFileDialog1.FileName);
            }
        }

        private void miGps_Click(object sender, EventArgs e)
        {
            _Core.SetGps(!miGps.Checked, true);
        }

		public void InitPlugins()
		{
			_AvailableTabPages.Clear();

			_Core.InitPlugins();

			foreach (VisualContext visc in _Core.SortedVisualPlugins)
			{
				try
				{
					TabPage tp = new TabPage();

					IVisualPlugin vis = (IVisualPlugin)visc.Plugin;

					tp.Text = visc.ToString();
					Control c = vis.ModuleControl;
					c.Dock = DockStyle.Fill;
					tp.Controls.Add(c);
					tp.Tag = vis;

					if (tp.Text.Equals("List"))
						_AvailableTabPages.Insert(0, tp);
					else
						_AvailableTabPages.Add(tp);

				}
				catch (Exception ex)
				{
					MessageBox.Show(SniffThat.ModuleAddError + " \"" + visc.ToString() + "\":\r\n" + ex.Message);
				}
			}

			_AvailableTabPages.Add(tpAbout);

			RefreshTabs();

		}

        void mi_Click(object sender, EventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            int idx = mi.Parent.MenuItems.IndexOf(mi);

            tcVisuals.SelectedIndex = idx;
        }

        private void miExport_Click(object sender, EventArgs e)
        {
            FormExport frmEx = new FormExport(_Core.Settings, _Core.Entries);
            frmEx.ShowDialog();
        }

        private void tcVisuals_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage tp in tcVisuals.TabPages)
                {
                    if (tcVisuals.TabPages.IndexOf(tp) == tcVisuals.SelectedIndex)
                    {
                        if (tcVisuals.TabPages[tcVisuals.SelectedIndex].Tag != null)
                        {
							_Core.StartVisualPlugin((tcVisuals.TabPages[tcVisuals.SelectedIndex].Tag as IVisualPlugin));
                        }
                    }
                    else
                        if(tp.Tag != null)
                            (tp.Tag as IVisualPlugin).Stop();
                }
            }
            catch (Exception)
            { }
        }

        private void miRefreshGpsInfo_Click(object sender, EventArgs e)
        {
			_Core.RefreshGpsInfo();
        }
    }
}