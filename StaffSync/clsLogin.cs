//using C1.Framework;
using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public void getLoggedInUserInfo(int txtEmpID)
        {
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();
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
                    CurrentUser.EmpID = objLoggedInUser[0].EmpID;
                    CurrentUser.EmpCode = objLoggedInUser[0].EmpCode;
                    CurrentUser.EmpName = objLoggedInUser[0].EmpName;
                    CurrentUser.ClientID = objLoggedInUser[0].ClientID;
                    CurrentUser.DesignationTitle = objLoggedInUser[0].DesignationTitle;
                    CurrentUser.DepartmentTitle = objLoggedInUser[0].DepartmentTitle;
                    CurrentUser.BloodGroupTitle = objLoggedInUser[0].BloodGroupTitle;
                    CurrentUser.DOB = objLoggedInUser[0].DOB;
                    CurrentUser.DOJ = objLoggedInUser[0].DOJ;
                    CurrentUser.Address1 = objLoggedInUser[0].Address1;
                    CurrentUser.Address2 = objLoggedInUser[0].Address2;
                    CurrentUser.Area = objLoggedInUser[0].Area;
                    CurrentUser.City = objLoggedInUser[0].City;
                    CurrentUser.StateTitle = objLoggedInUser[0].StateTitle;
                    CurrentUser.PIN = objLoggedInUser[0].Area;
                    CurrentUser.CountryTitle = objLoggedInUser[0].CountryTitle;
                    CurrentUser.SexTitle = objLoggedInUser[0].SexTitle;
                    CurrentUser.NomineePerson = objLoggedInUser[0].NomineePerson;
                    CurrentUser.RelationShipTitle = objLoggedInUser[0].RelationShipTitle;
                    CurrentUser.ContactNumber1 = objLoggedInUser[0].ContactNumber1;
                    CurrentUser.ContactNumber2 = objLoggedInUser[0].ContactNumber2;
                    CurrentUser.EmpACNumber = objLoggedInUser[0].EmpACNumber;
                    CurrentUser.BankName = objLoggedInUser[0].BankName;
                    CurrentUser.BankAddress = objLoggedInUser[0].BankAddress;
                    CurrentUser.IFSCCode = objLoggedInUser[0].IFSCCode;
                    CurrentUser.BalanceLeaves = objLoggedInUser[0].BalanceLeaves;
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

            return;
        }

        public int InsertUserInfo(int txtEmpID, bool IsActive, bool IsDeleted, string txtUserName, string txtUserPassword)
        {
            int affectedRows = 0;

            try
            {
                int maxRowCount = getMaxRowCount("Users", "UserID");

                conn = objDBClass.openDBConnection();

                string strQuery = "INSERT INTO Users (UserID, EmpID, IsActive, IsDeleted, EmpUserName, EmpPassword) VALUES " +
                "(" + maxRowCount + "," + txtEmpID + "," + IsActive + "," + IsDeleted + ",'" + txtUserName + "','"+ txtUserPassword + "')";

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

    //public class UserInfo
    //{
    //    public int UserID { get; set; }
    //    public int EmpID { get; set; }
    //    public string EmpUserName { get; set; }
    //    public string EmpPassword { get; set; }
    //    public bool IsActive { get; set; }
    //    public bool IsDeleted { get; set; }
    //    public bool IsLocked { get; set; }
    //}

    //public class LoggedInUser
    //{
    //    public  int EmpID { get; set; }
    //    public int ClientID { get; set; }

    //    [DisplayName("Employee Code")]
    //    public string EmpCode { get; set; }
        
    //    [DisplayName("Employee Name")] 
    //    public string EmpName { get; set; }
        
    //    [DisplayName("Designation")] 
    //    public string DesignationTitle { get; set; }

    //    [DisplayName("Department")]
    //    public string DepartmentTitle { get; set; }

    //    [DisplayName("Blood Group")] 
    //    public string BloodGroupTitle { get; set; }

    //    [DisplayName("Date Of Birth")]
    //    public DateTime DOB { get; set; }

    //    [DisplayName("Date Of Joining")] 
    //    public DateTime DOJ { get; set; }


    //    [DisplayName("Address")] 
    //    public string Address1 { get; set; }

    //    [DisplayName("Address")]
    //    public string Address2 { get; set; }

    //    [DisplayName("Area")] 
    //    public string Area { get; set; }

    //    [DisplayName("City")]
    //    public string City { get; set; }

    //    [DisplayName("State")]
    //    public string StateTitle { get; set; }

    //    [DisplayName("PIN")]
    //    public string PIN { get; set; }

    //    [DisplayName("Country")]
    //    public string CountryTitle { get; set; }

    //    [DisplayName("Gender")]
    //    public string SexTitle { get; set; }

    //    [DisplayName("Nominee Name")]
    //    public string NomineePerson { get; set; }

    //    [DisplayName("Relationship")]
    //    public string RelationShipTitle { get; set; }

    //    [DisplayName("Contact Number")]
    //    public string ContactNumber1 { get; set; }

    //    [DisplayName("Mail ID")]
    //    public string ContactNumber2 { get; set; }

    //    [DisplayName("Bank Account Number")]
    //    public string EmpACNumber { get; set; }

    //    [DisplayName("Bank Name")]
    //    public string BankName { get; set; }

    //    [DisplayName("Bank Address")]
    //    public string BankAddress { get; set; }

    //    [DisplayName("Bank IFSC Code")]
    //    public string IFSCCode { get; set; }

    //    [DisplayName("Outstanding Leave Balance")]
    //    public decimal BalanceLeaves { get; set; }
    //}

}
