using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace dbStaffSync
{
    public class clsEmpReimbursRequestInfo
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset = null;
        clsGenFunc objGenFunc = new clsGenFunc();

        public clsEmpReimbursRequestInfo() { 

        }

        public List<ReimbursementInfoPendingList> FirstApprovalPendingReimbursmentApprovalList(int txtClientID)
        {
            List<ReimbursementInfoPendingList> objReimbursementInfoPendingListInfo = new List<ReimbursementInfoPendingList>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "";

                strQuery = @"SELECT
                                EmpMas.EmpID,
                                EmpMas.EmpCode,
                                EmpMas.EmpName,
                                DesigMas.DesignationTitle,
                                DepMas.DepartmentTitle,
                                ReimbursRequestInfo.ReimbursRequestID,
                                ReimbursRequestInfo.ReimbursRequestCode,
                                ReimbursementHeaderMas.ReimbTitle,
                                ReimbursRequestInfo.ReimbursRequestDate,
                                ReimbursRequestInfo.ReimbursRequestAmount,
                                ReimbursRequestInfo.IsActive,
                                ReimbursRequestInfo.IsDeleted,
                                ReimbursRequestInfo.ReimbursAproveStatus1,
                                ReimbursRequestInfo.ReimbursAproveStatus2,
                                ClientMas.ClientID
                            FROM
                                ReimbursementHeaderMas
                                INNER JOIN (
                                    (
                                        (
                                            DesigMas
                                            INNER JOIN (
                                                DepMas
                                                INNER JOIN (
                                                    ClientMas
                                                    INNER JOIN EmpMas ON ClientMas.ClientID = EmpMas.ClientID
                                                ) ON DepMas.DepartmentID = EmpMas.DepartmentID
                                            ) ON DesigMas.DesignationID = EmpMas.EmpDesignationID
                                        )
                                        INNER JOIN PersonalInfoMas ON EmpMas.EmpID = PersonalInfoMas.EmpID
                                    )
                                    INNER JOIN ReimbursRequestInfo ON PersonalInfoMas.PersonalInfoID = ReimbursRequestInfo.PersonalInfoID
                                ) ON ReimbursementHeaderMas.ReimbID = ReimbursRequestInfo.ReimbID
                            WHERE
                                (
                                    ((EmpMas.EmpID) > 1)
                                    AND ((ReimbursRequestInfo.IsActive) = True)
                                    AND ((ReimbursRequestInfo.IsDeleted) = False)
                                    AND (
                                        (ReimbursRequestInfo.ReimbursAproveStatus1) = False
                                    )
                                    AND (
                                        (ReimbursRequestInfo.ReimbursAproveStatus2) = False
                                    )
                                    AND ((ClientMas.ClientID) = " + txtClientID + @")
                                )
                            ORDER BY
                                EmpMas.EmpID,
                                ReimbursRequestInfo.ReimbursRequestID,
                                ReimbursRequestInfo.ReimbursRequestDate;";
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objReimbursementInfoPendingListInfo = JsonConvert.DeserializeObject<List<ReimbursementInfoPendingList>>(DataTableToJSon);
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

            return objReimbursementInfoPendingListInfo;
        }

        public List<ReimbursementInfoPendingList> SecondApprovalPendingReimbursmentApprovalList(int txtClientID)
        {
            List<ReimbursementInfoPendingList> objReimbursementInfoPendingListInfo = new List<ReimbursementInfoPendingList>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "";

                strQuery = @"SELECT
                                E.EmpID,
                                E.EmpCode,
                                E.EmpName,
                                D.DesignationTitle,
                                P.DepartmentTitle,
                                C.AssetCatMasID,
                                C.AssetName AS AssetCategoryName,
                                A.AssetID,
                                A.AssetCode,
                                A.AssetName,
                                R.AssetRequestID,
                                R.AssetRequestCode,
                                R.AssetRequestDate,
                                R.AssetRequestComments,
                                R.AssetRequestByStatus,
                                IIf(R.AssetRequestByStatus = False, ""Pending"", """") AS RequestStatus,
                                R.RequestedTo,
                                R.RequestedDate,
                                R.RequestedToStatus,
                                R.RequestedToComments,
                                R.AssetRequestStatus,
                                CL.ClientID
                            FROM
                                DesigMas AS D
                                INNER JOIN (
                                    DepMas AS P
                                    INNER JOIN (
                                        ClientMas AS CL
                                        INNER JOIN (
                                            (
                                                AssetCategoryMas AS C
                                                INNER JOIN AssetMas AS A ON C.AssetCatMasID = A.AssetCatMasID
                                            )
                                            INNER JOIN (
                                                EmpAssetRequest AS R
                                                INNER JOIN EmpMas AS E ON R.AssetRequestByID = E.EmpID
                                            ) ON A.AssetID = R.AssetID
                                        ) ON CL.ClientID = E.ClientID
                                    ) ON P.DepartmentID = E.DepartmentID
                                ) ON D.DesignationID = E.EmpDesignationID
                            WHERE
                                (
                                    ((E.EmpID) > 1)
                                    AND ((R.AssetRequestByStatus) = False)
                                    AND ((R.RequestedToStatus) = False)
                                    AND ((R.AssetRequestStatus) = False)
                                    AND ((R.IsActive) = True)
                                    AND ((R.IsDeleted) = False)
                                    AND ((CL.ClientID) = " + txtClientID + @")
                                    AND ((C.ClientID) = [CL].[ClientID])
                                )
                            ORDER BY
                                R.AssetRequestID,
                                R.AssetRequestDate;";
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objReimbursementInfoPendingListInfo = JsonConvert.DeserializeObject<List<ReimbursementInfoPendingList>>(DataTableToJSon);
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

            return objReimbursementInfoPendingListInfo;
        }

        public int InsertEmployeeReimbursmentRequestInfo(int PersonalInfoID, bool IsActive, bool IsDeleted, int ReimbID, DateTime? ReimbursRequestDate, decimal ReimbursRequestAmount, 
            bool ReimbursAproveStatus1, DateTime? ReimbursAproveDate1, int ReimbursAproveBy1, string ReimbursAproveComments1, bool ReimbursAproveStatus2, DateTime? ReimbursApproveDate2, int ReimbursAproveBy2, string ReimbursAproveComments2, bool ReimbursRequestFinalStatus, bool IncludeInNextPayslipProcess)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("ReimbursRequestInfo", "ReimbursRequestID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO ReimbursRequestInfo (ReimbursRequestID, ReimbursRequestCode, PersonalInfoID, IsActive, IsDeleted, ReimbID, ReimbursRequestDate, ReimbursRequestAmount, ReimbursAproveStatus1, ReimbursAproveDate1, ReimbursAproveBy1, ReimbursAproveComments1, ReimbursAproveStatus2, ReimbursApproveDate2, ReimbursAproveBy2, ReimbursAproveComments2, ReimbursRequestFinalStatus, IncludeInNextPayslipProcess) VALUES " +
                 "(" + maxRowCount.Data + ",'" + "RMB-REQ-" + (maxRowCount.Data).ToString().PadLeft(4, '0').Trim() + "'," + PersonalInfoID + "," + IsActive + "," + IsDeleted + "," + ReimbID + ",#" + Convert.ToDateTime(ReimbursRequestDate).ToString("dd-MMM-yyyy") + "#," + ReimbursRequestAmount + "," + ReimbursAproveStatus1 + ",#" + Convert.ToDateTime(ReimbursAproveDate1).ToString("dd-MMM-yyyy") + "#," + ReimbursAproveBy1 + ",'" + ReimbursAproveComments1 + "'," + ReimbursAproveStatus2 + ",#" + Convert.ToDateTime(ReimbursApproveDate2).ToString("dd-MMM-yyyy") + "#," + ReimbursAproveBy2 + ",'" + ReimbursAproveComments2 + "'," + ReimbursRequestFinalStatus + "," + IncludeInNextPayslipProcess + ")";

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
