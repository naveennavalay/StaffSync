using Common.Attibutes;
using ModelStaffSync.Enum;
using ModelStaffSync.Enums;
using ModelStaffSync.Reports.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    /// <summary>
    /// Represents a summary item.
    /// </summary>
    public class ReportSummary
    {
        [ReportColumnAttribute(Header = "Attribute", Width = 6, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        public string Caption { get; set; }

        [ReportColumnAttribute(Header = "Value", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        public string Value { get; set; }

        [ReportIgnore]
        public bool Bold { get; set; }

        public ReportSummary()
        {
            Bold = true;
        }

        public ReportSummary(string caption, string value) //: this()
        {
            Caption = caption;
            Value = value;
            Bold = true;
        }
    }
}
