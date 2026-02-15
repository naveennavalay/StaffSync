using ModelStaffSync;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class clsEmpMnthlyAttdInfo
    {
        dbStaffSync.clsEmpMnthlyAttdInfo objEmpMnthlyAttdInfo = new dbStaffSync.clsEmpMnthlyAttdInfo();

        public clsEmpMnthlyAttdInfo()
        {

        }

        public List<MonthlyAttendanceInfo> getConsolidatedMonthlyAttendanceInfo(DateTime AttendanceMonth)
        {
            List<MonthlyAttendanceInfo> objMonthlyAttendanceInfo = new List<MonthlyAttendanceInfo>();

            objMonthlyAttendanceInfo = objEmpMnthlyAttdInfo.getConsolidatedMonthlyAttendanceInfo(AttendanceMonth);

            return objMonthlyAttendanceInfo;
        }

        public List<MonthlyAttendanceInfo> getEmployeeMonthlyAttendanceInfo(int txtEmpID, DateTime AttendanceMonth)
        {
            List<MonthlyAttendanceInfo> objMonthlyAttendanceInfo = new List<MonthlyAttendanceInfo>();

            objMonthlyAttendanceInfo = objEmpMnthlyAttdInfo.getEmployeeMonthlyAttendanceInfo(txtEmpID, AttendanceMonth);

            return objMonthlyAttendanceInfo;
        }

        public int getMonthlyAttendanceInfo(int txtEmpID, DateTime AttendanceMonth)
        {
            int affectedRows = 0;

            affectedRows = objEmpMnthlyAttdInfo.getMonthlyAttendanceInfo(txtEmpID, AttendanceMonth);

            return affectedRows;
        }

        public int InsertMonthlyAttendanceInfo(int txtEmpID, DateTime AttendanceMonth)
        {
            int affectedRows = 0;

            affectedRows = objEmpMnthlyAttdInfo.InsertMonthlyAttendanceInfo(txtEmpID, AttendanceMonth);

            return affectedRows;
        }

        public int UpdateMonthlyAttendanceInfo(int SlNo, int txtEmpID, DateTime AttendanceMonth, string AttendanceDay, string AttendenceStatus)
        {
            int affectedRows = 0;
            
            affectedRows = objEmpMnthlyAttdInfo.UpdateMonthlyAttendanceInfo(SlNo, txtEmpID, AttendanceMonth, AttendanceDay, AttendenceStatus);

            return affectedRows;
        }

        public int InsertUnprocessedBatchAttendanceEntries(int txtEmpID, int txtBatchNumber, DateTime txtAttendanceSkipDate, string txtComments)
        {
            return objEmpMnthlyAttdInfo.InsertUnprocessedBatchAttendanceEntries(txtEmpID, txtBatchNumber, txtAttendanceSkipDate, txtComments);
        }

        public List<UnProcessedAttendanceInfo> GetUnprocessedBatchAttendanceEntries(int txtBatchProcessNumber)
        {
            return objEmpMnthlyAttdInfo.GetUnprocessedBatchAttendanceEntries(txtBatchProcessNumber);
        }
    }
}
