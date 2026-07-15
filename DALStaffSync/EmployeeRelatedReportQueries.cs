using ModelStaffSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class EmployeeRelatedReportQueries
    {
        dbStaffSync.EmployeeRelatedReportQueries objEmployeeRelatedReportQueries = new dbStaffSync.EmployeeRelatedReportQueries();

        public List<ActiveEmployeeListReport> getActiveEmployeeListReport(int ClientID, string Filter)
        {
            List<ActiveEmployeeListReport> objActiveEmployeeListReportList = new List<ActiveEmployeeListReport>();
            objActiveEmployeeListReportList = objEmployeeRelatedReportQueries.getActiveEmployeeListReport(ClientID, Filter);
            return objActiveEmployeeListReportList;
        }

        public List<MonthlyAttendanceReport> getMonthlyAttendanceRegister(int ClientID, DateTime dtFrom, DateTime dtTo)
        {
            List<MonthlyAttendanceReport> objMonthlyAttendanceReport = new List<MonthlyAttendanceReport>();
            objMonthlyAttendanceReport = objEmployeeRelatedReportQueries.getMonthlyAttendanceRegister(ClientID, dtFrom, dtTo);
            return objMonthlyAttendanceReport;
        }
    }
}
