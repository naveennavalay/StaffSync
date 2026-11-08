using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace dbStaffSync
{
    public class clsResignationInfo
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public IsResignationAppliedInfo getIsResignationAppliedInfotatements(int txtEmpID)
        {
            List<IsResignationAppliedInfo> objIsResignationAppliedInfoList = new List<IsResignationAppliedInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = @"SELECT
                                            EmpResignationInfo.EmpResignID,
                                            EmpResignationInfo.EmpResignStatus
                                        FROM
                                            (
                                                EmpMas
                                                INNER JOIN PersonalInfoMas ON EmpMas.EmpID = PersonalInfoMas.EmpID
                                            )
                                            INNER JOIN EmpResignationInfo ON PersonalInfoMas.PersonalInfoID = EmpResignationInfo.PersonalInfoID
                                        WHERE
                                            (
                                                ((EmpResignationInfo.EmpResignStatus) > 0)
                                                AND ((EmpMas.EmpID) = " + txtEmpID + @")
                                            );";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objIsResignationAppliedInfoList = JsonConvert.DeserializeObject<List<IsResignationAppliedInfo>>(DataTableToJSon);
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

            if(objIsResignationAppliedInfoList.Count > 0)
                return objIsResignationAppliedInfoList[0];
            else
                return new IsResignationAppliedInfo();
        }

        public List<ResignationInfo> getResignationInfotatements()
        {
            List<ResignationInfo> objResignationReasonList = new List<ResignationInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = @"SELECT
                                        ResignationMasInfo.ResigMasID,
                                        ResignationMasInfo.ResigMasCode,
                                        ResignationMasInfo.ResigMasTitle,
                                        ResignationMasInfo.IsActive,
                                        ResignationMasInfo.IsDeleted
                                    FROM
                                        ResignationMasInfo
                                    WHERE
                                        (
                                            ((ResignationMasInfo.IsActive) = True)
                                            AND ((ResignationMasInfo.IsDeleted) = False)
                                        )
                                    ORDER BY
                                        ResignationMasInfo.ResigMasID;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objResignationReasonList = JsonConvert.DeserializeObject<List<ResignationInfo>>(DataTableToJSon);
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

            return objResignationReasonList;
        }

        public int InsertEmployeeResignationInfo(int PersonalInfoID, int ResigMasID, string EmpResignMoreInfo, DateTime EmpResignDate, 
            DateTime EmpResignApprovalDate1, int EmpResignApprovedBy1, bool EmpResignApprovalStatus1, 
            DateTime EmpResignApprovalDate2, int EmpResignApprovedBy2, bool EmpResignApprovalStatus2, 
            int EmpResignFinalStatus, DateTime EmpFinalResignApprovalDate, 
            int EmpNoticePeriod, DateTime EmpLastWorkingDay, string EmpNoticePeriodServeStatus)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("EmpResignationInfo", "EmpResignID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO EmpResignationInfo (EmpResignID, EmpResignCode, PersonalInfoID, ResigMasID, EmpResignMoreInfo, EmpResignDate, " + 
                    " EmpResignApprovalDate1, EmpResignApprovedBy1, EmpResignApprovalStatus1, " + 
                    " EmpResignApprovalDate2, EmpResignApprovedBy2, EmpResignApprovalStatus2, " + 
                    " EmpResignStatus, EmpFinalResignApprovalDate, EmpNoticePeriod, EmpLastWorkingDay, EmpNoticePeriodServeStatus) VALUES " +
                    "(" + maxRowCount.Data + ", '" + "RSGN-" + (maxRowCount.Data).ToString().PadLeft(4, '0').Trim() + "', " + PersonalInfoID + ", " + ResigMasID + ", '" + EmpResignMoreInfo + "', #" + EmpResignDate.ToString("dd-MMM-yyyy") + "#, " + 
                    "#" + EmpResignApprovalDate1.ToString("dd-MMM-yyyy") + "#, " + EmpResignApprovedBy1 + ", " + EmpResignApprovalStatus1 + ", " + 
                    "#" + EmpResignApprovalDate2.ToString("dd-MMM-yyyy") + "#, " + EmpResignApprovedBy2 + ", " + EmpResignApprovalStatus2 + ", " + 
                    EmpResignFinalStatus + ", #" + EmpFinalResignApprovalDate.ToString("dd-MMM-yyyy") + "#, " + EmpNoticePeriod + ", #" + EmpLastWorkingDay.ToString("dd-MMM-yyyy") + "#, '" + EmpNoticePeriodServeStatus + "')";
                    
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
    }
}
