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
            DateTime startDate = job.StartDate ?? DateTime.Today;
            DateTime runTime = job.RunTime ?? DateTime.Now;

            DateTime firstRun = new DateTime(
                startDate.Year,
                startDate.Month,
                startDate.Day,
                runTime.Hour,
                runTime.Minute,
                runTime.Second);

            string cron =
                $"0 {runTime.Minute} {runTime.Hour} * * ?";

            return TriggerBuilder.Create()
                .WithIdentity(job.JobCode + "_Trigger")
                .StartAt(firstRun)
                .WithCronSchedule(cron)
                .Build();
        }


        private static ITrigger CreateWeeklyTrigger(SchedulerJobModel job)
        {
            DateTime startDate = job.StartDate ?? DateTime.Today;
            DateTime runTime = job.RunTime ?? DateTime.Now;

            DateTime firstRun = new DateTime(
                startDate.Year,
                startDate.Month,
                startDate.Day,
                runTime.Hour,
                runTime.Minute,
                runTime.Second);

            DayOfWeek day = (DayOfWeek)job.DayOfWeek;

            string cron =
                $"0 {runTime.Minute} {runTime.Hour} ? * {(int)day + 1}";

            return TriggerBuilder.Create()
                .WithIdentity(job.JobCode + "_Trigger")
                .StartAt(firstRun)
                .WithCronSchedule(cron)
                .Build();
        }


        private static ITrigger CreateMonthlyTrigger(SchedulerJobModel job)
        {
            DateTime startDate = job.StartDate ?? DateTime.Today;
            DateTime runTime = job.RunTime ?? DateTime.Now;

            int day = job.DayOfMonth <= 0 ? 1 : job.DayOfMonth;

            DateTime firstRun = new DateTime(
                startDate.Year,
                startDate.Month,
                startDate.Day,
                runTime.Hour,
                runTime.Minute,
                runTime.Second);

            string cron =
                $"0 {runTime.Minute} {runTime.Hour} {day} * ?";

            return TriggerBuilder.Create()
                .WithIdentity(job.JobCode + "_Trigger")
                .StartAt(firstRun)
                .WithCronSchedule(cron)
                .Build();
        }

        private static ITrigger CreateYearlyTrigger(SchedulerJobModel job)
        {
            DateTime startDate = job.StartDate ?? DateTime.Today;
            DateTime runTime = job.RunTime ?? DateTime.Now;

            DateTime firstRun = new DateTime(
                startDate.Year,
                startDate.Month,
                startDate.Day,
                runTime.Hour,
                runTime.Minute,
                runTime.Second);

            string cron =
                $"0 {runTime.Minute} {runTime.Hour} {startDate.Day} {startDate.Month} ?";

            return TriggerBuilder.Create()
                .WithIdentity(job.JobCode + "_Trigger")
                .StartAt(firstRun)
                .WithCronSchedule(cron)
                .Build();
        }

        private static ITrigger CreateCronTrigger(SchedulerJobModel job)
        {
            DateTime startDate = job.StartDate ?? DateTime.Now;

            return TriggerBuilder.Create()
                .WithIdentity(job.JobCode + "_Trigger")
                .StartAt(startDate)
                .WithCronSchedule(job.CronExpression)
                .Build();
        }
    }
}
