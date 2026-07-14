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
    public class clsAppReports
    {
        dbStaffSync.clsAppReports objAppReports = new dbStaffSync.clsAppReports();

        public List<AppReports> GetReportsList(string ReportType)
        {
            List<AppReports> objAppReportsList = new List<AppReports>();

            objAppReportsList = objAppReports.GetReportsList(ReportType);

            return objAppReportsList;
        }
    }
}
