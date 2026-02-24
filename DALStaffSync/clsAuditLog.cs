using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace DALStaffSync
{
    public class clsDashboardWidgetData
    {
        dbStaffSync.clsDashboardWidgetData objDashboardWidgetData = new dbStaffSync.clsDashboardWidgetData();

        public DataTable GetDepartmentExposure()
        {
            return objDashboardWidgetData.GetDepartmentExposure();
        }
    }
}
