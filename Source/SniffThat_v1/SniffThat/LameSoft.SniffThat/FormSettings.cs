//==========================================================================================
//
//		LameSoft.SniffThat.FormSettings
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
using OpenNETCF.Net;
using System.Reflection;
using LameSoft.SniffThat.Common;
using System.IO;
using LameSoft.SniffThat.Core;

namespace LameSoft.SniffThat
{
    public partial class FormSettings : Form
    {
        private WLanSnifferSettings _Settings;

        private static int[] _AvailableBaudRates = new int[] { 4800, 9600, 19200, 38400, 57600, 76800, 115200 };

        private OpenNETCF.Media.SoundPlayer _Player;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:FormSettings"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public FormSettings(WLanSnifferSettings settings)
        {
            _Settings = settings;

            _Player = new OpenNETCF.Media.SoundPlayer();
            
            InitializeComponent();

            FillControls();

            SetLocaleToControls();

            try
            {
                this.wbModuleInfo.ScriptErrorsSuppressed = true;
            }
            catch (NotSupportedException ex)
            {
				ex.ToString();
			}
            catch
            {
                throw;
            }
        }

        private void SetLocaleToControls()
        {
            lWLanAdapter.Text = SniffThat.lWLanAdapter + ":";
            lScanInterval.Text = SniffThat.lScanInterval + ":";
            tpWireless.Text = SniffThat.tpWireless;
            lGpsComPort.Text = SniffThat.lGpsComPort + ":";
            lGpsBaud.Text = SniffThat.lGpsBaud + ":";
            lMaxGpsRadius.Text = SniffThat.lMaxGpsRadius + ":";
            lGpsInfoFile.Text = SniffThat.lGpsInfoFile + ":";
            tpGps.Text = SniffThat.tpGps;
            miSaveSettings.Text = SniffThat.miSaveSettings;
            miCancelSettings.Text = SniffThat.miCancelSettings;
            tpModules.Text = SniffThat.tpModules;
			miSettings.Text = SniffThat.miSettings;
            cbPlaySound.Text = SniffThat.OpenWLanSound;
            lSoundFile.Text = SniffThat.SoundFile;
            bStop.Text = SniffThat.bStop;
            bTest.Text = SniffThat.bPlay;
        }

        /// <summary>
        /// Fills the controls.
        /// </summary>
        private void FillControls()
        {
            FillComboBox();
            FillSoundComboBox();

            nudInterval.Value = Math.Max(Math.Min(_Settings.Interval, nudInterval.Maximum), nudInterval.Minimum);
            nudGpsInfoRadius.Value = Math.Max(Math.Min(_Settings.GpsInfoRadius, nudGpsInfoRadius.Maximum), nudGpsInfoRadius.Minimum);

            cbComPort.Text = _Settings.ComPort;

            tbGpsInfo.Text = _Settings.GpsInfoFile;

            TreeNode visualRootNode = tvModules.Nodes.Add(SniffThat.NodeVisual);
            visualRootNode.Tag = String.Format("<html><body><b>{0}</b></body></html>", SniffThat.NodeVisual);
            TreeNode exportRootNode = tvModules.Nodes.Add(SniffThat.NodeExport);
            exportRootNode.Tag = String.Format("<html><body><b>{0}</b></body></html>", SniffThat.NodeExport);

            foreach (VisualContext vc in _Settings.VisualPlugins)
            {
                //AddPluginTab(vc.VisualPlugin);
                AddPluginTreeEntry(vc, visualRootNode);
            }

            CheckChecked(visualRootNode);
            visualRootNode.ExpandAll();

            foreach (ExportContext expc in _Settings.ExportPlugins)
            {
                //AddPluginTab(expc.ExportPlugin);
                AddPluginTreeEntry(expc, exportRootNode);
            }

            CheckChecked(exportRootNode);
            exportRootNode.ExpandAll();

            tvModules.SelectedNode = visualRootNode;

            cbPlaySound.Checked = _Settings.PlaySoundOnOpenWLan;

            foreach (object item in comboSoundFile.Items)
            {
                if (Object.Equals((item as string).ToLower(), _Settings.OpenWLanSoundFile.ToLower().Replace("\\windows\\", "")))
                {
                    comboSoundFile.SelectedItem = item;
                }
            }
        }

