using ModelStaffSync;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public class clsEmployeeMaster
    {
        myDBClass objDBClass = new myDBClass();
        OleDbConnection conn = null;
        DataSet dtDataset;
        public List<ReportingManagerInfo> objReportingManagerInfo { get; set; }

        public clsEmployeeMaster() { 

        }

        public int getMaxRowCount(string tableName, string ColumnName, int CurrentCompanyID)
        {
            int rowCount = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT MAX(" + ColumnName.ToString().Trim() + ") FROM " + tableName;
                //string strQuery = "SELECT MAX(EmpCode) FROM EMPMAS WHERE ClientID = " + CurrentCompanyID;

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

        public List<ReportingManagerInfo> getCompleteEmployeesList()
        {
            List<ReportingManagerInfo> employeesList = new List<ReportingManagerInfo>();

            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT * FROM EmpMasInfo WHERE IsActive = true AND IsDeleted = false and ClientID = " + CurrentUser.ClientID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                employeesList = JsonConvert.DeserializeObject<List<ReportingManagerInfo>>(DataTableToJSon);
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

            return employeesList;
        }

        public DataTable GetEmployeeList()
        {
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "SELECT * FROM EMPMas WHERE IsActive = true AND IsDeleted = false";

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

        public DataTable GetEmployeeList(string filterText)
        {
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "SELECT * FROM EMPMas WHERE IsActive = true AND IsDeleted = false AND EmpName LIKE '" + filterText + "%'";

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


        public List<LoggedInUser> getMyEmployeeInformation(int txtEmpID)
        {
            List<LoggedInUser> objEmployeePaySlipList = new List<LoggedInUser>();
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT " +
                                        "EmpMas.EmpID, " +
                                        "EmpMas.EmpCode, " +
                                        "EmpMas.EmpName, " +
                                        "DesigMas.DesignationTitle, " +
                                        "DepMas.DepartmentTitle, " +
                                        "BloodGroupMas.BloodGroupTitle, " +
                                        "PersonalInfoMas.DOB AS DOB, " +
                                        "PersonalInfoMas.DOJ AS DOJ, " +
                                        "AddressMas.Address1, " +
                                        "AddressMas.Address2, " +
                                        "AddressMas.Area, " +
                                        "AddressMas.City, " +
                                        "StateMas.StateTitle, " +
                                        "AddressMas.PIN, " +
                                        "CountryMas.CountryTitle, " +
                                        "SexMas.SexTitle, " +
                                        "NomineeMas.NomineePerson, " +
                                        "RelationShipMas.RelationShipTitle, " +
                                        "PersonalInfoMas.ContactNumber1, " +
                                        "PersonalInfoMas.ContactNumber2, " +
                                        "EmpBankInfo.EmpACNumber, " +
                                        "BankMasInfo.BankName, " +
                                        "BankMasInfo.BankAddress, " +
                                        "BankMasInfo.IFSCCode, " +
                                        "LeaveMas.BalanceLeaves " +
                                    "FROM " +
                                        "(" +
                                            "RelationShipMas " +
                                            "INNER JOIN(" +
                                                "(" +
                                                    "StateMas " +
                                                    "INNER JOIN ( " +
                                                        "SexMas " +
                                                        "INNER JOIN ( " +
                                                            "(" +
                                                                "(" +
                                                                    "DesigMas " +
                                                                    "INNER JOIN ( " +
                                                                        "DepMas " +
                                                                        "INNER JOIN ( " +
                                                                            "BloodGroupMas " +
                                                                            "INNER JOIN EmpMas ON BloodGroupMas.BloodGroupID = EmpMas.BloodGroupID " +
                                                                        ") ON DepMas.DepartmentID = EmpMas.DepartmentID " +
                                                                    ") ON DesigMas.DesignationID = EmpMas.EmpDesignationID " +
                                                                ") " +
                                                                "INNER JOIN LeaveMas ON EmpMas.EmpID = LeaveMas.EmpID " +
                                                            ") " +
                                                            "INNER JOIN(" +
                                                                "CountryMas " +
                                                                "INNER JOIN ( " +
                                                                    "AddressMas " +
                                                                    "INNER JOIN PersonalInfoMas ON AddressMas.AddressID = PersonalInfoMas.PerAddressID " +
                                                                ") ON CountryMas.CountryID = AddressMas.CountryID " +
                                                            ") ON EmpMas.EmpID = PersonalInfoMas.EmpID " +
                                                        ") ON SexMas.SexID = PersonalInfoMas.SexID " +
                                                    ") ON StateMas.StateID = AddressMas.StateID " +
                                                ") " +
                                                "INNER JOIN NomineeMas ON EmpMas.EmpID = NomineeMas.EmpID " +
                                            ") ON RelationShipMas.RelationShipID = NomineeMas.RelationShipID " +
                                        ") " +
                                        "INNER JOIN ( " +
                                            "BankMasInfo " +
                                            "INNER JOIN EmpBankInfo ON BankMasInfo.BankID = EmpBankInfo.BankID " +
                                        ") ON EmpMas.EmpID = EmpBankInfo.EmpID " +
                                    "WHERE " +
                                        "(((EmpMas.EmpID) = " + txtEmpID + ")); ";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objEmployeePaySlipList = JsonConvert.DeserializeObject<List<LoggedInUser>>(DataTableToJSon);

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

        public EmployeeInfo GetSelectedEmployeeInfo(int EmployeeID)
        {
            EmployeeInfo employeeInfo = new EmployeeInfo();

            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "SELECT * FROM EmpMas WHERE EmpID = " + EmployeeID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                List<EmployeeInfo> objEmployeeInfo = JsonConvert.DeserializeObject<List<EmployeeInfo>>(DataTableToJSon); 
                if(objEmployeeInfo.Count > 0)
                {
                    employeeInfo.EmpID = objEmployeeInfo[0].EmpID;
                    employeeInfo.EmpCode = objEmployeeInfo[0].EmpCode;
                    employeeInfo.EmpName = objEmployeeInfo[0].EmpName;
                    employeeInfo.EmpDesignationID = objEmployeeInfo[0].EmpDesignationID;
                    employeeInfo.EmpRepManID = objEmployeeInfo[0].EmpRepManID;
                    employeeInfo.DepartmentID = objEmployeeInfo[0].DepartmentID;
                    employeeInfo.BloodGroupID = objEmployeeInfo[0].BloodGroupID;
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

            return employeeInfo;
        }

        public ReportingManagerInfo GetEmployeesList()
        {
            ReportingManagerInfo reportingManagerInfo = new ReportingManagerInfo();

            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "SELECT * FROM EMPMasInfo ORDER BY EmpID;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                List<ReportingManagerInfo> objReportingManagerInfo = JsonConvert.DeserializeObject<List<ReportingManagerInfo>>(DataTableToJSon);
                if (objReportingManagerInfo.Count > 0)
                {
                    reportingManagerInfo.EmpID = objReportingManagerInfo[0].EmpID;
                    reportingManagerInfo.EmpCode = objReportingManagerInfo[0].EmpCode;
                    reportingManagerInfo.EmpName = objReportingManagerInfo[0].EmpName;
                    reportingManagerInfo.DesignationTitle = objReportingManagerInfo[0].DesignationTitle;
                    reportingManagerInfo.DepartmentTitle = objReportingManagerInfo[0].DepartmentTitle;
                    reportingManagerInfo.ContactNumber1 = objReportingManagerInfo[0].ContactNumber1;
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

            return reportingManagerInfo;
        }

        public ReportingManagerInfo GetReportingManagerInfo(int EmployeeID)
        {
            ReportingManagerInfo reportingManagerInfo = new ReportingManagerInfo();

            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "SELECT * FROM EMPMasInfo WHERE EmpID = " + EmployeeID + "";
                //EmpID, EmpCode, EmpName, DesignationTitle, Department, ContactNumber1

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                List<ReportingManagerInfo> objReportingManagerInfo = JsonConvert.DeserializeObject<List<ReportingManagerInfo>>(DataTableToJSon); 
                if(objReportingManagerInfo.Count > 0)
                {
                    reportingManagerInfo.EmpID = objReportingManagerInfo[0].EmpID;
                    reportingManagerInfo.EmpCode = objReportingManagerInfo[0].EmpCode;
                    reportingManagerInfo.EmpName = objReportingManagerInfo[0].EmpName;
                    reportingManagerInfo.DesignationTitle = objReportingManagerInfo[0].DesignationTitle;
                    reportingManagerInfo.DepartmentTitle = objReportingManagerInfo[0].DepartmentTitle;
                    reportingManagerInfo.ContactNumber1= objReportingManagerInfo[0].ContactNumber1;
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

            return reportingManagerInfo;
        }

        public int InsertEmployeeMaster(int txtEmployeeID, string txtEmployeeCode, string txtEmployeeTitle, int txtEmployeeDesignationID, int txtReportingManagerID, int txtEmployeeDepartmentID, int txtEmployeeBloodGroupID, bool IsActive, bool IsDeleted, int txtClientID)
        {
            int affectedRows = 0;
            try
            {

                int maxRowCount = getMaxRowCount("EmpMas", "EmpID", txtClientID);
                
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO EmpMas (EmpID, EmpCode, EmpName, EmpDesignationID, EmpRepManID, DepartmentID, BloodGroupID, IsActive, IsDeleted, ClientID) VALUES " +
                 "(" + maxRowCount + ",'" + "EMP-" + (maxRowCount).ToString().PadLeft(4, '0').Trim() + "','" + txtEmployeeTitle.Trim() + "'," + txtEmployeeDesignationID + "," + txtReportingManagerID + 
                 "," + txtEmployeeDepartmentID + "," + txtEmployeeBloodGroupID + "," + IsActive + "," + IsDeleted + "," + txtClientID + ")";

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

        public int UpdateEmployeeMaster(int txtEmployeeID, string txtEmployeeCode, string txtEmployeeTitle, int txtEmployeeDesignationID, int txtReportingManagerID, int txtEmployeeDepartmentID, int txtEmployeeBloodGroupID, bool IsActive, bool IsDeleted, int txtClientID)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpMas SET EmpCode = '" + txtEmployeeCode + "', EmpName = '" + txtEmployeeTitle + "', EmpDesignationID = " + txtEmployeeDesignationID + ", EmpRepManID = " + txtReportingManagerID + ", DepartmentID = " + txtEmployeeDepartmentID + ", BloodGroupID = " + txtEmployeeBloodGroupID + ", IsActive = " + IsActive + ", ClientID = " + txtClientID +
                 " WHERE EmpID = " + txtEmployeeID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtEmployeeID;
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

        public int DeleteEmployeeMaster(int txtEmployeeID)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE FROM EmpMas WHERE EmpID = " + txtEmployeeID;

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

    //public class EmployeeInfo
    //{
    //    public int EmpID { get; set; }
    //    public string EmpCode { get; set; }
    //    public string EmpName { get; set; }
    //    public int EmpDesignationID { get; set; }
    //    public string DesignationTitle { get; set; }
    //    public int EmpRepManID { get; set; }
    //    public int DepartmentID { get; set; }
    //    public string DepartmentTitle { get; set; }
    //    public int BloodGroupID { get; set; }
    //    public string BloodGroupTitle { get; set; }
    //    public bool IsActive { get; set; }
    //    public bool IsDeleted { get; set; }
    //}

    //public class ReportingManagerInfo
    //{
    //    public int EmpID { get; set; }

    //    [DisplayName("Employee Code")]
    //    public string EmpCode { get; set; }

    //    [DisplayName("Employee Name")]
    //    public string EmpName { get; set; }

    //    [DisplayName("Designation")]
    //    public string DesignationTitle { get; set; }

    //    [DisplayName("Department")]
    //    public string DepartmentTitle { get; set; }

    //    [DisplayName("Contact Number")]
    //    public string ContactNumber1 { get; set; }

    //    [DisplayName("Mail ID")]
    //    public string ContactNumber2 { get; set; }
    //}
}
