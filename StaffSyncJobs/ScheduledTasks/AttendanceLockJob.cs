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
    public class AttendanceLockJob : BaseScheduledJob
    {
        protected override async Task ExecuteJob(IJobExecutionContext context)
        {
            DateTime startTime = context.FireTimeUtc.LocalDateTime;

            DALStaffSync.clsJobAuditLog objJobAuditLog = new DALStaffSync.clsJobAuditLog();
            DALStaffSync.SchedulerRepository objSchedulerRepository = new DALStaffSync.SchedulerRepository();

            SchedulerJobMasterAndSetting objSchedulerJobMasterAndSetting = objSchedulerRepository.GetSchedulerJobSettingsIDByJobID(1); //Attendance Lock Job ID is 1

            objJobAuditLog.InsertSchedulerJobHistoryInfo(objSchedulerJobMasterAndSetting.JobSchedulerSettingsID, startTime, null, 0, "AttendanceLockJob : Execute", $"Attendance Lock job started successfully", "SYSTEM");

            Console.WriteLine("Attendance Lock Job Executed");

            await Task.CompletedTask;

            DateTime endTime = DateTime.Now;
            TimeSpan duration = endTime - startTime;

            int durationSeconds = (int)duration.TotalSeconds;
            string durationText = duration.ToString(@"hh\:mm\:ss\.fff");

            objJobAuditLog.InsertSchedulerJobHistoryInfo(objSchedulerJobMasterAndSetting.JobSchedulerSettingsID, startTime, endTime, durationSeconds, "AttendanceLockJob : Execute", $"Attendance Lock job completed successfully. Duration: {durationText}", "SYSTEM");
        }
    }
}
