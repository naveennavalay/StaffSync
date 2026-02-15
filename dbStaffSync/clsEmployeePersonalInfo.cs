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
    public class clsEmployeePersonalInfo
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public clsEmployeePersonalInfo() { 

        }

        public int InsertEmployeePersonalInfo(int txtEmployeeID, DateTime txtEmployeeDOB, DateTime txtEmployeeDOJ, int txtEmployeeQualID, int txtPermanentAddressID, int txtCurrentAddressID, string ContactNumber1, string ContactNumber2, int txtContactID1, int txtContactID2, int txtEmployeeSexID, int txtEmployeeLastCompayID, int txtClientBranchID)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("PersonalInfoMas", "PersonalInfoID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO PersonalInfoMas (PersonalInfoID, EmpID, DOB, DOJ, EduQualID, PerAddressID, CurAddressID, ContactNumber1, ContactNumber2, ContactID1, ContactID2, SexID, LastCompanyInfoID, ClientBranchID) VALUES " +
                 "(" + maxRowCount.Data + "," + txtEmployeeID + ",'" + txtEmployeeDOB.ToString("dd-MMM-yyyy") + "','" + txtEmployeeDOJ.ToString("dd-MMM-yyyy") + "'," + txtEmployeeQualID + "," + txtPermanentAddressID + "," + txtCurrentAddressID + ",'" + ContactNumber1 + "','" + ContactNumber2 +"'," + txtContactID1 + "," + txtContactID2 + "," + txtEmployeeSexID + "," + txtEmployeeLastCompayID + "," + txtClientBranchID + ")";

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

        public int UpdateEmployeePersonalInfo(int txtEmployeeID, DateTime txtEmployeeDOB, DateTime txtEmployeeDOJ, int txtEmployeeQualID, int txtPermanentAddressID, int txtCurrentAddressID, string ContactNumber1, string ContactNumber2, int txtContactID1, int txtContactID2, int txtEmployeeSexID, int txtEmployeeLastCompayID, int txtClientBranchID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE PersonalInfoMas SET " +
                "DOB = '" + txtEmployeeDOB.ToString("dd-MMM-yyyy") + "', DOJ = '" + txtEmployeeDOJ.ToString("dd-MMM-yyyy") + "', EduQualID = " + txtEmployeeQualID + ", PerAddressID = " + txtPermanentAddressID + ", CurAddressID = " + txtCurrentAddressID + ", ContactNumber1 = '" + ContactNumber1 + "', ContactNumber2 = '" + ContactNumber2 + "', ContactID1 = " + txtContactID1 + ", ContactID2 = " + txtContactID2 + ", SexID = " + txtEmployeeSexID + ", LastCompanyInfoID = " + txtEmployeeLastCompayID + ", ClientBranchID = " + txtClientBranchID +
                " WHERE EmpID = " + txtEmployeeID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
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

        public EmpPersonalPersonalInfo GetEmpPersonalPersonalInfo(int EmployeeID)
        {
            EmpPersonalPersonalInfo empPersonalPersonalInfo = new EmpPersonalPersonalInfo();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

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
                    empPersonalPersonalInfo.ClientBranchID = objEmpPersonalPersonalInfo[0].ClientBranchID;
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

            return empPersonalPersonalInfo;
        }
    }
}
