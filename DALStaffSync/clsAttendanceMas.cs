using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class clsAttendanceMas
    {
        dbStaffSync.clsAttendanceMas objAttendanceMas = new dbStaffSync.clsAttendanceMas();

        public clsAttendanceMas() { 

        }

        public List<EmployeeAttendanceInfo> GetDefaultEmployeeAttendanceInfo(int txtEmpID, DateTime dtSelectedMonth)
        {
            List<EmployeeAttendanceInfo> objReturnEmployeeAttendanceInfoList = new List<EmployeeAttendanceInfo>();
            
            objReturnEmployeeAttendanceInfoList = objAttendanceMas.GetDefaultEmployeeAttendanceInfo(txtEmpID, dtSelectedMonth);

            return objReturnEmployeeAttendanceInfoList;
        }

        public List<EmployeeAttendanceInfo> GetDefaultEmployeeAttendanceInfoForJobs(int txtEmpID, DateTime dtSelectedMonth)
        {
            List<EmployeeAttendanceInfo> objReturnEmployeeAttendanceInfoList = new List<EmployeeAttendanceInfo>();

            objReturnEmployeeAttendanceInfoList = objAttendanceMas.GetDefaultEmployeeAttendanceInfoForJobs(txtEmpID, dtSelectedMonth);

            return objReturnEmployeeAttendanceInfoList;
        }

        public EmployeeTotalWorkingInfo GetEmployeeMonthlyWorkingDays(int txtEmpID, DateTime dtSelectedDateFrom, DateTime dtSelectedDateTo)
        {
            EmployeeTotalWorkingInfo objReturnEmployeeTotalWorkingInfo = new EmployeeTotalWorkingInfo();

            objReturnEmployeeTotalWorkingInfo = objAttendanceMas.GetEmployeeMonthlyWorkingDays(txtEmpID, dtSelectedDateFrom, dtSelectedDateTo);

            return objReturnEmployeeTotalWorkingInfo;
        }

        public EmployeeTotalWorkingInfo getTotalPresentDays(int txtEmpID, DateTime dtSelectedDateFrom, DateTime dtSelectedDateTo)
        {
            EmployeeTotalWorkingInfo objReturnEmployeeTotalWorkingInfo = new EmployeeTotalWorkingInfo();

            objReturnEmployeeTotalWorkingInfo = objAttendanceMas.getTotalPresentDays(txtEmpID, dtSelectedDateFrom, dtSelectedDateTo);

            return objReturnEmployeeTotalWorkingInfo;
        }

        public EmployeeTotalWorkingInfo getTotalLossOfPayDays(int txtEmpID, DateTime dtSelectedDateFrom, DateTime dtSelectedDateTo)
        {
            EmployeeTotalWorkingInfo objReturnEmployeeTotalWorkingInfo = new EmployeeTotalWorkingInfo();

            objReturnEmployeeTotalWorkingInfo = objAttendanceMas.getTotalLossOfPayDays(txtEmpID, dtSelectedDateFrom, dtSelectedDateTo);

            return objReturnEmployeeTotalWorkingInfo;
        }

        public EmployeeTotalWorkingInfo getTotalPaidLeave(int txtEmpID, DateTime dtSelectedDateFrom, DateTime dtSelectedDateTo)
        {
            EmployeeTotalWorkingInfo objReturnEmployeeTotalWorkingInfo = new EmployeeTotalWorkingInfo();

            objReturnEmployeeTotalWorkingInfo = objAttendanceMas.getTotalPaidLeave(txtEmpID, dtSelectedDateFrom, dtSelectedDateTo);

            return objReturnEmployeeTotalWorkingInfo;
        }

        public EmployeeAttendanceInfo GetEmployeeSpecificDailyAttendanceInfo(int txtEmpID, DateTime dtSelectedDate)
        {
            EmployeeAttendanceInfo objReturnEmployeeAttendanceInfo = new EmployeeAttendanceInfo();

            objReturnEmployeeAttendanceInfo = objAttendanceMas.GetEmployeeSpecificDailyAttendanceInfo(txtEmpID, dtSelectedDate);

            return objReturnEmployeeAttendanceInfo;
        }

        public List<MonthlyAttendanceInfo> EmployeeSpecificMonthlyAttendanceInfo(int EmpID, DateTime ReportForTheMonth)
        {
            List<MonthlyAttendanceInfo> objMonthlyAttendanceReport = new List<MonthlyAttendanceInfo>();

            objMonthlyAttendanceReport = objAttendanceMas.EmployeeSpecificMonthlyAttendanceInfo(EmpID, ReportForTheMonth);

            return objMonthlyAttendanceReport;
        }

        public List<MonthlyAttendanceInfo> MonthlyAttendanceReport(DateTime ReportForTheMonth)
        {
            List<MonthlyAttendanceInfo> objMonthlyAttendanceReport = new List<MonthlyAttendanceInfo>();
            
            objMonthlyAttendanceReport = objAttendanceMas.MonthlyAttendanceReport(ReportForTheMonth);

            return objMonthlyAttendanceReport;
        }

        public List<MonthlyAttendanceInfo> DailyBatchAttendance(int txtCompanyID, DateTime ReportForTheMonth, string DayNumber)
        {
            List<MonthlyAttendanceInfo> objDailyBatchAttendance = new List<MonthlyAttendanceInfo>();
            
            objDailyBatchAttendance = objAttendanceMas.DailyBatchAttendance(txtCompanyID, ReportForTheMonth, DayNumber);
            
            return objDailyBatchAttendance;
        }

        public int InsertDailyAttendance(int txtEmpID, DateTime AttendanceDate, string AttendanceStatus, int LeaveTRID)
        {
            int affectedRows = 0;

            affectedRows = objAttendanceMas.InsertDailyAttendance(txtEmpID, AttendanceDate, AttendanceStatus, LeaveTRID);

            return affectedRows;
        }

        public int UpdateDailyAttendance(int txtEmpID, DateTime AttendanceDate, string AttendanceStatus, int LeaveTRID)
        {
            int affectedRows = 0;

            affectedRows = objAttendanceMas.UpdateDailyAttendance(txtEmpID, AttendanceDate, AttendanceStatus, LeaveTRID);

            return affectedRows;
        }
    }
}
