using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class SchedulerJobModel
    {
        // SchedulerJobMaster
        public int JobID { get; set; }

        [DisplayName("Job Code")]
        public string JobCode { get; set; }

        [DisplayName("Job Name")] 
        public string JobName { get; set; }

        [DisplayName("Job Class Name")]
        public string ClassName { get; set; }

        [DisplayName("Job Description")]
        public string Description { get; set; }
        
        [DisplayName("System Job?")]
        public bool IsSystemJob { get; set; }

        [DisplayName("Active?")]
        public bool IsActive { get; set; }

        [DisplayName("Job SchedulerSettingsID")]
        public int JobSchedulerSettingsID { get; set; }

        [DisplayName("Enabled?")]
        public bool IsEnabled { get; set; }

        [DisplayName("Schedule Type")]
        public string ScheduleType { get; set; }

        [DisplayName("Start Date")]
        public DateTime? StartDate { get; set; }

        [DisplayName("End Date")]
        public DateTime? EndDate { get; set; }

        [DisplayName("Run Time")]
        public DateTime? RunTime { get; set; }

        [DisplayName("Interval")]
        public int? IntervalValue { get; set; }

        [DisplayName("Inveral Type")]
        public string IntervalType { get; set; }

        [DisplayName("Repeat?")]
        public bool RepeatForever { get; set; }

        [DisplayName("Day Of Week")]
        public int? DayOfWeek { get; set; }

        [DisplayName("Day Name (Week)")]
        public string WeeklyDayName { get; set; }

        [DisplayName("Day Of Month")]
        public int DayOfMonth { get; set; }

        [DisplayName("Day Name (Month)")]
        public string MonthlyDayName { get; set; }

        [DisplayName("Cron Expression")]
        public string CronExpression { get; set; }

        [DisplayName("Last Run")]
        public DateTime? LastRun { get; set; }

        [DisplayName("Last Status")]
        public string LastStatus { get; set; }

        [DisplayName("Next Run")]
        public DateTime? NextRun { get; set; }

        [DisplayName("Time Left")]
        public string TimeLeft { get; set; }

        [DisplayName("Retry Count")]
        public int? RetryCount { get; set; }

        [DisplayName("Client ID")]
        public int ClientID { get; set; }
    }
}
