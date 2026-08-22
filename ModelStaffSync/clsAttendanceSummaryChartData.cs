using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class AttendanceSummaryChartData
    {
        public string Department { get; set; }

        public double TotalPresent { get; set; }

        public double TotalLeave { get; set; }

        public double TotalLOP { get; set; }
    }
}