        private void FillSoundComboBox()
        {
            foreach (string file in Directory.GetFiles("\\Windows", "*.wav"))
            {
                comboSoundFile.Items.Add(Path.GetFileName(file));
            }
        }

        private void AddPluginTreeEntry(IModuleContext context, TreeNode parent)
        {
            TreeNode node = parent.Nodes.Add(context.Plugin.PluginName);
            node.Tag = context;
            node.Checked = context.Active;
        }

        private string GetPluginHtml(IPlugin plugin)
        {
            StringBuilder sb = new StringBuilder();

			Stream manifestStream = SniffThatCore.PluginTemplate;
            StreamReader sr = new StreamReader(manifestStream);
            string html = sr.ReadToEnd();

            sr.Close();

            //TODO Version hinzufügen

            return String.Format(html, plugin.PluginName, plugin.PluginAuthor, plugin.PluginDescription);
        }

        private const int C_LeftIndent = 8;

        private void AddPluginTab(IPlugin plugin)
        {
            TabPage tp = new TabPage();
            tp.Text = plugin.PluginName;

            tabControl1.TabPages.Add(tp);

            Label lAuthorText = new Label();
            lAuthorText.Text = SniffThat.Author + ":";
            lAuthorText.Width = 80;
            lAuthorText.Left = C_LeftIndent;
            lAuthorText.Top = 8;

            tp.Controls.Add(lAuthorText);

            Label lAuthor = new Label();
            lAuthor.Top = lAuthorText.Top;
            lAuthor.Left = lAuthorText.Left + lAuthorText.Width + 8;
            lAuthor.Width = tp.ClientRectangle.Width - lAuthor.Left - 8;
            lAuthor.Text = plugin.PluginAuthor;

            tp.Controls.Add(lAuthor);

            Label lDesc = new Label();
            lDesc.Text = plugin.PluginDescription;
            lDesc.Top = lAuthorText.Top + lAuthorText.Height + 8;
            lDesc.Left = C_LeftIndent;

            Graphics g = this.CreateGraphics();
            SizeF sf = g.MeasureString(lDesc.Text, lDesc.Font);

            g.Dispose();

            lDesc.Height = (int)(2 * sf.Height);
            lDesc.Left = C_LeftIndent;
            lDesc.Width = tp.ClientRectangle.Width - 2 * C_LeftIndent;

            tp.Controls.Add(lDesc);

            Panel settingsPanel = new Panel();
            settingsPanel.Top = lDesc.Top + lDesc.Height;
            settingsPanel.Left = C_LeftIndent;
            settingsPanel.Width = lDesc.Width;
            settingsPanel.Height = tp.ClientRectangle.Height - lDesc.Top - lDesc.Height - 8;
            settingsPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            tp.Controls.Add(settingsPanel);

            if (plugin.GlobalSettingsControl != null)
            {
                Control ctrl = plugin.GlobalSettingsControl;
                ctrl.Dock = DockStyle.Fill;
                settingsPanel.Controls.Add(ctrl);
            }
        }

        /// <summary>
        /// Fills the combo box.
        /// </summary>
        private void FillComboBox()
        {
            cbWLan.Items.Clear();

            AdapterCollection ac = Networking.GetAdapters();

            cbWLan.DisplayMember = "Name";

            foreach (Adapter a in ac)
            {
                cbWLan.Items.Add(a);
            }

            cbWLan.Text = _Settings.Adapter;

            cbBaud.Items.Clear();

            foreach (int b in _AvailableBaudRates)
            {
                cbBaud.Items.Add(b);
            }

            if (cbBaud.Items.IndexOf(_Settings.BaudRate) > 0)
                cbBaud.SelectedIndex = cbBaud.Items.IndexOf(_Settings.BaudRate);
            else
                cbBaud.SelectedIndex = 0;
        }

