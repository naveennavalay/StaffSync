using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace dbStaffSync
{
    public class clsAttendanceMas
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public clsAttendanceMas() { 

        }

        public List<EmployeeAttendanceInfo> GetDefaultEmployeeAttendanceInfo(int txtEmpID, DateTime dtSelectedMonth)
        {
            List<EmployeeAttendanceInfo> objEmployeeAttendanceInfo = new List<EmployeeAttendanceInfo>();
            List<EmployeeAttendanceInfo> objReturnEmployeeAttendanceInfoList = new List<EmployeeAttendanceInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                //DateTime.Now.Month
                //string strQuery = "SELECT * FROM qryEmpAttendanceList WHERE EmpID = " + txtEmpID + " AND AttDate = #" + dtSelectedMonth.Date.ToString("dd-MMM-yyyy") + "# ORDER BY AttDate, OrderID Asc";
                string strQuery = "SELECT * FROM qryEmpAttendanceList WHERE EmpID = " + txtEmpID + " AND Month(AttDate) = " + dtSelectedMonth.Date.Month + " AND Year(AttDate) = " + dtSelectedMonth.Date.Year + " ORDER BY AttDate, OrderID Asc";

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
                //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = dbStaffSync.closeDBConnection();
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }
            return objReturnEmployeeAttendanceInfoList;
        }

        public List<EmployeeAttendanceInfo> GetDefaultEmployeeAttendanceInfoForJobs(int txtEmpID, DateTime dtSelectedMonth)
        {
            List<EmployeeAttendanceInfo> objEmployeeAttendanceInfo = new List<EmployeeAttendanceInfo>();
            List<EmployeeAttendanceInfo> objReturnEmployeeAttendanceInfoList = new List<EmployeeAttendanceInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                //DateTime.Now.Month
                string strQuery = "SELECT * FROM qryEmpAttendanceList WHERE EmpID = " + txtEmpID + " AND AttDate = #" + dtSelectedMonth.Date.ToString("dd-MMM-yyyy") + "# ORDER BY AttDate, OrderID Asc";
                //string strQuery = "SELECT * FROM qryEmpAttendanceList WHERE EmpID = " + txtEmpID + " AND Month(AttDate) = " + dtSelectedMonth.Date.Month + " AND Year(AttDate) = " + dtSelectedMonth.Date.Year + " ORDER BY AttDate, OrderID Asc";

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
                //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = dbStaffSync.closeDBConnection();
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }
            return objReturnEmployeeAttendanceInfoList;
        }

        public List<MonthlyAttendanceInfo> EmployeeSpecificMonthlyAttendanceInfo(int EmpID, DateTime ReportForTheMonth)
        {
            List<MonthlyAttendanceInfo> objMonthlyAttendanceReport = new List<MonthlyAttendanceInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                //DateTime.Now.Month
                string strQuery = "SELECT * FROM qryMnthlyAttdInfo WHERE EmpID = " + EmpID + " AND AttdMonth = #" + ReportForTheMonth.Date.ToString("dd-MMM-yyyy") + "#";

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
                //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = dbStaffSync.closeDBConnection();
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }

            return objMonthlyAttendanceReport;
        }

        public List<MonthlyAttendanceInfo> MonthlyAttendanceReport(DateTime ReportForTheMonth)
        {
            List<MonthlyAttendanceInfo> objMonthlyAttendanceReport = new List<MonthlyAttendanceInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
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
                //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = dbStaffSync.closeDBConnection();
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }
            
            return objMonthlyAttendanceReport;
        }

        public int InsertDailyAttendance(int txtEmpID, DateTime AttendanceDate, string AttendanceStatus, int LeaveTRID)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("EmpDailyAttendanceInfo", "AttID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO EmpDailyAttendanceInfo (AttID, EmpID, AttDate, AttStatus, LeaveTRID) VALUES " +
                 "(" + maxRowCount.Data + "," + txtEmpID + ",'" + AttendanceDate.ToString("dd-MMM-yyyy") + "','" + AttendanceStatus + "'," + LeaveTRID + ")";

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

        public int UpdateDailyAttendance(int txtEmpID, DateTime AttendanceDate, string AttendanceStatus, int LeaveTRID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpDailyAttendanceInfo SET AttStatus = '" + AttendanceStatus + "', LeaveTRID = " + LeaveTRID + 
                 " WHERE EmpID = " + txtEmpID + " AND  AttDate = #" + AttendanceDate.ToString("dd-MMM-yyyy") + "#";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = affectedRows;
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
