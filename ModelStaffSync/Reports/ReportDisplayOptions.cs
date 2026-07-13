using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Linq;
using System.Text;

namespace ModelStaffSync
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

        public bool ShowWatermark { get; set; }

        public string WatermarkText { get; set; }

        public string WatermarkColorHex { get; set; } = "#D3D3D3";

        public double WatermarkFontSize { get; set; }
        public int WatermarkAngle { get; set; }
        public double WatermarkOpacity { get; set; }
    }
}
