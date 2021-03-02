//==========================================================================================
//
//		LameSoft.SniffThat.Core.VisualContext
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
//      - 06.02.2007 (Michael Lamers, info@lamesoft.de): 
//          * file added
//      - 07.02.2007 (Michael Lamers, info@lamesoft.de): 
//          * no more visible access points that are watched now get re-inserted in the
//            ComboBox
//          * added resources
//          * size adjustments
//
//==========================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using LameSoft.SniffThat.Common;
using LameSoft.Mobile.WirelessLan;

namespace LameSoft.SniffThat.WLanGraphVisualPlugin
{
    public partial class WLanGraphControl : UserControl
    {
        public WLanGraphControl()
        {
            InitializeComponent();
            //whg1.History = GetFakeHistory();
            SetLocaleToControls();
        }

        private void SetLocaleToControls()
        {
            lAccessPoint1.Text = WLanGraph.lAccessPoint + " 1";
            lAccessPoint2.Text = WLanGraph.lAccessPoint + " 2";
        }

        private WLanHistory GetFakeHistory()
        {
            WLanHistory result = new WLanHistory("TestSSID");

            double[] strengths = new double[] { 40, 40, 40, 50, 50, 50, 60, 30, 70, 80, 80, 75, 70, 65, 60, 55, 50, 45, 40, 80, 40, 30, 20 };

            DateTime current = DateTime.Now.AddMilliseconds(-500 * strengths.Length);

            foreach (double s in strengths)
            {
                result.Add(s, true, false, current);
                current = current.AddMilliseconds(500);
            }

            return result;
        }

        private IList<AccessPointEntry> _LastEntries = null;

        public AccessPointEntry ActiveAPE1
        {
            get
            {
                if (cbAP1.SelectedItem is AccessPointEntry)
                    return (AccessPointEntry)cbAP1.SelectedItem;
                return null;
            }
            set
            {
                if (value != null)
                    SetComboValue(cbAP1, value.MacAddressString);
            }
        }

        public AccessPointEntry ActiveAPE2
        {
            get
            {
                if (cbAP2.SelectedItem is AccessPointEntry)
                    return (AccessPointEntry)cbAP2.SelectedItem;
                return null;
            }
            set
            {
                if (value != null)
                    SetComboValue(cbAP2, value.MacAddressString);
            }
        }

        public void SetData(IList<AccessPointEntry> entries)
        {
            lock (this)
            {
                _LastEntries = entries;
            }
            UpdateCombo(cbAP1, entries);
            UpdateCombo(cbAP2, entries);

            foreach (AccessPointEntry entry in entries)
            {
                if (entry.Equals(cbAP1.SelectedItem))
                    whg1.History = entry.WLanHistory;
                if (entry.Equals(cbAP2.SelectedItem))
                    whg2.History = entry.WLanHistory;
            }
        }

        private void SetComboValue(ComboBox combo, string mac)
        {
            foreach (object item in combo.Items)
                if (item is AccessPointEntry && ((AccessPointEntry)item).MacAddressString == mac)
                {
                    SetComboValue(combo, item as AccessPointEntry);
                    break;
                }
        }

        private void SetComboValue(ComboBox combo, AccessPointEntry value)
        {
            if (combo.Items.Contains(value))
                combo.SelectedItem = value;
        }

        private void UpdateCombo(ComboBox comboBox, IList<AccessPointEntry> entries)
        {
            AccessPointEntry currentItem = (AccessPointEntry)comboBox.SelectedItem;
            comboBox.BeginUpdate();
            comboBox.Items.Clear();

            Dictionary<string, AccessPointEntry> accessPointMap = new Dictionary<string, AccessPointEntry>();

            foreach (AccessPointEntry entry in entries)
                if (entry.Visible && !accessPointMap.ContainsKey(entry.MacAddressString))
                    accessPointMap.Add(entry.MacAddressString, entry);

            foreach (string key in accessPointMap.Keys)
                comboBox.Items.Add(accessPointMap[key]);

            if (currentItem != null && !comboBox.Items.Contains(currentItem))
                comboBox.Items.Add(currentItem);

            SetComboValue(comboBox, currentItem);

            comboBox.EndUpdate();
        }

        private bool _InUpdate = false;

        private void cbAP1_SelectedValueChanged(object sender, EventArgs e)
        {
            lock (this)
            {
                if (_InUpdate)
                    return;
                _InUpdate = true;
            }
            if (_LastEntries != null)
            {
                SetData(_LastEntries);
            }

            lock (this)
                _InUpdate = false;
        }
    }
}
