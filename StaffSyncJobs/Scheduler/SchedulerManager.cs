using ModelStaffSync;
using Quartz;
using Quartz.Impl;
using StaffSyncJobs.ScheduledTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffSyncJobs.Scheduler
{
    public static class SchedulerManager
    {
        private static IScheduler _scheduler;
        
        public static async Task Start()
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();

            _scheduler = await factory.GetScheduler();

            // Read Jobs from DB
            DALStaffSync.SchedulerRepository objRepository = new DALStaffSync.SchedulerRepository();

            List<SchedulerJobModel> jobs = objRepository.GetEnabledJobsList();

            SynchronizeSchedulerJobs(jobs);

            // Register all jobs
            await RegisterJobs(jobs);

            await _scheduler.Start();

        }

        private static void SynchronizeSchedulerJobs(List<SchedulerJobModel> jobs)
        {
            // Read Jobs from DB
            DALStaffSync.SchedulerRepository objRepository = new DALStaffSync.SchedulerRepository();

            foreach (var job in jobs)
            {
                DateTime nextRun = SchedulerHelper.CalculateNextRun(job);

                objRepository.UpdateNextRun(job.JobSchedulerSettingsID, nextRun);
            }
        }

        public static async Task Stop(bool waitForJobsToComplete = false)
        {
            if (_scheduler == null)
                return;

            if (_scheduler.IsShutdown)
                return;

            await _scheduler.Shutdown(waitForJobsToComplete);
        }

        public static async Task Restart()
        {
            await Stop();

            await Start();
        }

        public static async Task Reload()
        {
            if (_scheduler == null)
                return;

            await _scheduler.Clear();

            SchedulerRepository repository = new SchedulerRepository();

            DALStaffSync.SchedulerRepository objRepository = new DALStaffSync.SchedulerRepository();

            List<SchedulerJobModel> jobs = objRepository.GetEnabledJobsList();

            await RegisterJobs(jobs);
        }

        public static async Task StopJob(string jobCode)
        {
            if (_scheduler == null)
                return;

            JobKey key = new JobKey(jobCode);

            if (await _scheduler.CheckExists(key))
            {
                await _scheduler.DeleteJob(key);
            }
        }

        public static async Task PauseJob(string jobCode)
        {
            if (_scheduler == null)
                return;

            JobKey key = new JobKey(jobCode);

            if (await _scheduler.CheckExists(key))
            {
                await _scheduler.PauseJob(key);
            }
        }

        public static async Task RunJob(string jobCode)
        {
            if (_scheduler == null)
                return;

            JobKey key = new JobKey(jobCode);

            if (await _scheduler.CheckExists(key))
            {
                await _scheduler.TriggerJob(key);
            }
        }

        public static async Task PauseAllJobs()
        {
            if (_scheduler == null)
                return;

            await _scheduler.PauseAll();
        }

        public static async Task ResumeJob(string jobCode)
        {
            if (_scheduler == null)
                return;

            JobKey key = new JobKey(jobCode);

            if (await _scheduler.CheckExists(key))
            {
                await _scheduler.ResumeJob(key);
            }
        }

        public static async Task ResumeAllJobs()
        {
            if (_scheduler == null)
                return;

            await _scheduler.ResumeAll();
        }

        public static IScheduler GetScheduler()
        {
            return _scheduler;
        }

        public static bool IsRunning()
        {
            if (_scheduler == null)
                return false;

            return !_scheduler.IsShutdown;
        }


        public static async Task RemoveAllJobs()
        {
            if (_scheduler == null)
                return;

            await _scheduler.Clear();
        }

        private static async Task RegisterJobs(List<SchedulerJobModel> jobs)
        {
            foreach (SchedulerJobModel job in jobs)
            {
                Type jobType = Type.GetType(job.ClassName);

                if (jobType == null)
                {
                    Console.WriteLine("Unable to load : " + job.ClassName);
                    continue;
                }

                IJobDetail jobDetail =
                    JobBuilder.Create(jobType)
                              .WithIdentity(job.JobCode + "_" + job.ClientID)
                              .UsingJobData("JobID", job.JobID)
                              .UsingJobData("JobSchedulerSettingsID", job.JobSchedulerSettingsID)
                              .Build();

                ITrigger trigger = TriggerFactory.CreateTrigger(job);

                await _scheduler.ScheduleJob(jobDetail, trigger);

                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine("--------------------------------------");
                Console.WriteLine("Job : " + job.JobCode);
                Console.WriteLine("Schedule Type : " + job.ScheduleType);

                Console.WriteLine("Trigger : " + trigger.Key.Name);

                Console.WriteLine("Start Time : " + trigger.StartTimeUtc.LocalDateTime);

                Console.WriteLine("Next Fire : " +
                    trigger.GetNextFireTimeUtc()?.LocalDateTime);

                Console.WriteLine("--------------------------------------");
                
                Console.ResetColor();

                Console.WriteLine(job.JobName + " Registered Successfully.");
            }
        }
    }
}
