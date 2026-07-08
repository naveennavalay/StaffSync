using ModelStaffSync;
using Quartz;
using StaffSyncJobs.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StaffSyncJobs.ScheduledTasks
{
    public class BirthdayWishJob : BaseScheduledJob
    {
        
        protected override async Task ExecuteJob(IJobExecutionContext context)
        {
            DateTime startTime = context.FireTimeUtc.LocalDateTime;

            DALStaffSync.clsJobAuditLog objJobAuditLog = new DALStaffSync.clsJobAuditLog();
            DALStaffSync.SchedulerRepository objSchedulerRepository = new DALStaffSync.SchedulerRepository();

            SchedulerJobMasterAndSetting objSchedulerJobMasterAndSetting = objSchedulerRepository.GetSchedulerJobSettingsIDByJobID(3); //Birthday Wishes Job ID is 3

            objJobAuditLog.InsertSchedulerJobHistoryInfo(objSchedulerJobMasterAndSetting.JobSchedulerSettingsID, startTime, null, 0, "BirthdayWishJob : Execute", $"Birthday wishes job started successfully", "SYSTEM");

            Console.ForegroundColor = ConsoleColor.Magenta;
            //Console.WriteLine("Birthday Wishes Job Started");

            BirthdayWishManager objBirthdayWishManager = new BirthdayWishManager();

            await objBirthdayWishManager.Execute();

            await Task.CompletedTask;

            DateTime endTime = DateTime.Now;
            TimeSpan duration = endTime - startTime;

            int durationSeconds = (int)duration.TotalSeconds;
            string durationText = duration.ToString(@"hh\:mm\:ss\.fff");

            objJobAuditLog.InsertSchedulerJobHistoryInfo(objSchedulerJobMasterAndSetting.JobSchedulerSettingsID, startTime, endTime, durationSeconds, "BirthdayWishJob : Execute", $"Birthday wishes job completed successfully. Duration: {durationText}", "SYSTEM");
            //Console.WriteLine("Birthday Wishes Job Completed");
        }
    }
}
