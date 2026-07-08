using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;

namespace dbStaffSync
{
    public class clsJobAuditLog
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();
        private static readonly object _historyLock = new object();

        public List<SchedulerJobHistory> getClientSpecificSchedulerJobHistoryStatements(int txtClientiD)
        {
            List<SchedulerJobHistory> objAuditStatementsList = new List<SchedulerJobHistory>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " +
                                        " SchedulerJobMaster.JobID, " +
                                        " SchedulerJobMaster.JobCode, " +
                                        " SchedulerJobMaster.JobName, " +
                                        " SchedulerJobMaster.IsActive, " +
                                        " SchedulerJobHistory.SchedulerJobHistoryID, " +
                                        " SchedulerJobHistory.StartTime, " +
                                        " SchedulerJobHistory.EndTime, " +
                                        " SchedulerJobHistory.DurationSeconds, " +
                                        " SchedulerJobHistory.Status, " +
                                        " SchedulerJobHistory.Message, " +
                                        " SchedulerJobHistory.TriggeredBy, " +
                                        " SchedulerJobHistory.ExecutionDate, " +
                                        " ClientMas.ClientID " +
                                    " FROM " +
                                        " ( " +
                                            " SchedulerJobMaster " +
                                            " INNER JOIN ( " +
                                                " ClientMas " +
                                                " INNER JOIN SchedulerJobSettings ON ClientMas.ClientID = SchedulerJobSettings.ClientID " +
                                            " ) ON SchedulerJobMaster.JobID = SchedulerJobSettings.JobID " +
                                        " ) " +
                                        " INNER JOIN SchedulerJobHistory ON SchedulerJobSettings.JobSchedulerSettingsID = SchedulerJobHistory.JobSchedulerSettingsID " +
                                    " WHERE " +
                                        " ( " +
                                            " ((SchedulerJobMaster.IsActive) = True) " +
                                            " AND ((DateValue([SchedulerJobHistory].[ExecutionDate])) = #" + DateTime.Today.ToString("dd-MMM-yyyy") + "#) " +
                                            " AND ((ClientMas.ClientID) = " + txtClientiD + ") " +
                                        " ) " +
                                    " ORDER BY " +
                                        " SchedulerJobHistory.ExecutionDate DESC";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAuditStatementsList = JsonConvert.DeserializeObject<List<SchedulerJobHistory>>(DataTableToJSon);
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

            return objAuditStatementsList;
        }

        public List<SchedulerJobHistory> getClientSpecificSchedulerJobSpecificHistoryStatements(int txtClientiD, int txtJobID)
        {
            List<SchedulerJobHistory> objAuditStatementsList = new List<SchedulerJobHistory>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " +
                                        " SchedulerJobMaster.JobID, " +
                                        " SchedulerJobMaster.JobCode, " +
                                        " SchedulerJobMaster.JobName, " +
                                        " SchedulerJobMaster.IsActive, " +
                                        " SchedulerJobHistory.SchedulerJobHistoryID, " +
                                        " SchedulerJobHistory.StartTime, " +
                                        " SchedulerJobHistory.EndTime, " +
                                        " SchedulerJobHistory.DurationSeconds, " +
                                        " SchedulerJobHistory.Status, " +
                                        " SchedulerJobHistory.Message, " +
                                        " SchedulerJobHistory.TriggeredBy, " +
                                        " SchedulerJobHistory.ExecutionDate, " +
                                        " ClientMas.ClientID " +
                                    " FROM " +
                                        " ( " +
                                            " SchedulerJobMaster " +
                                            " INNER JOIN ( " +
                                                " ClientMas " +
                                                " INNER JOIN SchedulerJobSettings ON ClientMas.ClientID = SchedulerJobSettings.ClientID " +
                                            " ) ON SchedulerJobMaster.JobID = SchedulerJobSettings.JobID " +
                                        " ) " +
                                        " INNER JOIN SchedulerJobHistory ON SchedulerJobSettings.JobSchedulerSettingsID = SchedulerJobHistory.JobSchedulerSettingsID " +
                                    " WHERE " +
                                        " ( " +
                                            " ((SchedulerJobMaster.JobID) = " + txtJobID + ") " +
                                            " AND ((SchedulerJobMaster.IsActive) = True) " +
                                            " AND ((ClientMas.ClientID) = " + txtClientiD + ") " +
                                        " ) " +
                                    " ORDER BY " +
                                        " SchedulerJobHistory.SchedulerJobHistoryID, " +
                                        " SchedulerJobHistory.ExecutionDate DESC";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAuditStatementsList = JsonConvert.DeserializeObject<List<SchedulerJobHistory>>(DataTableToJSon);
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

            return objAuditStatementsList;
        }

        public int InsertSchedulerJobHistoryInfo(int JobSchedulerSettingsID, DateTime? StartTime, DateTime? EndTime, int DurationSeconds, string Status, string Message, string TriggeredBy)
        {
            int affectedRows = 0;

            lock (_historyLock)
            {
                try
                {
                    Response<int> maxRowCount = objGenFunc.getMaxRowCount("SchedulerJobHistory", "SchedulerJobHistoryID");

                    conn = dbStaffSync.openDBConnection();
                    dtDataset = new DataSet();

                    string strQuery = "";
                    //"INSERT INTO SchedulerJobHistory (SchedulerJobHistoryID, JobSchedulerSettingsID, StartTime, EndTime, DurationSeconds, Status, Message, TriggeredBy, ExecutionDate) VALUES " +
                    // "(" + maxRowCount.Data + "," + JobSchedulerSettingsID + ", '" + Convert.ToDateTime(StartTime).ToString("dd-MMM-yyyy hh:mm:ss tt") + "', '" + Convert.ToDateTime(EndTime).ToString("dd-MMM-yyyy hh:mm:ss tt") + "', " + DurationSeconds + ", '" + Status + "', '" + Message + "', 'SYSTEM', '" + DateTime.Today.ToString("dd-MMM-yyyy hh:mm:ss tt") + "')";

                    if (EndTime != null)
                    {
                        strQuery = "INSERT INTO SchedulerJobHistory (SchedulerJobHistoryID, JobSchedulerSettingsID, StartTime, EndTime, DurationSeconds, Status, Message, TriggeredBy, ExecutionDate) VALUES " +
                            "(" + maxRowCount.Data + "," + JobSchedulerSettingsID + ", '" + Convert.ToDateTime(StartTime).ToString("dd-MMM-yyyy hh:mm:ss tt") + "', '" + Convert.ToDateTime(EndTime).ToString("dd-MMM-yyyy hh:mm:ss tt") + "', " + DurationSeconds + ", '" + Status + "', '" + Message + "', 'SYSTEM', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") + "')";
                    }
                    else if (EndTime is null)
                    {
                        strQuery = "INSERT INTO SchedulerJobHistory (SchedulerJobHistoryID, JobSchedulerSettingsID, StartTime, DurationSeconds, Status, Message, TriggeredBy, ExecutionDate) VALUES " +
                            "(" + maxRowCount.Data + "," + JobSchedulerSettingsID + ", '" + Convert.ToDateTime(StartTime).ToString("dd-MMM-yyyy hh:mm:ss tt") + "', " + DurationSeconds + ", '" + Status + "', '" + Message + "', 'SYSTEM', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") + "')";
                    }

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
                    InsertSchedulerJobHistoryInfo(JobSchedulerSettingsID, StartTime, EndTime, DurationSeconds, Status, Message, TriggeredBy);
                }
                finally
                {
                    conn = dbStaffSync.closeDBConnection();
                }
            }

     
            return affectedRows;
        }
    }
}
