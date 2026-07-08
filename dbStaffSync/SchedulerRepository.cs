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
        OleDbConnection conn = new OleDbConnection();
        DataSet dtDataset = new DataSet();
        clsGenFunc objGenFunc = new clsGenFunc();

        private static readonly object _historyLock = new object();

        public SchedulerJobMasterAndSetting GetSchedulerJobSettingsIDByJobID(int jobID)
        {
            List<SchedulerJobMasterAndSetting> objSchedulerJobMasterAndSettingList = new List<SchedulerJobMasterAndSetting>();
            SchedulerJobMasterAndSetting objSchedulerJobMasterAndSetting = new SchedulerJobMasterAndSetting();
            DataTable dt = new DataTable();

            try
            {
                string strQuery = "SELECT " +
                        " SchedulerJobSettings.JobSchedulerSettingsID, " +
                        " SchedulerJobMaster.JobID " +
                    " FROM " +
                        " SchedulerJobMaster " +
                        " INNER JOIN SchedulerJobSettings ON SchedulerJobMaster.JobID = SchedulerJobSettings.JobID " +
                    " WHERE " +
                        " ( " +
                            " ((SchedulerJobMaster.JobID) = " + jobID + ") " +
                            " AND ((SchedulerJobMaster.IsActive) = True) " +
                        " )"; 
                
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objSchedulerJobMasterAndSettingList = JsonConvert.DeserializeObject<List<SchedulerJobMasterAndSetting>>(DataTableToJSon);
                if(objSchedulerJobMasterAndSettingList.Count > 0)
                    objSchedulerJobMasterAndSetting = objSchedulerJobMasterAndSettingList[0];
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

            return objSchedulerJobMasterAndSetting;
        }

        public List<SchedulerJobModel> GetAllJobsList()
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
                                    "Switch ( " +
                                    "    [SchedulerJobSettings].[DayOfWeek] = 1, " +
                                    "    \"Sunday\", " +
                                    "    [SchedulerJobSettings].[DayOfWeek] = 2, " +
                                    "    \"Monday\", " +
                                    "    [SchedulerJobSettings].[DayOfWeek] = 3, " +
                                    "    \"Tuesday\", " + 
                                    "    [SchedulerJobSettings].[DayOfWeek] = 4, " +
                                    "    \"Wednesday\", " + 
                                    "    [SchedulerJobSettings].[DayOfWeek] = 5, " +
                                    "    \"Thursday\", " + 
                                    "    [SchedulerJobSettings].[DayOfWeek] = 6, " +
                                    "    \"Friday\", " + 
                                    "    [SchedulerJobSettings].[DayOfWeek] = 7, " +
                                    "    \"Saturday\"" +
                                    ") AS WeeklyDayName, " +
                                    "SchedulerJobSettings.DayOfMonth, " +
                                    "Switch ( " + 
                                    "    [SchedulerJobSettings].[DayOfMonth] = 1, " +
                                    "    \"Sunday\", " + 
                                    "    [SchedulerJobSettings].[DayOfMonth] = 2, " +
                                    "    \"Monday\", " + 
                                    "    [SchedulerJobSettings].[DayOfMonth] = 3, " +
                                    "    \"Tuesday\", " + 
                                    "    [SchedulerJobSettings].[DayOfMonth] = 4, " +
                                    "    \"Wednesday\", " + 
                                    "    [SchedulerJobSettings].[DayOfMonth] = 5, " +
                                    "    \"Thursday\", " + 
                                    "    [SchedulerJobSettings].[DayOfMonth] = 6, " +
                                    "    \"Friday\", " + 
                                    "    [SchedulerJobSettings].[DayOfMonth] = 7, " +
                                    "    \"Saturday\"" +
                                    ") AS MonthlyDayName, " + 
                                    "SchedulerJobSettings.CronExpression, " +
                                    "SchedulerJobSettings.LastRun, " +
                                    "SchedulerJobSettings.LastStatus, " +
                                    "SchedulerJobSettings.NextRun, " +
                                    "SchedulerJobSettings.RetryCount, " +
                                    "SchedulerJobSettings.ClientID " +
                                " FROM " +
                                    "SchedulerJobMaster " +
                                    "INNER JOIN SchedulerJobSettings ON SchedulerJobMaster.JobID = SchedulerJobSettings.JobID " +
                                "ORDER BY " +
                                    "SchedulerJobMaster.JobID, " +
                                    "SchedulerJobSettings.JobSchedulerSettingsID;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                //foreach (DataRow dr in dt.Rows)
                //{
                //    Console.WriteLine("RAW DataTable : " + dr["NextRun"]);
                //}

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

        public SchedulerJobModel GetScheduledJobByID(int jobID)
        {
            List<SchedulerJobModel> SchedulerJobModelList = new List<SchedulerJobModel>();
            SchedulerJobModel objSelectedSchedulerJob = new SchedulerJobModel();

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
                                    "Switch ( " +
                                    "    [SchedulerJobSettings].[DayOfWeek] = 1, " +
                                    "    \"Sunday\", " +
                                    "    [SchedulerJobSettings].[DayOfWeek] = 2, " +
                                    "    \"Monday\", " +
                                    "    [SchedulerJobSettings].[DayOfWeek] = 3, " +
                                    "    \"Tuesday\", " +
                                    "    [SchedulerJobSettings].[DayOfWeek] = 4, " +
                                    "    \"Wednesday\", " +
                                    "    [SchedulerJobSettings].[DayOfWeek] = 5, " +
                                    "    \"Thursday\", " +
                                    "    [SchedulerJobSettings].[DayOfWeek] = 6, " +
                                    "    \"Friday\", " +
                                    "    [SchedulerJobSettings].[DayOfWeek] = 7, " +
                                    "    \"Saturday\"" +
                                    ") AS WeeklyDayName, " +
                                    "SchedulerJobSettings.DayOfMonth, " +
                                    "Switch ( " +
                                    "    [SchedulerJobSettings].[DayOfMonth] = 1, " +
                                    "    \"Sunday\", " +
                                    "    [SchedulerJobSettings].[DayOfMonth] = 2, " +
                                    "    \"Monday\", " +
                                    "    [SchedulerJobSettings].[DayOfMonth] = 3, " +
                                    "    \"Tuesday\", " +
                                    "    [SchedulerJobSettings].[DayOfMonth] = 4, " +
                                    "    \"Wednesday\", " +
                                    "    [SchedulerJobSettings].[DayOfMonth] = 5, " +
                                    "    \"Thursday\", " +
                                    "    [SchedulerJobSettings].[DayOfMonth] = 6, " +
                                    "    \"Friday\", " +
                                    "    [SchedulerJobSettings].[DayOfMonth] = 7, " +
                                    "    \"Saturday\"" +
                                    ") AS MonthlyDayName, " +
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
                                    "((SchedulerJobMaster.JobID) = " + jobID + " ) " +
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
                if (SchedulerJobModelList.Count > 0)
                    objSelectedSchedulerJob = SchedulerJobModelList[0];
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

            return objSelectedSchedulerJob;
        }

        public int UpdateScheduledJobConfigByID(SchedulerJobModel objSchedulerJobModel)
        {
            int affectedRows = 0;
            lock (_historyLock)
            {
                try
                {
                    conn = dbStaffSync.openDBConnection();
                    dtDataset = new DataSet();

                    string strQuery = "UPDATE SchedulerJobMaster " +
                                            " INNER JOIN SchedulerJobSettings ON SchedulerJobMaster.JobID = SchedulerJobSettings.JobID " +
                                            " SET " +
                                                " SchedulerJobMaster.JobCode = '" + objSchedulerJobModel.JobCode + "', " +
                                                " SchedulerJobMaster.JobName = '" + objSchedulerJobModel.JobName + "', " +
                                                " SchedulerJobMaster.Description = '" + objSchedulerJobModel.Description + "', " +
                                                " SchedulerJobMaster.IsSystemJob = " + objSchedulerJobModel.IsSystemJob + ", " +
                                                " SchedulerJobSettings.IsEnabled = " + objSchedulerJobModel.IsEnabled + ", " +
                                                " SchedulerJobSettings.ScheduleType = '" + objSchedulerJobModel.ScheduleType + "', " +
                                                " SchedulerJobSettings.StartDate = '" + Convert.ToDateTime(objSchedulerJobModel.StartDate).ToString("dd-MMM-yyyy") + "', " +
                                                " SchedulerJobSettings.EndDate = '" + Convert.ToDateTime(objSchedulerJobModel.EndDate).ToString("dd-MMM-yyyy") + "', " +
                                                " SchedulerJobSettings.RunTime = '" + Convert.ToDateTime(objSchedulerJobModel.RunTime).ToString("HH:mm:ss") + "', " +
                                                " SchedulerJobSettings.IntervalValue = " + objSchedulerJobModel.IntervalValue + ", " +
                                                " SchedulerJobSettings.IntervalType = '" + objSchedulerJobModel.IntervalType + "', " +
                                                " SchedulerJobSettings.RepeatForever = " + objSchedulerJobModel.RepeatForever + ", " +
                                                " SchedulerJobSettings.DayOfWeek = " + objSchedulerJobModel.DayOfWeek + ", " +
                                                " SchedulerJobSettings.DayOfMonth = " + objSchedulerJobModel.DayOfMonth + " " +
                                            " WHERE " +
                                                " SchedulerJobMaster.JobID = " + objSchedulerJobModel.JobID;
                    OleDbCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strQuery;
                    affectedRows = cmd.ExecuteNonQuery();
                    if (affectedRows > 0)
                        affectedRows = objSchedulerJobModel.JobID;
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
            }
            return affectedRows;
        }

        public int UpdateLastRun(int schedulerSettingsID, DateTime lastRun)
        {
            int affectedRows = 0;
            lock (_historyLock)
            {
                try
                {
                    conn = dbStaffSync.openDBConnection();
                }
                catch (Exception ex)
                {
                    if (ex.Message.ToLower().Equals("unspecified error"))
                    {
                        conn = dbStaffSync.closeDBConnection();
                        conn = dbStaffSync.openDBConnection();
                    }
                }

                try
                {
                    //conn = dbStaffSync.openDBConnection();
                    dtDataset = new DataSet();

                    string strQuery = "UPDATE SchedulerJobSettings " +
                                            "SET " +
                                                "SchedulerJobSettings.LastRun = '" + lastRun.ToString("dd-MMM-yyyy hh:mm:ss tt") + "'" +
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
            }
            return affectedRows;
        }

        public int UpdateLastStatus(int schedulerSettingsID, string LastStatus)
        {
            int affectedRows = 0;
            lock (_historyLock)
            {

                try
                {
                    conn = dbStaffSync.openDBConnection();
                }
                catch (Exception ex)
                {
                    if(ex.Message.ToLower().Equals("unspecified error"))
                    {
                        conn = dbStaffSync.closeDBConnection();
                        conn = dbStaffSync.openDBConnection();
                    }
                }

                try
                {
                    //conn = dbStaffSync.openDBConnection();
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
            }
            return affectedRows;
        }

        public int UpdateNextRun(int schedulerSettingsID, DateTime? nextRun)
        {
            lock (_historyLock)
            {
                try
                {
                    conn = dbStaffSync.openDBConnection();
                }
                catch (Exception ex)
                {
                    if (ex.Message.ToLower().Equals("unspecified error"))
                    {
                        conn = dbStaffSync.closeDBConnection();
                        conn = dbStaffSync.openDBConnection();
                    }
                }

                //conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string sql =
                        @"UPDATE SchedulerJobSettings
                    SET NextRun = ?
                    WHERE JobSchedulerSettingsID = ?";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.Add("@NextRun", OleDbType.Date).Value =  Convert.ToDateTime(nextRun).ToString("dd-MMM-yyyy hh:mm:ss tt");
                cmd.Parameters.Add("@JobSchedulerSettingsID", OleDbType.Integer).Value = schedulerSettingsID;
                cmd.ExecuteNonQuery();
            }
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
