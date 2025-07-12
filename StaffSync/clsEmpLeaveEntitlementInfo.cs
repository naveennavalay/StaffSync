//using C1.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaffSync
{
    public class clsEmpLeaveEntitlementInfo
    {
        myDBClass objDBClass = new myDBClass();
        clsLeaveTypeMas clsLeaveTypeMas = new clsLeaveTypeMas();
        OleDbConnection conn = null;
        DataSet dtDataset;

        public clsEmpLeaveEntitlementInfo() { 

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
                if (ex.Message.ToString().ToLower() == "Specified cast is not valid.".ToLower())
                {
                    rowCount = 1;
                }
                else
                {
                    MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return rowCount;
        }

        public int getLeaveMasMaxRowCount(int txtLeaveMasID)
        {
            int rowCount = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT " +
                                        "MAX(OrderID) AS OrderID " + 
                                    "FROM " + 
                                        "EmpLeaveEntitlement " + 
                                    "WHERE " + 
                                        "(((EmpLeaveEntitlement.LeaveMasID) = " + txtLeaveMasID + "));";

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
                if (ex.Message.ToString().ToLower() == "Specified cast is not valid.".ToLower())
                {
                    rowCount = 1;
                }
                else
                {
                    MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return rowCount;
        }

        public List<LeaveEntitlementInfo> getDefaultLeaveEntitilementList()
        {
            List<LeaveEntitlementInfo> LeaveEntitlementInfoList = new List<LeaveEntitlementInfo>();

            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();
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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return LeaveEntitlementInfoList;
        }

        public List<LeaveEntitlementInfo> getEmployeeLeaveEntitilementList(int txtEmpID, int txtLeaveMasID)
        {
            List<LeaveEntitlementInfo> LeaveEntitlementInfoList = new List<LeaveEntitlementInfo>();

            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();
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
                    indLeaveEntitlementInfo.OrderID = indLeaveEntitlementInfo.OrderID;
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

            return LeaveEntitlementInfoList;
        }

        public List<LeaveEntitlementInfo> AddNewEntryOnGridEmployeeLeaveEntitilementList(int txtEmpID, int[] intLeaveTypeIDs, int txtNewLeaveTypeID)
        {
            List<LeaveEntitlementInfo> LeaveEntitlementInfoList = new List<LeaveEntitlementInfo>();

            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();
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
                                    "AND ((LeaveTypeMas.LeaveTypeID) IN (" + string.Join(",", intLeaveTypeIDs.ToArray()) + ")) " + 
                                ") " + 
                            "ORDER BY " + 
                                "LeaveTypeMas.OrderID;";

                if (string.Join(",", intLeaveTypeIDs.ToArray()) != "")
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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return LeaveEntitlementInfoList;
        }

        public DataTable GetLeaveEntitlementInfo(int LeaveEntitlementID)
        {
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return dt;
        }

        public int InsertLeaveEntitlementInfo(int txtLeaveMasID, int txtLeaveTypeID, decimal txtTotalLeaves, decimal txtBalanceLeaves, int txtOrderID)
        {
            int affectedRows = 0;
            try
            {
                int maxRowCount = getMaxRowCount("EmpLeaveEntitlement", "LeaveEntmtID");
                txtOrderID = getLeaveMasMaxRowCount(txtLeaveMasID);

                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO EmpLeaveEntitlement (LeaveEntmtID, LeaveMasID, LeaveTypeID, TotalLeaves, BalanceLeaves, OrderID) VALUES " +
                 "(" + maxRowCount + "," + txtLeaveMasID + ", " + txtLeaveTypeID + "," + txtTotalLeaves + ", " + txtBalanceLeaves + ", " + txtOrderID + ")";

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

        public int UpadateLeaveEntitlementInfo(int txtLeaveEntmtID, int txtLeaveMasID, int txtLeaveTypeID, decimal txtTotalLeaves, decimal txtBalanceLeaves, int txtOrderID)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpLeaveEntitlement SET " +
                "LeaveMasID = " + txtLeaveMasID + ", LeaveTypeID = " + txtLeaveTypeID + ", TotalLeaves = " + txtTotalLeaves + ", BalanceLeaves = " + txtBalanceLeaves + ", OrderID = " + txtOrderID +
                " WHERE LeaveEntmtID = " + txtLeaveEntmtID;

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

        public int DeleteLeaveEntitlementInfo(int txtLeaveEntmtID)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE FROM EmpLeaveEntitlement WHERE LeaveEntmtID = " + txtLeaveEntmtID;

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
    }

    public class LeaveEntitlementInfo
    {
        public int LeaveEntmtID { get; set; }
        public int EmpID { get; set; }
        public int LeaveMasID { get; set; }
        public int LeaveTypeID { get; set; }

        [DisplayName("Leave Type")]
        public string LeaveTypeTitle { get; set; }
        
        [DisplayName("Total Leaves Allotted")] 
        public decimal TotalLeaves { get; set; }

        [DisplayName("Total Leaves Available")] 
        public decimal BalanceLeaves { get; set; }
        public int OrderID { get; set; }
    }
}
