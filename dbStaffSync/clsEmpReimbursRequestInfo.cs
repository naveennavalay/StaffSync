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

        public List<ReimbursementPaymentsPendingList> getSelectedReimbursmentPaymentPendingList(int txtRequestID)
        {
            List<ReimbursementPaymentsPendingList> objReimbursementPaymentsPendingList = new List<ReimbursementPaymentsPendingList>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "";

                strQuery = @"SELECT
                                EmpMas.EmpCode,
                                EmpMas.EmpName,
                                DesigMas.DesignationTitle,
                                DepMas.DepartmentTitle,
                                ReimbursementHeaderMas.ReimbID,
                                ReimbursementHeaderMas.ReimbTitle,
                                ReimbRequestMas.ReimbRequestDescription,
                                ReimbRequestMas.ReimbRequestID,
                                ReimbRequestMas.ReimbRequestCode,
                                ReimbRequestMas.ReimbRequestDate,
                                ReimbRequestMas.ReimbRequestStatus,
                                LatestDetail.ReimbRequestDetailID,
                                LatestDetail.ReimbRequestTRDate,
                                LatestDetail.CBalance,
                                LatestDetail.Comments,
                                EmpMas.IsActive AS EmpIsActive,
                                EmpMas.IsDeleted AS EmpIsDeleted,
                                ReimbRequestMas.IsActive AS RequestIsActive,
                                ReimbRequestMas.IsDeleted AS RequestIsDeleted,
                                EmpMas.EmpID,
                                ClientMas.ClientID,
                                ReimbursRequestInfo.IncludeInNextPayslipProcess
                            FROM
                                (
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
                                )
                                INNER JOIN (
                                    ReimbRequestMas
                                    INNER JOIN ReimbRequestDetailInfo AS LatestDetail ON ReimbRequestMas.ReimbRequestID = LatestDetail.ReimbRequestID
                                ) ON ReimbursRequestInfo.ReimbursRequestID = ReimbRequestMas.ReimbursRequestID
                            WHERE
                                (
                                    ((ReimbRequestMas.ReimbRequestID) = " + txtRequestID + @")
                                    AND ((ReimbRequestMas.ReimbRequestStatus) = ""In Progress"")
                                    AND ((LatestDetail.ReimbRequestDetailID) = (SELECT MAX(D.ReimbRequestDetailID) FROM ReimbRequestDetailInfo AS D WHERE
                                                D.ReimbRequestID = ReimbRequestMas.ReimbRequestID))
                                    AND ((EmpMas.IsActive) = True)
                                    AND ((EmpMas.IsDeleted) = False)
                                    AND ((ReimbRequestMas.IsActive) = True)
                                    AND ((ReimbRequestMas.IsDeleted) = False)
                                    AND ((EmpMas.EmpID) > 1)
                                    AND ((ReimbursRequestInfo.IncludeInNextPayslipProcess) = False))
                            ORDER BY
                                ReimbursementHeaderMas.ReimbID,
                                ReimbRequestMas.ReimbRequestID,
                                ReimbRequestMas.ReimbRequestDate,
                                LatestDetail.ReimbRequestDetailID,
                                LatestDetail.ReimbRequestTRDate;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objReimbursementPaymentsPendingList = JsonConvert.DeserializeObject<List<ReimbursementPaymentsPendingList>>(DataTableToJSon);
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

            return objReimbursementPaymentsPendingList;
        }

        public List<ReimbursementPaymentsPendingList> GetReimbursmentPaymentPendingList(int txtClientID)
        {
            List<ReimbursementPaymentsPendingList> objReimbursementPaymentsPendingList = new List<ReimbursementPaymentsPendingList>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "";

                strQuery = @"SELECT
                                EmpMas.EmpCode,
                                EmpMas.EmpName,
                                DesigMas.DesignationTitle,
                                DepMas.DepartmentTitle,
                                ReimbursementHeaderMas.ReimbID,
                                ReimbursementHeaderMas.ReimbTitle,
                                ReimbRequestMas.ReimbRequestDescription,
                                ReimbRequestMas.ReimbRequestID,
                                ReimbRequestMas.ReimbRequestCode,
                                ReimbRequestMas.ReimbRequestDate,
                                ReimbRequestMas.ReimbRequestStatus,
                                LatestDetail.ReimbRequestDetailID,
                                LatestDetail.ReimbRequestTRDate,
                                LatestDetail.CBalance,
                                LatestDetail.Comments,
                                EmpMas.IsActive AS EmpIsActive,
                                EmpMas.IsDeleted AS EmpIsDeleted,
                                ReimbRequestMas.IsActive AS RequestIsActive,
                                ReimbRequestMas.IsDeleted AS RequestIsDeleted,
                                EmpMas.EmpID,
                                ClientMas.ClientID,
                                ReimbursRequestInfo.IncludeInNextPayslipProcess
                            FROM
                                (
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
                                )
                                INNER JOIN (
                                    ReimbRequestMas
                                    INNER JOIN ReimbRequestDetailInfo AS LatestDetail ON ReimbRequestMas.ReimbRequestID = LatestDetail.ReimbRequestID
                                ) ON ReimbursRequestInfo.ReimbursRequestID = ReimbRequestMas.ReimbursRequestID
                            WHERE
                                (
                                    (
                                        (ReimbRequestMas.ReimbRequestStatus) = ""In Progress""
                                    )
                                    AND (
                                        (LatestDetail.ReimbRequestDetailID) = (
                                            SELECT
                                                MAX(D.ReimbRequestDetailID)
                                            FROM
                                                ReimbRequestDetailInfo AS D
                                            WHERE
                                                D.ReimbRequestID = ReimbRequestMas.ReimbRequestID
                                        )
                                    )
                                    AND ((EmpMas.IsActive) = True)
                                    AND ((EmpMas.IsDeleted) = False)
                                    AND ((ReimbRequestMas.IsActive) = True)
                                    AND ((ReimbRequestMas.IsDeleted) = False)
                                    AND ((EmpMas.EmpID) > 1)
                                    AND ((ClientMas.ClientID) = " + txtClientID + @")
                                    AND ((ReimbursRequestInfo.IncludeInNextPayslipProcess) = False))
                            ORDER BY
                                ReimbursementHeaderMas.ReimbID,
                                ReimbRequestMas.ReimbRequestID,
                                ReimbRequestMas.ReimbRequestDate,
                                LatestDetail.ReimbRequestDetailID,
                                LatestDetail.ReimbRequestTRDate;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objReimbursementPaymentsPendingList = JsonConvert.DeserializeObject<List<ReimbursementPaymentsPendingList>>(DataTableToJSon);
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

            return objReimbursementPaymentsPendingList;
        }

        public List<ReimbursementInfoPendingList> FirstApprovalPendingReimbursmentApprovalList(int txtClientID, int txtApproverID)
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
                                ReimbursementHeaderMas.ReimbID,
                                ReimbursementHeaderMas.ReimbTitle,
                                ReimbursRequestInfo.ReimbursAproveBy1,
                                ReimbursRequestInfo.ReimbursRequestDate,
                                ReimbursRequestInfo.ReimbursRequestAmount,
                                ReimbursRequestInfo.ReimbursAproveComments1,
                                ReimbursRequestInfo.ReimbursAproveStatus1,
                                ReimbursRequestInfo.ReimbursRequestFinalStatus,
                                ReimbursRequestInfo.ReimbursAproveStatus2,
                                ReimbursRequestInfo.ReimbursAproveBy2,
                                IIf(ReimbursRequestInfo.ReimbursAproveStatus1 = False, ""Pending"", """") AS RequestStatus,
                                ReimbursRequestInfo.IsActive,
                                ReimbursRequestInfo.IsDeleted,
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
                                    AND ((ReimbursRequestInfo.ReimbursAproveBy1) = " + txtApproverID + @")
                                    AND ((ReimbursRequestInfo.ReimbursAproveStatus1) = False)
                                    AND ((ReimbursRequestInfo.ReimbursRequestFinalStatus) = False)
                                    AND ((ReimbursRequestInfo.ReimbursAproveStatus2) = False)
                                    AND ((ReimbursRequestInfo.IsActive) = True)
                                    AND ((ReimbursRequestInfo.IsDeleted) = False)
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

        public List<ReimbursementInfoPendingList> SecondApprovalPendingReimbursmentApprovalList(int txtClientID, int txtApproverID)
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
                                ReimbursementHeaderMas.ReimbID,
                                ReimbursementHeaderMas.ReimbTitle,
                                ReimbursRequestInfo.ReimbursAproveBy2,
                                ReimbursRequestInfo.ReimbursRequestDate,
                                ReimbursRequestInfo.ReimbursRequestAmount,
                                ReimbursRequestInfo.ReimbursAproveComments1,
                                ReimbursRequestInfo.ReimbursAproveStatus1,
                                ReimbursRequestInfo.ReimbursRequestFinalStatus,
                                ReimbursRequestInfo.ReimbursAproveStatus2,
                                IIf(ReimbursRequestInfo.ReimbursAproveStatus2 = False, ""Pending"", """") AS RequestStatus,
                                ReimbursRequestInfo.IsActive,
                                ReimbursRequestInfo.IsDeleted,
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
                                    AND ((ReimbursRequestInfo.ReimbursAproveBy2) = " + txtApproverID + @")
                                    AND (
                                        (ReimbursRequestInfo.ReimbursAproveStatus1) = True
                                    )
                                    AND (
                                        (ReimbursRequestInfo.ReimbursRequestFinalStatus) = False
                                    )
                                    AND (
                                        (ReimbursRequestInfo.ReimbursAproveStatus2) = False
                                    )
                                    AND ((ReimbursRequestInfo.IsActive) = True)
                                    AND ((ReimbursRequestInfo.IsDeleted) = False)
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

        public List<ReimbursementTransactionList> getReimbursmentTransactionList(int txtReimbRequestID)
        {
            List<ReimbursementTransactionList> objReimbursementTransactionListInfo = new List<ReimbursementTransactionList>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "";

                strQuery = @"SELECT
                                    ReimbRequestDetailInfo.ReimbRequestDetailID,
                                    ReimbRequestDetailInfo.ReimbRequestID,
                                    ReimbRequestDetailInfo.ReimbRequestTRDate,
                                    ReimbRequestDetailInfo.OBalance,
                                    ReimbRequestDetailInfo.CrAmount,
                                    ReimbRequestDetailInfo.DrAmount,
                                    ReimbRequestDetailInfo.CBalance,
                                    ReimbRequestDetailInfo.TRType,
                                    ReimbRequestDetailInfo.Comments,
                                    ReimbRequestDetailInfo.OrderID
                                FROM
                                    ReimbRequestDetailInfo
                                WHERE
                                    (((ReimbRequestDetailInfo.ReimbRequestID) = " + txtReimbRequestID + @"))
                                ORDER BY
                                    ReimbRequestDetailInfo.ReimbRequestDetailID,
                                    ReimbRequestDetailInfo.ReimbRequestTRDate,
                                    ReimbRequestDetailInfo.OrderID;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objReimbursementTransactionListInfo = JsonConvert.DeserializeObject<List<ReimbursementTransactionList>>(DataTableToJSon);
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

            return objReimbursementTransactionListInfo;
        }

        public int UpdateReimbursmentRequestFirstApprover(int ReimbursRequestID, string FirstApproverStatus)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "";

                if (FirstApproverStatus == "Approved")
                {
                    strQuery = "UPDATE ReimbursRequestInfo SET ReimbursAproveDate1 = #" + Convert.ToDateTime(DateTime.Today).ToString("dd-MMM-yyyy") + "#, ReimbursAproveStatus1 = True, ReimbursAproveStatus2 = False, ReimbursRequestFinalStatus = False WHERE ReimbursRequestID = " + ReimbursRequestID;
                }
                else if (FirstApproverStatus == "In Progress")
                {
                    strQuery = "UPDATE ReimbursRequestInfo SET ReimbursAproveDate1 = #" + Convert.ToDateTime(DateTime.Today).ToString("dd-MMM-yyyy") + "#, ReimbursAproveStatus1 = False, ReimbursAproveStatus2 = False, ReimbursRequestFinalStatus = False WHERE ReimbursRequestID = " + ReimbursRequestID;
                }
                else if (FirstApproverStatus == "Cancelled")
                {
                    strQuery = "UPDATE ReimbursRequestInfo SET ReimbursAproveDate1 = #" + Convert.ToDateTime(DateTime.Today).ToString("dd-MMM-yyyy") + "#, IsActive = False, IsDeleted = True, ReimbursAproveStatus1 = True, ReimbursAproveStatus2 = False, ReimbursRequestFinalStatus = False WHERE ReimbursRequestID = " + ReimbursRequestID;
                }
                else if (FirstApproverStatus == "Rejected")
                {
                    strQuery = "UPDATE ReimbursRequestInfo SET ReimbursAproveDate1 = #" + Convert.ToDateTime(DateTime.Today).ToString("dd-MMM-yyyy") + "#, IsActive = False, IsDeleted = True, ReimbursAproveStatus1 = False, ReimbursAproveStatus2 = False, ReimbursRequestFinalStatus = False WHERE ReimbursRequestID = " + ReimbursRequestID;
                }

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = ReimbursRequestID;
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

        public int UpdateReimbursmentRequestSecondApprover(int ReimbursRequestID, string SecondApproverStatus)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "";

                if (SecondApproverStatus == "Approved")
                {
                    strQuery = "UPDATE ReimbursRequestInfo SET ReimbursApproveDate2 = #" + Convert.ToDateTime(DateTime.Today).ToString("dd-MMM-yyyy") + "#, ReimbursAproveStatus2 = True, ReimbursRequestFinalStatus = True WHERE ReimbursRequestID = " + ReimbursRequestID;
                }
                else if (SecondApproverStatus == "In Progress")
                {
                    strQuery = "UPDATE ReimbursRequestInfo SET ReimbursApproveDate2 = #" + Convert.ToDateTime(DateTime.Today).ToString("dd-MMM-yyyy") + "#, ReimbursAproveStatus2 = False, ReimbursAproveStatus2 = False, ReimbursRequestFinalStatus = False WHERE ReimbursRequestID = " + ReimbursRequestID;
                }
                else if (SecondApproverStatus == "Cancelled")
                {
                    strQuery = "UPDATE ReimbursRequestInfo SET ReimbursApproveDate2 = #" + Convert.ToDateTime(DateTime.Today).ToString("dd-MMM-yyyy") + "#, IsActive = False, IsDeleted = True, ReimbursAproveStatus2 = True, ReimbursAproveStatus2 = False, ReimbursRequestFinalStatus = False WHERE ReimbursRequestID = " + ReimbursRequestID;
                }
                else if (SecondApproverStatus == "Rejected")
                {
                    strQuery = "UPDATE ReimbursRequestInfo SET ReimbursApproveDate2 = #" + Convert.ToDateTime(DateTime.Today).ToString("dd-MMM-yyyy") + "#, IsActive = False, IsDeleted = True, ReimbursAproveStatus2 = False, ReimbursRequestFinalStatus = False WHERE ReimbursRequestID = " + ReimbursRequestID;
                }

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = ReimbursRequestID;
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

        public int InsertEmployeeReimbursmentRequestMasInfo(DateTime ReimbRequestDate, string ReimbRequestDescription, string ReimbRequestStatus, bool IsActive, bool IsDeleted, int ReimbursRequestID)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("ReimbRequestMas", "ReimbRequestID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO ReimbRequestMas (ReimbRequestID, ReimbRequestCode, ReimbRequestDate, ReimbRequestDescription, ReimbRequestStatus, IsActive, IsDeleted, OrderID, ReimbursRequestID) VALUES " +
                 "(" + maxRowCount.Data + ",'" + "RMB-REQ-" + (maxRowCount.Data).ToString().PadLeft(4, '0').Trim() + "',#" + Convert.ToDateTime(ReimbRequestDate).ToString("dd-MMM-yyyy") + "#, '" + ReimbRequestDescription + "', '" + ReimbRequestStatus + "', " + IsActive + "," + IsDeleted + "," + maxRowCount.Data + ", " + ReimbursRequestID + ")";

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

        public int UpdateEmployeeReimbursmentRequestStatus(int ReimbRequestID, string ReimbStatus)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE ReimbRequestMas SET ReimbRequestStatus = '" + ReimbStatus + "' WHERE ReimbRequestID = " + ReimbRequestID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = ReimbRequestID;
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

        public int InsertEmployeeReimbursmentRequestDetailedInfo(int ReimbRequestID, DateTime ReimbRequestTRDate, decimal OBalance, decimal CrAmount, decimal DrAmount, decimal CBalance, string TRType, string Comments)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("ReimbRequestDetailInfo", "ReimbRequestDetailID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO ReimbRequestDetailInfo (ReimbRequestDetailID, ReimbRequestID, ReimbRequestTRDate, OBalance, CrAmount, DrAmount, CBalance, TRType, Comments, OrderID) VALUES " +
                 "(" + maxRowCount.Data + ", " + ReimbRequestID + ", #" + Convert.ToDateTime(ReimbRequestTRDate).ToString("dd-MMM-yyyy") + "#, " + OBalance + ", " + CrAmount + ", " + DrAmount + ", " + CBalance + ", '" + TRType + "', '" + Comments + "', " + maxRowCount.Data + ")";

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
