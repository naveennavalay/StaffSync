using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public sealed class MonthlyAttendanceRegisterRow
    {
        public string RecordType { get; set; }

        public int? MonthNo { get; set; }
        public string MonthName { get; set; }

        public int? EmpID { get; set; }
        public string EmployeeName { get; set; }

        public DateTime? AttendanceDate { get; set; }
        public string AttendanceStatus { get; set; }

        public decimal? PresentCount { get; set; }
        public decimal? LeaveCount { get; set; }
        public decimal? CancelledCount { get; set; }
        public decimal? RejectedCount { get; set; }
    }
}
