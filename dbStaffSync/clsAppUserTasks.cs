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

        public AppUserTasks getSpecificTaskInfo(int TaskSourceID, string TaskStatus, string TaskType)
        {
            List<AppUserTasks> objAssetRegisterInfo = new List<AppUserTasks>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " + 
                                        " AppUserTasks.TaskID, " + 
                                        " AppUserTasks.TaskSourceID, " + 
                                        " AppUserTasks.TaskDate, " + 
                                        " AppUserTasks.TaskRaisedByID, " + 
                                        " AppUserTasks.TaskOwnerID, " + 
                                        " AppUserTasks.TaskDescription, " + 
                                        " AppUserTasks.TaskStatus, " + 
                                        " AppUserTasks.TaskCompletionDate, " + 
                                        " AppUserTasks.OrderID, " + 
                                        " AppUserTasks.TaskType " + 
                                    " FROM " + 
                                        " AppUserTasks " + 
                                    " WHERE " +
                                        " AppUserTasks.TaskSourceID = " + TaskSourceID +
                                        " AND AppUserTasks.TaskStatus = 'Initiated'" + 
                                        " AND AppUserTasks.TaskType = '" + TaskType + "'";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAssetRegisterInfo = JsonConvert.DeserializeObject<List<AppUserTasks>>(DataTableToJSon);
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

            if (objAssetRegisterInfo.Count > 0)
                return objAssetRegisterInfo[0];
            else
                return new AppUserTasks();
        }

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

        public int UpdateUserTask(int TaskID, int txtTaskSourceID, DateTime txtTaskDate, int txtTaskRaisedByID, int TaskOwnerID, string txtTaskDescription, string txtTaskStatus, DateTime txtTaskCompletionDate, string txtTaskType)
        {
            int affectedRows = 0;
            try
            {

                Response<int> maxRowCount = objGenFunc.getMaxRowCount("AppUserTasks", "TaskID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE AppUserTasks Set " +
                                        " TaskID = " + TaskID + ", " +
                                        " TaskSourceID = " + txtTaskSourceID + ", " +
                                        " TaskDate = #" + txtTaskDate.ToString("dd-MMM-yyyy hh:mm:ss tt") + "#, " +
                                        " TaskRaisedByID = " + txtTaskRaisedByID + ", " +
                                        " TaskOwnerID = " + TaskOwnerID + ", " +
                                        " TaskDescription = '" + txtTaskDescription + "', " +
                                        " TaskStatus = " + txtTaskStatus + ", " +
                                        " TaskCompletionDate = #" + txtTaskCompletionDate.ToString("dd-MMM-yyyy hh:mm:ss tt") + "#, " +
                                        " TaskType = '" + txtTaskType + "' " +
                                    " WHERE TaskID = " + TaskID;

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
