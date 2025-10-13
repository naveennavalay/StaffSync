using dbStaffSync;
using ModelStaffSync;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class clsLogin
    {
        dbStaffSync.clsLogin objLogin = new dbStaffSync.clsLogin();
        clsEncryptDecrypt objEncryptDecrypt = new clsEncryptDecrypt();

        public clsLogin()
        {

        }

        public UserInfo getSpecificUserInfo(int txtEmpID)
        {
            UserInfo userInfo = objLogin.getSpecificUserInfo(txtEmpID);
            return userInfo;
        }

        public UserInfo getSpecificUserInfo(string UserName)
        {
            UserInfo userInfo = objLogin.getSpecificUserInfo(UserName);
            return userInfo;
        }

        public void getLoggedInUserInfo(int txtEmpID)
        {
            objLogin.getLoggedInUserInfo(txtEmpID);
            return;
        }

        public int InsertUserInfo(int txtEmpID, bool IsActive, bool IsDeleted, string txtUserName, string txtUserPassword)
        {
            int affectedRows = 0;

            affectedRows = objLogin.InsertUserInfo(txtEmpID, IsActive, IsDeleted, txtUserName, txtUserPassword);

            return affectedRows;
        }

        public int UpdateUserInfo(int txtEmpID, bool IsActive, bool IsDeleted, string txtUserPassword)
        {
            int affectedRows = 0;

            affectedRows = objLogin.UpdateUserInfo(txtEmpID, IsActive, IsDeleted, txtUserPassword);

            return affectedRows;
        }

        public int UpdatePassword(int txtEmpID, string txtUserPassword)
        {
            int affectedRows = 0;

            affectedRows = objLogin.UpdatePassword(txtEmpID, txtUserPassword);

            return affectedRows;
        }

        public int LockOrUnlockUserInfo(int txtEmpID, bool LockOrUnlock)
        {
            int affectedRows = 0;

            affectedRows = objLogin.LockOrUnlockUserInfo(txtEmpID, LockOrUnlock);

            return affectedRows;
        }

        public bool AuthenticateUserInfo(string txtUserName, string txtUserPassword)
        {
            bool UserAuthenticated = true;

            UserInfo objLoggingInUserInfo = objLogin.getSpecificUserInfo(txtUserName);
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

    }
}
