using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class EmployeeAttendanceClockInfo
    {
        public bool Success { get; set; }

        public string Status { get; set; }

        public string ShiftTitle { get; set; }
        public string ShiftLoginTime { get; set; }
        public string ShiftLogoutTime { get; set; }

        public DateTime? LoginTime { get; set; }

        public DateTime? LogoutTime { get; set; }

        public string Message { get; set; }

        public EmployeeAttendanceClockInfo()
        {
            Success = false;
            ShiftTitle = null;
            ShiftLoginTime = null;
            ShiftLogoutTime = null;
            Status = "NotLoggedIn";
            LoginTime = null;
            LogoutTime = null;
            Message = string.Empty;
        }
    }
}

