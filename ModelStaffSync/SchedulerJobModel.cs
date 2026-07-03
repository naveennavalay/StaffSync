using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class SchedulerJobModel
    {
        // SchedulerJobMaster
        public int JobID { get; set; }

        public string JobCode { get; set; }

        public string JobName { get; set; }

        public string ClassName { get; set; }

        public string Description { get; set; }

        public bool IsSystemJob { get; set; }

        public bool IsActive { get; set; }

        // SchedulerJobSettings
        public int JobSchedulerSettingsID { get; set; }

        public bool IsEnabled { get; set; }

        public string ScheduleType { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? RunTime { get; set; }

        public int? IntervalValue { get; set; }

        public string IntervalType { get; set; }

        public bool RepeatForever { get; set; }

        public byte? DayOfWeek { get; set; }

        public byte? DayOfMonth { get; set; }

        public string CronExpression { get; set; }

        public DateTime? LastRun { get; set; }

        public string LastStatus { get; set; }

        public DateTime? NextRun { get; set; }

        public byte? RetryCount { get; set; }

        public int ClientID { get; set; }
    }
}
