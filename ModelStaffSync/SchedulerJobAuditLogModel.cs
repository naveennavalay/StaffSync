using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class SchedulerJobAuditLogModel
    {

    }

    public class SchedulerJobHistory
    {
        [DisplayName("Scheduler Job History ID")]
        public int SchedulerJobHistoryID { get; set; }

        [DisplayName("Job Scheduler Settings ID")]
        public int JobSchedulerSettingsID { get; set; }

        [DisplayName("Start Time")]
        public DateTime StartTime { get; set; }

        [DisplayName("End Time")]
        public DateTime? EndTime { get; set; }

        [DisplayName("Duration (Sec)")]
        public float DurationSeconds { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }

        [DisplayName("Message")]
        public string Message { get; set; }

        [DisplayName("Triggered By")]
        public string TriggeredBy { get; set; } = "SYSTEM";

        [DisplayName("Execution Date")]
        public DateTime ExecutionDate { get; set; }

        [DisplayName("Client ID")]
        public int ClientID { get; set; }
    }
}
