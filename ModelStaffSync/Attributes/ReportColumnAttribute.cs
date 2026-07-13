using ModelStaffSync.Enum;
using ModelStaffSync.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelStaffSync.Reports.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ReportColumnAttribute : Attribute
    {
        public string Header { get; set; }

        public int Order { get; set; }

        public double Width { get; set; } = 2;

        public double MinimumWidth { get; set; } = 1.5;

        public double MaximumWidth { get; set; } = 8;

        public string Format { get; set; }

        public bool Visible { get; set; } = true;

        public bool AutoFit { get; set; } = true;
        public ReportColumnSizeMode SizeMode {  get; set; } = ReportColumnSizeMode.Fixed;

        public bool ShowTotal { get; set; }

        public bool TotalBold { get; set; }

        public bool GroupFooterTotal { get; set; }

        public ReportAlignment Alignment { get; set; } = ReportAlignment.Left;
    }
}
