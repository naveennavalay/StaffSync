using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaffSync
{
    public class clsEmployeePersonalInfo
    {
        myDBClass objDBClass = new myDBClass();
        OleDbConnection conn = null;
        DataSet dtDataset;

        public clsEmployeePersonalInfo() { 

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

        public int InsertEmployeePersonalInfo(int txtEmployeeID, DateTime txtEmployeeDOB, DateTime txtEmployeeDOJ, int txtEmployeeQualID, int txtPermanentAddressID, int txtCurrentAddressID, string ContactNumber1, string ContactNumber2, int txtContactID1, int txtContactID2, int txtEmployeeSexID, int txtEmployeeLastCompayID)
        {
            int affectedRows = 0;
            try
            {

                int maxRowCount = getMaxRowCount("PersonalInfoMas", "PersonalInfoID");

                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO PersonalInfoMas (PersonalInfoID, EmpID, DOB, DOJ, EduQualID, PerAddressID, CurAddressID, ContactNumber1, ContactNumber2, ContactID1, ContactID2, SexID, LastCompanyInfoID) VALUES " +
                 "(" + maxRowCount + "," + txtEmployeeID + ",'" + txtEmployeeDOB + "','" + txtEmployeeDOJ + "'," + txtEmployeeQualID + "," + txtPermanentAddressID + "," + txtCurrentAddressID + ",'" + ContactNumber1 + "','" + ContactNumber2 +"'," + txtContactID1 + "," + txtContactID2 + "," + txtEmployeeSexID + "," + txtEmployeeLastCompayID + ")";

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

        public int UpdateEmployeePersonalInfo(int txtEmployeeID, DateTime txtEmployeeDOB, DateTime txtEmployeeDOJ, int txtEmployeeQualID, int txtPermanentAddressID, int txtCurrentAddressID, string ContactNumber1, string ContactNumber2, int txtContactID1, int txtContactID2, int txtEmployeeSexID, int txtEmployeeLastCompayID)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE PersonalInfoMas SET " +
                "DOB = '" + txtEmployeeDOB + "', DOJ = '" + txtEmployeeDOJ + "', EduQualID = " + txtEmployeeQualID + ", PerAddressID = " + txtPermanentAddressID + ", CurAddressID = " + txtCurrentAddressID + ", ContactNumber1 = '" + ContactNumber1 + "', ContactNumber2 = '" + ContactNumber2 + "', ContactID1 = " + txtContactID1 + ", ContactID2 = " + txtContactID2 + ", SexID = " + txtEmployeeSexID + ", LastCompanyInfoID = " + txtEmployeeLastCompayID + 
                " WHERE EmpID = " + txtEmployeeID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
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

        public EmpPersonalPersonalInfo GetEmpPersonalPersonalInfo(int EmployeeID)
        {
            EmpPersonalPersonalInfo empPersonalPersonalInfo = new EmpPersonalPersonalInfo();

            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "SELECT * FROM PersonalInfoMas WHERE PersonalInfoID = " + EmployeeID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                List<EmpPersonalPersonalInfo> objEmpPersonalPersonalInfo = JsonConvert.DeserializeObject<List<EmpPersonalPersonalInfo>>(DataTableToJSon);
                if (objEmpPersonalPersonalInfo.Count > 0)
                {
                    empPersonalPersonalInfo.PersonalInfoID = objEmpPersonalPersonalInfo[0].PersonalInfoID;
                    empPersonalPersonalInfo.EmpID = objEmpPersonalPersonalInfo[0].EmpID;
                    empPersonalPersonalInfo.DOB = objEmpPersonalPersonalInfo[0].DOB;
                    empPersonalPersonalInfo.DOJ = objEmpPersonalPersonalInfo[0].DOJ;
                    empPersonalPersonalInfo.EduQualID = objEmpPersonalPersonalInfo[0].EduQualID;
                    empPersonalPersonalInfo.PerAddressID = objEmpPersonalPersonalInfo[0].PerAddressID;
                    empPersonalPersonalInfo.CurAddressID = objEmpPersonalPersonalInfo[0].CurAddressID;
                    empPersonalPersonalInfo.ContactNumber1 = objEmpPersonalPersonalInfo[0].ContactNumber1;
                    empPersonalPersonalInfo.ContactNumber2 = objEmpPersonalPersonalInfo[0].ContactNumber2;
                    empPersonalPersonalInfo.ContactID1 = objEmpPersonalPersonalInfo[0].ContactID1;
                    empPersonalPersonalInfo.ContactID2 = objEmpPersonalPersonalInfo[0].ContactID2;
                    empPersonalPersonalInfo.SexID = objEmpPersonalPersonalInfo[0].SexID;
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

            return empPersonalPersonalInfo;
        }
    }

    public class EmpPersonalPersonalInfo
    {
        public int PersonalInfoID { get; set; }
        public int EmpID { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DOJ { get; set; }
        public int EduQualID { get; set; }
        public int PerAddressID { get; set; }
        public int CurAddressID { get; set; }
        public string ContactNumber1 { get; set; }
        public string ContactNumber2 { get; set; }
        public int ContactID1 { get; set; }
        public int ContactID2 { get; set; }
        public int SexID { get; set; }
        public int LastCompanyInfoID { get; set; }
    }
}
