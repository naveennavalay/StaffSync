using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    /// <summary>
    /// Represents a report filter.
    /// </summary>
    public class ReportFilter
    {
        public string Caption { get; set; }

        public string Value { get; set; }

        public bool Visible { get; set; }

        public ReportFilter()
        {
            Visible = true;
        }

        public ReportFilter(string caption, string value)
            : this()
        {
            Caption = caption;
            Value = value;
        }
    }
}
