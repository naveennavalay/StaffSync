using MigraDoc.DocumentObjectModel.Tables;
using ReportingEngine.Factories;
using ReportingEngine.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Layout
{
    /// <summary>
    /// Represents one horizontal row containing one or more tables.
    /// Example:
    ///
    /// +-----------+-----------+
    /// | Table 1   | Table 2   |
    /// +-----------+-----------+
    ///
    /// </summary>
    public class ReportTableRow
    {

        /// <summary>
        /// Tables displayed in the same row.
        /// </summary>
        public List<ReportDynamicTable> Tables
        {
            get;
            set;
        }

        /// <summary>
        /// Space before this row (cm).
        /// </summary>
        public double SpaceBefore
        {
            get;
            set;
        }

        /// <summary>
        /// Space after this row (cm).
        /// </summary>
        public double SpaceAfter
        {
            get;
            set;
        }

        /// <summary>
        /// Keeps the entire row together on a page whenever possible.
        /// </summary>
        public bool KeepTogether
        {
            get;
            set;
        }

        public double TableSpacing 
        { 
            get; 
            set; 
        } = 0.30;

        public int MaxTablesPerRow 
        { 
            get; 
            set; 
        } = 5;

        public string Caption 
        { 
            get; 
            set; 
        }

        public bool ShowCaption 
        { 
            get; 
            set; 
        } = true;

        public double CaptionFontSize 
        { 
            get; 
            set; 
        } = 10;

        public bool CaptionBold 
        { 
            get; 
            set; 
        } = true;

        public bool CompactLayout 
        { 
            get; 
            set; 
        } = true;

        public ReportTableRow()
        {
            Tables = new List<ReportDynamicTable>();

            SpaceBefore = 0.20;

            SpaceAfter = 0.20;

            KeepTogether = true;
        }

        public ReportTableRow AddTable(ReportDynamicTable table)
        {
            if (table == null)
                throw new ArgumentNullException(nameof(table));

            Tables.Add(table);

            return this;
        }

        public ReportTableRow AddTables(params ReportDynamicTable[] tables)
        {
            if (tables == null)
                return this;

            foreach (ReportDynamicTable table in tables)
            {
                if (table != null)
                    Tables.Add(table);
            }

            return this;
        }

        public void AddTable<T>(IEnumerable<T> collection)
        {
            if (collection == null)
                return;

            AddTable(DynamicTableFactory.Create(collection));
        }

        public void AddTable(
            DataTable table)
        {
            if (table == null)
                return;

            AddTable(DynamicTableFactory.Create(table));
        }

        public int TableCount
        {
            get
            {
                return Tables.Count;
            }
        }
    }
}
