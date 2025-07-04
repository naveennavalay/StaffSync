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
    public class clsRolesAndResponsibilities
    {
        myDBClass objDBClass = new myDBClass();
        OleDbConnection conn = null;
        DataSet dtDataset;

        public clsRolesAndResponsibilities() { 

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


        public string GetRoleTitleByID(int txtRoleID)
        {
            string selectedRoleTItle = "";
            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "SELECT RoleTitle FROM Roles WHERE RoleID = " + txtRoleID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                selectedRoleTItle = (string)cmd.ExecuteScalar();
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

            return selectedRoleTItle;
        }

        public List<RolesAndResponsibilitiesInfo> GetDefaultRolesAndResponsibilitiesInfo()
        {

            List<RolesAndResponsibilitiesInfo> objRolesAndResponsibilitiesInfo = new List<RolesAndResponsibilitiesInfo>();
            List<RolesAndResponsibilitiesInfo> objReturnRolesAndResponsibilitiesList = new List<RolesAndResponsibilitiesInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "SELECT * FROM Roles WHERE IsActive = true AND IsDeleted = false Order by OrderID";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objRolesAndResponsibilitiesInfo = JsonConvert.DeserializeObject<List<RolesAndResponsibilitiesInfo>>(DataTableToJSon);
                foreach (RolesAndResponsibilitiesInfo indRolesAndResponsibilitiesInfo in objRolesAndResponsibilitiesInfo)
                {
                    objReturnRolesAndResponsibilitiesList.Add(new RolesAndResponsibilitiesInfo
                    {
                        RoleID = indRolesAndResponsibilitiesInfo.RoleID,
                        RoleTitle = indRolesAndResponsibilitiesInfo.RoleTitle,
                        RoleDescription = indRolesAndResponsibilitiesInfo.RoleDescription,
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
            return objReturnRolesAndResponsibilitiesList;
        }

        public List<RolesAndResponsibilitiesInfo> GetRolesAndResponsibilitiesInfo(int txtUserID)
        {

            List<RolesAndResponsibilitiesInfo> objRolesAndResponsibilitiesInfo = new List<RolesAndResponsibilitiesInfo>();
            List<RolesAndResponsibilitiesInfo> objReturnRolesAndResponsibilitiesList = new List<RolesAndResponsibilitiesInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "SELECT * FROM Roles WHERE IsActive = true AND IsDeleted = false Order by OrderID";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objRolesAndResponsibilitiesInfo = JsonConvert.DeserializeObject<List<RolesAndResponsibilitiesInfo>>(DataTableToJSon);
                foreach (RolesAndResponsibilitiesInfo indRolesAndResponsibilitiesInfo in objRolesAndResponsibilitiesInfo)
                {
                    objReturnRolesAndResponsibilitiesList.Add(new RolesAndResponsibilitiesInfo
                    {
                        RoleID = indRolesAndResponsibilitiesInfo.RoleID,
                        RoleTitle = indRolesAndResponsibilitiesInfo.RoleTitle,
                        RoleDescription = indRolesAndResponsibilitiesInfo.RoleDescription,
                        Access = indRolesAndResponsibilitiesInfo.UserID != null && indRolesAndResponsibilitiesInfo.UserID == txtUserID ? true : false
                        //UserID = txtUserID
                    });
                }

                List<UserRoles> objUserRolesList = new List<UserRoles>();
                strQuery = "SELECT * FROM UserRoles WHERE UserID = " + txtUserID;
                
                dt = new DataTable();

                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objUserRolesList = JsonConvert.DeserializeObject<List<UserRoles>>(DataTableToJSon);
                foreach (UserRoles objTempRolesAndResponsibilitiesInfo in objUserRolesList)
                {
                    RolesAndResponsibilitiesInfo tempRolesAndResponsibilitiesInfo = objReturnRolesAndResponsibilitiesList.FirstOrDefault(p => p.RoleID == objTempRolesAndResponsibilitiesInfo.RoleID);
                    tempRolesAndResponsibilitiesInfo.UserID = txtUserID;
                    tempRolesAndResponsibilitiesInfo.Access = true;
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
            return objReturnRolesAndResponsibilitiesList;
        }

        public int InsertUsersRolesAndResponsibilitiesInfo(int txtUserID, int txtRoleID)
        {
            int affectedRows = 0;
            try
            {

                int maxRowCount = getMaxRowCount("UserRoles", "UserRoleID");

                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO UserRoles (UserRoleID, UserID, RoleID) VALUES " +
                 "(" + maxRowCount + "," + txtUserID + "," + txtRoleID + ")";

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

        public int RemoveUsersRolesAndResponsibilitiesInfo(int txtUserID)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE FROM UserRoles WHERE UserID = " + txtUserID;

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

        public List<RolesProfile> GetDefaultRolesProfileList()
        {

            List<RolesProfile> objobjReturnRolesProfileInfoList = new List<RolesProfile>();
            List<RolesProfile> objReturnRolesProfileList = new List<RolesProfile>();
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "SELECT * FROM qryRoleProfile";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objobjReturnRolesProfileInfoList = JsonConvert.DeserializeObject<List<RolesProfile>>(DataTableToJSon);
                foreach (RolesProfile indRolesProfile in objobjReturnRolesProfileInfoList)
                {
                    objReturnRolesProfileList.Add(new RolesProfile
                    {
                        RoleID = indRolesProfile.RoleID,
                        RoleTitle = indRolesProfile.RoleTitle,
                        RoleDefID = indRolesProfile.RoleDefID,
                        CanAdd = indRolesProfile.CanAdd,
                        CanUpdate = indRolesProfile.CanUpdate,
                        CanDelete = indRolesProfile.CanDelete,
                        CanView = indRolesProfile.CanView,
                        CanExport = indRolesProfile.CanExport
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
            return objReturnRolesProfileList;
        }

    }

    public class RolesAndResponsibilitiesInfo
    {
        public int RoleID { get; set; }
        public string RoleTitle { get; set; }
        public string RoleDescription { get; set; }
        public bool Access { get; set; }
        public int? UserID { get; set; }
        //public bool IsActive { get; set; }
        //public bool IsDeleted { get; set; }
    }

    public class UserRoles
    {
        public int UserRoleID { get; set; }
        public int RoleID { get; set; }
        public int? UserID { get; set; }
    }

    public class RolesProfile
    {
        public int RoleID { get; set; }
        public string RoleTitle { get; set; }
        public int RoleDefID { get; set; }
        public bool CanAdd { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanView { get; set; }
        public bool CanExport { get; set; }
    }

}
