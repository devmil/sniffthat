//==========================================================================================
//
//		LameSoft.SniffThat.Plugins.ListVisualModule.AccessPointList
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
//      - 07.02.2007 (Michael Lamers, info@lamesoft.de): 
//          * added sort functionality
//          * added list summary
//
//==========================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using LameSoft.SniffThat.Common;
using System.Collections;
using System.Threading;

namespace LameSoft.SniffThat.Plugins.ListVisualModule
{
    public partial class AccessPointList : UserControl
    {
        public AccessPointList()
        {
            InitializeComponent();

            SetLocaleToControls();

            _Columns = new List<DataGridTextBoxColumn>();

            for (int i = 0; i < dataGridTableStyle1.GridColumnStyles.Count; i++)
            {
                _Columns.Add((DataGridTextBoxColumn)dataGridTableStyle1.GridColumnStyles[i]);
            }
        }

        private List<DataGridTextBoxColumn> _Columns;

        private void SetLocaleToControls()
        {
            dgtbColLatitude.HeaderText = ListResources.ColLatitude;
            dgtbColLong.HeaderText = ListResources.ColLong;
            dgtbColMAC.HeaderText = ListResources.ColMAC;
            dgtbColProtected.HeaderText = ListResources.ColProtected;
            dgtbColSSID.HeaderText = ListResources.ColSSID;
            dgtbColStrength.HeaderText = ListResources.ColStrength;
            dgtbGpsInfo.HeaderText = ListResources.GpsInfo;
            dgtbGpsInfoDistance.HeaderText = ListResources.GpsInfoDistance;
            dgtbPos.HeaderText = ListResources.Pos;
            dgtbSignalStrengthDB.HeaderText = ListResources.SignalStrengthDB;
            dgtbSupportedRates.HeaderText = ListResources.SupportedRates;
			dgtbFirstSeen.HeaderText = ListResources.ColFirstSeen;
			dgtbLastSeen.HeaderText = ListResources.ColLastSeen;
            lOrderBy.Text = ListResources.lOrderBy;
            cbOrderBy.Items.Clear();
            cbOrderBy.Items.Add(ListResources.itemOrderByNr);
            cbOrderBy.Items.Add(ListResources.itemOrderByStrength);
            cbOrderBy.Items.Add(ListResources.itemOrderByVisibility);
            cbOrderBy.Items.Add(ListResources.itemOrderBySSID);
            cbInvert.Text = ListResources.cbInvert;
        }

        public List<ColumnDefinition> ColumnDefinitions
        {
            set
            {
                BuildColumns(value);
            }
        }

