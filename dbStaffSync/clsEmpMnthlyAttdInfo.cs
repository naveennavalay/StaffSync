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
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("MnthlyAttdInfo", "SlNo");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE MnthlyAttdInfo " +
                                        " SET " +
                                            " MnthlyAttdInfo." + AttendanceDay + " = '" + AttendenceStatus + "','" +
                                        " WHERE " +
                                                " MnthlyAttdInfo.SlNo = " + SlNo + "";

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

    }
}
