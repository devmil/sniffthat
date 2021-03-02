//==========================================================================================
//
//		LameSoft.SniffThat.Program
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
//      - 07.02.2007 (Michael Lamers, info@lamesoft.de): 
//          * added lastfail.txt
//          * replaces "Build" with "Version"
//
//==========================================================================================
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LameSoft.Mobile.Utils;
using System.Reflection;
using System.Drawing;
using System.IO;
using LameSoft.SniffThat.Core;

namespace LameSoft.SniffThat
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            SplashScreen.ShowSplash(FormWLanSniffer.C_SPLASHKEY, "SniffThat", SniffThatCore.Logo);

            SplashScreen.SetHeader(FormWLanSniffer.C_SPLASHKEY, String.Format("Version {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString(3)));

            SplashScreen.SetState(FormWLanSniffer.C_SPLASHKEY, "Initializing...");

            try
            {
                Application.Run(new FormWLanSniffer());
            }
            catch (Exception ex)
            {
                try
                {
                    Uri uri = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase);
                    using (TextWriter writer = File.CreateText(Path.Combine(Path.GetDirectoryName(uri.LocalPath), "LastFailiure.txt")))
                    {
                        writer.WriteLine(ex.Message);
                        writer.WriteLine(ex.StackTrace);
                    }
                }
                catch
                { }
            }
        }
    }
}