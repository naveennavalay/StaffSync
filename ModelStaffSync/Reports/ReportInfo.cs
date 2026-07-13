using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    /// <summary>
    /// Represents report specific information.
    /// </summary>
    public class ReportInfo
    {
        public string ReportTitle { get; set; }

        public string ReportSubTitle { get; set; }

        public string ReportCode { get; set; }

        public string Version { get; set; }

        public string GeneratedBy { get; set; }

        public DateTime GeneratedOn { get; set; }

        public string FinancialYear { get; set; }

        public bool ShowLogo { get; set; }

        public bool ShowFooter { get; set; }

        public bool ShowPageNumber { get; set; }

        public bool ShowGeneratedInformation { get; set; }

        public ReportInfo()
        {
            ReportTitle = string.Empty;
            ReportSubTitle = string.Empty;
            ReportCode = string.Empty;
            Version = "1.0";
            GeneratedBy = string.Empty;
            GeneratedOn = DateTime.Now;
            FinancialYear = string.Empty;

            ShowLogo = true;
            ShowFooter = true;
            ShowPageNumber = true;
            ShowGeneratedInformation = true;
        }
    }
}
