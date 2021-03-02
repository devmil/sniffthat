//==========================================================================================
//
//		LameSoft.SniffThat.Core.Events.StatusChangedEvent
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
	/// Gps Handler for a chenged status
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="args"></param>
	public delegate void StatusChangedEventHandler(object sender, StatusChangedEventArgs args);

	/// <summary>
	/// Status
	/// </summary>
	public enum Status
	{
		/// <summary>
		/// No Status
		/// </summary>
		None,
		/// <summary>
		/// Gps Status changed
		/// </summary>
		GpsStatus,
		/// <summary>
		/// Gps stopped
		/// </summary>
		GpsStopped
	}

	/// <summary>
	/// Event Args for the status changed Event Handler
	/// </summary>
	public class StatusChangedEventArgs
	{
		private string _Text;

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>The text.</value>
		public string Text
		{
			get { return _Text; }
		}

		private Status _Status;

		/// <summary>
		/// Gets the status.
		/// </summary>
		/// <value>The status.</value>
		public Status Status
		{
			get { return _Status; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:StatusChangedEventArgs"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		public StatusChangedEventArgs(string text)
			: this(Status.None, text)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:StatusChangedEventArgs"/> class.
		/// </summary>
		/// <param name="status">The status.</param>
		/// <param name="text">The text.</param>
		public StatusChangedEventArgs(Status status, string text)
		{
			_Status = status;
			_Text = text;
		}
	}
}
