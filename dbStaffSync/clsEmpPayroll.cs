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
    public class clsEmpPayroll
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset = null;
        clsGenFunc objGenFunc = new clsGenFunc();

        public clsEmpPayroll() { 

        }

        public List<EmployeePaySlipList> getAllEmployeePayslipList()
        {
            List<EmployeePaySlipList> objEmployeePaySlipList = new List<EmployeePaySlipList>();
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();
                DataTable dt = new DataTable();

                string strQuery = "SELECT " +
                                    "EmpMas.EmpID, " +
                                    "EmpMas.EmpCode, " +
                                    "EmpMas.EmpName, " +
                                    "DesigMas.DesignationTitle, " +
                                    "DepMas.DepartmentTitle, " +
                                    "EmpSalMas.EmpSalID, " +
                                    "EmpSalMas.EmpSalDate, " +
                                    "EmpSalMas.EmpSalMonthYear, " +
                                    "EmpSalMas.OrderID " +
                                "FROM " +
                                    "(" +
                                        "DesigMas " +
                                        "INNER JOIN (" +
                                            "DepMas " +
                                            "INNER JOIN EmpMas ON DepMas.DepartmentID = EmpMas.DepartmentID " +
                                        ") ON DesigMas.DesignationID = EmpMas.EmpDesignationID " +
                                    ") " +
                                    "INNER JOIN EmpSalMas ON EmpMas.EmpID = EmpSalMas.EmpID " +
                                "WHERE " +
                                    "(" +
                                        "((EmpSalMas.EmpSalMonthYear) <> 'Jan - 1900') " +
                                        "AND ((EmpMas.IsActive) = True) " +
                                        "AND ((EmpMas.IsDeleted) = False) " +
                                    ");";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objEmployeePaySlipList = JsonConvert.DeserializeObject<List<EmployeePaySlipList>>(DataTableToJSon);
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

            return objEmployeePaySlipList;
        }

        public List<EmployeePayslipMasterDetails> getSelectedSpecificMonthSalaryMasterDetails(int txtEmpID, int txtSelectedSalMonthID)
        {
            List<EmployeePayslipMasterDetails> objEmployeePaySlipList = new List<EmployeePayslipMasterDetails>();
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();
                DataTable dt = new DataTable();

                string strQuery = "SELECT " + 
                                    "EmpSalMas.EmpSalID, " +
                                    "EmpSalMas.EmpSalDate, " +
                                    "EmpSalMas.EmpSalMonthYear, " +
                                    "EmpSalMas.TotalDaysInMonth, " +
                                    "EmpSalMas.TotalDaysWorked, " +
                                    "EmpSalMas.TotalDaysOnLeave, " +
                                    "EmpSalMas.NetPayable, " +
                                    "EmpSalMas.EmpID " +
                                "FROM " +
                                    "EmpSalMas " +
                                "WHERE " +
                                    "(" +
                                        "((EmpSalMas.EmpSalID) = " + txtSelectedSalMonthID + ") " +
                                        "AND ((EmpSalMas.EmpID) = " + txtEmpID + ") " + 
                                    "); ";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objEmployeePaySlipList = JsonConvert.DeserializeObject<List<EmployeePayslipMasterDetails>>(DataTableToJSon);
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

            return objEmployeePaySlipList;
        }

        public List<EmployeePayslipDetails> getSelectedSpecificMonthSalaryDetails(int txtEmpSalID)
        {
            List<EmployeePayslipDetails> objEmployeePaySlipList = new List<EmployeePayslipDetails>();
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();
                DataTable dt = new DataTable();

                string strQuery = "SELECT " +
                                        "EmpSalDetails.EmpSalDetID, " + 
                                        "EmpSalDetails.EmpSalID, " +
                                        "EmpSalDetails.SalProDetID, " +
                                        "EmpSalDetails.SalHeaderID as HeaderID, " + 
                                        "EmpSalDetails.SalHeaderTitle as HeaderTitle, " + 
                                        "EmpSalDetails.SalHeaderType as HeaderType, " +
                                        "EmpSalDetails.CalcFormula, " +
                                        "EmpSalDetails.AllowanceAmount, " + 
                                        "EmpSalDetails.DeductionAmount, " + 
                                        "EmpSalDetails.ReimbursmentAmount, " + 
                                        "EmpSalDetails.OrderID " + 
                                    "FROM " + 
                                        "EmpSalDetails " + 
                                    "WHERE " + 
                                        "(((EmpSalDetails.EmpSalID) = " + txtEmpSalID + ")) " + 
                                    "ORDER BY " + 
                                        "EmpSalDetails.OrderID; ";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objEmployeePaySlipList = JsonConvert.DeserializeObject<List<EmployeePayslipDetails>>(DataTableToJSon);
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

            return objEmployeePaySlipList;
        }


        public int InsertEmployeeSalaryMasterInfo(int txtEmpID, DateTime txtSalaryDate, string txtSalaryMonthYear, decimal txtTotalWorkingDays, decimal txtTotalWorkedDays, decimal txtTotalLeavesTaken, decimal txtTotalAllowance, decimal txtTotalDeduction, decimal txtTotalReimbursement, decimal txtNetPayableAmount)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("EmpSalMas", "EmpSalID");
                Response<int> OrderID = objGenFunc.getEmployeeSpecificOrderID("EmpSalMas", "OrderID", txtEmpID);
                //if (txtOrderID == 0)
                //    OrderID.Data = 0;

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO EmpSalMas (EmpSalID, EmpID, EmpSalDate, EmpSalMonthYear, TotalDaysInMonth, TotalDaysWorked, TotalDaysOnLeave, TotalAllowance, TotalDeduction, TotalReimbursement, NetPayable, OrderID) VALUES " +
                 "(" + maxRowCount.Data + "," + txtEmpID + ",'" + txtSalaryDate.ToString("dd-MMM-yyyy") + "','" + txtSalaryMonthYear + "'," + txtTotalWorkingDays + "," + txtTotalWorkedDays + "," + txtTotalLeavesTaken + "," + txtTotalAllowance + "," + txtTotalDeduction + "," + txtTotalReimbursement + "," + txtNetPayableAmount  + "," + OrderID.Data + ")";

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

        public int UpdateEmployeeSalaryMasterInfo(int txtEmpSalID, int txtEmpID, DateTime txtSalaryDate, string txtSalaryMonthYear, decimal txtTotalWorkingDays, decimal txtTotalWorkedDays, decimal txtTotalLeavesTaken, decimal txtTotalAllowance, decimal txtTotalDeduction, decimal txtTotalReimbursement, decimal txtNetPayableAmount)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpSalMas SET EmpSalDate = '" + txtSalaryDate.ToString("dd-MMM-yyyy") + "', EmpSalMonthYear = '" + txtSalaryMonthYear + "', TotalDaysInMonth = " + txtTotalWorkingDays + ", " +
                    " TotalDaysWorked = " + txtTotalWorkedDays + ", TotalDaysOnLeave = " + txtTotalLeavesTaken + ", TotalAllowance = " + txtTotalAllowance + ", TotalDeduction = " + txtTotalDeduction + ", TotalReimbursement = " + txtTotalReimbursement +  ", NetPayable = " + txtNetPayableAmount +
                    " WHERE EmpSalID = " + txtEmpSalID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtEmpSalID;
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

        public int InsertEmployeeSalaryDetailsInfo(int txtEmpSalID, int txtSalProDetID, int txtSalHeaderID, string txtSalHeaderTitle, string txtSalSalHeaderType, string txtCalcFormula, decimal txtAllAmount, decimal txtDedAmount, decimal txtReimbAmount, int txtOrderID)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("EmpSalDetails", "EmpSalDetID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO EmpSalDetails (EmpSalDetID, EmpSalID, SalProDetID, SalHeaderID, SalHeaderTitle, SalHeaderType, CalcFormula, AllowanceAmount, DeductionAmount, ReimbursmentAmount, OrderID) VALUES " +
                 "(" + maxRowCount.Data + "," + txtEmpSalID + "," + txtSalProDetID + "," + txtSalHeaderID + ",'" + txtSalHeaderTitle + "','" + txtSalSalHeaderType + "','" + txtCalcFormula + "'," + txtAllAmount + "," + txtDedAmount + "," + txtReimbAmount + "," + txtOrderID + ")";

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

        public int UpdateEmployeeSalaryDetailsInfo(int txtEmpSalDetID, int txtEmpSalID, int txtSalProDetID, int txtSalHeaderID, string txtSalHeaderTitle, string txtSalSalHeaderType, string txtCalcFormula, decimal txtAllAmount, decimal txtDedAmount, decimal txtReimbAmount, int txtOrderID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpSalDetails SET EmpSalID = " + txtEmpSalID + ", SalProDetID = " + txtSalProDetID + ", SalHeaderID = " + txtSalHeaderID + ", SalHeaderTitle = '" + txtSalHeaderTitle + "', SalHeaderType = '" + txtSalSalHeaderType + "', CalcFormula = '" + txtCalcFormula + "'," +
                    " AllowanceAmount = " + txtAllAmount + ", DeductionAmount = " + txtDedAmount + ", ReimbursmentAmount = " + txtReimbAmount + ", OrderID = " + txtOrderID +
                    " WHERE EmpSalDetID = " + txtEmpSalDetID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtEmpSalDetID;
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
