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

                await _scheduler.Start();

                // Read Jobs from DB
                DALStaffSync.SchedulerRepository objRepository = new DALStaffSync.SchedulerRepository();

                List<SchedulerJobModel> jobs = objRepository.GetEnabledJobsList();

                // Register all jobs
                await RegisterJobs(jobs);
            }

            public static async Task Stop()
            {
                if (_scheduler != null)
                    await _scheduler.Shutdown();
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

                Console.WriteLine(job.JobName + " Registered Successfully.");
            }
        }
    }
}
