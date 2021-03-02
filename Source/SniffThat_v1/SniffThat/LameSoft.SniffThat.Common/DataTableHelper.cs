//==========================================================================================
//
//		LameSoft.SniffThat.Common.DataTableHelper
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
using System.Collections;
using System.Reflection;
using System.Data;

namespace LameSoft.SniffThat.Common
{
    /// <summary>
    /// Helper Methods for creating DataTables from objects
    /// </summary>
    public class DataTableHelper
    {
        /// <summary>
        /// Gets the items from object.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        public static object[] GetItemsFromObject(object o, int position)
        {
            PropertyInfo[] fields = o.GetType().GetProperties();

            ArrayList values = new ArrayList();
            foreach (PropertyInfo pi in fields)
            {
                values.Add(pi.GetValue(o, null));
            }

            values.Add(o);
            values.Add(position);

            return values.ToArray();
        }

        private static bool RowChanged(object o, object[] rowEntries, int position)
        {
            PropertyInfo[] fields = o.GetType().GetProperties();

            if (rowEntries.Length != fields.Length + 1)
                return true;

            for (int i = 0; i < fields.Length; i++)
            {
                if (!object.Equals(rowEntries[i], fields[i].GetValue(o, null)))
                    return true;
            }

            if (!object.Equals(rowEntries[fields.Length], position))
                return true;

            return false;
        }

        /// <summary>
        /// Creates the type of the table from.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="tablename">The tablename.</param>
        /// <param name="objectrefColName">Name of the objectref col.</param>
        /// <returns></returns>
        public static DataTable CreateTableFromType(Type t, string tablename, string objectrefColName)
        {
            PropertyInfo[] fields = t.GetProperties();

            DataTable result = new DataTable(tablename);

            foreach (PropertyInfo pi in fields)
            {
                result.Columns.Add(pi.Name, pi.PropertyType);
            }

            result.Columns.Add(objectrefColName, t);
            result.Columns.Add("__Position", typeof(int));

            return result;
        }

        /// <summary>
        /// Sets the row count from list.
        /// </summary>
        /// <param name="tbl">The TBL.</param>
        /// <param name="list">The list.</param>
        public static void SetRowCountFromList(DataTable tbl, IList list)
        {
            ArrayList dummyVals = new ArrayList();

            for (int i = 0; i < tbl.Columns.Count; i++)
                dummyVals.Add(null);

            while (tbl.Rows.Count < list.Count)
                tbl.Rows.Add(dummyVals.ToArray());

            while (tbl.Rows.Count > list.Count)
                tbl.Rows.RemoveAt(tbl.Rows.Count - 1);
        }

        /// <summary>
        /// Sets the list to table.
        /// </summary>
        /// <param name="tbl">The TBL.</param>
        /// <param name="list">The list.</param>
        public static void SetListToTable(DataTable tbl, IList list)
        {
            SetRowCountFromList(tbl, list);

            for (int i = 0; i < list.Count; i++)
            {
                Object o = list[i];

                bool mustUpdate = RowChanged(o, tbl.Rows[i].ItemArray, i);

                if (mustUpdate)
                    tbl.Rows[i].ItemArray = GetItemsFromObject(o, i + 1);
            }
        }
    }
}
