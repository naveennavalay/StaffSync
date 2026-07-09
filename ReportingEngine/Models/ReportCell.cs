using ReportingEngine.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Models
{
    /// <summary>
    /// Represents a single report cell.
    /// </summary>
    public class ReportCell
    {
        public string Text { get; set; }

        public ReportAlignment Alignment { get; set; }

        public bool Bold { get; set; }

        public bool IsHeader { get; set; }

        public bool Visible { get; set; }

        public int ColumnSpan { get; set; }

        public int RowSpan { get; set; }

        public ReportCell()
        {
            Alignment = ReportAlignment.Left;
            Visible = true;
            ColumnSpan = 1;
            RowSpan = 1;
        }

        public ReportCell(string text)
            : this()
        {
            Text = text;
        }
    }
}
