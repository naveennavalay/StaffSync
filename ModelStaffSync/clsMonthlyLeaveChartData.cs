using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class LeaveStatusSummary
    {
        public string MonthName { get; set; }
        public string LeaveApprovalComments { get; set; }
        public bool Canceled { get; set; }
        public int LeaveYear { get; set; }
        public int LeaveMonth { get; set; }
        public double TotalApplication { get; set; }
        public double TotalApproved { get; set; }
        public double TotalRejected { get; set; }
        public double TotalPending { get; set; }
        public double TotalCancelled { get; set; }
        public double TotalLeaveDays { get; set; }
    }
}
