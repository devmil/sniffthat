//==========================================================================================
//
//		LameSoft.SniffThat.Core.Events.ShowMessageEvent
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
	/// ShowMessage Event Handler
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="args"></param>
	public delegate void ShowMessageEventHandler(object sender, ShowMessageEventArgs args);

	/// <summary>
	/// Type of the message
	/// </summary>
	public enum MessageType
	{
		/// <summary>
		/// Wireless Adapter not found
		/// </summary>
		WirelessAdapterNotFound,
		/// <summary>
		/// Gps Open Error
		/// </summary>
		GpsOpenError,
		/// <summary>
		/// Gps Close Error
		/// </summary>
		GpsCloseError,
		/// <summary>
		/// No Caption
		/// </summary>
		NoCaption,
		/// <summary>
		/// Message from visual plugin
		/// </summary>
		VisualMessage,
		/// <summary>
		/// Error from visual plugin
		/// </summary>
		VisualError,
		/// <summary>
		/// Message from export plugin
		/// </summary>
		ExportMessage,
		/// <summary>
		/// Error from export plugin
		/// </summary>
		ExportError
	}

	/// <summary>
	/// Event Args for the ShowMessageEvent Handler
	/// </summary>
	public class ShowMessageEventArgs
	{
		private MessageType _MessageType;

		/// <summary>
		/// Gets the type of the message.
		/// </summary>
		/// <value>The type of the message.</value>
		public MessageType MessageType
		{
			get { return _MessageType; }
		}

		private string _MessageText = null;

		/// <summary>
		/// Gets the message text.
		/// </summary>
		/// <value>The message text.</value>
		public string MessageText
		{
			get { return _MessageText; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ShowMessageEventArgs"/> class.
		/// </summary>
		/// <param name="messageType">Type of the message.</param>
		public ShowMessageEventArgs(MessageType messageType)
			: this(messageType, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ShowMessageEventArgs"/> class.
		/// </summary>
		/// <param name="messageType">Type of the message.</param>
		/// <param name="messageText">The message text.</param>
		public ShowMessageEventArgs(MessageType messageType, string messageText)
		{
			_MessageType = messageType;
			_MessageText = messageText;
		}
	}
}
