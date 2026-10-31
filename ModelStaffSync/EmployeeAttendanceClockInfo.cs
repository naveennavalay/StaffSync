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
        public int ShiftID {  get; set; }
        public string ShiftTitle { get; set; }
        public DateTime? ShiftLoginTime { get; set; }
        public DateTime? ShiftLogoutTime { get; set; }
        public int AttID { get; set; }
        public DateTime AttDate { get; set; }
        public bool Success { get; set; }
        public string Status { get; set; }
        public DateTime? LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public DateTime? TotalTime { get; set; }

        public string Message { get; set; }

        public EmployeeAttendanceClockInfo()
        {
            ShiftID = 0;
            ShiftTitle = null;
            ShiftLoginTime = null;
            ShiftLogoutTime = null;
            AttID = 0;
            AttDate = DateTime.Today;
            LoginTime = null;
            LogoutTime = null;
            Success = false;
            Status = "NotLoggedIn";
            Message = string.Empty;
        }
    }
}

