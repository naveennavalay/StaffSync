using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace dbStaffSync
{
    public class clsAppModule
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public clsAppModule() { 

        }

        public string GetModuleTitleByID(int txtModuleID)
        {
            string selectedModuleTitle = "";
            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT ModuleTitle FROM AppModules WHERE ModuleID = " + txtModuleID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                selectedModuleTitle = (string)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = dbStaffSync.closeDBConnection();
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }

            return selectedModuleTitle;
        }

        public List<AppModuleInfo> GetDefaultAppModuleInfo()
        {

            List<AppModuleInfo> objAppModuleInfo = new List<AppModuleInfo>();
            List<AppModuleInfo> objUserAppModuleList = new List<AppModuleInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT ModuleID, ModuleTitle, IsActive, IsDeleted FROM AppModules WHERE IsActive = true AND IsDeleted = false ORDER BY OrderID";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAppModuleInfo = JsonConvert.DeserializeObject<List<AppModuleInfo>>(DataTableToJSon);
                foreach (AppModuleInfo indAppModuleInfo in objAppModuleInfo)
                {
                    objUserAppModuleList.Add(new AppModuleInfo
                    {
                        ModuleID = indAppModuleInfo.ModuleID,
                        ModuleTitle = indAppModuleInfo.ModuleTitle,
                        Access = false
                    });
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = dbStaffSync.closeDBConnection();
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }
            return objUserAppModuleList;
        }

        public List<AppModuleInfo> GetModulesInfo(int txtUserID)
        {

            List<AppModuleInfo> objAppModuleInfo = new List<AppModuleInfo>();
            List<AppModuleInfo> objReturnAppModuleInfoList = new List<AppModuleInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT ModuleID, ModuleTitle, IsActive, IsDeleted FROM AppModules WHERE IsActive = true AND IsDeleted = false ORDER BY OrderID";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAppModuleInfo = JsonConvert.DeserializeObject<List<AppModuleInfo>>(DataTableToJSon);
                foreach (AppModuleInfo indAppModuleInfo in objAppModuleInfo)
                {
                    objReturnAppModuleInfoList.Add(new AppModuleInfo
                    {
                        ModuleID = indAppModuleInfo.ModuleID,
                        ModuleTitle = indAppModuleInfo.ModuleTitle,
                        Access = indAppModuleInfo.UserID != null && indAppModuleInfo.UserID == txtUserID ? true : false
                        //UserID = txtUserID
                    });
                }

                List<UserAppModuleInfo> objUserRolesList = new List<UserAppModuleInfo>();
                strQuery = "SELECT * FROM UserModules WHERE UserID = " + txtUserID;

                dt = new DataTable();

                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objUserRolesList = JsonConvert.DeserializeObject<List<UserAppModuleInfo>>(DataTableToJSon);
                foreach (UserAppModuleInfo objTempAppModuleInfo in objUserRolesList)
                {
                    AppModuleInfo tempAppModuleInfo = objReturnAppModuleInfoList.FirstOrDefault(p => p.ModuleID == objTempAppModuleInfo.ModuleID);
                    tempAppModuleInfo.UserID = txtUserID;
                    tempAppModuleInfo.Access = true;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = dbStaffSync.closeDBConnection();
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }
            return objReturnAppModuleInfoList;
        }

        public int InsertUsersAppModuleInfo(int txtUserID, int txtModuleID)
        {
            int affectedRows = 0;
            try
            {

                Response<int> maxRowCount = objGenFunc.getMaxRowCount("UserModules", "UserModuleID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO UserModules (UserModuleID, UserID, ModuleID) VALUES " +
                 "(" + maxRowCount.Data + "," + txtUserID + "," + txtModuleID + ")";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = maxRowCount.Data;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = dbStaffSync.closeDBConnection();
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }

            return affectedRows;
        }

        public int RemoveUsersAppModuleInfo(int txtUserID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE FROM UserModules WHERE UserID = " + txtUserID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtUserID;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = dbStaffSync.closeDBConnection();
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }

            return affectedRows;
        }
    }
}
