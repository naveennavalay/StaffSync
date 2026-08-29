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

        public AttendanceCalendarChartResponse getAttendanceCalendarChartData(int txtClientID, DateTime dtFrom, DateTime dtTo)
        {
            AttendanceCalendarChartResponse objAttendanceCalendarChartResponse = new AttendanceCalendarChartResponse();
            
            objAttendanceCalendarChartResponse = objDashboardChartWidgets.getAttendanceCalendarChartData(txtClientID, dtFrom, dtTo);

            return objAttendanceCalendarChartResponse;
        }

        public List<LeaveInfoDepartmentInfoChartData> GetLeaveUtilisationDepartmentInfo(int txtClientID, DateTime dtTillDate)
        {
            List<LeaveInfoDepartmentInfoChartData> objLeaveUtilisationDepartmentInfoList = new List<LeaveInfoDepartmentInfoChartData>();

            objLeaveUtilisationDepartmentInfoList = objDashboardChartWidgets.GetLeaveUtilisationDepartmentInfo(txtClientID, dtTillDate);

            return objLeaveUtilisationDepartmentInfoList;
        }

        public List<LeaveInfoEmpWiseChartData> GetEmpWiseLeaveUtilisationInfo(int txtClientID, int txtDepartmentID, DateTime dtTillDate)
        {
            List<LeaveInfoEmpWiseChartData> objEmpWiseLeaveUtilisationInfoList = new List<LeaveInfoEmpWiseChartData>();

            objEmpWiseLeaveUtilisationInfoList = objDashboardChartWidgets.GetEmpWiseLeaveUtilisationInfo(txtClientID, txtDepartmentID, dtTillDate);

            return objEmpWiseLeaveUtilisationInfoList;
        }

        public List<AttendanceCalendarChartData01> displayAttendanceCalendarChartData(int txtClientID, DateTime dtFromDate, DateTime dtToDate)
        {
            List<AttendanceCalendarChartData01> objAttendanceCalendarList = new List<AttendanceCalendarChartData01>();

            objAttendanceCalendarList = objDashboardChartWidgets.displayAttendanceCalendarChartData(txtClientID, dtFromDate, dtToDate);

            return objAttendanceCalendarList;
        }

        public List<EmployeeAttendanceSummaryChartData> displayAttendanceSummaryChartData(int clientID, DateTime fromDate, DateTime toDate)
        {
            List<EmployeeAttendanceSummaryChartData> objEmployeeAttendanceSummaryList = new List<EmployeeAttendanceSummaryChartData>();

            objEmployeeAttendanceSummaryList = objDashboardChartWidgets.displayAttendanceSummaryChartData(clientID, fromDate, toDate);

            return objEmployeeAttendanceSummaryList;
        }

        public List<UpcomingPlannedLeaveChartData> displayUpcomingPlannedLeavesChartData(int clientID, DateTime fromDate, DateTime toDate)
        {
            List<UpcomingPlannedLeaveChartData> objUpcomingPlannedLeaveList = new List<UpcomingPlannedLeaveChartData>();

            objUpcomingPlannedLeaveList = objDashboardChartWidgets.displayUpcomingPlannedLeavesChartData(clientID, fromDate, toDate);

            return objUpcomingPlannedLeaveList;
        }
    }
}
