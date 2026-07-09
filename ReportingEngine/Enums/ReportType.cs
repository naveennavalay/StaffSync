using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Enum
{
    /// <summary>
    /// All report types supported by StaffSync.
    /// </summary>
    public enum ReportType
    {
        EmployeeMaster,
        EmployeeList,
        EmployeeProfile,

        AttendanceRegister,
        AttendanceSummary,
        AttendanceDaily,
        AttendanceMonthly,

        LeaveRegister,
        LeaveSummary,
        LeaveBalance,

        SalarySlip,
        SalaryRegister,
        PayrollRegister,
        PayrollSummary,
        BankTransfer,

        CandidateList,
        InterviewSchedule,

        CompanyProfile,
        HolidayList,

        SchedulerJobs,
        SchedulerHistory,
        SchedulerExecution,

        AdvanceRegister,
        LoanRegister,
        AssetRegister,

        Birthday,
        WorkAnniversary,

        Custom
    }
}
