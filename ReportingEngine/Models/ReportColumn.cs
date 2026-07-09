using ReportingEngine.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Models
{
    /// <summary>
    /// Represents a report column.
    /// </summary>
    public class ReportColumn
    {
        public string Header { get; set; }

        public string FieldName { get; set; }

        public double Width { get; set; }

        public ReportAlignment Alignment { get; set; }

        public bool Visible { get; set; }

        public bool AllowWrap { get; set; }

        public ReportColumn()
        {
            Visible = true;
            Width = 3;
            Alignment = ReportAlignment.Left;
            AllowWrap = true;
        }

        public ReportColumn(string header, string fieldName)
            : this()
        {
            Header = header;
            FieldName = fieldName;
        }
    }
}
