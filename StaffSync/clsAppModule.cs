using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaffSync
{
    public class clsAppModule
    {
        myDBClass objDBClass = new myDBClass();
        OleDbConnection conn = null;
        DataSet dtDataset;

        public clsAppModule() { 

        }

        public int getMaxRowCount(string tableName, string ColumnName)
        {
            int rowCount = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT MAX(" + ColumnName.ToString().Trim() + ") FROM " + tableName;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                int maxRow = (Int32)cmd.ExecuteScalar();
                if (maxRow == 0)
                    rowCount = 1;
                else if (maxRow > 0)
                    rowCount = maxRow + 1;

            }
            catch (Exception ex)
            {
                if (ex.Message.ToString().ToLower() == "Specified cast is not valid.".ToLower())
                {
                    rowCount = 1;
                }
                else
                {
                    MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return rowCount;
        }

        public string GetModuleTitleByID(int txtModuleID)
        {
            string selectedModuleTitle = "";
            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "SELECT ModuleTitle FROM AppModules WHERE ModuleID = " + txtModuleID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                selectedModuleTitle = (string)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
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
                conn = objDBClass.openDBConnection();

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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
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
                conn = objDBClass.openDBConnection();

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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }
            return objReturnAppModuleInfoList;
        }

        public int InsertUsersAppModuleInfo(int txtUserID, int txtModuleID)
        {
            int affectedRows = 0;
            try
            {

                int maxRowCount = getMaxRowCount("UserModules", "UserModuleID");

                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO UserModules (UserModuleID, UserID, ModuleID) VALUES " +
                 "(" + maxRowCount + "," + txtUserID + "," + txtModuleID + ")";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = maxRowCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return affectedRows;
        }

        public int RemoveUsersAppModuleInfo(int txtUserID)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return affectedRows;
        }

    }

    public class AppModuleInfo
    {
        public int ModuleID { get; set; }
        //public string ModuleCode { get; set; }
        public string ModuleTitle { get; set; }
        //public string ModuleInitial { get; set; }
        //public bool IsActive { get; set; }
        //public bool IsDeleted { get; set; }
        public bool Access {  get; set; }
        public int? UserID { get; set; }
    }

    public class UserAppModuleInfo
    {
        public int UserModuleID { get; set; }
        public int UserID { get; set; }
        public int ModuleID { get; set; }
    }

}
