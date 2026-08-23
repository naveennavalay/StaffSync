using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class AttendanceCalendarChartResponse
    {
        public List<AttendanceCalendarMonthData> MonthData { get; set; }

        public List<AttendanceCalendarDateData> DateData { get; set; }

        public List<AttendanceCalendarDepartmentData> DepartmentData { get; set; }

        public AttendanceCalendarChartResponse()
        {
            MonthData = new List<AttendanceCalendarMonthData>();

            DateData = new List<AttendanceCalendarDateData>();

            DepartmentData = new List<AttendanceCalendarDepartmentData>();
        }
    }
}
