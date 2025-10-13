using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DALStaffSync
{
    public class clsCurrentUserInfo
    {
        dbStaffSync.clsCurrentUserInfo objCurrentUserInfo = new dbStaffSync.clsCurrentUserInfo();
        dbStaffSync.clsAppModule objAppModule = new dbStaffSync.clsAppModule();
        dbStaffSync.clsDepartment objDepartment = new dbStaffSync.clsDepartment();
        dbStaffSync.clsDesignation objDesignation = new dbStaffSync.clsDesignation();
        dbStaffSync.clsEmployeeMaster objEmployeeInfo = new dbStaffSync.clsEmployeeMaster();
        dbStaffSync.clsRolesAndResponsibilities objRoles = new dbStaffSync.clsRolesAndResponsibilities();

        public clsCurrentUserInfo() { 

        }

        public clsCurrentUserInfo(int txtEmpID)
        {
            //objCurrentUserInfo = dbStaffSync.clsCurrentUserInfo(txtEmpID);
        }

        public bool UserModuleAccessInfo(int txtUserID, int txtAppModuleID)
        {
            bool AccessAvailable = true;

            objCurrentUserInfo = new dbStaffSync.clsCurrentUserInfo(txtUserID);

            return AccessAvailable;
        }

        public bool UserRoleAccessInfo(int txtUserID, int txtRoleID)
        {
            bool AccessAvailable = true;

            AccessAvailable = objCurrentUserInfo.UserRoleAccessInfo(txtUserID, txtRoleID);

            return AccessAvailable;
        }
    }
}
