//==========================================================================================
//
//		LameSoft.Mobile.Utils.SplashScreen
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
//      - 06.02.2007 (Michael Lamers, info@lamesoft.de): 
//          * added header label
//
//==========================================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace LameSoft.Mobile.Utils
{
    public class SplashScreen
    {
        private static Dictionary<string, FormSplash> _SplashForms = new Dictionary<string, FormSplash>();

        /// <summary>
        /// Shows the splash.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="title">The title.</param>
        /// <param name="splash">The splash.</param>
        /// <returns></returns>
        public static Form ShowSplash(string key, string title, Image splash)
        {
            FormSplash frmSplash;
            if (_SplashForms.ContainsKey(key))
                frmSplash = _SplashForms[key];
            else
            {
                frmSplash = new FormSplash();
                _SplashForms.Add(key, frmSplash);
            }

            frmSplash.Text = title;
            frmSplash.Image = splash;

            frmSplash.Height = splash.Height;
            frmSplash.Width = splash.Width;

            Rectangle wa = System.Windows.Forms.Screen.PrimaryScreen.Bounds;

            frmSplash.Left = wa.Left + (wa.Width - frmSplash.Width) / 2;
            frmSplash.Top = wa.Top + (wa.Height - frmSplash.Height) / 2;

            frmSplash.Show();

            Application.DoEvents();

            return frmSplash;
        }

        /// <summary>
        /// Sets the state.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="state">The state.</param>
        public static void SetState(string key, string state)
        {
            if (_SplashForms.ContainsKey(key))
            {
                _SplashForms[key].lState.Text = state;
                Application.DoEvents();
            }
        }

        /// <summary>
        /// Sets the header.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="header">The header.</param>
        public static void SetHeader(string key, string header)
        {
            if (_SplashForms.ContainsKey(key))
            {
                _SplashForms[key].Header = header;
            }
        }

        /// <summary>
        /// Sets the state.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="state">The state.</param>
        /// <param name="activeBitmap">The active bitmap.</param>
        /// <param name="inactiveBitmap">The inactive bitmap.</param>
        public static void SetState(string key, string state, Bitmap activeBitmap, Bitmap inactiveBitmap)
        {
            if (_SplashForms.ContainsKey(key))
            {
                _SplashForms[key].SetProgressBitmap(activeBitmap, inactiveBitmap);
            }
            SetState(key, state);
        }

        /// <summary>
        /// Closes the splash.
        /// </summary>
        /// <param name="key">The key.</param>
        public static void CloseSplash(string key)
        {
            if (_SplashForms.ContainsKey(key))
            {
                _SplashForms[key].Close();
                _SplashForms.Remove(key);
            }
        }
    }
}
