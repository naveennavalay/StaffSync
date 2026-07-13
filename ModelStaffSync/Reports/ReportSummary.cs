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
        public string Caption { get; set; }

        public string Value { get; set; }

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
