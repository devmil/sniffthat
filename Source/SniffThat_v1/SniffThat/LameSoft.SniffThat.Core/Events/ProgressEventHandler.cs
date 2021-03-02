//==========================================================================================
//
//		LameSoft.SniffThat.Core.Events.ProgressEventHandler
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

namespace LameSoft.SniffThat.Core.Events
{
	/// <summary>
	/// Event Handler for progess
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="args"></param>
    public delegate void ProgressEventHandler(object sender, ProgressEventArgs args);

	/// <summary>
	/// Event Args for the progress Event Handler
	/// </summary>
    public class ProgressEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ProgressEventArgs"/> class.
        /// </summary>
        /// <param name="progress">The progress.</param>
        public ProgressEventArgs(double progress)
        {
            _Progress = progress;
        }

        private double _Progress;

        /// <summary>
        /// Gets or sets the progress.
        /// </summary>
        /// <value>The progress.</value>
        public double Progress
        {
            get { return _Progress; }
            set { _Progress = value; }
        }
    }
}
