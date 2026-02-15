//using C1.Framework;
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
    public class clsEmpLeaveEntitlementInfo
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();
        clsLeaveTypeMas clsLeaveTypeMas = new clsLeaveTypeMas();

        public clsEmpLeaveEntitlementInfo() { 

        }

        public List<LeaveEntitlementInfo> getDefaultLeaveEntitilementList()
        {
            List<LeaveEntitlementInfo> LeaveEntitlementInfoList = new List<LeaveEntitlementInfo>();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT " +
                                    "LeaveTypeMas.LeaveTypeID, " +
                                    "LeaveTypeMas.LeaveTypeTitle " +
                                "FROM " +
                                    "LeaveTypeMas " +
                                "WHERE " +
                                    "(" +
                                        "((LeaveTypeMas.IsActive) = True) " +
                                        "AND ((LeaveTypeMas.IsDelete) = False) " +
                                    ") " +
                                "ORDER BY " +
                                    "LeaveTypeMas.OrderID;";

                //strQuery = "SELECT " +
                //                "EmpLeaveEntitlement.LeaveEntmtID, " +
                //                "EmpMas.EmpID, " +
                //                "LeaveMas.LeaveMasID, " +
                //                "LeaveTypeMas.LeaveTypeID, " +
                //                "LeaveTypeMas.LeaveTypeTitle, " +
                //                "EmpLeaveEntitlement.TotalLeaves, " +
                //                "EmpLeaveEntitlement.BalanceLeaves, " +
                //                "LeaveTypeMas.OrderID " +
                //            "FROM " +
                //                "LeaveTypeMas " +
                //                "INNER JOIN( " +
                //                    "(" +
                //                        "EmpMas " +
                //                        "INNER JOIN LeaveMas ON EmpMas.EmpID = LeaveMas.EmpID " +
                //                    ") " +
                //                    "INNER JOIN EmpLeaveEntitlement ON LeaveMas.LeaveMasID = EmpLeaveEntitlement.LeaveMasID " +
                //                ") ON LeaveTypeMas.LeaveTypeID = EmpLeaveEntitlement.LeaveTypeID " +
                //            "WHERE " +
                //                "(((EmpMas.EmpID) = 1)) " +
                //            "ORDER BY " +
                //                "LeaveTypeMas.OrderID;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                LeaveEntitlementInfoList = JsonConvert.DeserializeObject<List<LeaveEntitlementInfo>>(DataTableToJSon);
                foreach (LeaveEntitlementInfo indLeaveEntitlementInfo in LeaveEntitlementInfoList)
                {
                    indLeaveEntitlementInfo.LeaveEntmtID = indLeaveEntitlementInfo.LeaveEntmtID;
                    indLeaveEntitlementInfo.EmpID = CurrentUser.EmpID;
                    indLeaveEntitlementInfo.LeaveMasID = indLeaveEntitlementInfo.LeaveMasID;
                    indLeaveEntitlementInfo.LeaveTypeID = indLeaveEntitlementInfo.LeaveTypeID;
                    indLeaveEntitlementInfo.TotalLeaves = indLeaveEntitlementInfo.TotalLeaves;
                    indLeaveEntitlementInfo.BalanceLeaves = indLeaveEntitlementInfo.BalanceLeaves;
                    indLeaveEntitlementInfo.OrderID = indLeaveEntitlementInfo.OrderID;
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

            return LeaveEntitlementInfoList;
        }

        public List<LeaveEntitlementInfo> getEmployeeLeaveEntitilementList(int txtEmpID, int txtLeaveMasID)
        {
            List<LeaveEntitlementInfo> LeaveEntitlementInfoList = new List<LeaveEntitlementInfo>();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT " +
                                    "LeaveTypeMas.LeaveTypeID, " +
                                    "LeaveTypeMas.LeaveTypeTitle " +
                                "FROM " +
                                    "LeaveTypeMas " +
                                "WHERE " +
                                    "(" +
                                        "((LeaveTypeMas.IsActive) = True) " +
                                        "AND ((LeaveTypeMas.IsDelete) = False) " +
                                    ") " +
                                "ORDER BY " +
                                    "LeaveTypeMas.OrderID;";

                strQuery = "SELECT " +
                                "EmpLeaveEntitlement.LeaveEntmtID, " +
                                "EmpMas.EmpID, " +
                                "LeaveMas.LeaveMasID, " +
                                "LeaveTypeMas.LeaveTypeID, " +
                                "LeaveTypeMas.LeaveTypeTitle, " +
                                "EmpLeaveEntitlement.TotalLeaves, " +
                                "EmpLeaveEntitlement.BalanceLeaves, " +
                                "0 As UsedLeaves, " +
                                "LeaveTypeMas.OrderID " +
                            "FROM " +
                                "LeaveTypeMas " +
                                "INNER JOIN( " +
                                    "(" +
                                        "EmpMas " +
                                        "INNER JOIN LeaveMas ON EmpMas.EmpID = LeaveMas.EmpID " +
                                    ") " +
                                    "INNER JOIN EmpLeaveEntitlement ON LeaveMas.LeaveMasID = EmpLeaveEntitlement.LeaveMasID " +
                                ") ON LeaveTypeMas.LeaveTypeID = EmpLeaveEntitlement.LeaveTypeID " +
                            "WHERE " +
                                "(((EmpMas.EmpID) = " + txtEmpID + ") AND ((LeaveMas.LeaveMasID) = " + txtLeaveMasID + ")) " +
                            "ORDER BY " +
                                "LeaveTypeMas.OrderID;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                LeaveEntitlementInfoList = JsonConvert.DeserializeObject<List<LeaveEntitlementInfo>>(DataTableToJSon);
                foreach (LeaveEntitlementInfo indLeaveEntitlementInfo in LeaveEntitlementInfoList)
                {
                    indLeaveEntitlementInfo.LeaveEntmtID = indLeaveEntitlementInfo.LeaveEntmtID;
                    indLeaveEntitlementInfo.EmpID = txtEmpID;
                    indLeaveEntitlementInfo.LeaveMasID = indLeaveEntitlementInfo.LeaveMasID;
                    indLeaveEntitlementInfo.LeaveTypeID = indLeaveEntitlementInfo.LeaveTypeID;
                    indLeaveEntitlementInfo.TotalLeaves = indLeaveEntitlementInfo.TotalLeaves;
                    indLeaveEntitlementInfo.BalanceLeaves = indLeaveEntitlementInfo.BalanceLeaves;
                    indLeaveEntitlementInfo.UsedLeaves = indLeaveEntitlementInfo.TotalLeaves - indLeaveEntitlementInfo.BalanceLeaves;
                    indLeaveEntitlementInfo.OrderID = indLeaveEntitlementInfo.OrderID;
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

            return LeaveEntitlementInfoList;
        }

        public List<LeaveOutStandingSummary> getConsolidatedLeaveOutStandingStatement(int txtClientID)
        {
            List<LeaveOutStandingSummary> LeaveOutStandingSummaryList = new List<LeaveOutStandingSummary>();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT " +
                                    "LeaveTypeMas.LeaveTypeID, " + 
                                    "LeaveTypeMas.LeaveTypeTitle " + 
                                "FROM " + 
                                    "LeaveTypeMas " + 
                                "WHERE " + 
                                    "(" + 
                                        "((LeaveTypeMas.IsActive) = True) " + 
                                        "AND ((LeaveTypeMas.IsDelete) = False) " + 
                                    ") " + 
                                "ORDER BY " + 
                                    "LeaveTypeMas.OrderID;";

                //strQuery = "SELECT " +
                //                "EmpLeaveEntitlement.LeaveEntmtID, " +
                //                "EmpMas.EmpID, " +
                //                "LeaveMas.LeaveMasID, " +
                //                "LeaveTypeMas.LeaveTypeID, " +
                //                "LeaveTypeMas.LeaveTypeTitle, " +
                //                "EmpLeaveEntitlement.TotalLeaves, " +
                //                "EmpLeaveEntitlement.BalanceLeaves, " +
                //                "0 As UsedLeaves, " +
                //                "LeaveTypeMas.OrderID " +
                //            "FROM " +
                //                "LeaveTypeMas " +
                //                "INNER JOIN( " +
                //                    "(" +
                //                        "EmpMas " +
                //                        "INNER JOIN LeaveMas ON EmpMas.EmpID = LeaveMas.EmpID " +
                //                    ") " +
                //                    "INNER JOIN EmpLeaveEntitlement ON LeaveMas.LeaveMasID = EmpLeaveEntitlement.LeaveMasID " +
                //                ") ON LeaveTypeMas.LeaveTypeID = EmpLeaveEntitlement.LeaveTypeID " +
                //            "WHERE " +
                //                "(((EmpMas.EmpID) = " + txtEmpID + ") AND ((LeaveMas.LeaveMasID) = " + txtLeaveMasID + ")) " +
                //            "ORDER BY " +
                //                "LeaveTypeMas.OrderID;";

                strQuery = "SELECT " +
                                " qryEmpCrosstabOutstandingLeaveStatement.EmpID, " +
                                " qryEmpCrosstabOutstandingLeaveStatement.EmpCode, " +
                                " qryEmpCrosstabOutstandingLeaveStatement.EmpName, " +
                                " qryEmpCrosstabOutstandingLeaveStatement.DesignationTitle, " +
                                " qryEmpCrosstabOutstandingLeaveStatement.DepartmentTitle, " +
                                " qryEmpCrosstabOutstandingLeaveStatement.[01 - Paid Leave] + qryEmpCrosstabOutstandingLeaveStatement.[02 - Compensatory Off] + qryEmpCrosstabOutstandingLeaveStatement.[03 - Unpaid Leave] + qryEmpCrosstabOutstandingLeaveStatement.[04 - Loss of Pay (LOP) / Leave Without Pay (LWP)] + qryEmpCrosstabOutstandingLeaveStatement.[05 - Sick Leave] + qryEmpCrosstabOutstandingLeaveStatement.[06 - Privilege Leave/Earned Leave] + qryEmpCrosstabOutstandingLeaveStatement.[07 - Casual Leave] + qryEmpCrosstabOutstandingLeaveStatement.[08 - Maternity Leave] + qryEmpCrosstabOutstandingLeaveStatement.[10 - Paternity Leave] + qryEmpCrosstabOutstandingLeaveStatement.[11 - Bereavement Leave] + qryEmpCrosstabOutstandingLeaveStatement.[12 - Public Holiday] + qryEmpCrosstabOutstandingLeaveStatement.[13 - Birthday Leave] AS LeaveBalance " +
                            " FROM " +
                                "qryEmpCrosstabOutstandingLeaveStatement;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                LeaveOutStandingSummaryList = JsonConvert.DeserializeObject<List<LeaveOutStandingSummary>>(DataTableToJSon);
                //foreach (LeaveOutStandingSummary indLeaveOutStandingSummary in LeaveOutStandingSummaryList)
                //{
                //    indLeaveOutStandingSummary.LeaveEntmtID = indLeaveOutStandingSummary.LeaveEntmtID;
                //    indLeaveOutStandingSummary.EmpID = txtEmpID;
                //    indLeaveOutStandingSummary.LeaveMasID = indLeaveOutStandingSummary.LeaveMasID;
                //    indLeaveOutStandingSummary.LeaveTypeID = indLeaveOutStandingSummary.LeaveTypeID;
                //    indLeaveOutStandingSummary.TotalLeaves = indLeaveOutStandingSummary.TotalLeaves;
                //    indLeaveOutStandingSummary.BalanceLeaves = indLeaveOutStandingSummary.BalanceLeaves;
                //    indLeaveOutStandingSummary.UsedLeaves = indLeaveOutStandingSummary.TotalLeaves - indLeaveOutStandingSummary.BalanceLeaves;
                //    indLeaveOutStandingSummary.OrderID = indLeaveOutStandingSummary.OrderID;
                //}
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

            return LeaveOutStandingSummaryList;
        }

        public List<ConsolidatedLeaveOutStandingStatement> getDetailedLeaveStatement(int txtClientID)
        {
            List<ConsolidatedLeaveOutStandingStatement> objConsolidatedLeaveOutStandingStatementList = new List<ConsolidatedLeaveOutStandingStatement>();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "TRANSFORM Sum(EmpLeaveEntitlement.BalanceLeaves) AS SumOfBalanceLeaves " + 
                                            " SELECT " +
                                                " EmpMas.EmpID, " +
                                                " EmpMas.EmpCode, " +
                                                " EmpMas.EmpName, " +
                                                " DesigMas.DesignationTitle, " +
                                                " DepMas.DepartmentTitle " +
                                            " FROM " +
                                                " ClientMas " +
                                                " INNER JOIN ( " +
                                                    " LeaveTypeMas " +
                                                    " INNER JOIN( " +
                                                        " (" +
                                                            " ( " +
                                                                " DesigMas " +
                                                                " INNER JOIN ( " +
                                                                    " DepMas " +
                                                                    " INNER JOIN EmpMas ON DepMas.DepartmentID = EmpMas.DepartmentID " +
                                                                " ) ON DesigMas.DesignationID = EmpMas.EmpDesignationID " +
                                                            " ) " +
                                                            " INNER JOIN EmpLeaveEntitlement ON EmpMas.EmpID = EmpLeaveEntitlement.EmpID " +
                                                        " ) " +
                                                        " INNER JOIN PersonalInfoMas ON EmpMas.EmpID = PersonalInfoMas.EmpID " +
                                                    " ) ON LeaveTypeMas.LeaveTypeID = EmpLeaveEntitlement.LeaveTypeID " +
                                                " ) ON ClientMas.ClientID = EmpMas.ClientID " +
                                            " WHERE " +
                                                " ( " + 
                                                    " ((EmpMas.IsActive) = True) " +
                                                    " AND ((EmpMas.IsDeleted) = False) " +
                                                    " AND ((ClientMas.ClientID) = " + txtClientID + ") " +
                                                " ) " +
                                            " GROUP BY " +
                                                " EmpMas.EmpID, " +
                                                " EmpMas.EmpCode, " +
                                                " EmpMas.EmpName, " +
                                                " DesigMas.DesignationTitle, " +
                                                " DepMas.DepartmentTitle, " +
                                                " EmpMas.IsActive, " +
                                                " EmpMas.IsDeleted, " +
                                                " ClientMas.ClientID " +
                                            " ORDER BY " +
                                                " EmpMas.EmpID PIVOT Format(LeaveTypeMas.LeaveTypeID, '00') & ' - ' & LeaveTypeMas.LeaveTypeTitle;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objConsolidatedLeaveOutStandingStatementList = JsonConvert.DeserializeObject<List<ConsolidatedLeaveOutStandingStatement>>(DataTableToJSon);
                //foreach (LeaveEntitlementInfo indLeaveEntitlementInfo in objConsolidatedLeaveOutStandingStatementList)
                //{
                //    indLeaveEntitlementInfo.LeaveEntmtID = indLeaveEntitlementInfo.LeaveEntmtID;
                //    indLeaveEntitlementInfo.EmpID = CurrentUser.EmpID;
                //    indLeaveEntitlementInfo.LeaveMasID = indLeaveEntitlementInfo.LeaveMasID;
                //    indLeaveEntitlementInfo.LeaveTypeID = indLeaveEntitlementInfo.LeaveTypeID;
                //    indLeaveEntitlementInfo.TotalLeaves = indLeaveEntitlementInfo.TotalLeaves;
                //    indLeaveEntitlementInfo.BalanceLeaves = indLeaveEntitlementInfo.BalanceLeaves;
                //    indLeaveEntitlementInfo.OrderID = indLeaveEntitlementInfo.OrderID;
                //}
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

            return objConsolidatedLeaveOutStandingStatementList;
        }

        public List<LeaveEntitlementInfo> AddNewEntryOnGridEmployeeLeaveEntitilementList(int txtEmpID, int[] intLeaveTypeIDs, int txtNewLeaveTypeID)
        {
            List<LeaveEntitlementInfo> LeaveEntitlementInfoList = new List<LeaveEntitlementInfo>();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT " +
                                    "LeaveTypeMas.LeaveTypeID, " +
                                    "LeaveTypeMas.LeaveTypeTitle " +
                                "FROM " +
                                    "LeaveTypeMas " +
                                "WHERE " +
                                    "(" +
                                        "((LeaveTypeMas.IsActive) = True) " +
                                        "AND ((LeaveTypeMas.IsDelete) = False) " +
                                    ") " +
                                "ORDER BY " +
                                    "LeaveTypeMas.OrderID;";

                strQuery = "SELECT " + 
                                "EmpLeaveEntitlement.LeaveEntmtID, " + 
                                "EmpMas.EmpID, " + 
                                "LeaveMas.LeaveMasID, " + 
                                "LeaveTypeMas.LeaveTypeID, " + 
                                "LeaveTypeMas.LeaveTypeTitle, " + 
                                "EmpLeaveEntitlement.TotalLeaves, " + 
                                "EmpLeaveEntitlement.BalanceLeaves, " + 
                                "LeaveTypeMas.OrderID " + 
                            "FROM " + 
                                "LeaveTypeMas " + 
                                "INNER JOIN( " + 
                                    "(" + 
                                        "EmpMas " + 
                                        "INNER JOIN LeaveMas ON EmpMas.EmpID = LeaveMas.EmpID " + 
                                    ") " + 
                                    "INNER JOIN EmpLeaveEntitlement ON LeaveMas.LeaveMasID = EmpLeaveEntitlement.LeaveMasID " + 
                                ") ON LeaveTypeMas.LeaveTypeID = EmpLeaveEntitlement.LeaveTypeID " + 
                            "WHERE " + 
                                "(" + 
                                    "((EmpMas.EmpID) = " + txtEmpID + ") " + 
                                    "AND ((LeaveTypeMas.LeaveTypeID) IN (" + string.Join(",", intLeaveTypeIDs.Select(x => x.ToString()).ToArray()) + ")) " + 
                                ") " + 
                            "ORDER BY " + 
                                "LeaveTypeMas.OrderID;";

                if (string.Join(",", intLeaveTypeIDs.Select(x => x.ToString()).ToArray()) != "")
                {
                    OleDbCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strQuery;
                    cmd.ExecuteNonQuery();

                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);

                    string DataTableToJSon = "";
                    DataTableToJSon = JsonConvert.SerializeObject(dt);
                    LeaveEntitlementInfoList = JsonConvert.DeserializeObject<List<LeaveEntitlementInfo>>(DataTableToJSon);
                    foreach (LeaveEntitlementInfo indLeaveEntitlementInfo in LeaveEntitlementInfoList)
                    {
                        indLeaveEntitlementInfo.LeaveEntmtID = indLeaveEntitlementInfo.LeaveEntmtID;
                        indLeaveEntitlementInfo.EmpID = txtEmpID;
                        indLeaveEntitlementInfo.LeaveMasID = indLeaveEntitlementInfo.LeaveMasID;
                        indLeaveEntitlementInfo.LeaveTypeID = indLeaveEntitlementInfo.LeaveTypeID;
                        indLeaveEntitlementInfo.TotalLeaves = indLeaveEntitlementInfo.TotalLeaves;
                        indLeaveEntitlementInfo.BalanceLeaves = indLeaveEntitlementInfo.BalanceLeaves;
                        indLeaveEntitlementInfo.OrderID = indLeaveEntitlementInfo.OrderID;
                    }
                }

                List<LeaveTypeInfoModel> newInfo = clsLeaveTypeMas.GetLeaveTypeInfo(txtNewLeaveTypeID);
                LeaveEntitlementInfoList.Add( new LeaveEntitlementInfo
                {
                    LeaveEntmtID = 0,
                    EmpID = txtEmpID,
                    LeaveMasID = 0, // Assuming LeaveMasID will be set later
                    LeaveTypeID = newInfo[0].LeaveTypeID,
                    LeaveTypeTitle = newInfo[0].LeaveTypeTitle,
                    TotalLeaves = 0,
                    BalanceLeaves = 0,
                    OrderID = 0
                });
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

            return LeaveEntitlementInfoList;
        }

        public DataTable GetLeaveEntitlementInfo(int LeaveEntitlementID)
        {
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT * FROM EmpLeaveEntitlement WHERE LeaveEntitlementID = " + LeaveEntitlementID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

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

            return dt;
        }

        public int InsertLeaveEntitlementInfo(int txtEmpID, int txtLeaveMasID, int txtLeaveTypeID, decimal txtTotalLeaves, decimal txtBalanceLeaves, int txtOrderID)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("EmpLeaveEntitlement", "LeaveEntmtID");
                Response<int> txtTempOrderID = objGenFunc.getLeaveMasMaxRowCount(txtLeaveMasID);
                //txtOrderID = objGenFunc.getLeaveMasMaxRowCount(txtLeaveMasID);

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO EmpLeaveEntitlement (LeaveEntmtID, EmpID, LeaveMasID, LeaveTypeID, TotalLeaves, BalanceLeaves, OrderID) VALUES " +
                 "(" + maxRowCount.Data + "," + txtLeaveMasID + ", " + txtEmpID  + ", " + txtLeaveTypeID + "," + txtTotalLeaves + ", " + txtBalanceLeaves + ", " + txtTempOrderID.Data + ")";

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

        public int UpadateLeaveEntitlementInfo(int txtLeaveEntmtID, int txtEmpID, int txtLeaveMasID, int txtLeaveTypeID, decimal txtTotalLeaves, decimal txtBalanceLeaves, int txtOrderID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpLeaveEntitlement SET " +
                "LeaveMasID = " + txtLeaveMasID + ", EmpID = " + txtEmpID + ", LeaveTypeID = " + txtLeaveTypeID + ", TotalLeaves = " + txtTotalLeaves + ", BalanceLeaves = " + txtBalanceLeaves + ", OrderID = " + txtOrderID +
                " WHERE LeaveEntmtID = " + txtLeaveEntmtID;

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

        public int UpadateSpecificLeaveBalanceOnly(int txtLeaveEntmtID, int txtEmpID, decimal txtBalanceLeaves)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpLeaveEntitlement SET " +
                "BalanceLeaves = " + txtBalanceLeaves +
                " WHERE LeaveEntmtID = " + txtLeaveEntmtID + " AND EmpID = " + txtEmpID;

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

        public int DeleteLeaveEntitlementInfo(int txtLeaveEntmtID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE FROM EmpLeaveEntitlement WHERE LeaveEntmtID = " + txtLeaveEntmtID;

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
    }
}