        /// <summary>
        /// Handles the Click event of the bSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void bSave_Click(object sender, EventArgs e)
        {           
            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        private void Save()
        {
            _Settings.Adapter = cbWLan.Text;

            _Settings.Interval = (int)nudInterval.Value;

            _Settings.GpsInfoRadius = (int)nudGpsInfoRadius.Value;

            _Settings.ComPort = cbComPort.Text;

            _Settings.BaudRate = (int)cbBaud.SelectedItem;

            _Settings.GpsInfoFile = tbGpsInfo.Text;

            _Settings.OpenWLanSoundFile = "\\Windows\\" + (string)comboSoundFile.SelectedItem;
            _Settings.PlaySoundOnOpenWLan = cbPlaySound.Checked;
        }

        /// <summary>
        /// Handles the Click event of the bCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void bCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the bRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void bRefresh_Click(object sender, EventArgs e)
        {
            FillComboBox();
        }

        /// <summary>
        /// Handles the Closing event of the FormSettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void FormSettings_Closing(object sender, CancelEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
                Save();
        }

        /// <summary>
        /// Handles the Click event of the bSelectGpsInfo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void bSelectGpsInfo_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = tbGpsInfo.Text;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                tbGpsInfo.Text = openFileDialog1.FileName;
        }

        private void tvModules_AfterSelect(object sender, TreeViewEventArgs e)
        {
			if ((e.Node.Nodes.Count == 0) && (e.Node.Tag is IModuleContext)) //Leaf
            {
                IModuleContext context = (IModuleContext)e.Node.Tag;
                wbModuleInfo.Tag = GetPluginHtml(context.Plugin);
				miSettings.Enabled = (context.Plugin.GlobalSettingsControl != null);
            }
            else
                wbModuleInfo.Tag = (string)e.Node.Tag;

            wbModuleInfo.DocumentText = (string)wbModuleInfo.Tag;
        }

        private bool _Navigated = false;

        private void wbModuleInfo_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {

            if (!_Navigated)
            {
                _Navigated = true;
                lock (this)
                    wbModuleInfo.DocumentText = (string)wbModuleInfo.Tag;
            }
            else
            {
                _Navigated = false;
            }
        }

        private void tvModules_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if ((e.Action == TreeViewAction.ByKeyboard) || (e.Action == TreeViewAction.ByMouse))
            {
                foreach (TreeNode child in e.Node.Nodes)
                {
                    child.Checked = e.Node.Checked;
                }

                if (e.Node.Parent != null)
                    CheckChecked(e.Node.Parent);
            }

            if (e.Node.Tag is IModuleContext)
                (e.Node.Tag as IModuleContext).Active = e.Node.Checked;
        }

        private void CheckChecked(TreeNode node)
        {
            bool check = true;
            foreach (TreeNode child in node.Nodes)
                check = check && child.Checked;

            if (check != node.Checked)
                node.Checked = check;
        }

        private void wbModuleInfo_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            tvModules.Focus();
        }

		private void miSettings_Click(object sender, EventArgs e)
		{
			if(tvModules.SelectedNode == null)
				return;
			
			IModuleContext context = (IModuleContext)tvModules.SelectedNode.Tag;

			if(context.Plugin.GlobalSettingsControl == null)
				return;

			Form settingsForm = new Form();
			settingsForm.Text = context.Plugin.PluginName;
			context.Plugin.GlobalSettingsControl.Dock = DockStyle.Fill;
			settingsForm.ClientSize = context.Plugin.GlobalSettingsControl.ClientSize;
			settingsForm.Top = this.Top + (this.Height - settingsForm.Height) / 2;
			settingsForm.Left = this.Left + (this.Width - settingsForm.Width) / 2;
			settingsForm.Controls.Add(context.Plugin.GlobalSettingsControl);
			settingsForm.ShowDialog();
		}

        private void bTest_Click(object sender, EventArgs e)
        {
            try
            {
                _Player.SoundLocation = "\\Windows\\" + (string)comboSoundFile.SelectedItem;
                _Player.Play();
            }
            catch(Exception ex)
            {
                MessageBox.Show(String.Format("Error playing the sound file!\r\n{0}", ex.Message));
            }
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            try
            {
                _Player.Stop();
            }
            catch (Exception)
            {
            }
        }
    }
}