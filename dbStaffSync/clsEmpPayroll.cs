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
        clsSalaryProfile objSalaryProfile = new clsSalaryProfile();
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

        public List<SalaryProfileInfo> getSelectedSpecificMonthSalaryDetails(int txtEmpID, int txtEmpSalID)
        {
            //List<EmployeePayslipDetails> objEmployeePaySlipList = new List<EmployeePayslipDetails>();
            //List<EmployeePayslipDetails> objReturnSalaryProfileInfoList = new List<EmployeePayslipDetails>();
            List<SalaryProfileInfo> objEmployeePaySlipList = new List<SalaryProfileInfo>();
            List<SalaryProfileInfo> objReturnSalaryProfileInfoList = new List<SalaryProfileInfo>();

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
                                        "EmpSalDetails.AllowanceAmount AS ActualAmount, " +
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
                objEmployeePaySlipList = JsonConvert.DeserializeObject<List<SalaryProfileInfo>>(DataTableToJSon);
                foreach (SalaryProfileInfo indSalaryProfileInfo in objEmployeePaySlipList)
                {
                    objReturnSalaryProfileInfoList.Add(new SalaryProfileInfo
                    {
                        EmpSalDetID = indSalaryProfileInfo.EmpSalDetID,
                        SalProDetID = indSalaryProfileInfo.SalProDetID,
                        SalProfileID = indSalaryProfileInfo.SalProfileID,
                        HeaderID = indSalaryProfileInfo.HeaderID,
                        HeaderTitle = indSalaryProfileInfo.HeaderTitle,
                        HeaderType = indSalaryProfileInfo.HeaderType,
                        CalcFormula = indSalaryProfileInfo.CalcFormula,
                        ActualAmount = objSalaryProfile.getOriginalSalaryActualAmount(txtEmpID, indSalaryProfileInfo.HeaderID, indSalaryProfileInfo.HeaderType),
                        AllowanceAmount = indSalaryProfileInfo.AllowanceAmount,
                        DeductionAmount = indSalaryProfileInfo.DeductionAmount,
                        ReimbursmentAmount = indSalaryProfileInfo.ReimbursmentAmount,
                        OrderID = indSalaryProfileInfo.OrderID
                    });
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

            return objReturnSalaryProfileInfoList;
        }

        public bool IsMasterInfoFound(int txtEmpID, DateTime txtMasterDataDate)
        {
            bool MasterInfoFound = false;

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();
                DataTable dt = new DataTable();

                string strQuery = "SELECT " +
                                        " EmpSalMas.EmpID " +
                                    " FROM " + 
                                        " EmpSalMas " + 
                                    " WHERE " + 
                                          " EmpSalMas.EmpSalDate = #" + txtMasterDataDate.ToString("dd-MMM-yyyy") + "# AND EmpSalMas.EmpID = " + txtEmpID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                //object result = cmd.ExecuteScalar();
                int a = Common.DBCommon.ExecuteScalarInt(cmd);


                //int a = result == null ? 0 : Convert.ToInt32(result);
                if (a > 0)
                    MasterInfoFound = true;
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

            return MasterInfoFound;
        }

        public bool IsSalaryAlreadyProcessed(int txtEmpID, DateTime txtMasterDataDate, string txtSalMonthYear)
        {
            bool MasterInfoFound = false;

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();
                DataTable dt = new DataTable();

                string strQuery = "SELECT " + 
                                          " EmpSalMas.EmpSalID " +
                                        " FROM " + 
                                            " EmpSalMas " + 
                                        " WHERE " + 
                                            " EmpSalMas.EmpSalMonthYear = '" + txtSalMonthYear + "'" + 
                                            " AND EmpSalMas.EmpID = " + txtEmpID;
                                            //" EmpSalMas.EmpSalDate = #" + txtMasterDataDate.ToString("dd-MMM-yyyy") + "# " + 

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                object result = cmd.ExecuteScalar();
                int a = result == null ? 0 : Convert.ToInt32(result);

                if (a > 0)
                    MasterInfoFound = true;
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

            return MasterInfoFound;
        }

        public int InsertEmployeeSalaryMasterInfo(int txtEmpID, DateTime txtSalaryDate, string txtSalaryMonthYear, decimal txtTotalWorkingDays, decimal txtTotalWorkedDays, decimal txtTotalLeavesTaken, decimal txtTotalUnpaidDaysLeave, decimal txtTotalPayableDays, decimal txtBasicPay, decimal txtBasicPerDay, decimal txtBasicPerHour, decimal txtTotalAllowance, decimal txtTotalDeduction, decimal txtTotalReimbursement, decimal txtNetPayableAmount, bool boolStructureEntry)
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

                string strQuery = "INSERT INTO EmpSalMas (EmpSalID, EmpID, EmpSalDate, EmpSalMonthYear, TotalDaysInMonth, TotalDaysWorked, TotalDaysOnLeave, TotalUnpaidDaysLeave, TotalPayableDays, BasicPay, BasicPerDay, BasicPerHour, TotalAllowance, TotalDeduction, TotalReimbursement, NetPayable, OrderID, StructureEntry) VALUES " +
                 "(" + maxRowCount.Data + "," + txtEmpID + ",'" + txtSalaryDate.ToString("dd-MMM-yyyy") + "','" + txtSalaryMonthYear + "'," + txtTotalWorkingDays + "," + txtTotalWorkedDays + "," + txtTotalLeavesTaken + "," + txtTotalUnpaidDaysLeave + "," + txtTotalPayableDays + "," + txtBasicPay  + "," + txtBasicPerDay + "," + txtBasicPerHour + "," + txtTotalAllowance + "," + txtTotalDeduction + "," + txtTotalReimbursement + "," + txtNetPayableAmount  + "," + OrderID.Data + ", " + boolStructureEntry + ")";

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

        public int UpdateEmployeeSalaryMasterInfo(int txtEmpSalID, int txtEmpID, DateTime txtSalaryDate, string txtSalaryMonthYear, decimal txtTotalWorkingDays, decimal txtTotalWorkedDays, decimal txtTotalLeavesTaken, decimal txtTotalUnpaidDaysLeave, decimal txtTotalPayableDays, decimal txtBasicPay, decimal txtBasicPerDay, decimal txtBasicPerHour, decimal txtTotalAllowance, decimal txtTotalDeduction, decimal txtTotalReimbursement, decimal txtNetPayableAmount, bool boolStructureEntry)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpSalMas SET EmpSalDate = '" + txtSalaryDate.ToString("dd-MMM-yyyy") + "', EmpSalMonthYear = '" + txtSalaryMonthYear + "', TotalDaysInMonth = " + txtTotalWorkingDays + ", " +
                    " TotalDaysWorked = " + txtTotalWorkedDays + ", TotalDaysOnLeave = " + txtTotalLeavesTaken + ", TotalUnpaidDaysLeave = " + txtTotalUnpaidDaysLeave + ", TotalPayableDays = " + txtTotalPayableDays + ", " +
                    " BasicPay = " + txtBasicPay + ", BasicPerDay = " + txtBasicPerDay + ", BasicPerHour = " + txtBasicPerDay + ", " +
                    " TotalAllowance = " + txtTotalAllowance + ", TotalDeduction = " + txtTotalDeduction + ", TotalReimbursement = " + txtTotalReimbursement +  ", NetPayable = " + txtNetPayableAmount + ", StructureEntry = " + boolStructureEntry +
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
