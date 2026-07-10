using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Models
{
    public class ReportDisplayOptions
    {
        public bool ShowCompanyLogo { get; set; } = true;

        public bool ShowReportTitle { get; set; } = true;

        public bool ShowGeneratedDate { get; set; } = true;

        public bool ShowHeader { get; set; } = true;

        public bool ShowFooter { get; set; } = true;

        public bool ShowPageNumbers { get; set; } = true;

        public bool ShowSummary { get; set; } = true;

        public bool Landscape { get; set; } = false;

        public bool AlternateRows { get; set; } = true;

        public bool RepeatHeader { get; set; } = true;
    }
}
