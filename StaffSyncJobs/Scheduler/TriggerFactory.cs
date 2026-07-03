using ModelStaffSync;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffSyncJobs.Scheduler
{
    public static class TriggerFactory
    {
        public static ITrigger CreateTrigger(SchedulerJobModel job)
        {
            if (job == null)
                throw new ArgumentNullException(nameof(job));

            string scheduleType = (job.ScheduleType ?? "").Trim().ToUpperInvariant();

            switch (scheduleType)
            {
                case "INTERVAL":
                    return CreateIntervalTrigger(job);

                case "DAILY":
                    return CreateDailyTrigger(job);

                case "WEEKLY":
                    return CreateWeeklyTrigger(job);

                case "MONTHLY":
                    return CreateMonthlyTrigger(job);

                case "YEARLY":
                    return CreateYearlyTrigger(job);

                case "CRON":
                    return CreateCronTrigger(job);

                default:
                    throw new Exception("Unsupported Schedule Type : " + job.ScheduleType);
            }
        }

        private static ITrigger CreateIntervalTrigger(SchedulerJobModel job)
        {
            string intervalType = (job.IntervalType ?? "").Trim().ToUpperInvariant();

            TriggerBuilder builder =
                TriggerBuilder.Create()
                              .WithIdentity(job.JobCode + "_Trigger");

            switch (intervalType)
            {
                case "SECONDS":

                    return builder
                        .StartNow()
                        .WithSimpleSchedule(x =>
                        {
                            x.WithIntervalInSeconds(job.IntervalValue ?? 1);

                            if (job.RepeatForever)
                                x.RepeatForever();
                            else
                                x.WithRepeatCount(0);
                        })
                        .Build();

                case "MINUTES":

                    return builder
                        .StartNow()
                        .WithSimpleSchedule(x =>
                        {
                            x.WithIntervalInMinutes(job.IntervalValue ?? 1);

                            if (job.RepeatForever)
                                x.RepeatForever();
                            else
                                x.WithRepeatCount(0);
                        })
                        .Build();

                case "HOURS":

                    return builder
                        .StartNow()
                        .WithSimpleSchedule(x =>
                        {
                            x.WithIntervalInHours(job.IntervalValue ?? 1);

                            if (job.RepeatForever)
                                x.RepeatForever();
                            else
                                x.WithRepeatCount(0);
                        })
                        .Build();

                default:

                    throw new Exception("Invalid Interval Type : " + job.IntervalType);
            }
        }

        private static ITrigger CreateDailyTrigger(SchedulerJobModel job)
        {
            throw new NotImplementedException();
        }

        
        private static ITrigger CreateWeeklyTrigger(SchedulerJobModel job)
        {
            throw new NotImplementedException();
        }

        
        private static ITrigger CreateMonthlyTrigger(SchedulerJobModel job)
        {
            throw new NotImplementedException();
        }

        private static ITrigger CreateYearlyTrigger(SchedulerJobModel job)
        {
            throw new NotImplementedException();
        }

        private static ITrigger CreateCronTrigger(SchedulerJobModel job)
        {
            if (string.IsNullOrWhiteSpace(job.CronExpression))
                throw new Exception("Cron Expression is empty.");

            return TriggerBuilder.Create()
                .WithIdentity(job.JobCode + "_Trigger")
                .WithCronSchedule(job.CronExpression)
                .Build();
        }
    }
}
