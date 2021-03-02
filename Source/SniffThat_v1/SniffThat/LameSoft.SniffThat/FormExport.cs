//==========================================================================================
//
//		LameSoft.SniffThat.FormExport
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
using LameSoft.SniffThat.Common;
using LameSoft.SniffThat.Core;

namespace LameSoft.SniffThat
{
    public partial class FormExport : Form
    {
        private WLanSnifferSettings _Settings;

        private IList<AccessPointEntry> _AccessPointEntries;

        public FormExport(WLanSnifferSettings settings, IList<AccessPointEntry> entries)        
        {
            _AccessPointEntries = entries;

            InitializeComponent();

            _Settings = settings;

            foreach (ExportContext ec in settings.ExportPlugins)
            {
                if(ec.Active)
                    cbExportPlugin.Items.Add(ec);
            }

            SetLocaleToControls();

            if (cbExportPlugin.Items.Count > 0)
                cbExportPlugin.SelectedIndex = 0;
        }

        private void SetLocaleToControls()
        {
            lFilter.Text = SniffThat.lFilter;
            cbOnlyNotProtected.Text = SniffThat.cbOnlyNotProtected;
            cbOnlyVisible.Text = SniffThat.cbOnlyVisible;
            lExportTo.Text = SniffThat.lExportTo;
            miStartExport.Text = SniffThat.miStartExport;
            miCancelExport.Text = SniffThat.miCancelExport;
        }

        private void cbExportPlugin_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPanel((cbExportPlugin.SelectedItem as ExportContext).ExportPlugin.ExportSettingsControl);
        }

        private void SetPanel(Control c)
        {
            pnlPluginSettings.Controls.Clear();
            Rectangle r = pnlPluginSettings.ClientRectangle;
            int offset = 5;
            r.Y = offset;
            r.X = offset;
            r.Height = r.Height - 2 * offset;
            r.Width = r.Width - 2 * offset;
            c.Bounds = r;

            c.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            pnlPluginSettings.Controls.Add(c);
        }

        private void pnlPluginSettings_Paint(object sender, PaintEventArgs e)
        {
            Rectangle r = pnlPluginSettings.ClientRectangle;
            r.X = 1;
            r.Y = 1;
            r.Height = r.Height - 3;
            r.Width = r.Width - 3;
            e.Graphics.DrawRectangle(new Pen(Color.Black), r);
        }

        private void miExport_Click(object sender, EventArgs e)
        {
            if ((cbExportPlugin.SelectedItem as ExportContext).ExportPlugin.Export(GetList()))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void miCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private IList<AccessPointEntry> GetList()
        {
            IList<AccessPointEntry> result = new List<AccessPointEntry>();

            foreach (AccessPointEntry ape in _AccessPointEntries)
            {
                if ((!cbOnlyNotProtected.Checked || !ape.Protected)
                    && (!cbOnlyVisible.Checked || ape.Visible))
                    result.Add(ape);
            }

            return result;
        }

        private void FormExport_Load(object sender, EventArgs e)
        {
            if (cbExportPlugin.Items.Count <= 0)
            {
                MessageBox.Show(SniffThat.NoExportModules);
                this.Close();
            }
        }
    }
}