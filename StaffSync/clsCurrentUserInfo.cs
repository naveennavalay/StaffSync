using ModelStaffSync;
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
//using static C1.Util.Win.Win32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace StaffSync
{
    public class clsCurrentUserInfo
    {
        myDBClass objDBClass = new myDBClass();
        OleDbConnection conn = null;
        DALStaffSync.clsEmployeeMaster objEmployeeInfo = new DALStaffSync.clsEmployeeMaster();
        DataSet dtDataset;
        DALStaffSync.clsAppModule objAppModule = new DALStaffSync.clsAppModule();
        DALStaffSync.clsRolesAndResponsibilities objRoles = new DALStaffSync.clsRolesAndResponsibilities();
        DALStaffSync.clsDesignation objDesignation = new DALStaffSync.clsDesignation();
        DALStaffSync.clsDepartment objDepartment = new DALStaffSync.clsDepartment();

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
                    clsCurrentUser.EmpID = objUserInfo[0].EmpID;
                    clsCurrentUser.UserID = objUserInfo[0].UserID;
                    clsCurrentUser.UserMailID= objUserInfo[0].EmpUserName;
                    clsCurrentUser.UserPassword = objUserInfo[0].EmpPassword;
                    clsCurrentUser.IsActive = objUserInfo[0].IsActive;
                    clsCurrentUser.IsDeleted = objUserInfo[0].IsDeleted;
                    clsCurrentUser.IsLocked = objUserInfo[0].IsLocked;
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

            EmployeeInfo objSelectedEmployeeInfo = objEmployeeInfo.GetSelectedEmployeeInfo(clsCurrentUser.EmpID);
            if (objSelectedEmployeeInfo != null)
            {
                clsCurrentUser.UserName = objSelectedEmployeeInfo.EmpName;
                clsCurrentUser.DesignationTitle = objDesignation.GetDesignationByID(objSelectedEmployeeInfo.EmpDesignationID);
                clsCurrentUser.DepartmentTitle = objDepartment.GetDepartmentTitleByID(objSelectedEmployeeInfo.DepartmentID);
            }

            clsCurrentUser.LoginDateTime = DateTime.Now;
            clsCurrentUser.LogoutDateTime = DateTime.Now;
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
                    clsCurrentUser.ModuleID = objUserInfo[0].ModuleID;
                    clsCurrentUser.ModuleTitle = objAppModule.GetModuleTitleByID(objUserInfo[0].ModuleID);
                }
                else
                {
                    clsCurrentUser.ModuleID = 0;
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
                    clsCurrentUser.RoleID = objUserInfo[0].RoleID;
                    clsCurrentUser.RoleTItle = objRoles.GetRoleTitleByID(objUserInfo[0].RoleID);
                }
                else
                {
                    clsCurrentUser.RoleID = 0;
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
