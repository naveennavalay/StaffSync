using ModelStaffSync;
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

        public DataTable GetDepartmentExposure(int txtClientID, int txtFinYearID)
        {
            return objDashboardWidgetData.GetDepartmentExposure(txtClientID, txtFinYearID);
        }

        public DataTable GetAdvanceSummary(int txtClientID, int txtFinYearID)
        {
            return objDashboardWidgetData.GetAdvanceSummary(txtClientID, txtFinYearID);
        }

        public List<AdvanceRiskBaseInfo> GetAdvanceRiskBaseInfo(int txtClientID, int txtFinYearID)
        {
            return objDashboardWidgetData.GetAdvanceRiskBaseInfo(txtClientID, txtFinYearID);
        }

        public DataTable GetAgingDistributionInfo(int txtClientID, int txtFinYearID)
        {
            return objDashboardWidgetData.GetAgingDistributionInfo(txtClientID, txtFinYearID);
        }

        public int GetActiveEmployeesCount(int txtClientID, int txtFinYearID)
        {
            return objDashboardWidgetData.GetActiveEmployeesCount(txtClientID, txtFinYearID);
        }


        public int GetTotalEmployeesPresent(int txtClientID, int txtFinYearID)
        {
            return objDashboardWidgetData.GetTotalEmployeesPresent(txtClientID, txtFinYearID);
        }

        public int GetTotalEmployeesOnLeave(int txtClientID, int txtFinYearID)
        {
            return objDashboardWidgetData.GetTotalEmployeesOnLeave(txtClientID, txtFinYearID);
        }

        public int GetCountOfAllEmployeesBirthdayCountToday(int txtClientID, int txtFinYearID)
        {
            return objDashboardWidgetData.GetCountOfAllEmployeesBirthdayCountToday(txtClientID, txtFinYearID);
        }

        public int GetCountOfAllEmployeesWorkAnniversaryCountToday(int txtClientID, int txtFinYearID)
        {
            return objDashboardWidgetData.GetCountOfAllEmployeesWorkAnniversaryCountToday(txtClientID, txtFinYearID);
        }

        public int GetPendingLeaveApprovalCount(int txtClientID, int txtFinYearID)
        {
            return objDashboardWidgetData.GetPendingLeaveApprovalCount(txtClientID, txtFinYearID);
        }

        public int GetEmployeesWeeklyOffCount(int txtClientID, int txtFinYearID)
        {
            return objDashboardWidgetData.GetEmployeesWeeklyOffCount(txtClientID, txtFinYearID);
        }

        public DataTable GetUpcomingHolidays(int txtClientID, int txtFinYearID)
        {
            return objDashboardWidgetData.GetUpcomingHolidays(txtClientID, txtFinYearID);
        }

        public DataTable GetAllEmployeesPresentList(int txtClientID, int txtFinYearID)
        {
            return objDashboardWidgetData.GetAllEmployeesPresentList(txtClientID, txtFinYearID);
        }

        public DataTable GetAllEmployeesWeeklyOffList(int txtClientID, int txtFinYearID)
        {
            return objDashboardWidgetData.GetAllEmployeesWeeklyOffList(txtClientID, txtFinYearID);
        }

        public DataTable GetTodaysEmployeesBirthdayList(int txtClientID, int txtFinYearID)
        {
            return objDashboardWidgetData.GetTodaysEmployeesBirthdayList(txtClientID, txtFinYearID);
        }

        public DataTable GetTodaysEmployeesWorkAnniversaryList(int txtClientID, int txtFinYearID)
        {
            return objDashboardWidgetData.GetTodaysEmployeesWorkAnniversaryList(txtClientID, txtFinYearID);
        }
    }
}
