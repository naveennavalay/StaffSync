using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net;
using System.Text;

namespace dbStaffSync
{
    public class clsAppUserTasks
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public int InsertUserTask(DateTime txtTaskDate, int txtTaskSourceID, int txtTaskRaisedByID, int TaskOwnerID, string txtTaskDescription, string txtTaskStatus, DateTime txtTaskCompletionDate, string txtTaskType)
        {
            int affectedRows = 0;
            try
            {

                Response<int> maxRowCount = objGenFunc.getMaxRowCount("AppUserTasks", "TaskID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO AppUserTasks (TaskID, TaskSourceID, TaskDate, TaskRaisedByID, TaskOwnerID, TaskDescription, TaskStatus, TaskCompletionDate, OrderID, TaskType) VALUES " +
                 "(" + maxRowCount.Data + "," + txtTaskSourceID + ", '" + txtTaskDate.ToString("dd-MMM-yyyy hh:mm:ss tt") + "'," + txtTaskRaisedByID + ", " + TaskOwnerID + ", '" + txtTaskDescription + "', '" + txtTaskStatus + "', '" + txtTaskCompletionDate.ToString("dd-MMM-yyyy hh:mm:ss tt") + "', " + maxRowCount.Data + ", '" + txtTaskType + "')";

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
