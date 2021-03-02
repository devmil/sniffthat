//==========================================================================================
//
//		LameSoft.Mobile.Utils.PlatformDetector
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
using System.Runtime.InteropServices;

namespace LameSoft.Mobile.Utils
{
	public class PlatformDetector
	{
		[DllImport("coredll.dll")]
		private static extern bool SystemParametersInfo(uint uiAction, uint uiParam, StringBuilder pvParam, uint fWinIni);

		private static uint SPI_GETPLATFORMTYPE = 257;

		public static Platform GetPlatform()
		{
			Platform plat = Platform.Unknown;
			switch (System.Environment.OSVersion.Platform)
			{
				case PlatformID.Win32NT:
					plat = Platform.Win32NT;
					break;
				case PlatformID.Win32S:
					plat = Platform.Win32S;
					break;
				case PlatformID.Win32Windows:
					plat = Platform.Win32Windows;
					break;
				case PlatformID.WinCE:
					plat = CheckWinCEPlatform();
					break;
			}

			return plat;
		}

		static Platform CheckWinCEPlatform()
		{
			Platform plat = Platform.WindowsCE;
			StringBuilder strbuild = new StringBuilder(200);
			SystemParametersInfo(SPI_GETPLATFORMTYPE, 200, strbuild, 0);
			string str = strbuild.ToString();
			switch (str)
			{
				case "PocketPC":
					plat = Platform.PocketPC;
					break;
				case "SmartPhone":
					// Note that the strbuild parameter from the
					// PInvoke returns "SmartPhone" with an
					// upper case P. The correct casing is
					// "Smartphone" with a lower case p.
					plat = Platform.Smartphone;
					break;
			}
			return plat;
		}
	}

	public enum Platform
	{
		PocketPC, WindowsCE, Smartphone, Win32NT, Win32S, Win32Windows, Unknown
	}
}
