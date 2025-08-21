//using C1.Framework;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.OleDb;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaffSync
{
    public class clsLeaveTRList
    {
        myDBClass objDBClass = new myDBClass();
        OleDbConnection conn = null;
        DataSet dtDataset = null;

        public clsLeaveTRList() { 

        }

        public int getMaxRowCount(string tableName, string ColumnName)
        {
            int rowCount = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT Max(" + ColumnName.ToString().Trim() + ") FROM " + tableName;

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

        public int getEmployeeSpecificOrderID(string tableName, string ColumnName, int EmpID)
        {
            int rowCount = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT Max(OrderID) FROM " + tableName + " WHERE EmpID = " + EmpID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                int maxRow = (int)cmd.ExecuteScalar();
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

        public int getMaxLeaveMasID(int txtEmpID)
        {
            int MaxLeaveMasID = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT " + 
                                        "Last(LeaveMas.LeaveMasID) AS LeaveMasID " + 
                                    "FROM " + 
                                        "LeaveMas " + 
                                    "WHERE " + 
                                        "(((LeaveMas.EmpID) = " + txtEmpID + ")) " + 
                                    "ORDER BY " + 
                                        "Last(LeaveMas.EffectiveDate); ";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                MaxLeaveMasID = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                if (ex.Message.ToString().ToLower() == "Specified cast is not valid.".ToLower())
                {
                    MaxLeaveMasID = 1;
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

            return Convert.ToInt16(MaxLeaveMasID.ToString());
        }

        public decimal getBalanceLeave(int EmpID)
        {
            string BalanceLeave = "0.00";
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT BalanceLeaves FROM LeaveMas WHERE EmpID = " + EmpID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                if(cmd.ExecuteScalar() != null)
                    BalanceLeave = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                if (ex.Message.ToString().ToLower() == "Specified cast is not valid.".ToLower())
                {
                    BalanceLeave = "0.00";
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

            return Convert.ToDecimal(BalanceLeave.ToString());
        }

        public decimal getSpecificLeaveTypeBalance(int txtLeaveMasID, int txtLeaveTypeID)
        {
            string BalanceLeave = "0.00";
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT " +
                                        "BalanceLeaves " + 
                                    "FROM " + 
                                        "EmpLeaveEntitlement " + 
                                    "WHERE " + 
                                        "(" + 
                                            "((LeaveMasID) = " + txtLeaveMasID + ") " + 
                                            "AND ((LeaveTypeID) = " + txtLeaveTypeID + ") " + 
                                        ")";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                if (cmd.ExecuteScalar() != null)
                    BalanceLeave = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                if (ex.Message.ToString().ToLower() == "Specified cast is not valid.".ToLower())
                {
                    BalanceLeave = "0.00";
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

            return Convert.ToDecimal(BalanceLeave.ToString());
        }

        public List<PendingLeaveApprovalList> getPendingLeaveApprovalList()
        {
            List<PendingLeaveApprovalList> empPendingLeaveApprovalList = new List<PendingLeaveApprovalList>();

            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT * FROM qryDailyLeaveRequest ORDER BY EmpID, LeaveTRID ASC;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                empPendingLeaveApprovalList = JsonConvert.DeserializeObject<List<PendingLeaveApprovalList>>(DataTableToJSon);
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

            return empPendingLeaveApprovalList;
        }

        public List<BulkPendingLeaveApproval> getBulkPendingLeaveApprovalList()
        {
            List<BulkPendingLeaveApproval> empPendingLeaveApprovalList = new List<BulkPendingLeaveApproval>();

            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT * FROM qryAllEmpLeavePendingStatement ORDER BY EmpID, LeaveTRID ASC;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                empPendingLeaveApprovalList = JsonConvert.DeserializeObject<List<BulkPendingLeaveApproval>>(DataTableToJSon);
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

            return empPendingLeaveApprovalList;
        }

        public List<BulkPendingLeaveApproval> getRejectedLeavelList()
        {
            List<BulkPendingLeaveApproval> empPendingLeaveApprovalList = new List<BulkPendingLeaveApproval>();

            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT * FROM qryAllEmpLeaveRejectedStatement ORDER BY EmpID, LeaveTRID ASC;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                empPendingLeaveApprovalList = JsonConvert.DeserializeObject<List<BulkPendingLeaveApproval>>(DataTableToJSon);
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

            return empPendingLeaveApprovalList;
        }

        public List<BulkPendingLeaveApproval> getConsolidatedLeaveStatement()
        {
            List<BulkPendingLeaveApproval> empPendingLeaveApprovalList = new List<BulkPendingLeaveApproval>();

            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT * FROM qryAllEmpLeaveRejectedStatement ORDER BY EmpID, LeaveTRID ASC;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                empPendingLeaveApprovalList = JsonConvert.DeserializeObject<List<BulkPendingLeaveApproval>>(DataTableToJSon);
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

            return empPendingLeaveApprovalList;
        }

        public List<OutstandingLeaveStatement> getOutStandingLeaveStaetment()
        {
            List<OutstandingLeaveStatement> empOutStandingLeaveStatement = new List<OutstandingLeaveStatement>();

            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT * FROM qryOutStandingLeaves ORDER BY EmpID ASC;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                empOutStandingLeaveStatement = JsonConvert.DeserializeObject<List<OutstandingLeaveStatement>>(DataTableToJSon);
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

            return empOutStandingLeaveStatement;
        }


        public List<EmployeeSpecificLeaveInfo> getSpecificEmployeeSpecificLeaveInfo(int LeaveTRID)
        {
            List<EmployeeSpecificLeaveInfo> employeeLeaveTRList = new List<EmployeeSpecificLeaveInfo>();

            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT * FROM EmpLeaveTransMas WHERE LeaveTRID = " + LeaveTRID + " ORDER BY OrderID ASC;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                employeeLeaveTRList = JsonConvert.DeserializeObject<List<EmployeeSpecificLeaveInfo>>(DataTableToJSon);
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

            return employeeLeaveTRList;
        }

        public List<EmployeeLeaveTRList> getEmployeeLeaveTRList(int txtEmpID)
        {
            List<EmployeeLeaveTRList> employeeLeaveTRList = new List<EmployeeLeaveTRList>();

            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT * FROM qryEmpLeaveTRList WHERE EmpID = " + txtEmpID + " ORDER BY OrderID ASC;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                employeeLeaveTRList = JsonConvert.DeserializeObject<List<EmployeeLeaveTRList>>(DataTableToJSon);
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

            return employeeLeaveTRList;
        }

        public List<EmployeeLeaveTRList> getAllEmployeesLeaveStatement(int txtEmpID)
        {
            List<EmployeeLeaveTRList> employeeLeaveTRList = new List<EmployeeLeaveTRList>();
            List<EmployeeLeaveTRList> EmployeeLeaveStatements = new List<EmployeeLeaveTRList>();

            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                //string strQuery = "SELECT * FROM qryAllEmpLeaveStatement WHERE EmpID = " + txtEmpID + " ORDER BY AttDate, OrderID ASC;";

                string strQuery = "SELECT " +
                                    "EmpMas.EmpID, " +
                                    "EmpMas.EmpCode, " +
                                    "EmpMas.EmpName, " +
                                    "DesigMas.DesignationTitle, " +
                                    "DepMas.DepartmentTitle, " +
                                    "EmpDailyAttendanceInfo.AttID, " +
                                    "EmpDailyAttendanceInfo.AttDate, " +
                                    "EmpDailyAttendanceInfo.AttStatus, " +
                                    "EmpDailyAttendanceInfo.LeaveTRID, " +
                                    "0 AS LeaveTypeID, " +
                                    "'' AS LeaveTypeTitle, " +
                                    "'' AS LeaveStatus, " +
                                    "'1900-01-01' AS LeaveAppliedDate, " +
                                    "'' AS LeaveComments, " +
                                    "null AS ActualLeaveDateFrom, " +
                                    "null AS ActualLeaveDateTo, " +
                                    "0 AS LeaveDuration, " +
                                    "null AS LeaveApprovedDate, " +
                                    "'' AS LeaveApprovalComments, " +
                                    "null AS LeaveRejectedDate, " +
                                    "'' AS LeaveRejectionComments, " +
                                    "false AS Canceled, " +
                                    "null AS CanceledDate, " +
                                    "0 AS OrderID " +
                                "FROM " +
                                    "(" +
                                        "DesigMas " +
                                        "INNER JOIN (" +
                                            "DepMas " +
                                            "INNER JOIN EmpMas ON DepMas.DepartmentID = EmpMas.DepartmentID " +
                                        ") ON DesigMas.DesignationID = EmpMas.EmpDesignationID " +
                                    ") " +
                                    "INNER JOIN EmpDailyAttendanceInfo ON EmpMas.EmpID = EmpDailyAttendanceInfo.EmpID " +
                                " WHERE " +
                                    "( " +
                                        " ((EmpMas.EmpID) = " + txtEmpID + ") " +
                                        " AND ((EmpDailyAttendanceInfo.LeaveTRID) = 0) " +
                                    ") " +
                                "ORDER BY " +
                                    "EmpMas.EmpID, " +
                                    "EmpDailyAttendanceInfo.AttID";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                employeeLeaveTRList = JsonConvert.DeserializeObject<List<EmployeeLeaveTRList>>(DataTableToJSon);
                foreach (EmployeeLeaveTRList indEmployeeLeaveTRList in employeeLeaveTRList)
                {
                    EmployeeLeaveStatements.Add(new EmployeeLeaveTRList
                    {
                        EmpID = indEmployeeLeaveTRList.EmpID,
                        EmpCode = indEmployeeLeaveTRList.EmpCode,
                        EmpName = indEmployeeLeaveTRList.EmpName,
                        DesignationTitle = indEmployeeLeaveTRList.DesignationTitle,
                        DepartmentTitle = indEmployeeLeaveTRList.DepartmentTitle,
                        AttDate = indEmployeeLeaveTRList.AttDate,
                        AttStatus = indEmployeeLeaveTRList.AttStatus,
                        LeaveTypeID = indEmployeeLeaveTRList.LeaveTypeID,
                        LeaveTypeTitle = indEmployeeLeaveTRList.LeaveTypeTitle,
                        ActualLeaveDateFrom = indEmployeeLeaveTRList.ActualLeaveDateFrom,
                        ActualLeaveDateTo = indEmployeeLeaveTRList.ActualLeaveDateTo,
                        LeaveDuration = indEmployeeLeaveTRList.LeaveDuration,
                        LeaveComments = indEmployeeLeaveTRList.LeaveComments,
                        LeaveApprovalComments = indEmployeeLeaveTRList.LeaveApprovalComments,
                        LeaveRejectionComments = indEmployeeLeaveTRList.LeaveRejectionComments,
                        LeaveStatus = indEmployeeLeaveTRList.LeaveStatus,
                        OrderID = indEmployeeLeaveTRList.OrderID
                    });
                }

                strQuery = "SELECT " +
                                "EmpMas.EmpID, " +
                                "EmpMas.EmpCode, " +
                                "EmpMas.EmpName, " +
                                "DesigMas.DesignationTitle, " +
                                "DepMas.DepartmentTitle, " +
                                "EmpDailyAttendanceInfo.AttID, " +
                                "EmpDailyAttendanceInfo.AttDate, " +
                                "EmpDailyAttendanceInfo.AttStatus, " +
                                "LeaveTypeMas.LeaveTypeID, " +
                                "LeaveTypeMas.LeaveTypeTitle, " +
                                "EmpDailyAttendanceInfo.LeaveTRID, " +
                                "EmpLeaveTransMas.LeaveAppliedDate, " +
                                "EmpLeaveTransMas.LeaveComments, " +
                                "'' AS LeaveStatus, " +
                                "EmpLeaveTransMas.ActualLeaveDateFrom, " +
                                "EmpLeaveTransMas.ActualLeaveDateTo, " +
                                "EmpLeaveTransMas.LeaveDuration, " +
                                "EmpLeaveTransMas.LeaveApprovedDate, " +
                                "EmpLeaveTransMas.LeaveApprovalComments, " +
                                "EmpLeaveTransMas.LeaveRejectedDate, " +
                                "EmpLeaveTransMas.LeaveRejectionComments, " +
                                "EmpLeaveTransMas.Canceled, " +
                                "EmpLeaveTransMas.CanceledDate, " +
                                "EmpLeaveTransMas.OrderID " +
                            "FROM " +
                                "LeaveTypeMas " +
                                "INNER JOIN ( " +
                                     "DesigMas " +
                                     "INNER JOIN ( " +
                                          "DepMas " +
                                          "INNER JOIN ( " +
                                               "EmpMas " +
                                               "INNER JOIN ( " +
                                                    "EmpLeaveTransMas " +
                                                    "INNER JOIN EmpDailyAttendanceInfo ON EmpLeaveTransMas.LeaveTRID = EmpDailyAttendanceInfo.LeaveTRID " +
                                               ") ON EmpMas.EmpID = EmpLeaveTransMas.EmpID " +
                                          ") ON DepMas.DepartmentID = EmpMas.DepartmentID " +
                                     ") ON DesigMas.DesignationID = EmpMas.EmpDesignationID " +
                                ") ON LeaveTypeMas.LeaveTypeID = EmpLeaveTransMas.LeaveTypeID " +
                            " WHERE " +
                                "( " +
                                     "((EmpMas.EmpID) = " + txtEmpID + ") " +
                                     "AND ((EmpDailyAttendanceInfo.LeaveTRID) <> 0) " +
                                     "AND ((EmpLeaveTransMas.OrderID) <> 0) " +
                                ") " +
                            " ORDER BY " +
                                "EmpMas.EmpID, " +
                                "EmpDailyAttendanceInfo.AttID, " +
                                "EmpDailyAttendanceInfo.LeaveTRID, " +
                                "EmpLeaveTransMas.OrderID;";

                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                dt = new DataTable();
                da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                employeeLeaveTRList = new List<EmployeeLeaveTRList>();
                employeeLeaveTRList = JsonConvert.DeserializeObject<List<EmployeeLeaveTRList>>(DataTableToJSon);
                foreach (EmployeeLeaveTRList indEmployeeLeaveTRList in employeeLeaveTRList)
                {
                    EmployeeLeaveStatements.Add(new EmployeeLeaveTRList
                    {
                        EmpID = indEmployeeLeaveTRList.EmpID,
                        EmpCode = indEmployeeLeaveTRList.EmpCode,
                        EmpName = indEmployeeLeaveTRList.EmpName,
                        DesignationTitle = indEmployeeLeaveTRList.DesignationTitle,
                        DepartmentTitle = indEmployeeLeaveTRList.DepartmentTitle,
                        AttDate = indEmployeeLeaveTRList.AttDate,
                        AttStatus = indEmployeeLeaveTRList.AttStatus,
                        LeaveTypeID = indEmployeeLeaveTRList.LeaveTypeID,
                        LeaveTypeTitle = indEmployeeLeaveTRList.LeaveTypeTitle,
                        ActualLeaveDateFrom = indEmployeeLeaveTRList.ActualLeaveDateFrom,
                        ActualLeaveDateTo = indEmployeeLeaveTRList.ActualLeaveDateTo,
                        LeaveDuration = indEmployeeLeaveTRList.LeaveDuration,
                        LeaveComments = indEmployeeLeaveTRList.LeaveComments,
                        LeaveApprovalComments = indEmployeeLeaveTRList.LeaveRejectionComments.ToString().StartsWith("Rejected") ? "" : indEmployeeLeaveTRList.LeaveApprovalComments,
                        LeaveRejectionComments = indEmployeeLeaveTRList.LeaveApprovalComments.ToString().StartsWith("Approved") ? "" : indEmployeeLeaveTRList.LeaveRejectionComments,
                        LeaveStatus = indEmployeeLeaveTRList.LeaveStatus,
                        OrderID = indEmployeeLeaveTRList.OrderID
                    });
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

            return EmployeeLeaveStatements;
        }

        public List<EmployeeOOOList> GetEmployeeOOOList()
        {
            List<EmployeeOOOList> EmpOOOList = new List<EmployeeOOOList>();
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "SELECT * FROM qryEmpOOOList WHERE ActualLeaveDateFrom = Date()";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                EmpOOOList = JsonConvert.DeserializeObject<List<EmployeeOOOList>>(DataTableToJSon);
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

            return EmpOOOList;
        }

        public bool AttendanceExistsForToday(int txtEmpID, DateTime dtDate)
        {
            bool attendanceExists = false;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();
                string strQuery = "SELECT COUNT(AttID) FROM EmpMas " +
                    " INNER JOIN EmpDailyAttendanceInfo ON EmpMas.EmpID = EmpDailyAttendanceInfo.EmpID " + 
                    " WHERE " + 
                    " ( ( " + 
                    " (EmpDailyAttendanceInfo.AttDate) = #" + dtDate.ToString("dd-MMM-yyyy") + "#" +
                    " )" +
                    " AND ((EmpMas.EmpID) = " +  txtEmpID + "));";


                strQuery = "SELECT Count(AttID) AS AttIDCount " +
                                   " FROM EmpMas INNER JOIN (EmpDailyAttendanceInfo INNER JOIN EmpLeaveTransMas ON EmpDailyAttendanceInfo.LeaveTRID = EmpLeaveTransMas.LeaveTRID) ON EmpMas.EmpID = EmpDailyAttendanceInfo.EmpID " + 
                           " WHERE " + 
                                   " ( " +
                                       " ( " + 
                                            " (EmpDailyAttendanceInfo.AttDate) = #" + dtDate.ToString("dd-MMM-yyyy") + "#" +
                                        " ) " + 
                                        " AND ((EmpMas.EmpID) = " + txtEmpID + ") " + 
                                        " AND ((EmpLeaveTransMas.Canceled) = false) " + 
                                    ")";
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                int count = (int)cmd.ExecuteScalar();
                
                if (count > 0)
                    attendanceExists = true;
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
            return attendanceExists;
        }

        public int InsertDefaultLeaveAllotment(int txtEmpID, decimal TotalLeaves, decimal TotalBalanceLeave, DateTime txtEffectiveDate)
        {
            int affectedRows = 0;
            try
            {
                int maxRowCount = getMaxRowCount("LeaveMas", "LeaveMasID");
                
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO LeaveMas (LeaveMasID, EmpID, TotalLeaves, BalanceLeaves, EffectiveDate) VALUES " +
                 "(" + maxRowCount + "," + txtEmpID + "," + TotalLeaves + "," + TotalBalanceLeave + ", '" + txtEffectiveDate.ToString("dd-MMM-yyyy") + "')";

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

        public int UpdateEmployeeLeaveBalance(int txtLeaveMasID, int txtEmpID, decimal TotalLeaves, decimal TotalBalanceLeave, DateTime txtEffectiveDate)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                //TotalLeaves = " + TotalLeaves + ", 
                string strQuery = "UPDATE LeaveMas SET BalanceLeaves = " + TotalBalanceLeave + ", EffectiveDate = '" + txtEffectiveDate.ToString("dd-MMM-yyyy") + "'" +
                " WHERE LeaveMasID = " + txtLeaveMasID + " AND EmpID = " + txtEmpID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtLeaveMasID;
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

        public int UpdateSpecificLeaveTypeBalance(int txtLeaveMasID, int txtLeaveTypeID, decimal TotalBalanceLeave)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpLeaveEntitlement SET BalanceLeaves = " + TotalBalanceLeave + " WHERE LeaveMasID = " + txtLeaveMasID + " AND LeaveTypeID = " + txtLeaveTypeID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtLeaveMasID;
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

        public int InsertLeaveTransaction(int txtEmpID, int txtLeaveTypeID, DateTime txtLeaveAppliedDate, string txtLeaveComments, DateTime txtLeaveFromDate, DateTime txtLeaveToDate, decimal txtLeaveDuration, string txtLeaveMode, DateTime txtLeaveApprovedDate, string txtLeaveApprovalComments, DateTime txtLeaveRejectedDate, string txtLeaveRejectionComment, int txtApproverID)
        {
            int affectedRows = 0;
            try
            {
                int maxRowCount = getMaxRowCount("EmpLeaveTransMas", "LeaveTRID");
                int maxLeaveCounter = getEmployeeSpecificOrderID("EmpLeaveTransMas", "OrderID", txtEmpID);
                if (txtLeaveComments.Trim().ToLower() == "By Leave Allotment".Trim().ToLower())
                    maxLeaveCounter = 0;

                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO EmpLeaveTransMas (LeaveTRID, EmpID, LeaveTypeID, LeaveAppliedDate, LeaveComments, ActualLeaveDateFrom, ActualLeaveDateTo, LeaveDuration, LeaveMode, LeaveApprovedDate, LeaveApprovalComments, LeaveRejectedDate, LeaveRejectionComments, ApprovedOrRejectedByEmpID, OrderID, Canceled, CanceledDate) VALUES " +
                 "(" + maxRowCount + "," + txtEmpID + "," + txtLeaveTypeID + ",'" + DateTime.Now.ToString("dd-MMM-yyyy") + "','" + txtLeaveComments + "','" + txtLeaveFromDate.ToString("dd-MMM-yyyy") + "','" + txtLeaveToDate.ToString("dd-MMM-yyyy") + "'," + txtLeaveDuration + ",'" + txtLeaveMode + "','" + txtLeaveApprovedDate.ToString("dd-MMM-yyyy") + "','" + txtLeaveApprovalComments +"','" + txtLeaveRejectedDate.ToString("dd-MMM-yyyy") + "','" + txtLeaveRejectionComment + "'," + txtApproverID + "," + maxLeaveCounter + ", false, '" + DateTime.Now.ToString("dd-MMM-yyyy") + "')";

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

        public int UpdateLeaveTransaction(int txtLeaveTRID, int txtEmpID, int txtLeaveTypeID, DateTime txtLeaveAppliedDate, string txtLeaveComments, DateTime txtLeaveFromDate, DateTime txtLeaveToDate, decimal txtLeaveDuration, string txtLeaveMode, DateTime txtLeaveApprovedDate, string txtLeaveApprovalComments, DateTime txtLeaveRejectedDate, string txtLeaveRejectionComment, int txtApproverID)
        {
            int affectedRows = 0;
            try
            {
                int maxLeaveCounter = getEmployeeSpecificOrderID("EmpLeaveTransMas", "OrderID", txtEmpID);

                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpLeaveTransMas LeaveTypeID = " + txtLeaveTypeID + ", LeaveAppliedDate = '" + DateTime.Now.ToString("dd-MMM-yyyy") + "', LeaveComments = '" + txtLeaveComments + "', ActualLeaveDateFrom = '" + txtLeaveFromDate.ToString("dd-MMM-yyyy") + "', ActualLeaveDateTo = '" + txtLeaveToDate.ToString("dd-MMM-yyyy") + "', LeaveDuration = " + txtLeaveDuration + ", LeaveMode = '" + txtLeaveMode + "', LeaveApprovedDate = '" + txtLeaveApprovedDate.ToString("dd-MMM-yyyy") + "', LeaveApprovalComments = '" + txtLeaveApprovalComments + "', LeaveRejectedDate = '" + txtLeaveRejectedDate.ToString("dd-MMM-yyyy") + "', LeaveRejectionComments = '" + txtLeaveRejectionComment + "', ApprovedOrRejectedByEmpID = " + txtApproverID + ", OrderID = " + maxLeaveCounter +
                 " WHERE LeaveTRID = " + txtLeaveTRID + " AND EmpID = " + txtEmpID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtLeaveTRID;
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

        public int CancelLeaveTransaction(int txtLeaveTRID, int txtEmpID, string txtLeaveComments)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpLeaveTransMas SET Canceled = true, CanceledDate = '" + DateTime.Now.ToString("dd-MMM-yyyy") + "', LeaveComments = 'Cancelling the Leave Request'" +
                 " WHERE LeaveTRID = " + txtLeaveTRID + " AND EmpID = " + txtEmpID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtLeaveTRID;
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

        public int RejectLeaveTransaction(int txtLeaveTRID, int txtEmpID, string txtLeaveComments)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpLeaveTransMas SET LeaveDuration = 0, LeaveApprovalComments = 'Request Approved', LeaveRejectedComments = 'Request Approved'" +
                 " WHERE LeaveTRID = " + txtLeaveTRID + " AND EmpID = " + txtEmpID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtLeaveTRID;
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

        public int ApproveLeave(int txtLeaveTRID, int txtEmpID, string txtLeaveApprovalComments, int txtApproverID)
        {
            int affectedRows = 0;
            try
            {
                if (txtApproverID == 0)
                    txtApproverID = 1;

                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpLeaveTransMas SET LeaveApprovedDate = '" + DateTime.Now.ToString("dd-MMM-yyyy") + "', LeaveApprovalComments = 'Approved : " + txtLeaveApprovalComments + "', LeaveRejectionComments ='Not Rejected', ApprovedOrRejectedByEmpID = " + txtApproverID + 
                 " WHERE LeaveTRID = " + txtLeaveTRID + " AND EmpID = " + txtEmpID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtLeaveTRID;
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

        public int RejectLeave(int txtLeaveTRID, int txtEmpID, string txtLeaveRejectionComments, int txtApproverID)
        {
            int affectedRows = 0;
            try
            {
                if (txtApproverID == 0)
                    txtApproverID = 1;

                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                //LeaveDuration = 0,
                string strQuery = "UPDATE EmpLeaveTransMas SET LeaveApprovedDate = '" + DateTime.Now.ToString("dd-MMM-yyyy") + "', LeaveApprovalComments = 'Rejected : Request Approved', LeaveRejectedDate = '" + DateTime.Now.ToString("dd-MMM-yyyy") + "', LeaveRejectionComments = 'Rejected : " + txtLeaveRejectionComments + "', ApprovedOrRejectedByEmpID = " + txtApproverID +
                 " WHERE LeaveTRID = " + txtLeaveTRID + " AND EmpID = " + txtEmpID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtLeaveTRID;
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

        public int ApproveLeaveCancellation(int txtLeaveTRID, int txtEmpID, string txtLeaveRejectionComments, int txtApproverID)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                //LeaveDuration = 0,
                string strQuery = "UPDATE EmpLeaveTransMas SET LeaveApprovedDate = '" + DateTime.Now.ToString("dd-MMM-yyyy") + "', LeaveApprovalComments = 'Approved : Request Approved', LeaveRejectedDate = '" + DateTime.Now.ToString("dd-MMM-yyyy") + "', LeaveRejectionComments = '', ApprovedOrRejectedByEmpID = " + txtApproverID +
                 " WHERE LeaveTRID = " + txtLeaveTRID + " AND EmpID = " + txtEmpID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtLeaveTRID;
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

    public class BulkPendingLeaveApproval
    {
        public bool Select { get; set; }
        public int EmpID { get; set; }

        [DisplayName("Employee Code")]
        public string EmpCode { get; set; }
        
        [DisplayName("Employee Name")] 
        public string EmpName { get; set; }
        
        [DisplayName("Designation")] 
        public string DesignationTitle { get; set; }

        [DisplayName("Department")] 
        public string DepartmentTitle { get; set; }

        [DisplayName("Leave Type ID")]
        public int LeaveTypeID { get; set; }
        
        [DisplayName("Leave Type")] 
        public string LeaveTypeTitle { get; set; }

        [DisplayName("Leave Trans ID")] 
        public int LeaveTRID { get; set; }

        [DisplayName("Leave From")]
        public DateTime ActualLeaveDateFrom { get; set; }
        
        [DisplayName("Leave To")] 
        public DateTime ActualLeaveDateTo { get; set; }

        [DisplayName("Leave Duration")] 
        public double LeaveDuration { get; set; }

        [DisplayName("Leave Mode")]
        public string LeaveMode { get; set; }

        [DisplayName("Leave Comments")] 
        public string LeaveComments { get; set; }

        [DisplayName("Leave Approval Comments")]
        public string LeaveApprovalComments { get; set; }

        [DisplayName("Leave Rejection Comments")] 
        public string LeaveRejectionComments { get; set; }

        [DisplayName("Leave Cancelled")] 
        public bool Canceled { get; set; }

        [DisplayName("Leave Cancelled Date")]
        public DateTime CanceledDate { get; set; }

        [DisplayName("OrderID")] 
        public int OrderID { get; set; }
    }

    public class EmployeeLeaveTRList
    {
        public int LeaveTRID { get; set; }
        
        [DisplayName("Employee ID")] 
        public int EmpID { get; set; }
        
        [DisplayName("Employee Code")] 
        public string EmpCode { get; set; }
        
        [DisplayName("Employee Name")] 
        public string EmpName { get; set; }

        [DisplayName("Designation")] 
        public string DesignationTitle { get; set; }
        
        [DisplayName("Department")] 
        public string DepartmentTitle { get; set; }

        [DisplayName("Attendance Date")] 
        public DateTime? AttDate { get; set; }

        [DisplayName("Attendance Status")]
        public string AttStatus { get; set; }

        [DisplayName("Leave Type ID")]
        public int LeaveTypeID { get; set; }

        [DisplayName("Leave Type")]
        public string LeaveTypeTitle { get; set; }

        [DisplayName("Leave Applied Date")] 
        public DateTime LeaveAppliedDate { get; set; }
        
        [DisplayName("Leave Comments")] 
        public string LeaveComments { get; set; }

        [DisplayName("Leave From")] 
        public DateTime? ActualLeaveDateFrom { get; set; }

        [DisplayName("Leave To")] 
        public DateTime? ActualLeaveDateTo { get; set; }

        [DisplayName("Leave Duration")] 
        public float LeaveDuration { get; set; }

        [DisplayName("Leave Mode")]
        public string LeaveMode { get; set; }

        public DateTime? LeaveApprovedDate { get; set; }

        [DisplayName("Approval Comments")]
        public string LeaveApprovalComments { get; set; }
        public DateTime? LeaveRejectedDate { get; set; }

        [DisplayName("Rejection Comments")] 
        public string LeaveRejectionComments { get; set; }
        public int OrderID { get; set; }
        public int ApprovedOrRejectedByEmpID { get; set; }
        public string LeaveStatus { get; set; }
        
        [DisplayName("Cancelled")] 
        public bool Canceled { get; set; }
        
        [DisplayName("Cancelled Date")] 
        public DateTime? CanceledDate { get; set; }
    }

    public class EmployeeOOOList
    {
        public int EmpID { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string DesignationTitle { get; set; }
        public string DepartmentTitle { get; set; }
        public string LeaveTypeTitle { get; set; }
        public DateTime ActualLeaveDateFrom { get; set; }
        public DateTime ActualLeaveDateTo { get; set; }
        public decimal LeaveDuration { get; set; }
        public string LeaveMode { get; set; }
        public int OrderID { get; set; }
    }

    public class EmployeeSpecificLeaveInfo
    {
        public int LeaveTRID { get; set; }
        public int EmpID { get; set; }
        public int LeaveTypeID { get; set; }
        public DateTime LeaveAppliedDate { get; set; }
        public string LeaveComments { get; set; }
        public DateTime LeaveApprovedDate { get; set; }
        public string LeaveApprovalComments { get; set; }
        public DateTime ActualLeaveDateFrom { get; set; }
        public DateTime ActualLeaveDateTo { get; set; }
        public double LeaveDuration { get; set; }
        public string LeaveMode { get; set; }
        public DateTime LeaveRejectedDate { get; set; }
        public string LeaveRejectionComments { get; set; }
        public int ApprovedOrRejectedByEmpID { get; set; }
        public int OrderID { get; set; }
    }

    public class PendingLeaveApprovalList
    {
        //[DisplayName("Select")]
        //public bool Select { get; set; }

        public int EmpID { get; set; }

        [DisplayName("Employee Code")]
        public string EmpCode { get; set; }

        [DisplayName("Employee Name")]
        public string EmpName { get; set; }
        
        [DisplayName("Designation")] 
        public string DesignationTitle { get; set; }

        [DisplayName("Department")]
        public string DepartmentTitle { get; set; }
        
        public int LeaveTRID { get; set; }

        public int LeaveTypeID { get; set; }
        
        [DisplayName("Leave Type")]
        public string LeaveTypeTitle { get; set; }

        [DisplayName("Applied Date")]
        public DateTime LeaveAppliedDate { get; set; }

        [DisplayName("Reason")] 
        public string LeaveComments { get; set; }

        [DisplayName("Leave From")]
        public DateTime ActualLeaveDateFrom { get; set; }

        [DisplayName("Leave Till")]
        public DateTime ActualLeaveDateTo { get; set; }

        [DisplayName("Duration")]
        public string LeaveDuration { get; set; }

        [DisplayName("Leave Mode")]
        public string LeaveMode { get; set; }
    }

    public class OutstandingLeaveStatement
    {
        public bool Select { get; set; }
        public int EmpID { get; set; }

        [DisplayName("Employee Code")]
        public string EmpCode { get; set; }

        [DisplayName("Employee Name")]
        public string EmpName { get; set; }

        [DisplayName("Designation")]
        public string DesignationTitle { get; set; }

        [DisplayName("Department")]
        public string DepartmentTitle { get; set; }

        [DisplayName("Total Leaves")]
        public float TotalLeaves { get; set; }

        [DisplayName("Balance Leaves")]
        public float BalanceLeaves { get; set; }

        [DisplayName("Utilised Leaves")]
        public float UtilisedLeaves { get; set; }
    }

}
