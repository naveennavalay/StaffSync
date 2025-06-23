using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaffSync
{
    public class clsLogin
    {
        myDBClass objDBClass = new myDBClass();
        clsEncryptDecrypt objEncryptDecrypt = new clsEncryptDecrypt();
        OleDbConnection conn = null;
        DataSet dtDataset = null;

        public clsLogin() {

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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return rowCount;
        }

        public UserInfo getSpecificUserInfo(int txtEmpID)
        {
            UserInfo userInfo = new UserInfo();

            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT * FROM Users WHERE EmpID = " + txtEmpID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                List<UserInfo> objUserInfo = JsonConvert.DeserializeObject<List<UserInfo>>(DataTableToJSon);
                if (objUserInfo.Count > 0)
                {
                    userInfo.UserID = objUserInfo[0].UserID;
                    userInfo.EmpID = objUserInfo[0].EmpID;
                    userInfo.IsActive = objUserInfo[0].IsActive;
                    userInfo.IsDeleted = objUserInfo[0].IsDeleted;
                    userInfo.EmpUserName = objUserInfo[0].EmpUserName;
                    userInfo.EmpPassword = objUserInfo[0].EmpPassword;
                    userInfo.IsLocked = objUserInfo[0].IsLocked;
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

            return userInfo;
        }

        public UserInfo getSpecificUserInfo(string UserName)
        {
            UserInfo userInfo = new UserInfo();

            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT * FROM Users WHERE EmpUserName = '" + UserName + "'";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                List<UserInfo> objUserInfo = JsonConvert.DeserializeObject<List<UserInfo>>(DataTableToJSon);
                if (objUserInfo.Count > 0)
                {
                    userInfo.UserID = objUserInfo[0].UserID;
                    userInfo.EmpID = objUserInfo[0].EmpID;
                    userInfo.IsActive = objUserInfo[0].IsActive;
                    userInfo.IsDeleted = objUserInfo[0].IsDeleted;
                    userInfo.EmpUserName = objUserInfo[0].EmpUserName;
                    userInfo.EmpPassword = objUserInfo[0].EmpPassword;
                    userInfo.IsLocked = objUserInfo[0].IsLocked;
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

            return userInfo;
        }

        public int InsertUserInfo(int txtEmpID, bool IsActive, bool IsDeleted, string txtUserPassword)
        {
            int affectedRows = 0;

            try
            {
                int maxRowCount = getMaxRowCount("Users", "UserID");

                conn = objDBClass.openDBConnection();

                string strQuery = "INSERT INTO Users (UserID, EmpID, IsActive, IsDeleted, EmpPassword) VALUES " +
                "(" + maxRowCount + "," + txtEmpID + "," + IsActive + "," + IsDeleted + ",'" + objEncryptDecrypt.encryptText(txtUserPassword) + "')";

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

        public int UpdateUserInfo(int txtEmpID, bool IsActive, bool IsDeleted, string txtUserPassword)
        {
            int affectedRows = 0;

            try
            {
                int maxRowCount = getMaxRowCount("Users", "UserID");

                conn = objDBClass.openDBConnection();

                string strQuery = "Update Users SET EmpPassword = '" + objEncryptDecrypt.encryptText(txtUserPassword) + "'" +
                " WHERE EmpID = " + txtEmpID;

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

        public int UpdatePassword(int txtEmpID, string txtUserPassword)
        {
            int affectedRows = 0;

            try
            {
                int maxRowCount = getMaxRowCount("Users", "UserID");

                conn = objDBClass.openDBConnection();

                string strQuery = "Update Users SET EmpPassword = '" + objEncryptDecrypt.encryptText(txtUserPassword) + "'" +
                " WHERE EmpID = " + txtEmpID;

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

        public int LockOrUnlockUserInfo(int txtEmpID, bool LockOrUnlock)
        {
            int affectedRows = 0;

            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "Update Users SET IsLocked = " + LockOrUnlock + " WHERE UserID = " + txtEmpID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtEmpID;
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

        public bool AuthenticateUserInfo(string txtUserName, string txtUserPassword)
        {
            bool UserAuthenticated = true;

            UserInfo objLoggingInUserInfo = getSpecificUserInfo(txtUserName);
            if (objLoggingInUserInfo.UserID != 0)
            {
                if (objLoggingInUserInfo.IsActive == true)
                {
                    if (objLoggingInUserInfo.IsLocked == false)
                    {
                        if (txtUserName.ToString().ToLower() == objLoggingInUserInfo.EmpUserName.ToString().ToLower())
                        {
                            UserAuthenticated = true;
                            if (txtUserPassword.ToString().Trim() == objEncryptDecrypt.decryptText(objLoggingInUserInfo.EmpPassword.ToString()))
                            {
                                UserAuthenticated = true;
                            }
                            else
                            {
                                MessageBox.Show("User Name or Password is wrong. \nPlease retry again.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                UserAuthenticated = false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("User Name or Password is wrong. \nPlease retry again.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            UserAuthenticated = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("User is Locked.\nPlease check your credentials to continue.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        UserAuthenticated = false;
                    }
                }
                else
                {
                    MessageBox.Show("User is no more active.\nPlease check your credentials to continue.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UserAuthenticated = false;
                }
            }
            else
            {
                UserAuthenticated = false;
            }
                


            return UserAuthenticated;
        }
    }

    public class UserInfo
    {
        public int UserID { get; set; }
        public int EmpID { get; set; }
        public string EmpUserName { get; set; }
        public string EmpPassword { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsLocked { get; set; }
    }
}
