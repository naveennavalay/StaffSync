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
    public class WorkAnniversaryJob : BaseScheduledJob
    {
        protected override async Task ExecuteJob(IJobExecutionContext context)
        {
            DateTime startTime = context.FireTimeUtc.LocalDateTime;

            DALStaffSync.clsJobAuditLog objJobAuditLog = new DALStaffSync.clsJobAuditLog();
            DALStaffSync.SchedulerRepository objSchedulerRepository = new DALStaffSync.SchedulerRepository();

            SchedulerJobMasterAndSetting objSchedulerJobMasterAndSetting = objSchedulerRepository.GetSchedulerJobSettingsIDByJobID(4); //Work Anniversary Wishes Job ID is 4

            objJobAuditLog.InsertSchedulerJobHistoryInfo(objSchedulerJobMasterAndSetting.JobSchedulerSettingsID, startTime, null, 0, "WorkAnniversaryJob : Execute", $"Work Anniversary wishes job started successfully", "SYSTEM");

            Console.ForegroundColor = ConsoleColor.Magenta;
            //Console.WriteLine("Work Anniversary Wishes Job Started");

            WorkAnniversaryManager objWorkAnniversaryManager = new WorkAnniversaryManager();

            await objWorkAnniversaryManager.Execute();

            await Task.CompletedTask;

            DateTime endTime = DateTime.Now;
            TimeSpan duration = endTime - startTime;

            int durationSeconds = (int)duration.TotalSeconds;
            string durationText = duration.ToString(@"hh\:mm\:ss\.fff");

            objJobAuditLog.InsertSchedulerJobHistoryInfo(objSchedulerJobMasterAndSetting.JobSchedulerSettingsID, startTime, endTime, durationSeconds, "WorkAnniversaryJob : Execute", $"Work Anniversary wishes job completed successfully. Duration: {durationText}", "SYSTEM");
            //Console.WriteLine("Work Anniversary Wishes Job Completed");
        }
    }
}
