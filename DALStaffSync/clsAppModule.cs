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
    public class clsAppModule
    {
        dbStaffSync.clsAppModule objAppModule = new dbStaffSync.clsAppModule();

        public clsAppModule() { 

        }

        public string GetModuleTitleByID(int txtModuleID)
        {
            string selectedModuleTitle = "";
            
            selectedModuleTitle = objAppModule.GetModuleTitleByID(txtModuleID);

            return selectedModuleTitle;
        }

        public List<AppModuleInfo> GetDefaultAppModuleInfo()
        {
            List<AppModuleInfo> objUserAppModuleList = new List<AppModuleInfo>();

            objUserAppModuleList = objAppModule.GetDefaultAppModuleInfo();

            return objUserAppModuleList;
        }

        public List<AppModuleInfo> GetModulesInfo(int txtUserID)
        {
            List<AppModuleInfo> objReturnAppModuleInfoList = new List<AppModuleInfo>();
            
            objReturnAppModuleInfoList = objAppModule.GetModulesInfo(txtUserID);

            return objReturnAppModuleInfoList;
        }

        public int InsertUsersAppModuleInfo(int txtUserID, int txtModuleID)
        {
            int affectedRows = 0;

            affectedRows = objAppModule.InsertUsersAppModuleInfo(txtUserID, txtModuleID);

            return affectedRows;
        }

        public int RemoveUsersAppModuleInfo(int txtUserID)
        {
            int affectedRows = 0;

            affectedRows = objAppModule.RemoveUsersAppModuleInfo(txtUserID);

            return affectedRows;
        }
    }
}
