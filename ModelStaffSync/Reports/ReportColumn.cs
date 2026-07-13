using ModelStaffSync.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    /// <summary>
    /// Represents a report column.
    /// </summary>
    public class ReportColumn
    {
        public string Header { get; set; }

        public string FieldName { get; set; }

        public string PropertyName { get; set; }

        public int Order { get; set; }

        public double Width { get; set; }

        public double MinimumWidth { get; set; } = 1.2;

        public double MaximumWidth { get; set; } = 8;

        public ReportAlignment Alignment { get; set; }

        public bool Visible { get; set; } = true;

        public bool AllowWrap { get; set; }

        public string Format { get; set; }

        public bool WordWrap { get; set; }

        public bool AutoFit { get; set; } = true;

        public bool Bold { get; set; }

        public bool AllowSort { get; set; }

        public bool AllowFilter { get; set; }

        public double FontSize { get; set; } = 9;

        public bool ShowTotal { get; set; }

        public string TotalCaption { get; set; }

        public bool TotalBold { get; set; }

        public string TotalFormat { get; set; }

        public ReportColumn()
        {
            Visible = true;
            Width = 3;
            Alignment = ReportAlignment.Left;
            AllowWrap = true;
            
            ShowTotal = false;
            TotalCaption = "Total";
            TotalBold = true;
            TotalFormat = "";
        }

        public ReportColumn(string header, string fieldName) : this()
        {
            Header = header;
            FieldName = fieldName;
        }
    }
}
