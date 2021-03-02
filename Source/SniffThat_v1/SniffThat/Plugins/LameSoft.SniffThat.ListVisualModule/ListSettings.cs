//==========================================================================================
//
//		LameSoft.SniffThat.Plugins.ListVisualModule.ListSettings
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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace LameSoft.SniffThat.Plugins.ListVisualModule
{
    public partial class ListSettings : UserControl
    {
		private LameSoft.Mobile.Utils.PlatformSave.Button bUp;
		private LameSoft.Mobile.Utils.PlatformSave.Button bDown;

        private AccessPointList _AccessPointList;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ListSettings"/> class.
        /// </summary>
        /// <param name="accessPointList">The access point list.</param>
        public ListSettings(AccessPointList accessPointList)
        {
            InitializeComponent();

			bUp = new LameSoft.Mobile.Utils.PlatformSave.Button();
			bUp.Click += new EventHandler(bUp_Click);
			bDown = new LameSoft.Mobile.Utils.PlatformSave.Button();
			bDown.Click += new EventHandler(bDown_Click);

			bUp.Dock = bDown.Dock = DockStyle.Fill;

			pnlbUp.Controls.Add(bUp);
			pnlbDown.Controls.Add(bDown);

            bUp.Text = ListResources.bUp;
            bDown.Text = ListResources.bDown;

            _AccessPointList = accessPointList;
        }

        private List<ColumnDefinition> _ColumnDefinitions;

        /// <summary>
        /// Gets or sets the column definitions.
        /// </summary>
        /// <value>The column definitions.</value>
        public List<ColumnDefinition> ColumnDefinitions
        {
            get { return _ColumnDefinitions; }
            set 
            { 
                _ColumnDefinitions = new List<ColumnDefinition>();

                for (int i = 0; i < value.Count; i++)
                {
                    _ColumnDefinitions.Add(value[i]);
                }

                List<ColumnDefinition> defaultDefinitions = _AccessPointList.CreateDefinitions();
                
                foreach (ColumnDefinition colDef in defaultDefinitions)
                    if (!_ColumnDefinitions.Contains(colDef))
                        _ColumnDefinitions.Add(colDef);



                for (int i = _ColumnDefinitions.Count - 1; i >= 0; i--)
                    if (!defaultDefinitions.Contains(_ColumnDefinitions[i]))
                        _ColumnDefinitions.RemoveAt(i);

                SetDefinitionsToList();
            }
        }

        private void SetDefinitionsToList()
        {
            int selIdx = lvColumns.SelectedIndices.Count > 0 ? lvColumns.SelectedIndices[0] : -1;

            lvColumns.Items.Clear();

            for (int i = 0; i < _ColumnDefinitions.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.Text = _ColumnDefinitions[i].ColumnName;
                item.Tag = _ColumnDefinitions[i];
                item.Checked = _ColumnDefinitions[i].Active;
                lvColumns.Items.Add(item);
            }

            if (selIdx >= 0)
                lvColumns.Items[selIdx].Selected = true;

            _AccessPointList.ColumnDefinitions = _ColumnDefinitions;
        }

        private void lvColumns_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            _ColumnDefinitions[e.Index].Active = e.NewValue == CheckState.Checked;

            _AccessPointList.ColumnDefinitions = _ColumnDefinitions;
        }

        private void bUp_Click(object sender, EventArgs e)
        {
            int selIdx = lvColumns.SelectedIndices.Count > 0 ? lvColumns.SelectedIndices[0] : -1;

            if (selIdx > 0)
            {
                int newIdx = selIdx - 1;
                SwitchItems(selIdx, newIdx);
                lvColumns.Items[newIdx].Selected = true;
            }
        }

        private void bDown_Click(object sender, EventArgs e)
        {
            int selIdx = lvColumns.SelectedIndices.Count > 0 ? lvColumns.SelectedIndices[0] : -1;

            if (selIdx < lvColumns.Items.Count - 1)
            {
                int newIdx = selIdx + 1;
                SwitchItems(selIdx, newIdx);
                lvColumns.Items[newIdx].Selected = true;
            }
        }

        private void SwitchItems(int idx1, int idx2)
        {
            ColumnDefinition def = _ColumnDefinitions[idx1];
            _ColumnDefinitions[idx1] = _ColumnDefinitions[idx2];
            _ColumnDefinitions[idx2] = def;

            SetDefinitionsToList();
        }
    }
}
