//==========================================================================================
//
//		LameSoft.SniffThat.Plugins.RadarVisualModule.RadarControl
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
using LameSoft.Mobile.Gps.Visual;
using LameSoft.SniffThat.Common;
using LameSoft.Mobile.Gps;

namespace LameSoft.SniffThat.Plugins.RadarVisualModule
{
    public partial class RadarControl : UserControl
    {
        private GpsRadar _Radar = new GpsRadar();

        private IList<AccessPointEntry> _AccessPoints;

        public void SetAccessPoints(IList<AccessPointEntry> aps)
        {
            _AccessPoints = aps;
            RefreshRadar();
        }

        private void RefreshRadar()
        {
			try
			{
				IList<GpsRadarItem> items = new List<GpsRadarItem>();
				if (_AccessPoints != null)
				{
                    foreach (AccessPointEntry entry in new List<AccessPointEntry>(_AccessPoints))
					{
						if ((_Radar.Center != null) && !_Radar.Center.Equals(new GpsPosition(0, 0)))
						{
							if (!_Protected && entry.Protected)
								continue;
						}
						string suffix = "";
						if (entry.Protected)
							suffix = " (P)";
						else
							suffix = " (O)";

						items.Add(new GpsRadarItem(new GpsPosition(entry.Longitude, entry.Latitude), entry.SSID + suffix));
					}
				}
				_Radar.SetPositions(items);
			}
			catch (Exception ex)
			{
				ex.ToString();
			}
        }

        /// <summary>
        /// Gets the max distance.
        /// </summary>
        /// <value>The max distance.</value>
        public double MaxDistance
        {
            get { return _Radar.Resolution; }
            set 
            { 
                _Radar.Resolution = value;
                cbRange.Text = value.ToString();
            }
        }

        private bool _Protected;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:RadarControl"/> is protected.
        /// </summary>
        /// <value><c>true</c> if protected; otherwise, <c>false</c>.</value>
        public bool Protected
        {
            get { return _Protected; }
            set 
            { 
                _Protected = value;
                cbProtected.Checked = value;
                RefreshRadar();
            }
        }

        public RadarControl()
        {
            InitializeComponent();
            _Radar.Dock = DockStyle.Fill;
            pnlRadar.Controls.Add(_Radar);
            lRange.Text = RadarVisualModuleResources.lRange + ":";
            cbProtected.Text = RadarVisualModuleResources.cbProtected;
        }

        private void cbProtected_CheckStateChanged(object sender, EventArgs e)
        {
            Protected = cbProtected.Checked;
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double val = Convert.ToDouble(cbRange.Text.Replace('.', ','));
                _Radar.Resolution = val;
                RefreshRadar();
            }
            catch
            { }
        }

        internal void SetCenter(GpsPosition position)
        {
            _Radar.Center = position;
        }

        internal void SetDegrees(double p)
        {
            _Radar.DirectionDegrees = p;
        }
    }
}
