//==========================================================================================
//
//		LameSoft.SniffThat.Core.Events.LoadStateEvent
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
using System.Drawing;

namespace LameSoft.SniffThat.Core.Events
{
	/// <summary>
	/// Event for a changed load state
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="args"></param>
	public delegate void LoadStateEventHandler(object sender, LoadStateEventArgs args);

	/// <summary>
	/// Load States
	/// </summary>
	public enum LoadState
	{
		/// <summary>
		/// Load Settings
		/// </summary>
		LoadSettings,
		/// <summary>
		/// Application Starts
		/// </summary>
		StartApplication,
		/// <summary>
		/// Searching Plugins
		/// </summary>
		SearchPlugins,
		/// <summary>
		/// No explicit state
		/// </summary>
		None,
		/// <summary>
		/// Only Text, no images
		/// </summary>
		TextOnly
	}

	/// <summary>
	/// Event Args for the Load State changed Event Handler
	/// </summary>
	public class LoadStateEventArgs
	{
		private LoadState _LoadState;

		/// <summary>
		/// Gets the state of the load.
		/// </summary>
		/// <value>The state of the load.</value>
		public LoadState LoadState
		{
			get { return _LoadState; }
		}

		private Bitmap _ActiveBitmap;

		/// <summary>
		/// Gets the active bitmap.
		/// </summary>
		/// <value>The active bitmap.</value>
		public Bitmap ActiveBitmap
		{
			get { return _ActiveBitmap; }
		}

		private Bitmap _InactiveBitmap;

		/// <summary>
		/// Gets the inactive bitmap.
		/// </summary>
		/// <value>The inactive bitmap.</value>
		public Bitmap InactiveBitmap
		{
			get { return _InactiveBitmap; }
		}

		private string _Text;

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>The text.</value>
		public string Text
		{
			get { return _Text; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:LoadStateEventArgs"/> class.
		/// </summary>
		/// <param name="loadState">State of the load.</param>
		/// <param name="activeBmp">The active BMP.</param>
		/// <param name="inactiveBmp">The inactive BMP.</param>
		public LoadStateEventArgs(LoadState loadState, Bitmap activeBmp, Bitmap inactiveBmp)
		{
			_LoadState = loadState;
			_ActiveBitmap = activeBmp;
			_InactiveBitmap = inactiveBmp;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:LoadStateEventArgs"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		public LoadStateEventArgs(string text)
		{
			_LoadState = LoadState.TextOnly;
			_Text = text;
		}
	}
}
