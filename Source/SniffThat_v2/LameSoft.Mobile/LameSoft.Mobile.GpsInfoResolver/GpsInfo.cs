//==========================================================================================
//
//		LameSoft.Mobile.GpsInfoResolver.GpsInfo
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
using System.Data.SQLite;
using System.Threading;
using System.Data;
using System.IO;
using LameSoft.Mobile.Gps;

namespace LameSoft.Mobile.GpsInfoResolver
{
    /// <summary>
    /// Searches for Gps Infos.
    /// </summary>
    public class GpsInfo
    {
        private List<GpsPositionJob> _Work = new List<GpsPositionJob>();

        private SQLiteConnection _Connection;

        private Thread _WorkerThread;

        private ManualResetEvent _StartWorking = new ManualResetEvent(false);

        private int _Radius;

        private const double C_KM_PER_BM = 1.852;
        private const int C_MAX_WORK = 20;
        private const int C_MAX_WORK_BUFFER = 40;

        private string _DBVersion = null;

        /// <summary>
        /// Gets the DB version.
        /// </summary>
        /// <value>The DB version.</value>
        public string DBVersion
        {
            get { return _DBVersion; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GpsInfo"/> class.
        /// </summary>
        /// <param name="gpsInfoFile">The GPS info file.</param>
        /// <param name="radius">The radius.</param>
        public GpsInfo(string gpsInfoFile, int radius)
        {
            Start(gpsInfoFile, radius);
        }

        private bool _CheckBuffer = true;

        /// <summary>
        /// Gets or sets a value indicating whether [check buffer].
        /// </summary>
        /// <value><c>true</c> if [check buffer]; otherwise, <c>false</c>.</value>
        public bool CheckBuffer
        {
            get { return _CheckBuffer; }
            set { _CheckBuffer = value; }
        }

        /// <summary>
        /// Adds the work.
        /// </summary>
        /// <param name="position">The position.</param>
        public void AddWork(GpsPositionJob position)
        {
            lock (_Work)
            {
                if (!_Work.Contains(position))
                {
                    _Work.Add(position);
                    if (_CheckBuffer)
                        if (_Work.Count > C_MAX_WORK_BUFFER)
                        {
                            if (GpsInfoRetrieved != null)
                                GpsInfoRetrieved(this, new GpsInfoRetrievedEventArgs("", _Work[0], -1));

                            if (VisibleGpsInfosChanged != null)
                                VisibleGpsInfosChanged(this, new VisibleGpsInfoChangedEventArgs(_Work[0], new List<GpsInfoEntry>()));

                            _Work.RemoveAt(0);
                        }
                }
            }
            _StartWorking.Set();
        }

        /// <summary>
        /// Works this instance.
        /// </summary>
        private void Work()
        {
            while (!_Abort)
            {
                List<GpsPositionJob> positions = new List<GpsPositionJob>();
                try
                {
                    _StartWorking.WaitOne();

                    if (_Abort)
                        return;

                    lock (_Work)
                    {
                        int wcount = _Work.Count;
                        for (int i = 0; i < Math.Min(wcount, C_MAX_WORK); i++)
                        {
                            positions.Add(_Work[0]);
                            _Work.RemoveAt(0);
                        }
                    }

                    if (positions.Count > 0)
                    {
                        try
                        {
                            SearchGpsInfo(positions);
                        }
                        catch (Exception ex)
                        {
                            string msg = ex.Message;
                            foreach (GpsPositionJob gpj in positions)
                            {
                                if (GpsInfoRetrieved != null)
                                    GpsInfoRetrieved(this, new GpsInfoRetrievedEventArgs("", gpj, -1));

                                if (VisibleGpsInfosChanged != null)
                                    VisibleGpsInfosChanged(this, new VisibleGpsInfoChangedEventArgs(gpj, new List<GpsInfoEntry>()));
                            }
                        }
                    }

                    if (_Work.Count == 0)
                        _StartWorking.Reset();
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    _Work.AddRange(positions);
                }
            }
        }

        /// <summary>
        /// Gets fired if a work has finished
        /// </summary>
        public event GpsInfoRetrievedEventHandler GpsInfoRetrieved;

        /// <summary>
        /// Gets fired each time the List of Visible GpsInfos gets populated
        /// </summary>
        public event VisibleGpsInfoChangedEventHandler VisibleGpsInfosChanged;

        /// <summary>
        /// Searches the GPS info.
        /// </summary>
        /// <param name="positions">The positions.</param>
        private void SearchGpsInfo(IList<GpsPositionJob> positions)
        {
            //_Radius + 2 to overcome rounding errors
            double degreeDiff = (((_Radius + 2) / (C_KM_PER_BM)) / 60d);

            SQLiteCommand cmd = _Connection.CreateCommand();

            StringBuilder cmdBuilder = new StringBuilder();

            cmdBuilder.Append("SELECT * FROM OpenGeoDB WHERE");
            
            foreach (GpsPositionJob pos in positions)
            {
                if (positions.IndexOf(pos) > 0)
                    cmdBuilder.Append(" OR ");

                //Rectangle Search for better performance
                cmdBuilder.Append("((Latitude <= ?) AND (Latitude >= ?) AND (Longitude <= ?) AND (Longitude >= ?))");
            }

            cmd.CommandText = cmdBuilder.ToString();

            foreach (GpsPositionJob pos in positions)
            {
                double maxLat = pos.Latitude + degreeDiff;
                double minLat = pos.Latitude - degreeDiff;
                double maxLong = pos.Longitude + degreeDiff;
                double minLong = pos.Longitude - degreeDiff;

                cmd.Parameters.Add(new SQLiteParameter(DbType.Double, maxLat));
                cmd.Parameters.Add(new SQLiteParameter(DbType.Double, minLat));
                cmd.Parameters.Add(new SQLiteParameter(DbType.Double, maxLong));
                cmd.Parameters.Add(new SQLiteParameter(DbType.Double, minLong));
            }

            DataSet ds = new DataSet();

            if (new SQLiteDataAdapter(cmd).Fill(ds) > 0)
            {
                IList<GpsInfoEntry> entries = new List<GpsInfoEntry>();

                foreach (GpsPositionJob pos in positions)
                {
                    //Search the nearest Location inside the given Radius
                    string desc = null;
                    //double minDiff = 2 * degreeDiff * degreeDiff;
                    double lastDist = -1;
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        GpsInfoEntry entry = new GpsInfoEntry(dr, _DBVersion);

                        entry.Distance = GpsHelper.GetDistance(entry.Latitude, pos.Latitude, entry.Longitude, pos.Longitude);

                        if (entry.Distance <= _Radius)
                        {
                            if(!entries.Contains(entry))
                                entries.Add(entry);

                            if ((lastDist == -1) || (entry.Distance < lastDist))
                            {
                                desc = entry.Info;
                                lastDist = entry.Distance;
                            }
                        }
                    }

                    if (GpsInfoRetrieved != null)
                        GpsInfoRetrieved(this, new GpsInfoRetrievedEventArgs(desc, pos, lastDist));

                    if (VisibleGpsInfosChanged != null)
                        VisibleGpsInfosChanged(this, new VisibleGpsInfoChangedEventArgs(pos, entries));
                }

            }

            ds.Dispose();
            cmd.Dispose();
        }

        private bool _Abort = false;

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            _Abort = true;
            _StartWorking.Set();
            _WorkerThread.Abort();
            bool ok = _WorkerThread.Join(1000);
            foreach (GpsPositionJob job in _Work)
            {
                if (GpsInfoRetrieved != null)
                    GpsInfoRetrieved(this, new GpsInfoRetrievedEventArgs("", job, -1));

                if (VisibleGpsInfosChanged != null)
                    VisibleGpsInfosChanged(this, new VisibleGpsInfoChangedEventArgs(job, new List<GpsInfoEntry>()));
            }

            _Work.Clear();
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        /// <param name="gpsInfoFile">The GPS info file.</param>
        /// <param name="radius">The radius.</param>
        public void Start(string gpsInfoFile, int radius)
        {
            try
            {
                Stop();
            }
            catch (Exception)
            {
            }
            try
            {
                _Abort = false;
                _Work.Clear();
                _Radius = radius;
                if (!File.Exists(gpsInfoFile))
                    throw new Exception("File not found!");
                _Connection = new SQLiteConnection(String.Format("Data Source=\"{0}\"", gpsInfoFile));
                _Connection.Open();
                SQLiteCommand cmd = _Connection.CreateCommand();
                cmd.CommandText = "SELECT Version FROM DBVersion";

                DataTable tbl = new DataTable();

                new SQLiteDataAdapter(cmd).Fill(tbl);

                _DBVersion = Convert.ToString(tbl.Rows[0]["Version"]);

            }
            catch (Exception)
            {
                _Connection = null;
            }

            _WorkerThread = new Thread(new ThreadStart(Work));
            _WorkerThread.Start();
        }
    }
}
