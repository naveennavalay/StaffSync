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

        public List<EmployeeAttendanceInfo> GetDefaultEmployeeAttendanceInfo(int txtEmpID)
        {

            List<EmployeeAttendanceInfo> objEmployeeAttendanceInfo = new List<EmployeeAttendanceInfo>();
            List<EmployeeAttendanceInfo> objReturnEmployeeAttendanceInfoList = new List<EmployeeAttendanceInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "SELECT * FROM qryEmpAttendanceList WHERE EmpID = " + txtEmpID + " AND MONTH(AttDate) = " + DateTime.Now.Month + " AND YEAR(AttDate) = " + DateTime.Now.Year  + " ORDER BY AttDate, OrderID Asc";

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

        public int InsertDailyAttendance(int txtEmpID, DateTime AttendanceDate, string AttendanceStatus, int LeaveTRID)
        {
            int affectedRows = 0;
            try
            {
                int maxRowCount = getMaxRowCount("EmpDailyAttendanceInfo", "AttID");

                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO EmpDailyAttendanceInfo (AttID, EmpID, AttDate, AttStatus, LeaveTRID) VALUES " +
                 "(" + maxRowCount + "," + txtEmpID + ",'" + AttendanceDate + "','" + AttendanceStatus + "'," + LeaveTRID + ")";

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
}
