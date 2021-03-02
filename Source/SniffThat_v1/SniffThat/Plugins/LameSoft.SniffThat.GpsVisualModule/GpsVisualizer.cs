//==========================================================================================
//
//		LameSoft.SniffThat.Plugins.GpsVisualModule.GpsVisualizer
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
using LameSoft.Mobile.Gps;
using LameSoft.Mobile.Gps.Visual;

namespace LameSoft.SniffThat.Plugins.GpsVisualModule
{
    public partial class GpsVisualizer : UserControl
    {
        private bool _Running = true;

        private SatelliteViewer _SatViewer;

        private GpsInfo _GpsInfo;

        public GpsVisualizer()
        {
            InitializeComponent();
            _SatViewer = new SatelliteViewer();
            _SatViewer.Dock = DockStyle.Fill;
            pnlSats.Controls.Add(_SatViewer);
            SetLocaleToControls();
        }

        private void SetLocaleToControls()
        {
            lLongitude.Text = GpsVisualResources.lLongitude + ":";
            lLatitude.Text = GpsVisualResources.lLatitude + ":";
            lAltitude.Text = GpsVisualResources.lAltitude + ":";
            lSpeedText.Text = GpsVisualResources.lSpeedText + ":";
        }

        internal void Start()
        {
            _Running = true;
        }

        internal void Stop()
        {
            _Running = false;
        }

        internal void SetGps(GpsPosition position, GpsMovement mov, IList<GpsSatellite> sats)
        {
            if (_Running)
            {
                this.Invoke(new ProcessGpsDataDelegate(ProcessGpsData), position, mov, sats);
            }
        }

        delegate void ProcessGpsDataDelegate(GpsPosition position, GpsMovement mov, IList<GpsSatellite> sats);

        /// <summary>
        /// Processes the GPS data.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="mov">The mov.</param>
        /// <param name="sats">The sats.</param>
        private void ProcessGpsData(GpsPosition position, GpsMovement mov, IList<GpsSatellite> sats)
        {
            ProcessPosition(position);
            ProcessMovement(mov);
            ProcessSatellites(sats);
        }

        private void ProcessMovement(GpsMovement mov)
        {
            if (mov != null)
                lSpeed.Text = mov.Speed.ToString("#,##0.00") + " km/h";
            else
                lSpeed.Text = "No Fix!";
        }

        private GpsPositionJob _SearchedGpsPosition = null;

        private void ProcessPosition(GpsPosition position)
        {
            if (position != null)
            {
                lLong.Text = position.Longitude.ToString("#,##0.00000") + " °";
                lLat.Text = position.Latitude.ToString("#,##0.00000") + " °";
                lAlt.Text = Convert.ToInt32(position.Altitude).ToString() + " m";

                lock(this)
                {
                    if (_SearchedGpsPosition == null)
                    {
                        _SearchedGpsPosition = new GpsPositionJob((double)position.Longitude, (double)position.Latitude);
                        _GpsInfo.AddWork(_SearchedGpsPosition);
                    }
                }
            }
            else
            {
                lLong.Text = "No Fix!";
                lLat.Text = "No Fix!";
                lAlt.Text = "No Fix!";
                lGpsInfo.Text = "";
            }
        }

        /// <summary>
        /// Processes the satellites.
        /// </summary>
        /// <param name="satellites">The satellites.</param>
        private void ProcessSatellites(IList<GpsSatellite> satellites)
        {
            IList<GpsSatellite> sats = new List<GpsSatellite>();

            if (satellites != null)
            {
                foreach (GpsSatellite s in satellites)
                {
                    try
                    {
                        if (Convert.ToDouble(s.PRN) > 0)
                            sats.Add(s);
                    }
                    catch(Exception)
                    {}
                }
            }

            _SatViewer.SetSatellites(sats);
        }

        /// <summary>
        /// Sets the GPS info.
        /// </summary>
        /// <param name="gpsinfo">The gpsinfo.</param>
        internal void SetGpsInfo(GpsInfo gpsinfo)
        {
            _GpsInfo = gpsinfo;
            _GpsInfo.GpsInfoRetrieved += new GpsInfoRetrievedEventHandler(_GpsInfo_GpsInfoRetrieved);
        }

        /// <summary>
        /// Handles the GpsInfoRetrieved event of the Handle_GpsInfo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="T:LameSoft.SniffThat.GpsInfoResolver.GpsInfoRetrievedEventArgs"/> instance containing the event data.</param>
        private void Handle_GpsInfo_GpsInfoRetrieved(object sender, GpsInfoRetrievedEventArgs args)
        {
            lGpsInfo.Text = (args.GpsInfo == null) ? "" : args.GpsInfo;
            if (args.Distance > -1)
                lGpsInfo.Text += " (" + args.Distance.ToString("0.00") + " km)";
            _SearchedGpsPosition = null;
        }

        /// <summary>
        /// Handles the GpsInfoRetrieved event of the _GpsInfo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="T:LameSoft.SniffThat.GpsInfoResolver.GpsInfoRetrievedEventArgs"/> instance containing the event data.</param>
        void _GpsInfo_GpsInfoRetrieved(object sender, GpsInfoRetrievedEventArgs args)
        {
            if(args.Position.Equals(_SearchedGpsPosition))
                this.Invoke(new GpsInfoRetrievedEventHandler(Handle_GpsInfo_GpsInfoRetrieved), sender, args);
        }
    }
}
