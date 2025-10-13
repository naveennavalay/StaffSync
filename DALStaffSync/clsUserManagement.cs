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
    public class clsUserManagement
    {
        dbStaffSync.clsUserManagement objUserManagement = new dbStaffSync.clsUserManagement();

        public clsUserManagement() { 

        }

        public UserManagementList GetUserManagementInfo(int txtEmpID)
        {
            UserManagementList userManagementList = new UserManagementList();

            userManagementList = objUserManagement.GetUserManagementInfo(txtEmpID);

            return userManagementList;
        }

        public List<UserManagementList> GetUserManagementList()
        {
            List<UserManagementList> userManagementList = new List<UserManagementList>();
            
            userManagementList = objUserManagement.GetUserManagementList();

            return userManagementList;
        }

        public int UpdateUserActiveStatus(int txtEmpID, bool IsActive)
        {
            int affectedRows = 0;

            affectedRows = objUserManagement.UpdateUserActiveStatus(txtEmpID, IsActive);

            return affectedRows;
        }

        public int UpdateUserLockStatus(int txtEmpID, bool IsLocked)
        {
            int affectedRows = 0;
            
            affectedRows = objUserManagement.UpdateUserLockStatus(txtEmpID, IsLocked);

            return affectedRows;
        }


        public List<UserRolesAndResponsibilities> GetRolesAndResponsibilitiesList(int txtEmpID)
        {
            List<UserRolesAndResponsibilities> userRolesAndResponsibilities = new List<UserRolesAndResponsibilities>();
            
            userRolesAndResponsibilities = objUserManagement.GetRolesAndResponsibilitiesList(txtEmpID);

            return userRolesAndResponsibilities;
        }
    }
}
