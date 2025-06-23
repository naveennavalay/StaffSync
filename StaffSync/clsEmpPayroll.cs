using C1.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaffSync
{
    public class clsEmpPayroll
    {
        myDBClass objDBClass = new myDBClass();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsStates objState = new clsStates();
        clsCountries objCountry = new clsCountries();

        public clsEmpPayroll() { 

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

        public int getEmployeeSpecificOrderID(string tableName, string ColumnName, int EmpID)
        {
            int rowCount = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT MAX(OrderID) FROM " + tableName + " WHERE EmpID = " + EmpID;

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

        public List<EmployeePaySlipList> getAllEmployeePayslipList()
        {
            List<EmployeePaySlipList> objEmployeePaySlipList = new List<EmployeePaySlipList>();
            try
            {
                conn = objDBClass.openDBConnection();
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
                                        "INNER JOIN(" +
                                            "DepMas " +
                                            "INNER JOIN EmpMas ON DepMas.DepartmentID = EmpMas.DepartmentID " +
                                        ") ON DesigMas.DesignationID = EmpMas.EmpDesignationID " +
                                    ") " +
                                    "INNER JOIN EmpSalMas ON EmpMas.EmpID = EmpSalMas.EmpID " +
                                "WHERE " +
                                    "(" +
                                        "((EmpMas.IsActive) = True) " +
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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return objEmployeePaySlipList;
        }

        public List<EmployeePayslipMasterDetails> getSelectedSpecificMonthSalaryMasterDetails(int txtEmpID, int txtSelectedSalMonthID)
        {
            List<EmployeePayslipMasterDetails> objEmployeePaySlipList = new List<EmployeePayslipMasterDetails>();
            try
            {
                conn = objDBClass.openDBConnection();
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
                                        "((EmpSalMas.EmpSalID) = " + txtEmpID + ") " +
                                        "AND ((EmpSalMas.EmpID) = " + txtSelectedSalMonthID + ") " + 
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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return objEmployeePaySlipList;
        }

        public List<EmployeePayslipDetails> getSelectedSpecificMonthSalaryDetails(int txtEmpSalID)
        {
            List<EmployeePayslipDetails> objEmployeePaySlipList = new List<EmployeePayslipDetails>();
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();
                DataTable dt = new DataTable();

                string strQuery = "SELECT * FROM EmpSalDetails WHERE EmpSalID = " + txtEmpSalID + " ORDER BY OrderID";

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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return objEmployeePaySlipList;
        }


        public int InsertEmployeeSalaryMasterInfo(int txtEmpID, DateTime txtSalaryDate, string txtSalaryMonthYear, decimal txtTotalWorkingDays, decimal txtTotalWorkedDays, decimal txtTotalLeavesTaken, decimal txtNetPayableAmount, int txtOrderID)
        {
            int affectedRows = 0;
            try
            {
                int maxRowCount = getMaxRowCount("EmpSalMas", "EmpSalID");
                int OrderID = getEmployeeSpecificOrderID("EmpSalMas", "OrderID", txtEmpID);
                if (txtOrderID == 0)
                    OrderID = 0;

                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO EmpSalMas (EmpSalID, EmpID, EmpSalDate, EmpSalMonthYear, TotalDaysInMonth, TotalDaysWorked, TotalDaysOnLeave, NetPayable, OrderID) VALUES " +
                 "(" + maxRowCount + "," + txtEmpID + ",'" + txtSalaryDate + "','" + txtSalaryMonthYear + "'," + txtTotalWorkingDays + "," + txtTotalWorkedDays + "," + txtTotalLeavesTaken + "," + txtNetPayableAmount  + "," + OrderID + ")";

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

        public int UpdateEmployeeSalaryMasterInfo(int txtEmpSalID, int txtEmpID, DateTime txtSalaryDate, string txtSalaryMonthYear, double txtTotalWorkingDays, double txtTotalWorkedDays, double txtTotalLeavesTaken, double txtNetPayableAmount)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpSalMas SET EmpSalDate = '" + txtSalaryDate + "', EmpSalMonthYear = '" + txtSalaryMonthYear + "', TotalDaysInMonth = " + txtTotalWorkingDays + ", " +
                    " TotalDaysWorked = " + txtTotalWorkedDays + ", TotalDaysOnLeave = " + txtTotalLeavesTaken + ", NetPayable = " + txtNetPayableAmount +
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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }
            return affectedRows;
        }

        public int InsertEmployeeSalaryDetailsInfo(int txtEmpSalID, int txtSalProDetID, string txtSalHeaderTitle, string txtSalSalHeaderType, decimal txtAllAmount, decimal txtDedAmount, decimal txtReimbAmount, int txtOrderID)
        {
            int affectedRows = 0;
            try
            {
                int maxRowCount = getMaxRowCount("EmpSalDetails", "EmpSalDetID");

                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO EmpSalDetails (EmpSalDetID, EmpSalID, SalProDetID, SalHeaderTitle, SalHeaderType, AllowanceAmount, DeductionAmount, ReimbursmentAmount, OrderID) VALUES " +
                 "(" + maxRowCount + "," + txtEmpSalID + "," + txtSalProDetID + ",'" + txtSalHeaderTitle + "','" + txtSalSalHeaderType + "'," + txtAllAmount + "," + txtDedAmount + "," + txtReimbAmount + "," + txtOrderID + ")";

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


        public int UpdateEmployeeSalaryDetailsInfo(int txtEmpSalDetID, int txtEmpSalID, int txtSalProDetID, string txtSalHeaderTitle, string txtSalSalHeaderType, decimal txtAllAmount, decimal txtDedAmount, decimal txtReimbAmount, int txtOrderID)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpSalDetails SET EmpSalID = " + txtEmpSalID + ", SalProDetID = " + txtSalProDetID + ", SalHeaderTitle = '" + txtSalHeaderTitle + "', SalHeaderType = '" + txtSalSalHeaderType + "'," +
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

    public class EmployeePaySlipList
    {
        [DisplayName("EmpID")]
        public int EmpID { get; set; }
        
        [DisplayName("Employee Code")] 
        public string EmpCode { get; set; }
        
        [DisplayName("Employee Name")] 
        public string EmpName { get; set; }

        [DisplayName("Designation")] 
        public string DesignationTitle { get; set; }
        
        [DisplayName("Department")]
        public string DepartmentTitle { get; set; }
        
        [DisplayName("EmpSalID")]
        public int EmpSalID { get; set; }

        [DisplayName("Salary Date")]
        public DateTime EmpSalDate { get; set; }

        [DisplayName("Salary Month")]
        public string EmpSalMonthYear { get; set; }

        [DisplayName("SalaryOrderID")]
        public int OrderID { get; set; }
    }

    public class EmployeePayslipMasterDetails
    {
        public int EmpSalID { get; set; }
        public DateTime EmpSalDate { get; set; }
        public string EmpSalMonthYear { get; set; }
        public double TotalDaysInMonth { get; set; }
        public double TotalDaysWorked { get; set; }
        public double TotalDaysOnLeave { get; set; }
        public double NetPayable { get; set; }
        public int EmpID { get; set; }
    }

    public class EmployeePayslipDetails
    {
        public int EmpSalDetID { get; set; }
        public int SalProDetID { get; set; }
        public int EmpSalID { get; set; }

        [DisplayName("Salary Header")]
        public string SalHeaderTitle { get; set; }
        
        [DisplayName("Type")] 
        public string SalHeaderType { get; set; }

        [DisplayName("Allowance Amount")]
        public decimal AllowanceAmount { get; set; }
        
        [DisplayName("Deduction Amount")] 
        public decimal DeductionAmount { get; set; }
        
        [DisplayName("Reimbursment Amount")] 
        public decimal ReimbursmentAmount { get; set; }
        public int OrderID { get; set; }
    }


}
