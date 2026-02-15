using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace dbStaffSync
{
    public class clsAuditLog
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public int InsertAuditLog(int txtEmpID, int txtSourceID, string txtAuditLogStatement, string txtUserName, string txtEventGroup)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("UserAuditLog", "UserAuditLogID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO UserAuditLog (UserAuditLogID, EmpID, SourceID, EventDateTime, AuditLogStatement, UserName, EventGroup) VALUES " +
                 "(" + maxRowCount.Data + "," + txtEmpID + ", " + txtSourceID + ", '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") + "','" + txtAuditLogStatement + "','" + txtUserName + "', '" + txtEventGroup + "')";

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
