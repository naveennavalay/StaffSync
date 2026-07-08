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
    public class ProbationCompletionJob : BaseScheduledJob
    {
        protected override async Task ExecuteJob(IJobExecutionContext context)
        {
            DateTime startTime = context.FireTimeUtc.LocalDateTime;

            DALStaffSync.clsJobAuditLog objJobAuditLog = new DALStaffSync.clsJobAuditLog();
            DALStaffSync.SchedulerRepository objSchedulerRepository = new DALStaffSync.SchedulerRepository();

            SchedulerJobMasterAndSetting objSchedulerJobMasterAndSetting = objSchedulerRepository.GetSchedulerJobSettingsIDByJobID(5); //Probation Completion Job ID is 5

            objJobAuditLog.InsertSchedulerJobHistoryInfo(objSchedulerJobMasterAndSetting.JobSchedulerSettingsID, startTime, null, 0, "ProbationCompletionJob : Execute", $"Probation Completion job started successfully", "SYSTEM");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Probation Completion Job Started");

            ProbationCompletionManager objProbationCompletionManager = new ProbationCompletionManager();

            await objProbationCompletionManager.Execute();

            await Task.CompletedTask;


            DateTime endTime = DateTime.Now;
            TimeSpan duration = endTime - startTime;

            int durationSeconds = (int)duration.TotalSeconds;
            string durationText = duration.ToString(@"hh\:mm\:ss\.fff");

            objJobAuditLog.InsertSchedulerJobHistoryInfo(objSchedulerJobMasterAndSetting.JobSchedulerSettingsID, startTime, endTime, durationSeconds, "ProbationCompletionJob : Execute", $"Probation Completion job completed successfully. Duration: {durationText}", "SYSTEM");
            //Console.WriteLine("Probation Completion Job Completed");
        }
    }
}
