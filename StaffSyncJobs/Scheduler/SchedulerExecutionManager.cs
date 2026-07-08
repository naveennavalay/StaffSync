using DALStaffSync;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffSyncJobs.Scheduler
{
    public class SchedulerExecutionManager
    {
        private readonly SchedulerRepository _repository;

        public SchedulerExecutionManager()
        {
            _repository = new SchedulerRepository();
        }

        public void JobStarted(IJobExecutionContext context)
        {
            int schedulerSettingsID =
                context.JobDetail.JobDataMap.GetInt("JobSchedulerSettingsID");

            lock (SchedulerLock.DatabaseLock)
            {
                _repository.UpdateLastStatus(schedulerSettingsID, "RUNNING");
            }

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine();
            Console.WriteLine("======================================================");
            Console.WriteLine("Job Started");
            Console.WriteLine("Job Name      : " + context.JobDetail.Key.Name);
            Console.WriteLine("Started At    : " + DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss"));
            Console.WriteLine("======================================================");

            //Later
            //Update Running Status

            //_repository.UpdateJobStatus(schedulerSettingsID, "RUNNING");
        }

        public void JobCompleted(IJobExecutionContext context)
        {
            int schedulerSettingsID = context.JobDetail.JobDataMap.GetInt("JobSchedulerSettingsID");

            Console.ForegroundColor = ConsoleColor.Green;

            int jobID = context.JobDetail.JobDataMap.GetInt("JobID");

            //int clientID = context.JobDetail.JobDataMap.GetInt("ClientID");

            //DateTime nextRun = context.NextFireTimeUtc?.LocalDateTime;
            //DateTime? nextRun = null; //context.Trigger.GetNextFireTimeUtc().LocalDateTime;
            //if (context.Trigger.GetNextFireTimeUtc().HasValue)
            //{
            //    nextRun = context.Trigger.GetNextFireTimeUtc().Value.LocalDateTime;
            //}

            lock (SchedulerLock.DatabaseLock)
            {
                _repository.UpdateLastRun(schedulerSettingsID, context.FireTimeUtc.LocalDateTime);
                _repository.UpdateNextRun(schedulerSettingsID, context.Trigger.GetNextFireTimeUtc()?.LocalDateTime);
                _repository.UpdateLastStatus(schedulerSettingsID, "SUCCESS");
            }

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Fire Time      : " + context.FireTimeUtc.LocalDateTime);
            Console.WriteLine("Prev Fire Time : " + context.PreviousFireTimeUtc?.LocalDateTime);
            Console.WriteLine("Next Fire Time : " + context.NextFireTimeUtc?.LocalDateTime);
            Console.WriteLine("Trigger Next   : " + context.Trigger.GetNextFireTimeUtc()?.LocalDateTime);
            Console.WriteLine("----------------------------------------");

            //lock (SchedulerLock.DatabaseLock)
            //{
            //    _repository.UpdateNextRun(schedulerSettingsID, context.Trigger.GetNextFireTimeUtc()?.LocalDateTime);
            //}

            //lock (SchedulerLock.DatabaseLock)
            //{
            //    _repository.UpdateLastStatus(schedulerSettingsID, "SUCCESS");
            //}

            //_repository.InsertHistory(schedulerSettingsID, jobID, clientID, DateTime.Now - sw.Elapsed, DateTime.Now, "SUCCESS", "", sw.ElapsedMilliseconds);

            Console.WriteLine();
            Console.WriteLine("Completed At  : " + DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss"));
        }

        public void JobFailed(IJobExecutionContext context, Exception ex, Stopwatch sw)
        {
            int schedulerSettingsID = context.JobDetail.JobDataMap.GetInt("JobSchedulerSettingsID");

            int jobID = context.JobDetail.JobDataMap.GetInt("JobID");

            //int clientID = context.JobDetail.JobDataMap.GetInt("ClientID");

            lock (SchedulerLock.DatabaseLock)
            {
                _repository.UpdateLastStatus(schedulerSettingsID, "FAILED");
            }

            //_repository.InsertHistory(schedulerSettingsID, jobID, clientID, DateTime.Now - sw.Elapsed, DateTime.Now, "FAILED", ex.ToString(), sw.ElapsedMilliseconds);
        }

        public void JobFailed(IJobExecutionContext context, Exception ex)
        {
            int schedulerSettingsID =
                context.JobDetail.JobDataMap.GetInt("JobSchedulerSettingsID");

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine();
            Console.WriteLine("Job Failed");

            lock (SchedulerLock.DatabaseLock)
            {
                _repository.UpdateLastStatus(schedulerSettingsID, "FAILED");
            }

            Console.WriteLine(ex.Message);

            //Later

            //_repository.UpdateStatus(...);

            //_repository.InsertHistory(...);
        }
    }
}
