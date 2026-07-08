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
    public class PayrollGenerationJob : BaseScheduledJob
    {
        protected override async Task ExecuteJob(IJobExecutionContext context)
        {
            DateTime startTime = context.FireTimeUtc.LocalDateTime;

            DALStaffSync.clsJobAuditLog objJobAuditLog = new DALStaffSync.clsJobAuditLog();
            DALStaffSync.SchedulerRepository objSchedulerRepository = new DALStaffSync.SchedulerRepository();

            SchedulerJobMasterAndSetting objSchedulerJobMasterAndSetting = objSchedulerRepository.GetSchedulerJobSettingsIDByJobID(2); //Birthday Wishes Job ID is 3

            objJobAuditLog.InsertSchedulerJobHistoryInfo(objSchedulerJobMasterAndSetting.JobSchedulerSettingsID, startTime, null, 0, "PayrollGenerationJob : Execute", $"Payroll Generation job started successfully", "SYSTEM");

            Console.WriteLine("Payroll Generation Job Executed");

            await Task.CompletedTask;

            DateTime endTime = DateTime.Now;
            TimeSpan duration = endTime - startTime;

            int durationSeconds = (int)duration.TotalSeconds;
            string durationText = duration.ToString(@"hh\:mm\:ss\.fff");

            objJobAuditLog.InsertSchedulerJobHistoryInfo(objSchedulerJobMasterAndSetting.JobSchedulerSettingsID, startTime, endTime, durationSeconds, "PayrollGenerationJob : Execute", $"Payroll Generation job completed successfully. Duration: {durationText}", "SYSTEM");
        }
    }
}
