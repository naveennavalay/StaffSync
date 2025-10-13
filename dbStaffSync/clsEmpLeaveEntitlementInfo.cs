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
