using ModelStaffSync;
using Newtonsoft.Json;
using ReportingEngine.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class clsEmployeeDashboardConfig
    {
        dbStaffSync.clsEmployeeDashboardConfig objEmployeeDashboardConfig = new dbStaffSync.clsEmployeeDashboardConfig();

        public List<EmployeeDashboardConfigModel> getEmployeeDashboardConfigInfoList(int ClientID, int EmpID)
        {
            List<EmployeeDashboardConfigModel> lstEmployeeDashboardConfigModel = objEmployeeDashboardConfig.getEmployeeDashboardConfigInfoList(ClientID, EmpID);

            return lstEmployeeDashboardConfigModel;
        }

        public int InsertEmployeeDashboardConfigInfo(int txtPersonalInfoID, int txtDBChartID, bool boolDBChartEnabled, int txtOrderID)
        {
            int intEmpDBChartID = objEmployeeDashboardConfig.InsertEmployeeDashboardConfigInfo(txtPersonalInfoID, txtDBChartID, boolDBChartEnabled, txtOrderID);

            return intEmpDBChartID;
        }

        public int UpdateEmployeeDashboardConfigInfo(int txtEmpDBChartID, int txtPersonalInfoID, int txtDBChartID, bool boolDBChartEnabled, int txtOrderID)
        {
            int intEmpDBChartID = objEmployeeDashboardConfig.UpdateEmployeeDashboardConfigInfo(txtEmpDBChartID, txtPersonalInfoID, txtDBChartID, boolDBChartEnabled, txtOrderID);

            return intEmpDBChartID;
        }
    }
}
