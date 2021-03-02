//==========================================================================================
//
//		LameSoft.SniffThat.Plugins.KmlExportModule.KmlExport
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
using LameSoft.SniffThat.Common;
using System.Xml.Serialization;
using System.IO;
using System.Globalization;

namespace LameSoft.SniffThat.Plugins.KmlExportModule
{
	public class KmlExport : IExportPlugin
	{
		public KmlExport()
		{
            KmlResources.Culture = CultureInfo.CurrentCulture;
			_SettingsPanel = new SettingsPanel();
		}

		#region IExportPlugin Member

		private SettingsPanel _SettingsPanel;

		public System.Windows.Forms.Control ExportSettingsControl
		{
			get 
			{
				return _SettingsPanel;
			}
		}

		public bool Export(IList<AccessPointEntry> entries)
		{
			_Context.StoreSettingsValue("ExportFileName", _SettingsPanel.ExportFileName);
			kml kml = new kml();

			Folder f = new Folder();

			string desc = "SniffThat Export " + DateTime.Now.ToString();

			f.name = new name();
			f.name.Text = new string[] { desc };

			f.description = new description();
			f.description.Text = new string[] { desc };

			f.open = new open();
			f.open.id = "0";


			foreach (AccessPointEntry entry in entries)
				AddAccessPointEntry(f, entry);

			kml.Item = f;

			string exportPath = _SettingsPanel.ExportFileName;

			using(FileStream outStream = new FileStream(exportPath, FileMode.Create, FileAccess.Write))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(kml));
				serializer.Serialize(outStream, kml);
			}

            _Context.ShowMessage(String.Format(KmlResources.ExportMessage, entries.Count, _SettingsPanel.ExportFileName));

			return true;
		}

		#endregion

		#region IPlugin Member

		private IModuleContext _Context;

		public IModuleContext Context
		{
			set 
			{
				_Context = value;
				_SettingsPanel.ExportFileName = _Context.GetSettingsValue("ExportFileName");
			}
		}

		/// <summary>
		/// Gets the name of the plugin.
		/// </summary>
		/// <value>The name of the plugin.</value>
		public string PluginName
		{
			get 
			{
				return KmlResources.PluginName;
			}
		}

		/// <summary>
		/// Gets the plugin description.
		/// </summary>
		/// <value>The plugin description.</value>
		public string PluginDescription
		{
			get 
			{
				return KmlResources.PluginDescription;
			}
		}

		/// <summary>
		/// Gets the plugin author.
		/// </summary>
		/// <value>The plugin author.</value>
		public string PluginAuthor
		{
			get 
			{
				return "LameSoft";
			}
		}

		/// <summary>
		/// Gets the global settings control.
		/// </summary>
		/// <value>The global settings control.</value>
		public System.Windows.Forms.Control GlobalSettingsControl
		{
			get 
			{
				return null;
			}
		}

		#endregion

		#region Kml Export

		private void AddAccessPointEntry(Folder folder, AccessPointEntry entry)
		{
			if(folder.Placemark == null)
				folder.Placemark = new Placemark[0];

			List<Placemark> placemarks = new List<Placemark>(folder.Placemark);

			Placemark newPlaceMark = new Placemark();

            newPlaceMark.name = new name();
            newPlaceMark.name.Text = new string[] { GetValidString(entry.SSID + (entry.Protected ? " (Prot)" : " (Open)")) };

            string lineBreak = "<br>";

			newPlaceMark.description = new description();
			newPlaceMark.description.Text = new string[7];
            newPlaceMark.description.Text[0] = GetValidString("SSID: " + entry.SSID + lineBreak);
            newPlaceMark.description.Text[1] = GetValidString("Protected: " + entry.Protected + lineBreak);
            newPlaceMark.description.Text[2] = GetValidString("Last Update: " + entry.LastSeenString + lineBreak);
            newPlaceMark.description.Text[3] = GetValidString("MAC Address: " + entry.MacAddressString + lineBreak);
            newPlaceMark.description.Text[4] = GetValidString("Gps Info: " + entry.GpsInfo + lineBreak);
            newPlaceMark.description.Text[5] = GetValidString("Gps Info Distance: " + entry.GpsInfoDistance + lineBreak);
            newPlaceMark.description.Text[6] = GetValidString("First Seen: " + entry.FirstSeenString + lineBreak);

			newPlaceMark.styleUrl = new styleUrl();
			newPlaceMark.styleUrl.id = "root://styles#default+icon=0x307";
			newPlaceMark.Point = new Point[1];
			newPlaceMark.Point[0] = new Point();
			newPlaceMark.Point[0].coordinates = new coordinates[1];
			newPlaceMark.Point[0].coordinates[0] = new coordinates();
			newPlaceMark.Point[0].coordinates[0].Text = new string[1];

			IFormatProvider coordinateFormatProvider = CultureInfo.CreateSpecificCulture("en-US");

            newPlaceMark.Point[0].coordinates[0].Text[0] = entry.Longitude.ToString("0.0000000", coordinateFormatProvider) + ", " + entry.Latitude.ToString("0.0000000", coordinateFormatProvider);

			placemarks.Add(newPlaceMark);

			folder.Placemark = placemarks.ToArray();
		}

        private string GetValidString(string @string)
        {
            StringBuilder result = new StringBuilder();

            foreach (char c in @string)
                if ((int)c < 10)
                    result.Append("\\" + ((int)c).ToString());
                else
                    result.Append(c);

            return result.ToString();
        }

		#endregion
	}
}
