//==========================================================================================
//
//		LameSoft.Mobile.Utils.StandbyPreventer
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
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;

namespace LameSoft.Mobile.Utils
{
    /// <summary>
    /// Prevents the Pocket PC from going into standby mode
    /// </summary>
    public class StandbyPreventer
    {
        private Timer _Timer;

        private int _Interval;

        private bool _Running;

        private bool RunningLocal
        {
            get 
            {
                lock (this)
                    return _Running;
            }
            set 
            {
                lock (this)
                    _Running = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:StandbyPreventer"/> is running.
        /// </summary>
        /// <value><c>true</c> if running; otherwise, <c>false</c>.</value>
        public bool Running
        {
            get
            {
                return RunningLocal;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StandbyPreventer"/> class.
        /// </summary>
        public StandbyPreventer() : this(1000) //Every second
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StandbyPreventer"/> class.
        /// </summary>
        /// <param name="refreshInterval">The refresh interval in ms.</param>
        public StandbyPreventer(int refreshInterval)
        {
            _Interval = refreshInterval;

            _Timer = new Timer(new TimerCallback(Timout), this, Timeout.Infinite, Timeout.Infinite); //Stopped Timer
        }

        [DllImport ("coredll.dll")]
        private static extern void SystemIdleTimerReset();

        private void Timout(object target)
        {
            lock (target)
                SystemIdleTimerReset();
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            RunningLocal = true;
            _Timer.Change(0, _Interval); //Start immediately
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            RunningLocal = false;
            _Timer.Change(Timeout.Infinite, Timeout.Infinite);
        }
    }
}
