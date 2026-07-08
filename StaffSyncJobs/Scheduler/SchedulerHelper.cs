using ModelStaffSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffSyncJobs.Scheduler
{
    public static class SchedulerHelper
    {
        public static DateTime CalculateNextRun(SchedulerJobModel job)
        {
            DateTime now = DateTime.Now;

            switch ((job.ScheduleType ?? "").ToUpper())
            {
                case "INTERVAL":
                    return CalculateInterval(job, now);

                case "DAILY":
                    return CalculateDaily(job, now);

                case "WEEKLY":
                    return CalculateWeekly(job, now);

                case "MONTHLY":
                    return CalculateMonthly(job, now);

                case "YEARLY":
                    return CalculateYearly(job, now);

                default:
                    return now;
            }
        }

        #region Interval

        private static DateTime CalculateInterval(SchedulerJobModel job, DateTime now)
        {
            //int? value = job.IntervalValue <= 0 ? 1 : job.IntervalValue;
            int value = (job.IntervalValue ?? 1) <= 0 ? 1 : job.IntervalValue.Value;

            switch ((job.IntervalType ?? "").ToUpper())
            {
                case "SECONDS":
                    return now.AddSeconds((double)value);

                case "MINUTES":
                    return now.AddMinutes((double)value);

                case "HOURS":
                    return now.AddHours(value);

                case "DAYS":
                    return now.AddDays((double)value);

                default:
                    return now.AddSeconds((double)value);
            }
        }

        #endregion

        #region Daily

        private static DateTime CalculateDaily(SchedulerJobModel job, DateTime now)
        {
            DateTime nextRun = now.Date + job.RunTime.Value.TimeOfDay;

            if (nextRun <= now)
                nextRun = nextRun.AddDays(1);

            return nextRun;
        }

        #endregion

        #region Weekly

        private static DateTime CalculateWeekly(SchedulerJobModel job, DateTime now)
        {
            DateTime nextRun = now.Date + job.RunTime.Value.TimeOfDay;

            //int days = ((job.DayOfWeek - (int)now.DayOfWeek) + 7) % 7;
            int days = (((job.DayOfWeek ?? 1) - (int)now.DayOfWeek) + 7) % 7;

            nextRun = nextRun.AddDays(days);

            if (nextRun <= now)
                nextRun = nextRun.AddDays(7);

            return nextRun;
        }

        #endregion

        #region Monthly

        private static DateTime CalculateMonthly(SchedulerJobModel job, DateTime now)
        {
            int day = Math.Min(job.DayOfMonth, DateTime.DaysInMonth(now.Year, now.Month));

            DateTime nextRun =
                new DateTime(now.Year,
                             now.Month,
                             day,
                             job.RunTime.Value.Hour,
                             job.RunTime.Value.Minute,
                             job.RunTime.Value.Second);

            if (nextRun <= now)
            {
                DateTime nextMonth = now.AddMonths(1);

                day = Math.Min(job.DayOfMonth,
                               DateTime.DaysInMonth(nextMonth.Year, nextMonth.Month));

                nextRun =
                    new DateTime(nextMonth.Year,
                                 nextMonth.Month,
                                 day,
                                 job.RunTime.Value.Hour,
                                 job.RunTime.Value.Minute,
                                 job.RunTime.Value.Second);
            }

            return nextRun;
        }

        #endregion

        #region Yearly

        private static DateTime CalculateYearly(SchedulerJobModel job, DateTime now)
        {
            // Placeholder
            return now.AddYears(1);
        }
        #endregion
    }
}
