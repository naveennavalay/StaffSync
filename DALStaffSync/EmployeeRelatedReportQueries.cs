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

        public List<ActiveEmployeeListReport> getActiveEmployeeListReport(int ClientID)
        {
            List<ActiveEmployeeListReport> objActiveEmployeeListReportList = new List<ActiveEmployeeListReport>();
            objActiveEmployeeListReportList = objEmployeeRelatedReportQueries.getActiveEmployeeListReport(ClientID);
            return objActiveEmployeeListReportList;
        }
    }
}
