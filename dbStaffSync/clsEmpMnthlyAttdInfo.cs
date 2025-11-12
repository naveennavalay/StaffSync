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
    public class clsEmpMnthlyAttdInfo
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset = null;
        clsGenFunc objGenFunc = new clsGenFunc();
        clsStates objState = new clsStates();
        clsCountries objCountry = new clsCountries();

        public clsEmpMnthlyAttdInfo()
        {

        }

        public List<MonthlyAttendanceInfo> getConsolidatedMonthlyAttendanceInfo(DateTime AttendanceMonth)
        {
            List<MonthlyAttendanceInfo> objMonthlyAttendanceInfo = new List<MonthlyAttendanceInfo>();

            int SlNo = 0;
            DataTable dt = new DataTable();
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();
                string strQuery = "SELECT * FROM MnthlyAttdInfo WHERE AttdMonth = #" + AttendanceMonth + "#";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objMonthlyAttendanceInfo = JsonConvert.DeserializeObject<List<MonthlyAttendanceInfo>>(DataTableToJSon);
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
            return objMonthlyAttendanceInfo;
        }

        public List<MonthlyAttendanceInfo> getEmployeeMonthlyAttendanceInfo(int txtEmpID, DateTime AttendanceMonth)
        {
            List<MonthlyAttendanceInfo> objMonthlyAttendanceInfo = new List<MonthlyAttendanceInfo>();

            int SlNo = 0;
            DataTable dt = new DataTable();
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();
                string strQuery = "SELECT * FROM MnthlyAttdInfo WHERE EmpID = " + txtEmpID + " AND AttdMonth = #" + AttendanceMonth + "#";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objMonthlyAttendanceInfo = JsonConvert.DeserializeObject<List<MonthlyAttendanceInfo>>(DataTableToJSon);
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
            return objMonthlyAttendanceInfo;
        }

        public int getMonthlyAttendanceInfo(int txtEmpID, DateTime AttendanceMonth)
        {
            int SlNo = 0;
            DataTable dt = new DataTable();
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();
                string strQuery = "SELECT SlNo FROM MnthlyAttdInfo WHERE EmpID = " + txtEmpID + " AND Month(AttdMonth) = " + AttendanceMonth.Month + "";
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    SlNo = Convert.ToInt32(dt.Rows[0]["SlNo"]);
                }
                else 
                {
                    SlNo = InsertMonthlyAttendanceInfo(txtEmpID, AttendanceMonth);
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
            return SlNo;
        }

        public int InsertMonthlyAttendanceInfo(int txtEmpID, DateTime AttendanceMonth)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("MnthlyAttdInfo", "SlNo");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO MnthlyAttdInfo (SlNo, EmpID, AttdMonth) VALUES " +
                 "(" + maxRowCount.Data + "," + txtEmpID + ",'" + AttendanceMonth.ToString("dd-MMM-yyyy") + "')";

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

        public int UpdateMonthlyAttendanceInfo(int SlNo, int txtEmpID, DateTime AttendanceMonth, string AttendanceDay, string AttendenceStatus)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE MnthlyAttdInfo " +
                                        " SET " +
                                            " " + AttendanceDay + " = '" + AttendenceStatus + "'" +
                                        " WHERE " +
                                                " SlNo = " + SlNo + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = SlNo;
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
