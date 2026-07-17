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

        public List<PersonalInformationListReport> getPersonalInformationListReport(int ClientID, string Filter)
        {
            List<PersonalInformationListReport> objPersonalInformationListReportList = new List<PersonalInformationListReport>();
            objPersonalInformationListReportList = objEmployeeRelatedReportQueries.getPersonalInformationListReport(ClientID, Filter);
            return objPersonalInformationListReportList;
        }

        public List<EmployeeActiveInactiveReport> getEmployeeActiveInactiveReport(int ClientID, string Filter)
        {
            List<EmployeeActiveInactiveReport> objEmployeeActiveInactiveReportList = new List<EmployeeActiveInactiveReport>();
            objEmployeeActiveInactiveReportList = objEmployeeRelatedReportQueries.getEmployeeActiveInactiveReport(ClientID, Filter);
            return objEmployeeActiveInactiveReportList;
        }

        public List<MonthlyAttendanceReport> getMonthlyAttendanceRegister(int ClientID, DateTime dtFrom, DateTime dtTo)
        {
            List<MonthlyAttendanceReport> objMonthlyAttendanceReport = new List<MonthlyAttendanceReport>();
            objMonthlyAttendanceReport = objEmployeeRelatedReportQueries.getMonthlyAttendanceRegister(ClientID, dtFrom, dtTo);
            return objMonthlyAttendanceReport;
        }

        public List<DailyAttendanceReport> getDailyAttendanceRegister(int ClientID, DateTime dtDate)
        {
            List<DailyAttendanceReport> objDailyAttendanceReport = new List<DailyAttendanceReport>();
            objDailyAttendanceReport = objEmployeeRelatedReportQueries.getDailyAttendanceRegister(ClientID, dtDate);
            return objDailyAttendanceReport;
        }
    }
}
