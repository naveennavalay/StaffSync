using ModelStaffSync.Reports.Attributes;
using ReportingEngine.Attributes;
using ReportingEngine.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Factories
{
    /// <summary>
    /// Creates ReportDynamicTable from
    /// Model collections,
    /// DataTable,
    /// Dictionary etc.
    /// </summary>
    public static class DynamicTableFactory
    {
        #region Public Methods

        /// <summary>
        /// Creates a dynamic table from any model collection.
        /// </summary>
        public static ReportDynamicTable Create<T>(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            ReportDynamicTable table = new ReportDynamicTable();

            //List<PropertyInfo> properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanRead).ToList();

            //List<PropertyInfo> properties =
            //    typeof(T)
            //    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            //    .Where(p => p.CanRead)
            //    .Where(p => !Attribute.IsDefined(p, typeof(ReportIgnoreAttribute)))
            //    .Where(p => p.GetCustomAttribute<ReportIgnoreAttribute>() == null)
            //    .Where(p =>
            //    {
            //        BrowsableAttribute browsable = p.GetCustomAttribute<BrowsableAttribute>();
            //        return browsable == null || browsable.Browsable;
            //    })
            //    .ToList();

            List<PropertyInfo> properties =
                typeof(T)
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead)
                    .Where(p => !Attribute.IsDefined(p, typeof(ReportIgnoreAttribute)))
                    .Where(p => p.GetCustomAttribute<ReportIgnoreAttribute>() == null)
                    .Where(p =>
                    {
                        ReportColumnAttribute attribute = p.GetCustomAttribute<ReportColumnAttribute>();

                        if (attribute == null)
                            return true;

                        return attribute.Visible;
                    })
                    .ToList();

            //----------------------------------------------------------
            // Columns
            //----------------------------------------------------------

            foreach (PropertyInfo property in properties)
            {
                table.AddColumn(
                    property.Name,
                    GetDefaultColumnWidth(property));
            }

            //----------------------------------------------------------
            // Rows
            //----------------------------------------------------------

            foreach (T item in collection)
            {
                List<object> values =
                    new List<object>();

                foreach (PropertyInfo property in properties)
                {
                    values.Add(
                        property.GetValue(item));
                }

                table.AddRow(values.ToArray());
            }

            return table;
        }

        /// <summary>
        /// Creates a dynamic table from DataTable.
        /// </summary>
        public static ReportDynamicTable Create(
            DataTable dataTable)
        {
            if (dataTable == null)
                throw new ArgumentNullException(nameof(dataTable));

            ReportDynamicTable table =
                new ReportDynamicTable();

            //----------------------------------------------------------
            // Columns
            //----------------------------------------------------------

            foreach (DataColumn column in dataTable.Columns)
            {
                table.AddColumn(
                    column.ColumnName,
                    4);
            }

            //----------------------------------------------------------
            // Rows
            //----------------------------------------------------------

            foreach (DataRow row in dataTable.Rows)
            {
                table.AddRow(
                    row.ItemArray);
            }

            return table;
        }

        #endregion

        #region Private Methods

        private static double GetDefaultColumnWidth(
            PropertyInfo property)
        {
            Type type =
                Nullable.GetUnderlyingType(property.PropertyType)
                ?? property.PropertyType;

            if (type == typeof(DateTime))
                return 4;

            if (type == typeof(decimal))
                return 3;

            if (type == typeof(double))
                return 3;

            if (type == typeof(float))
                return 3;

            if (type == typeof(int))
                return 2;

            if (type == typeof(long))
                return 2;

            if (type == typeof(bool))
                return 2;

            return 5;
        }

        #endregion
    }
}