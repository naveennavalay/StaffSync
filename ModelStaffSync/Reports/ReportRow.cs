using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    /// <summary>
    /// Represents a single report row.
    /// </summary>
    public class ReportRow
    {
        public List<ReportCell> Cells { get; }

        public bool IsHeaderRow { get; set; }

        public bool IsSummaryRow { get; set; }

        public bool AlternateColor { get; set; }

        public ReportRow()
        {
            Cells = new List<ReportCell>();
        }

        public void AddCell(string value)
        {
            Cells.Add(new ReportCell(value));
        }

        public void AddCell(ReportCell cell)
        {
            Cells.Add(cell);
        }
    }
}
