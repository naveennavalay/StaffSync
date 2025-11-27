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
    public class clsEmployeePersonalIDInfo
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public clsEmployeePersonalIDInfo() { 

        }

        public EmpPersonalIDInfo GetEmpPersonalIDInfo(int txtPersonalIDInfoID)
        {
            EmpPersonalIDInfo EmpPersonalIDInfo = new EmpPersonalIDInfo();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT * FROM EmpGovtIDInfo WHERE PersonalInfoID = " + txtPersonalIDInfoID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                List<EmpPersonalIDInfo> objEmpPersonalIDInfo = JsonConvert.DeserializeObject<List<EmpPersonalIDInfo>>(DataTableToJSon);
                if (objEmpPersonalIDInfo.Count > 0)
                {
                    EmpPersonalIDInfo.EmpGovtID = objEmpPersonalIDInfo[0].EmpGovtID;
                    EmpPersonalIDInfo.PersonalInfoID = objEmpPersonalIDInfo[0].PersonalInfoID;
                    EmpPersonalIDInfo.AadhaarCardNumber = objEmpPersonalIDInfo[0].AadhaarCardNumber;
                    EmpPersonalIDInfo.VoterCardNumber = objEmpPersonalIDInfo[0].VoterCardNumber;
                    EmpPersonalIDInfo.PANNumber = objEmpPersonalIDInfo[0].PANNumber;
                    EmpPersonalIDInfo.PassportNumber = objEmpPersonalIDInfo[0].PassportNumber;
                    EmpPersonalIDInfo.IssueDate = objEmpPersonalIDInfo[0].IssueDate;
                    EmpPersonalIDInfo.RenewalDate = objEmpPersonalIDInfo[0].RenewalDate;
                    EmpPersonalIDInfo.ID1 = objEmpPersonalIDInfo[0].ID1;
                    EmpPersonalIDInfo.ID2 = objEmpPersonalIDInfo[0].ID2;
                    EmpPersonalIDInfo.ID3 = objEmpPersonalIDInfo[0].ID3;
                    EmpPersonalIDInfo.ID4 = objEmpPersonalIDInfo[0].ID4;
                    EmpPersonalIDInfo.ID5 = objEmpPersonalIDInfo[0].ID5;
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

            return EmpPersonalIDInfo;
        }

        public int InsertEmployeePersonalIDInfo(int txtPersonalInfoID, string txtAadhaarCardNumber, string txtVoterCardNumber, string txtPANNumber, string txtPassportNumber, DateTime txtPassportIssueDate, DateTime txtPassportRenewalDate, string txtID1, string txtID2, string txtID3, string txtID4, string txtID5)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("EmpGovtIDInfo", "EmpGovtID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO EmpGovtIDInfo (EmpGovtID, PersonalInfoID, AadhaarCardNumber, VoterCardNumber, PANNumber, PassportNumber, IssueDate, RenewalDate, ID1, ID2, ID3, ID4, ID5) VALUES " +
                 "(" + maxRowCount.Data + "," + txtPersonalInfoID + ",'" + txtAadhaarCardNumber + "','" + txtVoterCardNumber + "','" + txtPANNumber + "','" + txtPassportNumber + "','" + txtPassportIssueDate.ToString("dd-MMM-yyyy") + "','" + txtPassportRenewalDate.ToString("dd-MMM-yyyy") + "','" + txtID1 + "','" + txtID2 +"','" + txtID3 + "','" + txtID4 + "','" + txtID5 + "')";

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

        public int UpdateEmployeePersonalIDInfo(int txtPersonalIDInfoID, int txtPersonalInfoID, string txtAadhaarCardNumber, string txtVoterCardNumber, string txtPANNumber, string txtPassportNumber, DateTime txtPassportIssueDate, DateTime txtPassportRenewalDate, string txtID1, string txtID2, string txtID3, string txtID4, string txtID5)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpGovtIDInfo SET " +
                "PersonalInfoID = " + txtPersonalIDInfoID + ", AadhaarCardNumber = '" + txtAadhaarCardNumber + "', VoterCardNumber = '" + txtVoterCardNumber + "', PANNumber = '" + txtPANNumber + "', PassportNumber = '" + txtPassportNumber + "', IssueDate = '" + txtPassportIssueDate.ToString("dd-MMM-yyyy") + "', RenewalDate = '" + txtPassportRenewalDate.ToString("dd-MMM-yyyy") + "', ID1 = '" + txtID1 + "', ID2 = '" + txtID2 + "', ID3 = '" + txtID3 + "', ID4 = '" + txtID4 + "', ID5 = '" + txtID5 + "'" + 
                " WHERE EmpGovtID = " + txtPersonalIDInfoID;

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
