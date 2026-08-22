using ModelStaffSync;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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

        public List<LeaveMatrixChartData> GetLeaveMatrixChartData(int txtClientID, DateTime dtFrom, DateTime dtTo)
        {
            List<LeaveMatrixChartData> objLeaveMatrixList = new List<LeaveMatrixChartData>();

            objLeaveMatrixList = objDashboardChartWidgets.GetLeaveMatrixChartData(txtClientID, dtFrom, dtTo);

            return objLeaveMatrixList;
        }

        public List<AttendanceSummaryChartData> getAttendanceSummaryChartData(int txtClientID, DateTime dtFrom, DateTime dtTo)
        {
            List<AttendanceSummaryChartData> objAttendanceSummaryList = new List<AttendanceSummaryChartData>();

            objAttendanceSummaryList = objDashboardChartWidgets.getAttendanceDepartmentChartData(txtClientID, dtFrom, dtTo);

            return objAttendanceSummaryList;
        }

        public List<UpcomingHolidayChartData> GetUpcomingHolidayChartData(int txtClientID, int txtFinYearID)
        {
            List<UpcomingHolidayChartData> objUpcomingHolidayList = new List<UpcomingHolidayChartData>();

            objUpcomingHolidayList = objDashboardChartWidgets.GetUpcomingHolidayChartData(txtClientID, txtFinYearID);

            return objUpcomingHolidayList;
        }
    }
}
