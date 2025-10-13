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
    public class clsRolesAndResponsibilities
    {
        dbStaffSync.clsRolesAndResponsibilities objRolesAndResponsibilities = new dbStaffSync.clsRolesAndResponsibilities();

        public clsRolesAndResponsibilities() { 

        }

        public string GetRoleTitleByID(int txtRoleID)
        {
            string selectedRoleTItle = "";
            
            selectedRoleTItle = objRolesAndResponsibilities.GetRoleTitleByID(txtRoleID);

            return selectedRoleTItle;
        }

        public List<RolesAndResponsibilitiesInfo> GetDefaultRolesAndResponsibilitiesInfo()
        {
            List<RolesAndResponsibilitiesInfo> objReturnRolesAndResponsibilitiesList = new List<RolesAndResponsibilitiesInfo>();
            
            objReturnRolesAndResponsibilitiesList = objRolesAndResponsibilities.GetDefaultRolesAndResponsibilitiesInfo();

            return objReturnRolesAndResponsibilitiesList;
        }

        public List<RolesAndResponsibilitiesInfo> GetRolesAndResponsibilitiesInfo(int txtUserID)
        {
            List<RolesAndResponsibilitiesInfo> objReturnRolesAndResponsibilitiesList = new List<RolesAndResponsibilitiesInfo>();

            objReturnRolesAndResponsibilitiesList = objRolesAndResponsibilities.GetRolesAndResponsibilitiesInfo(txtUserID);

            return objReturnRolesAndResponsibilitiesList;
        }

        public int InsertUsersRolesAndResponsibilitiesInfo(int txtUserID, int txtRoleID)
        {
            int affectedRows = 0;

            affectedRows = objRolesAndResponsibilities.InsertUsersRolesAndResponsibilitiesInfo(txtUserID, txtRoleID);

            return affectedRows;
        }

        public int RemoveUsersRolesAndResponsibilitiesInfo(int txtUserID)
        {
            int affectedRows = 0;

            affectedRows = objRolesAndResponsibilities.RemoveUsersRolesAndResponsibilitiesInfo(txtUserID);

            return affectedRows;
        }

        public List<RolesProfile> GetDefaultRolesProfileList()
        {
            List<RolesProfile> objReturnRolesProfileList = new List<RolesProfile>();

            objReturnRolesProfileList = objRolesAndResponsibilities.GetDefaultRolesProfileList();

            return objReturnRolesProfileList;
        }
    }
}