        /// <summary>
        /// Gets or sets the selected sort field.
        /// </summary>
        /// <value>The selected sort field.</value>
        public string SelectedSortField
        {
            get
            {
                return (string)cbOrderBy.SelectedItem;
            }
            set
            {
                cbOrderBy.Invoke(new ThreadStart(
                    delegate()
                    {
                        foreach (object item in cbOrderBy.Items)
                            if (String.Equals(item, value))
                            {
                                cbOrderBy.SelectedItem = item;
                                break;
                            }
                    }));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [sort invert].
        /// </summary>
        /// <value><c>true</c> if [sort invert]; otherwise, <c>false</c>.</value>
        public bool SortInvert
        {
            get
            {
                return cbInvert.Checked;
            }
            set
            {
                cbInvert.Invoke(new ThreadStart(
                    delegate()
                    {
                        cbInvert.Checked = value;
                    }));
            }
        }

        private void BuildColumns(List<ColumnDefinition> definitions)
        {
            dataGridTableStyle1.GridColumnStyles.Clear();

            foreach (ColumnDefinition colDef in definitions)
            {
                if(_Columns.Count > colDef.ColumnID)
                    if (colDef.Active)
                        dataGridTableStyle1.GridColumnStyles.Add(_Columns[colDef.ColumnID]);                    
            }
        }

        /// <summary>
        /// Creates the definitions.
        /// </summary>
        /// <returns></returns>
        public List<ColumnDefinition> CreateDefinitions()
        {
            List<ColumnDefinition> result = new List<ColumnDefinition>();

            for (int i = 0; i < _Columns.Count; i++)
                result.Add(new ColumnDefinition(i, _Columns[i].HeaderText, true));

            return result;
        }

        private bool _Running = true;

        public void SetEntriesDataSource(IList<AccessPointEntry> entries)
        {
            if (!_Running)
                return;

            UpdateUI(entries);
        }

        private void UpdateUI(IList<AccessPointEntry> entries)
        {
            this.Invoke(new ThreadStart(
                delegate()
                {
                    try
                    {
                        entries = SortEntries(entries, (string)cbOrderBy.SelectedItem, cbInvert.Checked);
                        lStatus.Text = GetStatusString(entries);
                        DataGridCell cell = dgAccessPoints.CurrentCell;
                        int y = cell.RowNumber;
                        int x = cell.ColumnNumber;

                        if (dgAccessPoints.DataSource == null)
                        {
                            dgAccessPoints.DataSource = new BindingList<AccessPointEntry>();
                            dgAccessPoints.TableStyles[0].MappingName = dgAccessPoints.DataSource.GetType().Name;
                        }

                        BindingList<AccessPointEntry> dataSource = (BindingList<AccessPointEntry>)dgAccessPoints.DataSource;

                        bool pauseBind = Math.Abs(dataSource.Count - entries.Count) >= 10;

                        if (pauseBind)
                            dataSource.RaiseListChangedEvents = false;

                        bool scrollToEnd = (y >= dataSource.Count - 1);

                        while (dataSource.Count > entries.Count)
                            dataSource.RemoveAt(dataSource.Count - 1);

                        for (int i = 0; i < entries.Count; i++)
                        {
                            if (dataSource.Count > i)
                                dataSource[i] = entries[i];
                            else
                                dataSource.Add(entries[i]);
                        }

                        if (scrollToEnd)
                            y = dataSource.Count - 1;

                        y = (int)Math.Min(dataSource.Count - 1, y);

                        if (pauseBind)
                        {
                            dataSource.RaiseListChangedEvents = true;
                            dataSource.ResetBindings();
                        }

                        CurrencyManager cmNew = (CurrencyManager)dgAccessPoints.BindingContext[dataSource];

                        if (y >= 0)
                        {
                            cmNew.Position = y;

                            dgAccessPoints.CurrentCell = new DataGridCell(y, cell.ColumnNumber);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.StackTrace);
                    }
                }));
        }

        private string GetStatusString(IList<AccessPointEntry> entries)
        {
            int numVisible = 0;
            foreach (AccessPointEntry entry in entries)
                if (entry.Visible)
                    numVisible++;
            return String.Format(ListResources.lStatusFormat, entries.Count, numVisible);
        }

        private IList<AccessPointEntry> SortEntries(IList<AccessPointEntry> entries, string expression, bool invert)
        {
            List<AccessPointEntry> result = new List<AccessPointEntry>(entries);
            result.Sort(new Comparison<AccessPointEntry>(
                delegate(AccessPointEntry x1, AccessPointEntry x2)
                {
                    int compareResult = 0;
                    if (expression == ListResources.itemOrderBySSID)
                        compareResult = String.Compare(x1.SSID, x2.SSID);
                    else if (expression == ListResources.itemOrderByVisibility)
                    {
                        if (x1.Visible == x2.Visible)
                            compareResult = 0;
                        else if (x1.Visible)
                            compareResult = -1;
                        else
                            compareResult = 1;
                    }
                    else if (expression == ListResources.itemOrderByStrength)
                    {
                        //negative, so this expression is correct
                        if (x1.StrengthDB - x2.StrengthDB > 0)
                            compareResult = -1;
                        else if(x1.StrengthDB - x2.StrengthDB < 0)
                            compareResult = 1;
                        else
                            compareResult = 0;
                    }
                    else
                        compareResult = x1.Nr - x2.Nr;

                    if (invert)
                        compareResult *= -1;

                    return compareResult;
                }));
            return result;
        }

        public void Start()
        {
            _Running = true;
        }

        public void Stop()
        {
            _Running = false;
        }
    }
}
