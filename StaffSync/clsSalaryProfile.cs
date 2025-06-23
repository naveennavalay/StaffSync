using C1.Framework;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static C1.Util.Win.Win32;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.MonthCalendar;

namespace StaffSync
{
    public class clsSalaryProfile
    {
        myDBClass objDBClass = new myDBClass();
        OleDbConnection conn = null;
        DataSet dtDataset;

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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return rowCount;
        }

        public SpecificEmployeeSalaryProfileInfo getEmployeeSpecificSalaryProfile(int txtEmpID)
        {
            SpecificEmployeeSalaryProfileInfo specificEmployeeSalaryProfileInfo = new SpecificEmployeeSalaryProfileInfo();
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();
                DataTable dt = new DataTable();

                string strQuery = "SELECT " +
                                    "EmpSalProfileInfo.EmpSalProID, " +
                                    "EmpSalProfileInfo.SalProfileID " +
                                "FROM " +
                                    "EmpSalProfileInfo " +
                                "WHERE " +
                                    "(" +
                                        "((EmpSalProfileInfo.EmpID) = " + txtEmpID + ") " +
                                        "AND((EmpSalProfileInfo.IsActive) = True) " +
                                        "AND((EmpSalProfileInfo.IsDeleted) = False) " +
                                        "AND((EmpSalProfileInfo.IsDefault) = True) " +
                                    ")";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                List<SpecificEmployeeSalaryProfileInfo> objEmployeeSalaryProfileInfo = JsonConvert.DeserializeObject<List<SpecificEmployeeSalaryProfileInfo>>(DataTableToJSon);
                if (objEmployeeSalaryProfileInfo.Count > 0)
                {
                    specificEmployeeSalaryProfileInfo.EmpID = objEmployeeSalaryProfileInfo[0].EmpID;
                    specificEmployeeSalaryProfileInfo.EmpSalProfileID = objEmployeeSalaryProfileInfo[0].EmpSalProfileID;
                    specificEmployeeSalaryProfileInfo.SalProfileID = objEmployeeSalaryProfileInfo[0].SalProfileID;
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

            return specificEmployeeSalaryProfileInfo;
        }

        public DataTable GetSalProfileTitleList()
        {
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "SELECT * FROM SalProfileMas WHERE IsActive = true AND IsDeleted = false";

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

        public List<SalaryProfileInfo> GetDefaultSalaryProfileInfo(int SalaryProfileID)
        {

            List<SalaryProfileInfo> objSalaryProfileInfo = new List<SalaryProfileInfo>();
            List<SalaryProfileInfo> objReturnSalaryProfileInfoList = new List<SalaryProfileInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "SELECT " +
                                    "SalProfileDetails.SalProDetID, " +
                                    "SalProfileDetails.SalProfileID, " +
                                    "AllowanceHeaderMas.AllID AS HeaderID, " +
                                    "AllowanceHeaderMas.AllTitle AS HeaderTitle " +
                                "FROM " +
                                    "SalProfileDetails " +
                                    "INNER JOIN AllowanceHeaderMas ON SalProfileDetails.AllID = AllowanceHeaderMas.AllID " +
                                "WHERE " +
                                    "(" +
                                        "((SalProfileDetails.SalProfileID) = " + SalaryProfileID + ") " +
                                        "AND ((AllowanceHeaderMas.IsActive) = True) " +
                                        "AND ((AllowanceHeaderMas.IsDeleted) = False) " +
                                    ") " +
                                "ORDER BY " +
                                    "AllowanceHeaderMas.OrderID";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objSalaryProfileInfo = JsonConvert.DeserializeObject<List<SalaryProfileInfo>>(DataTableToJSon);
                foreach (SalaryProfileInfo indSalaryProfileInfo in objSalaryProfileInfo)
                {
                    objReturnSalaryProfileInfoList.Add(new SalaryProfileInfo
                    {
                        EmpSalDetID = 0,
                        SalProDetID = indSalaryProfileInfo.SalProDetID,
                        SalProfileID = indSalaryProfileInfo.SalProfileID,
                        HeaderID = indSalaryProfileInfo.HeaderID,
                        HeaderTitle = indSalaryProfileInfo.HeaderTitle,
                        SalHeaderType = "Allowences",
                        AllowanceAmount = 0.00,
                        DeductionAmount = 0.00,
                        ReimbursmentAmount = 0.00
                    });
                }

                strQuery = "SELECT " + 
                                "SalProfileDetails.SalProDetID, " +
                                "SalProfileDetails.SalProfileID, " +
                                "DeductionHeaderMas.DedID AS HeaderID, " +
                                "DeductionHeaderMas.DedTitle AS HeaderTitle " +
                            "FROM " +
                                "SalProfileDetails " +
                                "INNER JOIN DeductionHeaderMas ON SalProfileDetails.DedID = DeductionHeaderMas.DedID " +
                            "WHERE " +
                                "(" +
                                    "((SalProfileDetails.SalProfileID) = " + SalaryProfileID + ") " +
                                    "AND ((DeductionHeaderMas.IsActive) = True) " +
                                    "AND ((DeductionHeaderMas.IsDeleted) = False) " +
                                ") " +
                            "ORDER BY " +
                                "DeductionHeaderMas.OrderID";

                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                dt = new DataTable();
                da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objSalaryProfileInfo = new List<SalaryProfileInfo>();
                objSalaryProfileInfo = JsonConvert.DeserializeObject<List<SalaryProfileInfo>>(DataTableToJSon);
                foreach (SalaryProfileInfo indSalaryProfileInfo in objSalaryProfileInfo)
                {
                    objReturnSalaryProfileInfoList.Add(new SalaryProfileInfo
                    {
                        EmpSalDetID = 0,
                        SalProDetID = indSalaryProfileInfo.SalProDetID,
                        SalProfileID = indSalaryProfileInfo.SalProfileID,
                        HeaderID = indSalaryProfileInfo.HeaderID,
                        HeaderTitle = indSalaryProfileInfo.HeaderTitle,
                        SalHeaderType = "Deductions",
                        AllowanceAmount = 0.00,
                        DeductionAmount = 0.00,
                        ReimbursmentAmount = 0.00
                    });
                }

                strQuery = "SELECT " + 
                            "SalProfileDetails.SalProDetID, " + 
                            "SalProfileDetails.SalProfileID, " +
                            "ReimbursementHeaderMas.ReimbID AS HeaderID, " +
                            "ReimbursementHeaderMas.ReimbTitle AS HeaderTitle " +
                        "FROM " +
                            "SalProfileDetails " +
                            "INNER JOIN ReimbursementHeaderMas ON SalProfileDetails.ReimbID = ReimbursementHeaderMas.ReimbID " +
                        "WHERE " +
                            "(" +
                                "((SalProfileDetails.SalProfileID) = " + SalaryProfileID + ") " +
                                "AND ((ReimbursementHeaderMas.IsActive) = True) " +
                                "AND ((ReimbursementHeaderMas.IsDeleted) = False) " +
                            ") " +
                        "ORDER BY " +
                            "ReimbursementHeaderMas.OrderID; ";

                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                dt = new DataTable();
                da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objSalaryProfileInfo = new List<SalaryProfileInfo>();
                objSalaryProfileInfo = JsonConvert.DeserializeObject<List<SalaryProfileInfo>>(DataTableToJSon);
                foreach (SalaryProfileInfo indSalaryProfileInfo in objSalaryProfileInfo)
                {
                    objReturnSalaryProfileInfoList.Add(new SalaryProfileInfo
                    {
                        EmpSalDetID = 0,
                        SalProDetID = indSalaryProfileInfo.SalProDetID,
                        SalProfileID = indSalaryProfileInfo.SalProfileID,
                        HeaderID = indSalaryProfileInfo.HeaderID,
                        HeaderTitle = indSalaryProfileInfo.HeaderTitle,
                        SalHeaderType = "Reimbursement",
                        AllowanceAmount = 0.00,
                        DeductionAmount = 0.00,
                        ReimbursmentAmount = 0.00
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
            return objReturnSalaryProfileInfoList;
        }

        public int InsertSalaryProfileInfo(int txtEmpID, string txtAuditLogStatement, string txtUserName)
        {
            int affectedRows = 0;
            try
            {
                int maxRowCount = getMaxRowCount("UserAuditLog", "UserAuditLogID");

                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO UserAuditLog (UserAuditLogID, EmpID, EventDateTime, AuditLogStatement, UserName) VALUES " +
                 "(" + maxRowCount + "," + txtEmpID + ",'" + DateTime.Now + "','" + txtAuditLogStatement + "','" + txtUserName + "')";

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
    }

    public class SpecificEmployeeSalaryProfileInfo
    {
        public int EmpID { get; set; }
        public int EmpSalProfileID { get; set; }
        public int SalProfileID { get; set; }
    }

    public class SalaryProfileInfo
    {
        public int EmpSalDetID { get; set; }
        public int SalProDetID { get; set; }
        public int SalProfileID { get; set; }
        public int HeaderID { get; set; }

        [DisplayName("Salary Header")]
        public string HeaderTitle { get; set; }

        [DisplayName("Type")]
        public string SalHeaderType { get; set; }

        [DisplayName("Allowance Amount")]
        public double AllowanceAmount { get; set; }

        [DisplayName("Deduction Amount")]
        public double DeductionAmount { get; set; }

        [DisplayName("Reimbursment Amount")]
        public double ReimbursmentAmount { get; set; }
        public int OrderID { get; set; }
    }
}
