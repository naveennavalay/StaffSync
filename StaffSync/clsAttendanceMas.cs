using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaffSync
{
    public class clsAttendanceMas
    {
        myDBClass objDBClass = new myDBClass();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsStates objState = new clsStates();
        clsCountries objCountry = new clsCountries();

        public clsAttendanceMas() { 

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

        public List<EmployeeAttendanceInfo> GetDefaultEmployeeAttendanceInfo(int txtEmpID, int MonthNumber)
        {
            List<EmployeeAttendanceInfo> objEmployeeAttendanceInfo = new List<EmployeeAttendanceInfo>();
            List<EmployeeAttendanceInfo> objReturnEmployeeAttendanceInfoList = new List<EmployeeAttendanceInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();
                //DateTime.Now.Month
                string strQuery = "SELECT * FROM qryEmpAttendanceList WHERE EmpID = " + txtEmpID + " AND MONTH(AttDate) = " + MonthNumber + " AND YEAR(AttDate) = " + DateTime.Now.Year  + " ORDER BY AttDate, OrderID Asc";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objEmployeeAttendanceInfo = JsonConvert.DeserializeObject<List<EmployeeAttendanceInfo>>(DataTableToJSon);
                foreach (EmployeeAttendanceInfo indEmployeeAttendanceInfo in objEmployeeAttendanceInfo)
                {
                    objReturnEmployeeAttendanceInfoList.Add(new EmployeeAttendanceInfo
                    {
                        AttID = indEmployeeAttendanceInfo.AttID,
                        AttDate = indEmployeeAttendanceInfo.AttDate,
                        AttStatus = indEmployeeAttendanceInfo.AttStatus,
                        EmpID = indEmployeeAttendanceInfo.EmpID,
                        LeaveTRID = indEmployeeAttendanceInfo.LeaveTRID == null ? 0 : indEmployeeAttendanceInfo.LeaveTRID,
                        LeaveComments = indEmployeeAttendanceInfo.LeaveComments == null ? "" : indEmployeeAttendanceInfo.LeaveComments
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
            return objReturnEmployeeAttendanceInfoList;
        }

        public List<MonthlyAttendanceInfo> MonthlyAttendanceReport(DateTime ReportForTheMonth)
        {
            List<MonthlyAttendanceInfo> objMonthlyAttendanceReport = new List<MonthlyAttendanceInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();
                //DateTime.Now.Month
                string strQuery = "SELECT * FROM qryMnthlyAttdInfo WHERE AttdMonth = #" + ReportForTheMonth.Date.ToString("dd-MMM-yyyy") + "#";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objMonthlyAttendanceReport = JsonConvert.DeserializeObject<List<MonthlyAttendanceInfo>>(DataTableToJSon);

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
            
            return objMonthlyAttendanceReport;
        }

        public int InsertDailyAttendance(int txtEmpID, DateTime AttendanceDate, string AttendanceStatus, int LeaveTRID)
        {
            int affectedRows = 0;
            try
            {
                int maxRowCount = getMaxRowCount("EmpDailyAttendanceInfo", "AttID");

                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO EmpDailyAttendanceInfo (AttID, EmpID, AttDate, AttStatus, LeaveTRID) VALUES " +
                 "(" + maxRowCount + "," + txtEmpID + ",'" + AttendanceDate.ToString("dd-MMM-yyyy") + "','" + AttendanceStatus + "'," + LeaveTRID + ")";

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

    public class EmployeeAttendanceInfo
    {
        public int AttID { get; set; }
        public DateTime AttDate { get; set; }
        public int EmpID { get; set; }
        public string AttStatus { get; set; }

        [JsonProperty("EmpDailyAttendanceInfo.LeaveTRID")]
        public int? LeaveTRID { get; set; }
        
        public string LeaveComments { get; set; }
    }

    public class MonthlyAttendanceInfo
    {
        public int EmpID { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string DesignationTitle { get; set; }
        public string DepartmentTitle { get; set; }
        public int SlNo { get; set; }
        public DateTime AttdMonth { get; set; }
        public string Day1 { get; set; }
        public string Day2 { get; set; }
        public string Day3 { get; set; }
        public string Day4 { get; set; }
        public string Day5 { get; set; }
        public string Day6 { get; set; }
        public string Day7 { get; set; }
        public string Day8 { get; set; }
        public string Day9 { get; set; }
        public string Day10 { get; set; }
        public object Day11 { get; set; }
        public object Day12 { get; set; }
        public object Day13 { get; set; }
        public object Day14 { get; set; }
        public object Day15 { get; set; }
        public object Day16 { get; set; }
        public object Day17 { get; set; }
        public object Day18 { get; set; }
        public object Day19 { get; set; }
        public object Day20 { get; set; }
        public object Day21 { get; set; }
        public object Day22 { get; set; }
        public object Day23 { get; set; }
        public object Day24 { get; set; }
        public object Day25 { get; set; }
        public object Day26 { get; set; }
        public object Day27 { get; set; }
        public object Day28 { get; set; }
        public object Day29 { get; set; }
        public object Day30 { get; set; }
        public object Day31 { get; set; }
        public object Day32 { get; set; }
    }
}
