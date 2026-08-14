using ModelStaffSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class clsDashboardChartWidgets
    {
        dbStaffSync.clsCurrentUserInfo objCurrentUserInfo = new dbStaffSync.clsCurrentUserInfo();
        dbStaffSync.clsAppModule objAppModule = new dbStaffSync.clsAppModule();
        dbStaffSync.clsRolesAndResponsibilities objRoles = new dbStaffSync.clsRolesAndResponsibilities();
        dbStaffSync.clsDashboardChartWidgets objDashboardChartWidgets = new dbStaffSync.clsDashboardChartWidgets(); 

        public List<LeaveStatusSummary> getLeaveStatusSummaryChartData(int txtClientID, DateTime dtFrom, DateTime dtTo)
        {
            List<LeaveStatusSummary> objLeaveStatusSummaryList = new List<LeaveStatusSummary>();

            objLeaveStatusSummaryList = objDashboardChartWidgets.getLeaveStatusSummaryChartData(txtClientID, dtFrom, dtTo);

            return objLeaveStatusSummaryList;
        }
    }
}
