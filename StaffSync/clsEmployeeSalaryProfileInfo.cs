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
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.MonthCalendar;

namespace StaffSync
{
    public class clsEmployeeSalaryProfileInfo
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

        public List<EmployeeSalaryProfileInfo> GetDefaultEmployeeSalaryProfileInfo(int SalaryProfileID)
        {

            List<EmployeeSalaryProfileInfo> objEmployeeSalaryProfileInfo = new List<EmployeeSalaryProfileInfo>();
            List<EmployeeSalaryProfileInfo> objReturnEmployeeSalaryProfileInfoList = new List<EmployeeSalaryProfileInfo>();
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
                objEmployeeSalaryProfileInfo = JsonConvert.DeserializeObject<List<EmployeeSalaryProfileInfo>>(DataTableToJSon);
                foreach (EmployeeSalaryProfileInfo indEmployeeSalaryProfileInfo in objEmployeeSalaryProfileInfo)
                {
                    objReturnEmployeeSalaryProfileInfoList.Add(new EmployeeSalaryProfileInfo
                    {
                        SalProDetID = indEmployeeSalaryProfileInfo.SalProDetID,
                        SalProfileID = indEmployeeSalaryProfileInfo.SalProfileID,
                        HeaderID = indEmployeeSalaryProfileInfo.HeaderID,
                        HeaderTitle = indEmployeeSalaryProfileInfo.HeaderTitle,
                        SalHeaderType = "Allowences"
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
                objEmployeeSalaryProfileInfo = new List<EmployeeSalaryProfileInfo>();
                objEmployeeSalaryProfileInfo = JsonConvert.DeserializeObject<List<EmployeeSalaryProfileInfo>>(DataTableToJSon);
                foreach (EmployeeSalaryProfileInfo indEmployeeSalaryProfileInfo in objEmployeeSalaryProfileInfo)
                {
                    objReturnEmployeeSalaryProfileInfoList.Add(new EmployeeSalaryProfileInfo
                    {
                        SalProDetID = indEmployeeSalaryProfileInfo.SalProDetID,
                        SalProfileID = indEmployeeSalaryProfileInfo.SalProfileID,
                        HeaderID = indEmployeeSalaryProfileInfo.HeaderID,
                        HeaderTitle = indEmployeeSalaryProfileInfo.HeaderTitle,
                        SalHeaderType = "Deductions"
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
                objEmployeeSalaryProfileInfo = new List<EmployeeSalaryProfileInfo>();
                objEmployeeSalaryProfileInfo = JsonConvert.DeserializeObject<List<EmployeeSalaryProfileInfo>>(DataTableToJSon);
                foreach (EmployeeSalaryProfileInfo indEmployeeSalaryProfileInfo in objEmployeeSalaryProfileInfo)
                {
                    objReturnEmployeeSalaryProfileInfoList.Add(new EmployeeSalaryProfileInfo
                    {
                        SalProDetID = indEmployeeSalaryProfileInfo.SalProDetID,
                        SalProfileID = indEmployeeSalaryProfileInfo.SalProfileID,
                        HeaderID = indEmployeeSalaryProfileInfo.HeaderID,
                        HeaderTitle = indEmployeeSalaryProfileInfo.HeaderTitle,
                        SalHeaderType = "Reimbursement"
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
            return objReturnEmployeeSalaryProfileInfoList;
        }

        public int InsertEmployeeEmployeeSalaryProfileInfo(int txtEmpID, int SalaryProfileID, DateTime EffectiveDate)
        {
            int affectedRows = 0;
            try
            {
                int maxRowCount = getMaxRowCount("EmpSalProfileInfo", "EmpSalProID");

                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpSalProfileInfo SET SalProfileEffectiveDate = date(), IsActive = false, IsDefault = false where EmpID = " + txtEmpID + " AND IsActive = true and IsDefault = true";
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();

                strQuery = "INSERT INTO EmpSalProfileInfo (EmpSalProID, EmpID, SalProfileID, SalProfileEffectiveDate, IsActive, IsDeleted, IsDefault) VALUES " +
                 "(" + maxRowCount + "," + txtEmpID + "," +SalaryProfileID + ",'" + DateTime.Now + "', true, false, true)";

                cmd = conn.CreateCommand();
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

    public class EmployeeSalaryProfileInfo
    {
        public int SalProDetID { get; set; }
        public int SalProfileID { get; set; }
        public int HeaderID { get; set; }

        [DisplayName("Salary Header")]
        public string HeaderTitle { get; set; }

        [DisplayName("Type")] 
        public string SalHeaderType { get; set; }
    }
}
