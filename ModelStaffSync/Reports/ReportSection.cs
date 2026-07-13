using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    /// <summary>
    /// Represents one logical section of a report.
    /// </summary>
    public class ReportSection
    {
        public string Title { get; set; }

        public List<ReportColumn> Columns { get; }

        public List<ReportRow> Rows { get; }

        public ReportSection()
        {
            Columns = new List<ReportColumn>();
            Rows = new List<ReportRow>();
        }

        public void AddColumn(ReportColumn column)
        {
            Columns.Add(column);
        }

        public void AddRow(ReportRow row)
        {
            Rows.Add(row);
        }
    }
}
