using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class SchedulerRepository
    {
        dbStaffSync.SchedulerRepository objSchedulerRepository = new dbStaffSync.SchedulerRepository();

        public SchedulerJobMasterAndSetting GetSchedulerJobSettingsIDByJobID(int jobID)
        {
            SchedulerJobMasterAndSetting objSchedulerJobMasterAndSetting = new SchedulerJobMasterAndSetting();
            objSchedulerJobMasterAndSetting = objSchedulerRepository.GetSchedulerJobSettingsIDByJobID(jobID);
            return objSchedulerJobMasterAndSetting;
        }
        public List<SchedulerJobModel> GetAllJobsList()
        {
            List<SchedulerJobModel> objSchedulerJobsList = new List<SchedulerJobModel>();
            objSchedulerJobsList = objSchedulerRepository.GetAllJobsList();
            return objSchedulerJobsList;
        }

        public List<SchedulerJobModel> GetEnabledJobsList()
        {
            List<SchedulerJobModel> objSchedulerJobsList = new List<SchedulerJobModel>();
            objSchedulerJobsList = objSchedulerRepository.GetEnabledJobsList();
            return objSchedulerJobsList;
        }

        public SchedulerJobModel GetScheduledJobByID(int jobID)
        {
            SchedulerJobModel objSchedulerJob = new SchedulerJobModel();
            objSchedulerJob = objSchedulerRepository.GetScheduledJobByID(jobID);
            return objSchedulerJob;
        }

        public int UpdateScheduledJobConfigByID(SchedulerJobModel objSchedulerJobModel)
        {
            return objSchedulerRepository.UpdateScheduledJobConfigByID(objSchedulerJobModel);
        }

        public int UpdateLastRun(int schedulerSettingsID, DateTime lastRun)
        {
            schedulerSettingsID = objSchedulerRepository.UpdateLastRun(schedulerSettingsID, lastRun);
            return schedulerSettingsID;
        }

        public int UpdateLastStatus(int schedulerSettingsID, string status)
        {
            schedulerSettingsID = objSchedulerRepository.UpdateLastStatus(schedulerSettingsID, status);
            return schedulerSettingsID;
        }

        public int UpdateNextRun(int schedulerSettingsID, DateTime? nextRun)
        {
            schedulerSettingsID = objSchedulerRepository.UpdateNextRun(schedulerSettingsID, nextRun);
            return schedulerSettingsID;
        }
    }
}
