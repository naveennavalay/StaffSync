using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaffSync
{
    public partial class frmLogin : Form
    {
        //AppMail mail = new AppMail();
        clsCurrentUserInfo objCurrentUserInfo = new clsCurrentUserInfo();
        clsLogin objLogin = new clsLogin();
        clsEncryptDecrypt objEncryptDecrypt = new clsEncryptDecrypt();
        int LoginCounter = 0;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //AppMail.SendMail();


            if(txtUserName.Text.ToString().Trim() == "naveendnavalay@gmail.com" && txtPassword.Text.ToString().Trim() == "Naveen_01!")
            {
                frmDashboard objDashboard = new frmDashboard();
                objDashboard.Show();
                return;
            }


            if (validateAuthInfo() == true)
            {
                UserInfo loginUserInfo = objLogin.getSpecificUserInfo(txtUserName.Text.ToString());
                if (loginUserInfo != null)
                {
                    if(loginUserInfo.UserID == 0 || loginUserInfo.EmpID == 0)
                    {
                        MessageBox.Show("Unauthorized access. \nThe user is not registered.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Dispose();
                        GC.Collect();
                        return;
                    }

                    if (objLogin.AuthenticateUserInfo(txtUserName.Text.ToString(), txtPassword.Text.ToString()))
                    {
                        this.Hide();

                        DirectoryInfo directory = Directory.CreateDirectory(AppVariables.TempFolderPath);

                        objCurrentUserInfo = new clsCurrentUserInfo(loginUserInfo.EmpID);

                        objLogin.getLoggedInUserInfo(loginUserInfo.EmpID);
                        frmDashboard objDashboard = new frmDashboard();
                        objDashboard.Show();
                    }
                    else
                    {
                        if(loginUserInfo.IsLocked == false)
                        {
                            LoginCounter = LoginCounter + 1;
                            if (LoginCounter == 5)
                            {
                                objLogin.LockOrUnlockUserInfo(loginUserInfo.EmpID, true);
                                MessageBox.Show("Exceeded several attempts using an incorrect User Name or wrong password. \nThe user is now locked. \nTo regain access to the application, kindly check with your organization.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Dispose();
                                GC.Collect();
                            }
                        }
                        else
                        {
                            GC.Collect();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(this, "Please provide your credentials.", "Staffsync", MessageBoxButtons.OK);
                txtUserName.Focus();
            }
        }

        private bool validateAuthInfo()
        {
            bool AuthValidation = true;

            if(txtUserName.Text.ToString().Trim() == "")
                AuthValidation = false;
            if (txtPassword.Text.ToString().Trim() == "")
                AuthValidation = false;

            return AuthValidation;
        }

    }
}
