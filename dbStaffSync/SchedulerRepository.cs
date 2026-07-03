using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace dbStaffSync
{
    public class SchedulerRepository
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public List<SchedulerJobModel> GetEnabledJobsList()
        {
            List<SchedulerJobModel> SchedulerJobModelList = new List<SchedulerJobModel>();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT " + 
                                    "SchedulerJobMaster.JobID, " + 
                                    "SchedulerJobMaster.JobCode, " + 
                                    "SchedulerJobMaster.JobName, " +
                                    "SchedulerJobMaster.ClassName, " + 
                                    "SchedulerJobMaster.Description, " +
                                    "SchedulerJobMaster.IsSystemJob, " +
                                    "SchedulerJobMaster.IsActive, " +
                                    "SchedulerJobSettings.JobSchedulerSettingsID, " +
                                    "SchedulerJobSettings.IsEnabled, " +
                                    "SchedulerJobSettings.ScheduleType, " +
                                    "SchedulerJobSettings.StartDate, " +
                                    "SchedulerJobSettings.EndDate, " +
                                    "SchedulerJobSettings.RunTime, " +
                                    "SchedulerJobSettings.IntervalValue, " +
                                    "SchedulerJobSettings.IntervalType, " +
                                    "SchedulerJobSettings.RepeatForever, " +
                                    "SchedulerJobSettings.DayOfWeek, " +
                                    "SchedulerJobSettings.DayOfMonth, " +
                                    "SchedulerJobSettings.CronExpression, " +
                                    "SchedulerJobSettings.LastRun, " +
                                    "SchedulerJobSettings.LastStatus, " +
                                    "SchedulerJobSettings.NextRun, " +
                                    "SchedulerJobSettings.RetryCount, " +
                                    "SchedulerJobSettings.ClientID " +
                                " FROM " +
                                    "SchedulerJobMaster " +
                                    "INNER JOIN SchedulerJobSettings ON SchedulerJobMaster.JobID = SchedulerJobSettings.JobID " +
                                "WHERE " +
                                    "((SchedulerJobMaster.IsActive) = True) " +
                                    "AND ((SchedulerJobSettings.IsEnabled) = True) " +
                                "ORDER BY " +
                                    "SchedulerJobMaster.JobID, " +
                                    "SchedulerJobSettings.JobSchedulerSettingsID;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                SchedulerJobModelList = JsonConvert.DeserializeObject<List<SchedulerJobModel>>(DataTableToJSon);
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

            return SchedulerJobModelList;
        }

        public int UpdateLastRun(int schedulerSettingsID, DateTime lastRun)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE SchedulerJobSettings " +
                                        "SET " +
                                            "SchedulerJobSettings.LastRun = '" + lastRun.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                        "WHERE " +
                                            "((SchedulerJobSettings.JobSchedulerSettingsID) = " + schedulerSettingsID + ")";
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = schedulerSettingsID;
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

        public int UpdateLastStatus(int schedulerSettingsID, string LastStatus)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE SchedulerJobSettings " +
                                        "SET " +
                                            "SchedulerJobSettings.LastStatus = '" + LastStatus + "'" +
                                        "WHERE " +
                                            "((SchedulerJobSettings.JobSchedulerSettingsID) = " + schedulerSettingsID + ")";
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = schedulerSettingsID;
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

        public int UpdateNextRun(int schedulerSettingsID, DateTime? nextRun)
        {
            conn = dbStaffSync.openDBConnection();
            dtDataset = new DataSet();

            string sql =
                    @"UPDATE SchedulerJobSettings
                    SET NextRun = ?
                    WHERE JobSchedulerSettingsID = ?";
                    OleDbCommand cmd = new OleDbCommand(sql, conn);
                    cmd.Parameters.Add("@NextRun", OleDbType.Date).Value = nextRun;
                    cmd.Parameters.Add("@JobSchedulerSettingsID", OleDbType.Integer).Value = schedulerSettingsID;
                    cmd.ExecuteNonQuery();

            return schedulerSettingsID;
            //int affectedRows = 0;
            //try
            //{
            //    conn = dbStaffSync.openDBConnection();
            //    dtDataset = new DataSet();

            //    string strQuery = "UPDATE SchedulerJobSettings " +
            //                            "SET " +
            //                                "SchedulerJobSettings.NextRun = '" + Convert.ToDateTime(nextRun).ToString("yyyy-MM-dd HH:mm:ss") + "'" +
            //                            "WHERE " +
            //                                "((SchedulerJobSettings.JobSchedulerSettingsID) = " + schedulerSettingsID + ")";
            //    OleDbCommand cmd = conn.CreateCommand();
            //    cmd.CommandType = CommandType.Text;
            //    cmd.CommandText = strQuery;
            //    affectedRows = cmd.ExecuteNonQuery();
            //    if (affectedRows > 0)
            //        affectedRows = schedulerSettingsID;
            //}
            //catch (Exception ex)
            //{
            //    //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    conn = dbStaffSync.closeDBConnection();
            //}
            //finally
            //{
            //    conn = dbStaffSync.closeDBConnection();
            //}
            //return affectedRows;
        }
    }
}
