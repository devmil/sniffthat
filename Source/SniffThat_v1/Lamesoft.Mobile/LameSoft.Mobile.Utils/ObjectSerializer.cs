//==========================================================================================
//
//		LameSoft.Mobile.Utils.ObjectSerializer
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
using System.Xml.Serialization;
using System.IO;

namespace LameSoft.Mobile.Utils
{
    /// <summary>
    /// Util Class to Serialize and Deserialize objects
    /// </summary>
    public class ObjectSerializer
    {
        /// <summary>
        /// Serializes the specified object.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        public static byte[] Serialize(Object o)
        {
            MemoryStream memStream = new MemoryStream();

            XmlSerializer ser = new XmlSerializer(o.GetType());
            ser.Serialize(memStream, o);

            return memStream.ToArray();
        }

        /// <summary>
        /// Deserializes an object
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static Object DeSerialize(byte[] bytes, Type type)
        {
            MemoryStream memStream = new MemoryStream(bytes);

            return DeSerialize(memStream, type);
        }

        /// <summary>
        /// Deserializes an object
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static Object DeSerialize(Stream stream, Type type)
        {
            XmlSerializer ser = new XmlSerializer(type);

            return ser.Deserialize(stream);

        }

        /// <summary>
        /// Deserializes an object from string.
        /// </summary>
        /// <param name="str">The String.</param>
        /// <param name="t">The type.</param>
        /// <returns>the deserialized object or null if deserialization fails</returns>
        public static Object DeSerializeFromString(string str, Type t)
        {
            try
            {
                XmlSerializer xmlser = new XmlSerializer(t);

                MemoryStream ms = new MemoryStream(Convert.FromBase64String(str));

                return xmlser.Deserialize(ms);
            }
            catch
            { }

            return null;
        }

        /// <summary>
        /// Serializes an object to string.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>string representation of the object or null if serialization fails</returns>
        public static string SerializeToString(Object obj)
        {
            try
            {
                XmlSerializer xmlser = new XmlSerializer(obj.GetType());

                MemoryStream ms = new MemoryStream();

                xmlser.Serialize(ms, obj);

                return Convert.ToBase64String(ms.ToArray());
            }
            catch
            { }

            return null;
        }

    }
}
