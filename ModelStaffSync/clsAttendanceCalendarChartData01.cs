using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class AttendanceCalendarChartData01
    {
        public DateTime AttendanceDate { get; set; }
        public double PresentCount { get; set; }
        public double LeaveCount { get; set; }
        public double CancelledCount { get; set; }
        public double RejectedCount { get; set; }
    }
}
