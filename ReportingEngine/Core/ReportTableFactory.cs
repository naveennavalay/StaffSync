using MigraDoc.DocumentObjectModel;
using ReportingEngine.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Core
{
    /// <summary>
    /// Factory class used to create ReportDynamicTable
    /// from DataTable or List<T>.
    /// </summary>
    public static class ReportTableFactory
    {
        #region DataTable

        public static ReportDynamicTable FromDataTable(DataTable table, string title)
        {
            ReportDynamicTable dynamicTable = new ReportDynamicTable();

            dynamicTable.Title = title;

            //---------------------------------------------------
            // Columns
            //---------------------------------------------------

            foreach (DataColumn column in table.Columns)
            {
                ReportDynamicColumn dynamicColumn = new ReportDynamicColumn();

                dynamicColumn.Header = column.ColumnName;

                dynamicColumn.PropertyName = column.ColumnName;

                dynamicColumn.Width = 3.0;

                dynamicColumn.Visible = true;

                dynamicColumn.Alignment = GetAlignment(column.DataType);

                dynamicTable.AddColumn(dynamicColumn);
            }

            //---------------------------------------------------
            // Rows
            //---------------------------------------------------

            foreach (DataRow dr in table.Rows)
            {
                ReportDynamicRow dynamicRow =
                    new ReportDynamicRow();

                foreach (DataColumn column in table.Columns)
                {
                    dynamicRow.Add(dr[column]);
                }

                dynamicTable.AddRow(dynamicRow);
            }

            return dynamicTable;
        }

        #endregion

        #region Generic List

        public static ReportDynamicTable FromList<T>(IList<T> list, string title)
        {
            ReportDynamicTable dynamicTable = new ReportDynamicTable();

            dynamicTable.Title = title;

            PropertyInfo[] properties = typeof(T).GetProperties();

            //---------------------------------------------------
            // Columns
            //---------------------------------------------------

            foreach (PropertyInfo property in properties)
            {
                ReportDynamicColumn column = new ReportDynamicColumn();

                column.Header = property.Name;

                column.PropertyName = property.Name;

                column.Width = 3.0;

                column.Visible = true;

                column.Alignment = GetAlignment(property.PropertyType);

                dynamicTable.AddColumn(column);
            }

            //---------------------------------------------------
            // Rows
            //---------------------------------------------------

            foreach (T item in list)
            {
                ReportDynamicRow row = new ReportDynamicRow();

                foreach (PropertyInfo property in properties)
                {
                    row.Add(property.GetValue(item, null));
                }

                dynamicTable.AddRow(row);
            }

            return dynamicTable;
        }

        #endregion

        #region Alignment

        private static ParagraphAlignment GetAlignment(Type type)
        {
            if (type == typeof(short) ||
                type == typeof(int) ||
                type == typeof(long) ||
                type == typeof(float) ||
                type == typeof(double) ||
                type == typeof(decimal))
            {
                return ParagraphAlignment.Right;
            }

            if (type == typeof(DateTime))
            {
                return ParagraphAlignment.Center;
            }

            return ParagraphAlignment.Left;
        }

        #endregion
    }
}
