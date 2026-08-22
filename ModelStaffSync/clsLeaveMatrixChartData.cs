using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class LeaveMatrixChartData
    {
        public string Department { get; set; }
        public decimal TotalApproved { get; set; }
        public decimal TotalRejected { get; set; }
        public decimal TotalPending { get; set; }
        public decimal TotalCancelled { get; set; }
    }
}
