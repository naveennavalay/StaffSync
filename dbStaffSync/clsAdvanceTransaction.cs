using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace dbStaffSync
{
    public class clsAdvanceTransaction
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public bool IsAdvanceAlreadyExist(int txtEmpPersonalInfoID)
        {
            List<AdvanceRequestStatusInfo> objAdvanceRequestStatusInfo = new List<AdvanceRequestStatusInfo>();

            try
            {
                conn = dbStaffSync.openDBConnection();
                //dtDataset = new DataSet();
                DataTable dt = new DataTable();

                string strQuery = "SELECT " +
                                        " TOP 1 AdvanceRequestStatus " + 
                                    " FROM " +
                                        " EmpAdvanceRequestMas " +
                                    " WHERE " +
                                        " PersonalInfoID = " + txtEmpPersonalInfoID + " AND AdvanceRequestStatus = false " +
                                    " ORDER BY  " +
                                        " EmpAdvanceRequestID, OrderID DESC;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAdvanceRequestStatusInfo = JsonConvert.DeserializeObject<List<AdvanceRequestStatusInfo>>(DataTableToJSon);
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

            if(objAdvanceRequestStatusInfo.Count > 0)
                return objAdvanceRequestStatusInfo[0].AdvanceRequestStatus == false ? true : false;
            else
                return false;
        }

        public List<AdvanceApprovalPendingList> AdvanceFirstApprovalPendingList(int txtClientID)
        {
            List<AdvanceApprovalPendingList> objAdvanceApprovalPendingList = new List<AdvanceApprovalPendingList>();

            try
            {
                conn = dbStaffSync.openDBConnection();
                //dtDataset = new DataSet();
                DataTable dt = new DataTable();

                string strQuery = "SELECT " + 
                                        " ClientMas.ClientID, " + 
                                        " EmpMas.EmpID AS RequesterEmpID, " + 
                                        " EmpMas.EmpCode AS RequesterEmpCode, " + 
                                        " EmpMas.EmpName AS RequesterEmpName, " + 
                                        " DesigMas.DesignationTitle AS RequesterDesignationTitle, " + 
                                        " DepMas.DepartmentTitle AS RequesterDepartmentTitle, " + 
                                        " EmpAdvanceRequestMas.EmpAdvanceRequestID, " + 
                                        " AdvanceTypeMas.AdvanceTypeTitle, " + 
                                        " EmpAdvanceRequestMas.EmpAdvReqCode, " + 
                                        " EmpAdvanceRequestMas.EmpAdvanceRequestDate, " +
                                        " EmpAdvanceRequestMas.AdvanceAmount, " +
                                        " EmpMas_1.EmpID AS ApproverEmpID1, " + 
                                        " EmpMas_1.EmpCode AS ApproverEmpCode1, " + 
                                        " EmpMas_1.EmpName AS ApproverEmpName1, " + 
                                        " PersonalInfoMas_1.ContactNumber2 AS ApproverEmpMailID1, " + 
                                        " EmpAdvanceRequestMas.RequestedToComments AS ApproverRequestedToComments1, " + 
                                        " EmpMas_2.EmpID AS ApproverEmpID2, " + 
                                        " EmpMas_2.EmpCode AS ApproverEmpCode2, " + 
                                        " EmpMas_2.EmpName AS ApproverEmpName2, " + 
                                        " PersonalInfoMas_2.ContactNumber2 AS ApproverEmpMailID2, " + 
                                        " EmpAdvanceRequestMas.RequestMovedTo, " + 
                                        " EmpAdvanceRequestMas.RequestMovedToComments, " + 
                                        " EmpAdvanceRequestMas.AdvanceRequestStatus " + 
                                    " FROM " +
                                        " AdvanceTypeMas " + 
                                        " INNER JOIN ( " + 
                                            " ClientMas " + 
                                            " INNER JOIN ( " + 
                                                " ( " +  
                                                    " EmpMas AS EmpMas_2 " + 
                                                    " INNER JOIN ( " + 
                                                        " ( " + 
                                                            " EmpMas AS EmpMas_1 " + 
                                                            " INNER JOIN PersonalInfoMas AS PersonalInfoMas_1 ON EmpMas_1.EmpID = PersonalInfoMas_1.EmpID " + 
                                                        " ) " + 
                                                        " INNER JOIN ( " + 
                                                            " ( " + 
                                                                " ( " + 
                                                                    " DesigMas " + 
                                                                    " INNER JOIN ( " + 
                                                                        " DepMas " + 
                                                                        " INNER JOIN EmpMas ON DepMas.DepartmentID = EmpMas.DepartmentID " + 
                                                                    " ) ON DesigMas.DesignationID = EmpMas.EmpDesignationID " + 
                                                                " ) " + 
                                                                " INNER JOIN PersonalInfoMas ON EmpMas.EmpID = PersonalInfoMas.EmpID " + 
                                                            " ) " + 
                                                            " INNER JOIN EmpAdvanceRequestMas ON PersonalInfoMas.PersonalInfoID = EmpAdvanceRequestMas.PersonalInfoID " + 
                                                        " ) ON EmpMas_1.EmpID = EmpAdvanceRequestMas.RequestedTo " + 
                                                    " ) ON EmpMas_2.EmpID = EmpAdvanceRequestMas.RequestMovedTo " + 
                                                " ) " + 
                                                " INNER JOIN PersonalInfoMas AS PersonalInfoMas_2 ON EmpMas_2.EmpID = PersonalInfoMas_2.EmpID " + 
                                            " ) ON ClientMas.ClientID = EmpMas.ClientID " + 
                                        " ) ON AdvanceTypeMas.AdvanceTypeID = EmpAdvanceRequestMas.AdvanceTypeID " + 
                                    " WHERE " + 
                                        " ( " + 
                                            " ((ClientMas.ClientID) = " + txtClientID + ") " + 
                                            " AND ( " +
                                                " (EmpAdvanceRequestMas.RequestedToComments) = 'Pending' OR (EmpAdvanceRequestMas.RequestedToComments) = 'In Progress'" + 
                                            " ) " + 
                                            " AND ( " + 
                                                " (EmpAdvanceRequestMas.AdvanceRequestStatus) = False " + 
                                            " ) " + 
                                        " );";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAdvanceApprovalPendingList = JsonConvert.DeserializeObject<List<AdvanceApprovalPendingList>>(DataTableToJSon);
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

            if (objAdvanceApprovalPendingList.Count > 0)
                return objAdvanceApprovalPendingList;
            else
                return new List<AdvanceApprovalPendingList>();
        }

        public List<AdvanceApprovalPendingList> AdvanceSecondApprovalPendingList(int txtClientID)
        {
            List<AdvanceApprovalPendingList> objAdvanceApprovalPendingList = new List<AdvanceApprovalPendingList>();

            try
            {
                conn = dbStaffSync.openDBConnection();
                //dtDataset = new DataSet();
                DataTable dt = new DataTable();

                string strQuery = "SELECT " + 
                                        " ClientMas.ClientID, " + 
                                        " EmpMas.EmpID AS RequesterEmpID, " + 
                                        " EmpMas.EmpCode AS RequesterEmpCode, " + 
                                        " EmpMas.EmpName AS RequesterEmpName, " + 
                                        " DesigMas.DesignationTitle AS RequesterDesignationTitle, " +
                                        " DepMas.DepartmentTitle AS RequesterDepartmentTitle, " +
                                        " EmpAdvanceRequestMas.EmpAdvanceRequestID, " + 
                                        " AdvanceTypeMas.AdvanceTypeTitle, " + 
                                        " EmpAdvanceRequestMas.EmpAdvReqCode, " + 
                                        " EmpAdvanceRequestMas.EmpAdvanceRequestDate, " +
                                        " EmpAdvanceRequestMas.AdvanceAmount, " +
                                        " EmpMas_1.EmpID AS ApproverEmpID1, " + 
                                        " EmpMas_1.EmpCode AS ApproverEmpCode1, " + 
                                        " EmpMas_1.EmpName AS ApproverEmpName1, " + 
                                        " PersonalInfoMas_1.ContactNumber2 AS ApproverEmpMailID1, " + 
                                        " EmpAdvanceRequestMas.RequestedToComments AS ApproverRequestedToComments1, " + 
                                        " EmpMas_2.EmpID AS ApproverEmpID2, " + 
                                        " EmpMas_2.EmpCode AS ApproverEmpCode2, " + 
                                        " EmpMas_2.EmpName AS ApproverEmpName2, " + 
                                        " PersonalInfoMas_2.ContactNumber2 AS ApproverEmpMailID2, " + 
                                        " EmpAdvanceRequestMas.RequestMovedTo, " + 
                                        " EmpAdvanceRequestMas.RequestMovedToComments, " + 
                                        " EmpAdvanceRequestMas.AdvanceRequestStatus " + 
                                    " FROM " + 
                                        " AdvanceTypeMas " + 
                                        " INNER JOIN ( " + 
                                            " ClientMas " + 
                                            " INNER JOIN ( " + 
                                                " ( " + 
                                                    " EmpMas AS EmpMas_2 " + 
                                                    " INNER JOIN ( " + 
                                                        " ( " + 
                                                            " EmpMas AS EmpMas_1 " + 
                                                            " INNER JOIN PersonalInfoMas AS PersonalInfoMas_1 ON EmpMas_1.EmpID = PersonalInfoMas_1.EmpID " + 
                                                        " ) " + 
                                                        " INNER JOIN ( " + 
                                                            " ( " + 
                                                                " ( " + 
                                                                    " DesigMas " + 
                                                                    " INNER JOIN ( " + 
                                                                        " DepMas " + 
                                                                        " INNER JOIN EmpMas ON DepMas.DepartmentID = EmpMas.DepartmentID " + 
                                                                    " ) ON DesigMas.DesignationID = EmpMas.EmpDesignationID " + 
                                                                " ) " + 
                                                                " INNER JOIN PersonalInfoMas ON EmpMas.EmpID = PersonalInfoMas.EmpID " + 
                                                            " ) " + 
                                                            " INNER JOIN EmpAdvanceRequestMas ON PersonalInfoMas.PersonalInfoID = EmpAdvanceRequestMas.PersonalInfoID " + 
                                                        " ) ON EmpMas_1.EmpID = EmpAdvanceRequestMas.RequestedTo " + 
                                                    " ) ON EmpMas_2.EmpID = EmpAdvanceRequestMas.RequestMovedTo " + 
                                                " ) " + 
                                                " INNER JOIN PersonalInfoMas AS PersonalInfoMas_2 ON EmpMas_2.EmpID = PersonalInfoMas_2.EmpID " + 
                                            " ) ON ClientMas.ClientID = EmpMas.ClientID " + 
                                        " ) ON AdvanceTypeMas.AdvanceTypeID = EmpAdvanceRequestMas.AdvanceTypeID " + 
                                    " WHERE " + 
                                        " ( " + 
                                            " ((ClientMas.ClientID) = " + txtClientID + ") " + 
                                            " AND ( " +
                                                " (EmpAdvanceRequestMas.RequestMovedToComments) = 'Pending' OR (EmpAdvanceRequestMas.RequestMovedToComments) = 'In Progress'" +
                                            " ) " + 
                                            " AND ( " + 
                                                " (EmpAdvanceRequestMas.AdvanceRequestStatus) = False " + 
                                            " ) " + 
                                        " );";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAdvanceApprovalPendingList = JsonConvert.DeserializeObject<List<AdvanceApprovalPendingList>>(DataTableToJSon);
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

            if (objAdvanceApprovalPendingList.Count > 0)
                return objAdvanceApprovalPendingList;
            else
                return new List<AdvanceApprovalPendingList>();
        }

        public int InsertEmpAdvanceRequestMas(int txtEmpPersonalInfoID, int txtAdvanceTypeID, bool IsActive, bool IsDeleted, DateTime txtEmpAdvanceRequestDate, string txtEmpAdvanceRequestComments, int txtRequestedTo, DateTime txtRequestedToDate, 
            bool RequestedToStatus, string txtRequestedToComments, int RequestMovedTo, DateTime txtRequestMovedToDate, bool txtRequestMovedToStatus, string txtRequestMovedToComments, DateTime txtRequestApprovalDate, 
            bool AdvanceRequestStatus, decimal txtAdvanceAmount, decimal txtAdvanceTenure, decimal AdvanceInstallment, DateTime txtAdvanceStartDate, DateTime AdvanceEndDate)
        {
            int affectedRows = 0;
            try
            {

                Response<int> maxRowCount = objGenFunc.getMaxRowCount("EmpAdvanceRequestMas", "EmpAdvanceRequestID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO EmpAdvanceRequestMas (EmpAdvanceRequestID, EmpAdvReqCode, PersonalInfoID, AdvanceTypeID, IsActive, IsDeleted, EmpAdvanceRequestDate, EmpAdvanceRequestComments, RequestedTo, RequestedToDate, RequestedToStatus, RequestedToComments, RequestMovedTo, RequestMovedToDate, RequestMovedToStatus, RequestMovedToComments, RequestApprovalDate, AdvanceRequestStatus, OrderID, AdvanceAmount, AdvanceTenure, AdvanceInstallment, AdvanceStartDate, AdvanceEndDate) VALUES " +
                 "(" + maxRowCount.Data + ",'" + "ADV-REQ-" + (maxRowCount.Data).ToString().PadLeft(4, '0').Trim() + "', " + txtEmpPersonalInfoID + "," + txtAdvanceTypeID + ", " + IsActive + ", " + IsDeleted + ", '" + txtEmpAdvanceRequestDate.ToString("dd-MMM-yyyy") + "', 'Advance Request', " + txtRequestedTo + ", '" + txtRequestedToDate.ToString("dd-MMM-yyyy") + "', " + RequestedToStatus + ", " + 
                 "'" + txtRequestedToComments + "', " + RequestMovedTo + ", '" + txtRequestMovedToDate.ToString("dd-MMM-yyyy")  + "', " + txtRequestMovedToStatus + ", '" + txtRequestMovedToComments + "', '" + txtRequestApprovalDate.ToString("dd-MMM-yyyy") + "', " + 
                 "" + AdvanceRequestStatus + ", " + maxRowCount.Data + ", " + txtAdvanceAmount + ", " + txtAdvanceTenure + ", " + AdvanceInstallment + ", '" + txtAdvanceStartDate.ToString("dd-MMM-yyyy") + "', '" + AdvanceEndDate.ToString("dd-MMM-yyyy") + "')";

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

        public int UpdateEmpAdvanceRequestMas(int EmpAdvanceRequestID, int txtEmpPersonalInfoID, bool IsActive, bool IsDeleted, int txtAdvanceTypeID, DateTime txtEmpAdvanceRequestDate, string txtEmpAdvanceRequestComments, int txtRequestedTo, DateTime txtRequestedToDate,
        bool RequestedToStatus, string txtRequestedToComments, int RequestMovedTo, DateTime txtRequestMovedToDate, bool txtRequestMovedToStatus, string txtRequestMovedToComments, DateTime txtRequestApprovalDate,
        bool AdvanceRequestStatus, decimal txtAdvanceAmount, decimal txtAdvanceTenure, decimal txtAdvanceInstallment, DateTime txtAdvanceStartDate, DateTime txtAdvanceEndDate)
        {
            int affectedRows = 0;
            try
            {

                Response<int> maxRowCount = objGenFunc.getMaxRowCount("AdvanceTypeMas", "AdvanceTypeID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpAdvanceRequestMas " +
                                        " SET " + 
                                            " PersonalInfoID = " + txtEmpPersonalInfoID + ", " + 
                                            " AdvanceTypeID = " + txtAdvanceTypeID + ", " +
                                            " IsActive = " + IsActive + ", " +
                                            " IsDeleted = " + IsDeleted + ", " +
                                            " EmpAdvanceRequestDate = #" + txtEmpAdvanceRequestDate.ToString("dd-MMM-yyyy") + "#, " + 
                                            " EmpAdvanceRequestComments = '" + txtEmpAdvanceRequestComments + "', " + 
                                            " RequestedTo = " + txtRequestedTo + ", " + 
                                            " RequestedToDate = #" + txtRequestedToDate.ToString("dd-MMM-yyyy") + "#, " + 
                                            " RequestedToStatus = " + RequestedToStatus + ", " +
                                            " RequestedToComments = '" + txtRequestedToComments + "', " + 
                                            " RequestMovedTo = " + txtRequestedTo + ", " + 
                                            " RequestMovedToDate = #" + txtRequestMovedToDate.ToString("dd-MMM-yyyy") + "#, " + 
                                            " RequestMovedToStatus = " + txtRequestMovedToStatus + ", " +
                                            " RequestMovedToComments = '" + txtRequestMovedToComments + "', " + 
                                            " RequestApprovalDate = #" + txtRequestApprovalDate.ToString("dd-MMM-yyyy") + "#, " + 
                                            " AdvanceRequestStatus = " + AdvanceRequestStatus + ", " + 
                                            " AdvanceAmount = " + txtAdvanceAmount + ", " + 
                                            " AdvanceTenure = " + txtAdvanceTenure + ", " + 
                                            " AdvanceInstallment = " + txtAdvanceInstallment + ", " + 
                                            " AdvanceStartDate = #" + txtAdvanceStartDate.ToString("dd-MMM-yyyy") + "#, " + 
                                            " AdvanceEndDate = #" + txtAdvanceEndDate.ToString("dd-MMM-yyyy") + "# " + 
                                        " WHERE " + 
                                            " EmpAdvanceRequestID = " + EmpAdvanceRequestID + "";

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

        public int DeleteEmpAdvanceRequestMas(int txtEmpAdvanceRequestID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE * FROM EmpAdvanceRequestMas WHERE EmpAdvanceRequestID = " + txtEmpAdvanceRequestID.ToString().Trim();

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

        public int UpdateFirstApproverStatus(int txtEmpAdvanceRequestID, string txtRequestedToComments, DateTime RequestedToDate)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpAdvanceRequestMas " + 
                                          " SET " + 
                                                " RequestedToComments = '" + txtRequestedToComments + "', " +
                                                " RequestedToDate = #" +  RequestedToDate.ToString("dd-MMM-yyyy") + "# " +
                                            " WHERE " + 
                                                " EmpAdvanceRequestID = " + txtEmpAdvanceRequestID;

                if(txtRequestedToComments == "Approved")
                    strQuery = "UPDATE EmpAdvanceRequestMas " +
                                          " SET " +
                                                " RequestedToComments = '" + txtRequestedToComments + "', " +
                                                " RequestedToDate = #" + RequestedToDate.ToString("dd-MMM-yyyy") + "#, " +
                                                " RequestedToStatus = true " +
                                            " WHERE " +
                                                " EmpAdvanceRequestID = " + txtEmpAdvanceRequestID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtEmpAdvanceRequestID;
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

        public int UpdateSecondApproverStatus(int txtEmpAdvanceRequestID, string txtRequestMovedToComments, DateTime RequestMovedToDate)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpAdvanceRequestMas " +
                                          " SET " +
                                                " RequestMovedToComments = '" + txtRequestMovedToComments + "', " +
                                                " RequestedToDate = #" + RequestMovedToDate.ToString("dd-MMM-yyyy") + "# " +
                                            " WHERE " +
                                                " EmpAdvanceRequestID = " + txtEmpAdvanceRequestID;

                if (txtRequestMovedToComments == "Approved")
                    strQuery = "UPDATE EmpAdvanceRequestMas " +
                                          " SET " +
                                                " RequestMovedToComments = '" + txtRequestMovedToComments + "', " +
                                                " RequestedToDate = #" + RequestMovedToDate.ToString("dd-MMM-yyyy") + "#, " +
                                                " RequestMovedToStatus = true " +
                                            " WHERE " +
                                                " EmpAdvanceRequestID = " + txtEmpAdvanceRequestID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtEmpAdvanceRequestID;
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

        public int RejectOrCancleApproverStatus(int txtEmpAdvanceRequestID, string CancelOrRejectedComments, DateTime CancelOrRejectionDate)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpAdvanceRequestMas " +
                                          " SET " +
                                                " RequestedToComments = '" + CancelOrRejectedComments + "', " +
                                                " RequestedToDate = #" + CancelOrRejectionDate.ToString("dd-MMM-yyyy") + "# " +
                                                " RequestedToStatus = false, " +
                                                " RequestMovedToComments = '" + CancelOrRejectedComments + "', " +
                                                " RequestedToDate = #" + CancelOrRejectionDate.ToString("dd-MMM-yyyy") + "# " +
                                                " RequestMovedToStatus = false, " +
                                                " AdvanceRequestStatus = true " +
                                            " WHERE " +
                                                " EmpAdvanceRequestID = " + txtEmpAdvanceRequestID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtEmpAdvanceRequestID;
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

        public int CloseEmployeeSpecificAdvanceRequest(int txtEmpAdvanceRequestID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpAdvanceRequestMas " +
                                          " SET " +
                                                " AdvanceRequestStatus = true" +
                                            " WHERE " +
                                                " EmpAdvanceRequestID = " + txtEmpAdvanceRequestID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtEmpAdvanceRequestID;
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

        public int InsertAdvanceTransaction(string txtEmpAdvReqCode, int EmpAdvanceRequestID, DateTime txtAdvanceDate, decimal txtOBalance, decimal txtCrBalance, decimal txtDrBalance, decimal txtCBalance, string txtTRType, string txtComments, int txtEmpSalID)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("EmpAdvanceDetails", "EmpAdvanceRecoveryID");
                Response<int> OrderID = objGenFunc.getMaxRowCount("EmpAdvanceDetails", "OrderID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO EmpAdvanceDetails (EmpAdvanceRecoveryID, EmpAdvReqCode, EmpAdvanceRequestID, AdvanceDate, OBalance, CrBalance, DrBalance, CBalance, TRType, Comments, OrderID, EmpSalID) VALUES " +
                 "(" + maxRowCount.Data + ",'" + txtEmpAdvReqCode + "', " + EmpAdvanceRequestID  + ", '" + txtAdvanceDate.ToString("dd-MMM-yyyy") + "', " + txtOBalance + ", " + txtCrBalance + ", " + txtDrBalance + ", " + txtCBalance + ", '" + txtTRType + "', '" + txtComments + "', " + OrderID.Data + ", " + txtEmpSalID + ")";

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

        public int DeleteAdvanceTransaction(int txtEmpAdvanceRecoveryID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE * FROM EmpAdvanceDetails WHERE  EmpAdvanceRecoveryID = " + txtEmpAdvanceRecoveryID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtEmpAdvanceRecoveryID;
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

        public List<EmployeeSpecificAdvanceInformation> EmployeeSpecificAdvanceInformation(int txtEmpID)
        {
            List<EmployeeSpecificAdvanceInformation> objEmployeeSpecificAdvanceInformation = new List<EmployeeSpecificAdvanceInformation>();

            try
            {
                conn = dbStaffSync.openDBConnection();
                //dtDataset = new DataSet();
                DataTable dt = new DataTable();

                string strQuery = "SELECT " + 
                                        " TOP 1 EmpMas.EmpID, " + 
                                        " EmpMas.EmpCode, " + 
                                        " EmpMas.EmpName, " + 
                                        " DesigMas.DesignationTitle, " +
                                        " DepMas.DepartmentTitle, " +
                                        " PersonalInfoMas.PersonalInfoID, " +
                                        " PersonalInfoMas.ContactNumber2, " +
                                        " EmpAdvanceRequestMas.EmpAdvanceRequestID, " + 
                                        " EmpAdvanceRequestMas.EmpAdvReqCode, " +
                                        " AdvanceTypeMas.AdvanceTypeID, " + 
                                        " AdvanceTypeMas.AdvanceTypeTitle, " + 
                                        " EmpAdvanceRequestMas.AdvanceAmount, " + 
                                        " EmpAdvanceRequestMas.AdvanceInstallment, " + 
                                        " EmpAdvanceRequestMas.AdvanceStartDate, " + 
                                        " EmpAdvanceRequestMas.AdvanceEndDate, " + 
                                        " EmpAdvanceRequestMas.AdvanceRequestStatus, " + 
                                        " EmpAdvanceDetails.AdvanceDate AS LastRepayDate, " + 
                                        " EmpAdvanceDetails.EmpAdvanceRecoveryID, " + 
                                        " EmpAdvanceDetails.CBalance, " +
                                        " EmpAdvanceDetails.CBalance As RePaymentBalance" +
                                    " FROM " + 
                                        " DesigMas " + 
                                        " INNER JOIN ( " + 
                                            " DepMas " + 
                                            " INNER JOIN ( " + 
                                                " ( " + 
                                                    " EmpMas " + 
                                                    " INNER JOIN PersonalInfoMas ON EmpMas.EmpID = PersonalInfoMas.EmpID " + 
                                                " ) " + 
                                                " INNER JOIN ( " + 
                                                    " ( " + 
                                                        " AdvanceTypeMas " + 
                                                        " INNER JOIN EmpAdvanceRequestMas ON AdvanceTypeMas.AdvanceTypeID = EmpAdvanceRequestMas.AdvanceTypeID " + 
                                                    " ) " + 
                                                    " INNER JOIN EmpAdvanceDetails ON EmpAdvanceRequestMas.EmpAdvanceRequestID = EmpAdvanceDetails.EmpAdvanceRequestID " + 
                                                " ) ON PersonalInfoMas.PersonalInfoID = EmpAdvanceRequestMas.PersonalInfoID " + 
                                            " ) ON DepMas.DepartmentID = EmpMas.DepartmentID " + 
                                        " ) ON DesigMas.DesignationID = EmpMas.EmpDesignationID " + 
                                    " WHERE " + 
                                        " (((EmpAdvanceRequestMas.AdvanceRequestStatus) = False) AND ((EmpMas.EmpID) = " + txtEmpID + ") AND ((EmpAdvanceRequestMas.IsActive) = True) AND ((EmpAdvanceRequestMas.IsDeleted) = False)) " + 
                                    " ORDER BY " + 
                                        " EmpAdvanceDetails.EmpAdvanceRecoveryID DESC;";

                if (txtEmpID == 0)
                    strQuery = "SELECT " +
                                    " TOP 1 EmpMas.EmpID, " +
                                    " EmpMas.EmpCode, " +
                                    " EmpMas.EmpName, " +
                                    " DesigMas.DesignationTitle, " +
                                    " DepMas.DepartmentTitle, " +
                                    " PersonalInfoMas.PersonalInfoID, " +
                                    " PersonalInfoMas.ContactNumber2, " +
                                    " EmpAdvanceRequestMas.EmpAdvanceRequestID, " +
                                    " EmpAdvanceRequestMas.EmpAdvReqCode, " +
                                    " AdvanceTypeMas.AdvanceTypeID, " +
                                    " AdvanceTypeMas.AdvanceTypeTitle, " +
                                    " EmpAdvanceRequestMas.AdvanceAmount, " +
                                    " EmpAdvanceRequestMas.AdvanceInstallment, " +
                                    " EmpAdvanceRequestMas.AdvanceStartDate, " +
                                    " EmpAdvanceRequestMas.AdvanceEndDate, " +
                                    " EmpAdvanceRequestMas.AdvanceRequestStatus, " +
                                    " EmpAdvanceDetails.AdvanceDate AS LastRepayDate, " +
                                    " EmpAdvanceDetails.EmpAdvanceRecoveryID, " +
                                    " EmpAdvanceDetails.CBalance " +
                                " FROM " +
                                    " ( " +
                                        " ( " +
                                            " DesigMas " +
                                            " INNER JOIN ( " +
                                                " DepMas " +
                                                " INNER JOIN EmpMas ON DepMas.DepartmentID = EmpMas.DepartmentID " +
                                            " ) ON DesigMas.DesignationID = EmpMas.EmpDesignationID " +
                                        " ) " +
                                        " INNER JOIN PersonalInfoMas ON EmpMas.EmpID = PersonalInfoMas.EmpID " +
                                    " ) " +
                                    " INNER JOIN ( " +
                                        " ( " +
                                            " AdvanceTypeMas " +
                                            " INNER JOIN EmpAdvanceRequestMas ON AdvanceTypeMas.AdvanceTypeID = EmpAdvanceRequestMas.AdvanceTypeID " +
                                        " ) " +
                                        " INNER JOIN EmpAdvanceDetails ON EmpAdvanceRequestMas.EmpAdvanceRequestID = EmpAdvanceDetails.EmpAdvanceRequestID " +
                                    " ) ON PersonalInfoMas.PersonalInfoID = EmpAdvanceRequestMas.PersonalInfoID " +
                                " WHERE " +
                                    " ( " +
                                        " ( " +
                                            " (EmpAdvanceRequestMas.AdvanceRequestStatus) = False " +
                                        " ) " +
                                        " AND ((EmpMas.IsActive) = True) " +
                                        " AND ((EmpMas.IsDeleted) = False) " +
                                   " ) " +
                                " ORDER BY EmpAdvanceDetails.EmpAdvanceRecoveryID DESC;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objEmployeeSpecificAdvanceInformation = JsonConvert.DeserializeObject<List<EmployeeSpecificAdvanceInformation>>(DataTableToJSon);
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

            if (objEmployeeSpecificAdvanceInformation.Count > 0)
                return objEmployeeSpecificAdvanceInformation;
            else
                return new List<EmployeeSpecificAdvanceInformation>();
        }

        public List<EmployeeSpecificAdvanceStatemetns> EmployeeSpecificAdvanceStatemetns(int txtEmpAdvanceRequestID)
        {
            List<EmployeeSpecificAdvanceStatemetns> objEmployeeSpecificAdvanceStatemetns = new List<EmployeeSpecificAdvanceStatemetns>();

            try
            {
                conn = dbStaffSync.openDBConnection();
                //dtDataset = new DataSet();
                DataTable dt = new DataTable();

                string strQuery = "SELECT " + 
                                        " EmpAdvanceDetails.EmpAdvanceRecoveryID, " + 
                                        " EmpAdvanceDetails.AdvanceDate, " + 
                                        " EmpAdvanceDetails.OBalance, " + 
                                        " EmpAdvanceDetails.CrBalance, " + 
                                        " EmpAdvanceDetails.DrBalance, " + 
                                        " EmpAdvanceDetails.CBalance, " + 
                                        " EmpAdvanceDetails.TRType, " + 
                                        " EmpAdvanceDetails.Comments, " + 
                                        " EmpAdvanceDetails.OrderID, " +
                                        " EmpAdvanceRequestMas.EmpAdvanceRequestID " +
                                    " FROM " + 
                                        " EmpAdvanceRequestMas " + 
                                        " INNER JOIN EmpAdvanceDetails ON EmpAdvanceRequestMas.EmpAdvanceRequestID = EmpAdvanceDetails.EmpAdvanceRequestID " +
                                    " WHERE " + 
                                        " EmpAdvanceRequestMas.EmpAdvanceRequestID =  " + txtEmpAdvanceRequestID  +
                                    " ORDER BY " + 
                                        " EmpAdvanceDetails.EmpAdvanceRecoveryID, EmpAdvanceDetails.OrderID Asc;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objEmployeeSpecificAdvanceStatemetns = JsonConvert.DeserializeObject<List<EmployeeSpecificAdvanceStatemetns>>(DataTableToJSon);
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

            if (objEmployeeSpecificAdvanceStatemetns.Count > 0)
                return objEmployeeSpecificAdvanceStatemetns;
            else
                return new List<EmployeeSpecificAdvanceStatemetns>();
        }
    }
}
