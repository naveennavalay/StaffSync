using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static C1.Util.Win.Win32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace StaffSync
{
    public class clsCurrentUserInfo
    {
        myDBClass objDBClass = new myDBClass();
        OleDbConnection conn = null;
        clsEmployeeMaster objEmployeeInfo = new clsEmployeeMaster();
        DataSet dtDataset;
        clsAppModule objAppModule = new clsAppModule();
        clsRolesAndResponsibilities objRoles = new clsRolesAndResponsibilities();
        clsDesignation objDesignation = new clsDesignation();
        clsDepartment objDepartment = new clsDepartment();

        public clsCurrentUserInfo() { 

        }

        public clsCurrentUserInfo(int txtEmpID)
        {

            UserInfo userInfo = new UserInfo();

            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT * FROM Users WHERE EmpID = " + txtEmpID;

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
                    CurrentLoggedInUserInfo.EmpID = objUserInfo[0].EmpID;
                    CurrentLoggedInUserInfo.UserID = objUserInfo[0].UserID;
                    CurrentLoggedInUserInfo.UserMailID= objUserInfo[0].EmpUserName;
                    CurrentLoggedInUserInfo.UserPassword = objUserInfo[0].EmpPassword;
                    CurrentLoggedInUserInfo.IsActive = objUserInfo[0].IsActive;
                    CurrentLoggedInUserInfo.IsDeleted = objUserInfo[0].IsDeleted;
                    CurrentLoggedInUserInfo.IsLocked = objUserInfo[0].IsLocked;
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

            EmployeeInfo objSelectedEmployeeInfo = objEmployeeInfo.GetSelectedEmployeeInfo(CurrentLoggedInUserInfo.EmpID);
            if (objSelectedEmployeeInfo != null)
            {
                CurrentLoggedInUserInfo.UserName = objSelectedEmployeeInfo.EmpName;
                CurrentLoggedInUserInfo.DesignationTitle = objDesignation.GetDesignationByID(objSelectedEmployeeInfo.EmpDesignationID);
                CurrentLoggedInUserInfo.DepartmentTitle = objDepartment.GetDepartmentTitleByID(objSelectedEmployeeInfo.DepartmentID);
            }

            CurrentLoggedInUserInfo.LoginDateTime = DateTime.Now;
            CurrentLoggedInUserInfo.LogoutDateTime = DateTime.Now;
        }

        public bool UserModuleAccessInfo(int txtUserID, int txtAppModuleID)
        {
            bool AccessAvailable = true;

            string strQuery = "";
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                strQuery = "SELECT * FROM UserModules WHERE UserID = " + txtUserID + " AND (ModuleID = 1 OR ModuleID = " + txtAppModuleID + ")";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                List<UserAppModuleInfo> objUserInfo = JsonConvert.DeserializeObject<List<UserAppModuleInfo>>(DataTableToJSon);
                if (objUserInfo.Count > 0)
                {
                    CurrentLoggedInUserInfo.ModuleID = objUserInfo[0].ModuleID;
                    CurrentLoggedInUserInfo.ModuleTitle = objAppModule.GetModuleTitleByID(objUserInfo[0].ModuleID);
                }
                else
                {
                    CurrentLoggedInUserInfo.ModuleID = 0;
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
            return AccessAvailable;
        }

        public bool UserRoleAccessInfo(int txtUserID, int txtRoleID)
        {
            bool AccessAvailable = true;

            string strQuery = "";
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                strQuery = "SELECT * FROM UserRoles WHERE UserID = " + txtUserID + " AND RoleID = " + txtRoleID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                List<UserRoles> objUserInfo = JsonConvert.DeserializeObject<List<UserRoles>>(DataTableToJSon);
                if (objUserInfo.Count > 0)
                {
                    CurrentLoggedInUserInfo.RoleID = objUserInfo[0].RoleID;
                    CurrentLoggedInUserInfo.RoleTItle = objRoles.GetRoleTitleByID(objUserInfo[0].RoleID);
                }
                else
                {
                    CurrentLoggedInUserInfo.RoleID = 0;
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

            return AccessAvailable;
        }
    }
}
