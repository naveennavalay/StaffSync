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
    public class clsLogin
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset = null;
        clsEncryptDecrypt objEncryptDecrypt = new clsEncryptDecrypt();
        clsGenFunc objGenFunc = new clsGenFunc();

        public clsLogin()
        {

        }

        public UserInfo getSpecificUserInfo(int txtEmpID)
        {
            UserInfo userInfo = new UserInfo();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
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
                //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = dbStaffSync.closeDBConnection();
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }

            return userInfo;
        }

        public UserInfo getSpecificUserInfo(string UserName)
        {
            UserInfo userInfo = new UserInfo();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
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
                //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = dbStaffSync.closeDBConnection();
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }

            return userInfo;
        }

        public void getLoggedInUserInfo(int txtEmpID)
        {
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT " +
                                        "EmpMas.EmpID, " +
                                        "EmpMas.EmpCode, " +
                                        "EmpMas.EmpName, " +
                                        "EmpMas.ClientID, " +
                                        "DesigMas.DesignationTitle, " +
                                        "DepMas.DepartmentTitle, " +
                                        "BloodGroupMas.BloodGroupTitle, " +
                                        "PersonalInfoMas.DOB, " +
                                        "PersonalInfoMas.DOJ, " +
                                        "AddressMas.Address1, " +
                                        "AddressMas.Address2, " +
                                        "AddressMas.Area, " +
                                        "AddressMas.City, " +
                                        "StateMas.StateTitle, " +
                                        "AddressMas.PIN, " +
                                        "CountryMas.CountryTitle, " +
                                        "SexMas.SexTitle, " +
                                        "NomineeMas.NomineePerson, " +
                                        "RelationShipMas.RelationShipTitle, " +
                                        "PersonalInfoMas.ContactNumber1, " +
                                        "PersonalInfoMas.ContactNumber2, " +
                                        "EmpBankInfo.EmpACNumber, " +
                                        "BankMasInfo.BankName, " +
                                        "BankMasInfo.BankAddress, " +
                                        "BankMasInfo.IFSCCode, " +
                                        "LeaveMas.BalanceLeaves " +
                                    "FROM " +
                                        "(" +
                                            "RelationShipMas " +
                                            "INNER JOIN(" +
                                                "(" +
                                                    "StateMas " +
                                                    "INNER JOIN ( " +
                                                        "SexMas " +
                                                        "INNER JOIN ( " +
                                                            "(" +
                                                                "(" +
                                                                    "DesigMas " +
                                                                    "INNER JOIN ( " +
                                                                        "DepMas " +
                                                                        "INNER JOIN ( " +
                                                                            "BloodGroupMas " +
                                                                            "INNER JOIN EmpMas ON BloodGroupMas.BloodGroupID = EmpMas.BloodGroupID " +
                                                                        ") ON DepMas.DepartmentID = EmpMas.DepartmentID " +
                                                                    ") ON DesigMas.DesignationID = EmpMas.EmpDesignationID " +
                                                                ") " +
                                                                "INNER JOIN LeaveMas ON EmpMas.EmpID = LeaveMas.EmpID " +
                                                            ") " +
                                                            "INNER JOIN(" +
                                                                "CountryMas " +
                                                                "INNER JOIN ( " +
                                                                    "AddressMas " +
                                                                    "INNER JOIN PersonalInfoMas ON AddressMas.AddressID = PersonalInfoMas.PerAddressID " +
                                                                ") ON CountryMas.CountryID = AddressMas.CountryID " +
                                                            ") ON EmpMas.EmpID = PersonalInfoMas.EmpID " +
                                                        ") ON SexMas.SexID = PersonalInfoMas.SexID " +
                                                    ") ON StateMas.StateID = AddressMas.StateID " +
                                                ") " +
                                                "INNER JOIN NomineeMas ON EmpMas.EmpID = NomineeMas.EmpID " +
                                            ") ON RelationShipMas.RelationShipID = NomineeMas.RelationShipID " +
                                        ") " +
                                        "INNER JOIN ( " +
                                            "BankMasInfo " +
                                            "INNER JOIN EmpBankInfo ON BankMasInfo.BankID = EmpBankInfo.BankID " +
                                        ") ON EmpMas.EmpID = EmpBankInfo.EmpID " +
                                    "WHERE " +
                                        "(((EmpMas.EmpID) = " + txtEmpID + ")); ";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                List<LoggedInUser> objLoggedInUser = JsonConvert.DeserializeObject<List<LoggedInUser>>(DataTableToJSon);
                if (objLoggedInUser.Count > 0)
                {
                    ModelStaffSync.CurrentUser.EmpID = objLoggedInUser[0].EmpID;
                    ModelStaffSync.CurrentUser.EmpCode = objLoggedInUser[0].EmpCode;
                    ModelStaffSync.CurrentUser.EmpName = objLoggedInUser[0].EmpName;
                    ModelStaffSync.CurrentUser.ClientID = objLoggedInUser[0].ClientID;
                    ModelStaffSync.CurrentUser.DesignationTitle = objLoggedInUser[0].DesignationTitle;
                    ModelStaffSync.CurrentUser.DepartmentTitle = objLoggedInUser[0].DepartmentTitle;
                    ModelStaffSync.CurrentUser.BloodGroupTitle = objLoggedInUser[0].BloodGroupTitle;
                    ModelStaffSync.CurrentUser.DOB = objLoggedInUser[0].DOB;
                    ModelStaffSync.CurrentUser.DOJ = objLoggedInUser[0].DOJ;
                    ModelStaffSync.CurrentUser.Address1 = objLoggedInUser[0].Address1;
                    ModelStaffSync.CurrentUser.Address2 = objLoggedInUser[0].Address2;
                    ModelStaffSync.CurrentUser.Area = objLoggedInUser[0].Area;
                    ModelStaffSync.CurrentUser.City = objLoggedInUser[0].City;
                    ModelStaffSync.CurrentUser.StateTitle = objLoggedInUser[0].StateTitle;
                    ModelStaffSync.CurrentUser.PIN = objLoggedInUser[0].Area;
                    ModelStaffSync.CurrentUser.CountryTitle = objLoggedInUser[0].CountryTitle;
                    ModelStaffSync.CurrentUser.SexTitle = objLoggedInUser[0].SexTitle;
                    ModelStaffSync.CurrentUser.NomineePerson = objLoggedInUser[0].NomineePerson;
                    ModelStaffSync.CurrentUser.RelationShipTitle = objLoggedInUser[0].RelationShipTitle;
                    ModelStaffSync.CurrentUser.ContactNumber1 = objLoggedInUser[0].ContactNumber1;
                    ModelStaffSync.CurrentUser.ContactNumber2 = objLoggedInUser[0].ContactNumber2;
                    ModelStaffSync.CurrentUser.EmpACNumber = objLoggedInUser[0].EmpACNumber;
                    ModelStaffSync.CurrentUser.BankName = objLoggedInUser[0].BankName;
                    ModelStaffSync.CurrentUser.BankAddress = objLoggedInUser[0].BankAddress;
                    ModelStaffSync.CurrentUser.IFSCCode = objLoggedInUser[0].IFSCCode;
                    ModelStaffSync.CurrentUser.BalanceLeaves = objLoggedInUser[0].BalanceLeaves;
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

            return;
        }

        public int InsertUserInfo(int txtEmpID, bool IsActive, bool IsDeleted, string txtUserName, string txtUserPassword)
        {
            int affectedRows = 0;

            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("Users", "UserID");

                conn = dbStaffSync.openDBConnection();

                string strQuery = "INSERT INTO Users (UserID, EmpID, IsActive, IsDeleted, EmpUserName, EmpPassword) VALUES " +
                "(" + maxRowCount.Data + "," + txtEmpID + "," + IsActive + "," + IsDeleted + ",'" + txtUserName + "','" + txtUserPassword + "')";

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

        public int UpdateUserInfo(int txtEmpID, bool IsActive, bool IsDeleted, string txtUserPassword)
        {
            int affectedRows = 0;

            try
            {
                int maxRowCount = 0;

                conn = dbStaffSync.openDBConnection();

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
                //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = dbStaffSync.closeDBConnection();
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }
            return affectedRows;
        }

        public int UpdatePassword(int txtEmpID, string txtUserPassword)
        {
            int affectedRows = 0;

            try
            {
                int maxRowCount = 0;

                conn = dbStaffSync.openDBConnection();

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
                //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = dbStaffSync.closeDBConnection();
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }
            return affectedRows;
        }

        public int LockOrUnlockUserInfo(int txtEmpID, bool LockOrUnlock)
        {
            int affectedRows = 0;

            try
            {
                conn = dbStaffSync.openDBConnection();

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
                //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = dbStaffSync.closeDBConnection();
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
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
                                //MessageBox.Show("User Name or Password is wrong. \nPlease retry again.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                UserAuthenticated = false;
                            }
                        }
                        else
                        {
                            //MessageBox.Show("User Name or Password is wrong. \nPlease retry again.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            UserAuthenticated = false;
                        }
                    }
                    else
                    {
                        //MessageBox.Show("User is Locked.\nPlease check your credentials to continue.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        UserAuthenticated = false;
                    }
                }
                else
                {
                    //MessageBox.Show("User is no more active.\nPlease check your credentials to continue.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UserAuthenticated = false;
                }
            }
            else
            {
                UserAuthenticated = false;
            }

            return UserAuthenticated;
        }

        public UserRolesAndResponsibilitiesInfo GetUserRolesAndResponsibilitiesInfo(int txtEmpID)
        {
            UserRolesAndResponsibilitiesInfo objUserRolesAndResponsibilitiesInfo = new UserRolesAndResponsibilitiesInfo();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT * FROM Users WHERE EmpID = " + txtEmpID + "";
                strQuery = "SELECT * FROM qryUserRolesResponsibilitiesInfo WHERE EmpID = " + txtEmpID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                List<UserRolesAndResponsibilitiesInfo> objUserInfo = JsonConvert.DeserializeObject<List<UserRolesAndResponsibilitiesInfo>>(DataTableToJSon);
                if (objUserInfo.Count > 0)
                {
                    objUserRolesAndResponsibilitiesInfo = objUserInfo[0];
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

            return objUserRolesAndResponsibilitiesInfo;
        }
    }
}
