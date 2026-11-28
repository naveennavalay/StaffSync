using System;
using System.Collections.Generic;

namespace WebStaffSync.Models
{
    public sealed class MyAttendanceViewModel
    {
        public string EmployeeName { get; set; } = "Employee";
        public int EmployeeId { get; set; }
        public DateTime SelectedMonth { get; set; }
        public decimal WorkingDays { get; set; }
        public decimal PresentDays { get; set; }
        public decimal PaidLeaveDays { get; set; }
        public decimal LossOfPayDays { get; set; }
        public int PresentCount { get; set; }
        public int LeaveCount { get; set; }
        public int CancelledCount { get; set; }
        public int RejectedCount { get; set; }
        public decimal AttendancePercentage { get; set; }
        public string EmployeeCode { get; set; } = "";
        public string Department { get; set; } = "";
        public string Designation { get; set; } = "";
        public List<MyAttendanceDayViewModel> Days { get; set; } = new List<MyAttendanceDayViewModel>();
        public List<MyAttendanceHistoryRowViewModel> MonthlyHistory { get; set; } = new List<MyAttendanceHistoryRowViewModel>();
        public MyAttendanceBehaviourSummaryViewModel Behaviour { get; set; } = new MyAttendanceBehaviourSummaryViewModel();
        public string ErrorMessage { get; set; }
    }

    public sealed class MyAttendanceDayViewModel
    {
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public bool HasRecord { get; set; }
        public string StatusClass { get; set; } = "status-none";
        public string Weekday { get; set; } = "";
    }

    public sealed class MyAttendanceHistoryRowViewModel
    {
        public DateTime Date { get; set; }
        public string Weekday { get; set; } = "";
        public string Status { get; set; } = "—";
        public string LoginTime { get; set; } = "—";
        public string LogoutTime { get; set; } = "—";
        public string EarlyLogin { get; set; } = "—";
        public string LateLogin { get; set; } = "—";
        public string EarlyLogout { get; set; } = "—";
        public string LateLogout { get; set; } = "—";
        public string TotalTime { get; set; } = "—";
        public decimal EarlyLoginMinutes { get; set; }
        public decimal LateLoginMinutes { get; set; }
        public decimal EarlyLogoutMinutes { get; set; }
        public decimal LateLogoutMinutes { get; set; }
        public decimal EarlyLoginPercent { get; set; }
        public decimal LateLoginPercent { get; set; }
        public decimal EarlyLogoutPercent { get; set; }
        public decimal LateLogoutPercent { get; set; }
    }

    public sealed class MyAttendanceBehaviourSummaryViewModel
    {
        public decimal EarlyLoginMinutes { get; set; }
        public decimal LateLoginMinutes { get; set; }
        public decimal EarlyLogoutMinutes { get; set; }
        public decimal LateLogoutMinutes { get; set; }
        public int EligibleDays { get; set; }
        public int EarlyLoginDays { get; set; }
        public int LateLoginDays { get; set; }
        public int EarlyLogoutDays { get; set; }
        public int LateLogoutDays { get; set; }
        public decimal EarlyLoginPercent => EligibleDays == 0 ? 0 : Math.Round(EarlyLoginDays * 100m / EligibleDays, 2);
        public decimal LateLoginPercent => EligibleDays == 0 ? 0 : Math.Round(LateLoginDays * 100m / EligibleDays, 2);
        public decimal EarlyLogoutPercent => EligibleDays == 0 ? 0 : Math.Round(EarlyLogoutDays * 100m / EligibleDays, 2);
        public decimal LateLogoutPercent => EligibleDays == 0 ? 0 : Math.Round(LateLogoutDays * 100m / EligibleDays, 2);
        public List<string> Insights { get; set; } = new List<string>();
    }

    public sealed class MyAttendanceDayDetailsViewModel
    {
        public string Date { get; set; }
        public string AttendanceStatus { get; set; }
        public string LeaveComments { get; set; }
        public string ShiftTitle { get; set; }
        public string ShiftLoginTime { get; set; }
        public string ShiftLogoutTime { get; set; }
        public string GracePeriod { get; set; }
        public string LoginTime { get; set; }
        public string LateLoginTime { get; set; }
        public string LateLoginDuration { get; set; }
        public string LogoutTime { get; set; }
        public string LateLogoutTime { get; set; }
        public string LateLogoutDuration { get; set; }
        public string TotalTime { get; set; }
        public string LeaveMode { get; set; }
        public string LeaveDuration { get; set; }
        public string Message { get; set; }
    }
}
