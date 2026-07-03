using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class SchedulerJobSetting
    {
        public int JobSchedulerSettingsID { get; set; }
        public int JobID { get; set; }
        public bool IsEnabled { get; set; }
        public string ScheduleType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RunTime { get; set; }
        public int IntervalValue { get; set; }
        public string IntervalType { get; set; }
        public bool RepeatForever { get; set; }
        public int DayOfWeek { get; set; }
        public int DayOfMonth { get; set; }
        public string CronExpression { get; set; }
        public DateTime LastRun { get; set; }
        public string LastRunStatus { get; set; }
        public DateTime NextRun { get; set; }
        public int RetryCount { get; set; }
        public int ClientID { get; set; }
    }
}
