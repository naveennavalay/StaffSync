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
    public class clsEmployeeFamilyMemberInfo
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public clsEmployeeFamilyMemberInfo() { 

        }

        public List<EmpPersonalFamilyMemberInfo> GetEmpPersonalFamilyMemberInfo(int txtPersonalIDInfoID)
        {
            List<EmpPersonalFamilyMemberInfo> lstEmpPersonalFamilyMemberInfo = new List<EmpPersonalFamilyMemberInfo>();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT * FROM EmpFamilyInfo WHERE PersonalInfoID = " + txtPersonalIDInfoID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                List<EmpPersonalFamilyMemberInfo> objEmpPersonalFamilyMemberInfo = JsonConvert.DeserializeObject<List<EmpPersonalFamilyMemberInfo>>(DataTableToJSon);
                if (objEmpPersonalFamilyMemberInfo.Count > 0)
                {
                    lstEmpPersonalFamilyMemberInfo = objEmpPersonalFamilyMemberInfo;
                    int minRecords = Math.Abs(objEmpPersonalFamilyMemberInfo.Count - 10);
                    for (int iDefaultRecord = 0; iDefaultRecord < minRecords; iDefaultRecord++)
                    {
                        lstEmpPersonalFamilyMemberInfo.Add(new EmpPersonalFamilyMemberInfo
                        {
                            EmpPerFamInfoID = 0,
                            PersonalInfoID = txtPersonalIDInfoID,
                            FamMemName = "",
                            FamMemDOB = null,
                            FamMemAge = null,
                            FamMemRelationship = "",
                            FamMemAddr1 = "",
                            FamMemAddr2 = "",
                            FamMemArea = "",
                            FamMemCity = "",
                            FamMemState = "",
                            FamMemPIN = "",
                            FamMemCountry = "",
                            FamMemContactNumber = "",
                            FamMemMailID = "",
                            FamMemBloodGroup = "",
                            FamMemInsuranceEnrolled = false
                        });
                    }
                }
                else
                {
                    for (int iDefaultRecord = 0; iDefaultRecord < 10; iDefaultRecord++)
                    {
                        lstEmpPersonalFamilyMemberInfo.Add(new EmpPersonalFamilyMemberInfo
                        {
                            EmpPerFamInfoID = 0,
                            PersonalInfoID = txtPersonalIDInfoID,
                            FamMemName = "",
                            FamMemDOB = null,
                            FamMemAge = null,
                            FamMemRelationship = "",
                            FamMemAddr1 = "",
                            FamMemAddr2 = "",
                            FamMemArea = "",
                            FamMemCity = "",
                            FamMemState = "",
                            FamMemPIN = "",
                            FamMemCountry = "",
                            FamMemContactNumber = "",
                            FamMemMailID = "",
                            FamMemBloodGroup = "",
                            FamMemInsuranceEnrolled = false
                        });
                    }
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

            return lstEmpPersonalFamilyMemberInfo;
        }

        public int InsertEmployeePersonalFamilyMemberInfo(int txtEmpPerFamInfoID, int txtPersonalInfoID, string txtFamMemName, DateTime txtFamMemDOB, int txtFamMemAge, string txtFamMemRelationship, string txtFamMemAddr1, string txtFamMemAddr2, string txtFamMemArea, string txtFamMemCity, string txtFamMemState, string txtFamMemPIN, string txtFamMemCountry, string txtFamMemContactNumber, string txtFamMemMailID, string txtFamMemBloodGroup, bool txtFamMemInsuranceEnrolled)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("EmpFamilyInfo", "EmpPerFamInfoID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO EmpFamilyInfo (EmpPerFamInfoID, PersonalInfoID, FamMemName, FamMemDOB, FamMemAge, FamMemRelationship, FamMemAddr1, FamMemAddr2, FamMemArea, FamMemCity, FamMemState, FamMemPIN, FamMemCountry, FamMemContactNumber, FamMemMailID, FamMemBloodGroup, FamMemInsuranceEnrolled) VALUES " +
                 "(" + maxRowCount.Data + "," + txtPersonalInfoID.ToString().Trim() + ",'" + txtFamMemName.ToString().Trim() + "','" + txtFamMemDOB.ToString("dd-MMM-yyyy") + "'," + txtFamMemAge.ToString().Trim() + ",'" + 
                 txtFamMemRelationship.ToString().Trim() + "','" + txtFamMemAddr1.ToString().Trim() + "','" + txtFamMemAddr2.ToString().Trim() + "','" + txtFamMemArea.ToString().Trim() + "','" + txtFamMemCity.ToString().Trim() + "','" + 
                 txtFamMemState.ToString().Trim() + "','" + txtFamMemPIN.ToString().Trim() + "','" + txtFamMemCountry.ToString().Trim() + "','" + txtFamMemContactNumber.ToString().Trim() + "','" + txtFamMemMailID.ToString().Trim() + "','" + txtFamMemBloodGroup.ToString().Trim() + "'," + txtFamMemInsuranceEnrolled + ")";

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

        public int UpdateEmployeePersonalFamilyMemberInfo(int txtEmpPerFamInfoID, int txtPersonalInfoID, string txtFamMemName, DateTime txtFamMemDOB, int txtFamMemAge, string txtFamMemRelationship, string txtFamMemAddr1, string txtFamMemAddr2, string txtFamMemArea, string txtFamMemCity, string txtFamMemState, string txtFamMemPIN, string txtFamMemCountry, string txtFamMemContactNumber, string txtFamMemMailID, string txtFamMemBloodGroup, bool txtFamMemInsuranceEnrolled)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpFamilyInfo SET " +
                "PersonalInfoID = " + txtPersonalInfoID + ", FamMemName = '" + txtFamMemName.ToString().Trim() + "', FamMemDOB = '" + txtFamMemDOB.ToString("dd-MMM-yyyy") + "', FamMemAge = " + txtFamMemAge.ToString().Trim() + ", FamMemRelationship = '" + txtFamMemRelationship.ToString().Trim() + "', " +
                "FamMemAddr1 = '" + txtFamMemAddr1.ToString().Trim() + "', FamMemAddr2 = '" + txtFamMemAddr2.ToString().Trim() + "', FamMemArea = '" + txtFamMemArea.ToString().Trim() + "', FamMemCity = '" + txtFamMemCity.ToString().Trim() + "', FamMemState = '" + txtFamMemState.ToString().Trim() + "', FamMemPIN = '" + txtFamMemPIN.ToString().Trim() + "', FamMemCountry = '" + txtFamMemCountry.ToString().Trim() + "', FamMemInsuranceEnrolled = " + txtFamMemInsuranceEnrolled +
                " WHERE EmpPerFamInfoID = " + txtEmpPerFamInfoID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtPersonalInfoID;
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
    }
}
