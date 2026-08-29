using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class EmployeeAttendanceSummaryChartData
    {
        public int EmpID { get; set; }

        public string EmpName { get; set; }

        public int DepartmentID { get; set; }
        public string DepartmentTitle { get; set; }
        public DateTime AttendanceDate { get; set; }
        public double PresentCount { get; set; }
        public double LeaveCount { get; set; }
        public double CancelledCount { get; set; }
        public double RejectedCount { get; set; }
        public int TotalAttendanceDays { get; set; }
    }
}
