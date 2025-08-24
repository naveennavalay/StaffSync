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
    public class clsUserManagement
    {
        myDBClass objDBClass = new myDBClass();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsStates objState = new clsStates();
        clsCountries objCountry = new clsCountries();

        public clsUserManagement() { 

        }

        public UserManagementList GetUserManagementInfo(int txtEmpID)
        {
            UserManagementList userManagementList = new UserManagementList();
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "SELECT * FROM qryUsersList WHERE IsActive = true AND IsDeleted = false";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                userManagementList = JsonConvert.DeserializeObject<UserManagementList>(DataTableToJSon);

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

            return userManagementList;
        }

        public List<UserManagementList> GetUserManagementList()
        {
            List<UserManagementList> userManagementList = new List<UserManagementList>();
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "SELECT * FROM qryUsersList WHERE IsActive = true AND IsDeleted = false AND ClientID = " + CurrentUser.ClientID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                userManagementList = JsonConvert.DeserializeObject<List<UserManagementList>>(DataTableToJSon);
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

            return userManagementList;
        }

        public int UpdateUserActiveStatus(int txtEmpID, bool IsActive)
        {
            int affectedRows = 0;

            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "Update Users SET IsActive = " + IsActive +
                " WHERE EmpID = " + txtEmpID;

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

        public int UpdateUserLockStatus(int txtEmpID, bool IsLocked)
        {
            int affectedRows = 0;

            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "Update Users SET IsLocked = " + IsLocked +
                " WHERE EmpID = " + txtEmpID;

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


        public List<UserRolesAndResponsibilities> GetRolesAndResponsibilitiesList(int txtEmpID)
        {
            List<UserRolesAndResponsibilities> userRolesAndResponsibilities = new List<UserRolesAndResponsibilities>();
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "SELECT * FROM qryRolesAndResp WHERE EmpID = " + txtEmpID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                userRolesAndResponsibilities = JsonConvert.DeserializeObject<List<UserRolesAndResponsibilities>>(DataTableToJSon);
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

            return userRolesAndResponsibilities;
        }
    }

    public class UserManagementList
    {
        public int EmpID { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string DesignationTitle { get; set; }
        public string DepartmentTitle { get; set; }
        public int UserID { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsLocked { get; set; }
    }

    public class UserRolesAndResponsibilities
    {
        public int EmpID { get; set; }
        public int ModuleID { get; set; }
        public string ModuleCode { get; set; }
        public string ModuleTitle { get; set; }
        public int RoleID { get; set; }
        public string RoleTitle { get; set; }
        public string RoleDescription { get; set; }
        public int RoleDefID { get; set; }
        public bool CanAdd {  get; set; }
        public bool CanUpdate { get; set; }
        public bool CanSave { get; set; }
        public bool CanRemove { get; set; }
        public bool CanView { get; set; }
        public bool CanExport { get; set; }
    }

}
