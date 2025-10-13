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

        public List<EmployeeAttendanceInfo> GetDefaultEmployeeAttendanceInfo(int txtEmpID, int MonthNumber)
        {
            List<EmployeeAttendanceInfo> objReturnEmployeeAttendanceInfoList = new List<EmployeeAttendanceInfo>();
            
            objReturnEmployeeAttendanceInfoList = objAttendanceMas.GetDefaultEmployeeAttendanceInfo(txtEmpID, MonthNumber);

            return objReturnEmployeeAttendanceInfoList;
        }

        public List<MonthlyAttendanceInfo> MonthlyAttendanceReport(DateTime ReportForTheMonth)
        {
            List<MonthlyAttendanceInfo> objMonthlyAttendanceReport = new List<MonthlyAttendanceInfo>();
            
            objMonthlyAttendanceReport = objAttendanceMas.MonthlyAttendanceReport(ReportForTheMonth);

            return objMonthlyAttendanceReport;
        }

        public int InsertDailyAttendance(int txtEmpID, DateTime AttendanceDate, string AttendanceStatus, int LeaveTRID)
        {
            int affectedRows = 0;

            affectedRows = objAttendanceMas.InsertDailyAttendance(txtEmpID, AttendanceDate, AttendanceStatus, LeaveTRID);

            return affectedRows;
        }
    }
}
