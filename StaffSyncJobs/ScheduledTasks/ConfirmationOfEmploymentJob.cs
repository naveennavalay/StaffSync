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
    public class ConfirmationOfEmploymentJob : BaseScheduledJob
    {
        protected override async Task ExecuteJob(IJobExecutionContext context)
        {
            DateTime startTime = context.FireTimeUtc.LocalDateTime;
            DALStaffSync.clsJobAuditLog objJobAuditLog = new DALStaffSync.clsJobAuditLog();
            DALStaffSync.SchedulerRepository objSchedulerRepository = new DALStaffSync.SchedulerRepository();

            SchedulerJobMasterAndSetting objSchedulerJobMasterAndSetting = objSchedulerRepository.GetSchedulerJobSettingsIDByJobID(6); //Confirmation Of Employment Job ID is 6

            objJobAuditLog.InsertSchedulerJobHistoryInfo(objSchedulerJobMasterAndSetting.JobSchedulerSettingsID, startTime, null, 0, "ConfirmationOfEmploymentJob : Execute", $"Confirmation Of Employment job started successfully", "SYSTEM");

            Console.ForegroundColor = ConsoleColor.Magenta;
            //Console.WriteLine("Confirmation Of Employment Job Started");

            ConfirmationOfEmploymentManager objConfirmationOfEmploymentManager = new ConfirmationOfEmploymentManager();

            await objConfirmationOfEmploymentManager.Execute();

            await Task.CompletedTask;

            DateTime endTime = DateTime.Now;
            TimeSpan duration = endTime - startTime;

            int durationSeconds = (int)duration.TotalSeconds;
            string durationText = duration.ToString(@"hh\:mm\:ss\.fff");

            objJobAuditLog.InsertSchedulerJobHistoryInfo(objSchedulerJobMasterAndSetting.JobSchedulerSettingsID, startTime, endTime, durationSeconds, "ConfirmationOfEmploymentJob : Execute", $"Confirmation Of Employment job completed successfully. Duration: {durationText}", "SYSTEM");
            //Console.WriteLine("Confirmation Of Employment Job Completed");
        }
    }
}
