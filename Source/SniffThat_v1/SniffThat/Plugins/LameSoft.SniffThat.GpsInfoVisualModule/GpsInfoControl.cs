//==========================================================================================
//
//		LameSoft.SniffThat.Plugins.GpsInfoVisualModule.GpsInfoControl
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
using LameSoft.Mobile.GpsInfoResolver;
using LameSoft.SniffThat.Common;

namespace LameSoft.SniffThat.Plugins.GpsInfoVisualModule
{
    public partial class GpsInfoControl : UserControl
    {
        public GpsInfoControl()
        {
            InitializeComponent();
            SetLocaleToControls();
        }

        private void SetLocaleToControls()
        {
            dgtbGpsInfo.HeaderText = GpsInfoResources.dgtbGpsInfo;
            dgtDistance.HeaderText = GpsInfoResources.dgtDistance;
            dgtLat.HeaderText = GpsInfoResources.dgtLat;
            dgtLong.HeaderText = GpsInfoResources.dgtLong;
        }

        DataTable _GpsInfoEntries = null;

        private delegate void SetGpsInfoHandler(List<GpsInfoEntry> entries);

        private delegate void SetCurrentGpsInfoHandler(string currentinfo);

        /// <summary>
        /// Sets the GPS infos.
        /// </summary>
        /// <param name="entries">The entries.</param>
        public void SetGpsInfos(List<GpsInfoEntry> entries)
        {
            this.Invoke(new SetGpsInfoHandler(SetGpsInfosInternal), entries);
        }

        private void SetGpsInfosInternal(List<GpsInfoEntry> entries)
        {
            if (_GpsInfoEntries == null)
            {
                _GpsInfoEntries = DataTableHelper.CreateTableFromType(typeof(GpsInfoEntry), "GpsInfos", "GpsInfoObject");

                dgGpsInfos.DataSource = _GpsInfoEntries;
            }

            entries.Sort(new GpsInfoEntryComparer());

            DataTableHelper.SetListToTable(_GpsInfoEntries, entries);
        }

        /// <summary>
        /// Sets the current GPS info.
        /// </summary>
        /// <param name="currentinfo">The currentinfo.</param>
        public void SetCurrentGpsInfo(string currentinfo)
        {
            this.Invoke(new SetCurrentGpsInfoHandler(SetCurrentGpsInfoInternal), currentinfo);
        }

        private void SetCurrentGpsInfoInternal(string currentinfo)
        {
            lCurrentGpsInfo.Text = currentinfo;
        }
    }
}
