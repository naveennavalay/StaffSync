using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class LeaveEntitlementInfo
    {
        public int LeaveEntmtID { get; set; }
        public int EmpID { get; set; }
        public int LeaveMasID { get; set; }
        public int LeaveTypeID { get; set; }

        [DisplayName("Leave Type")]
        public string LeaveTypeTitle { get; set; }

        [DisplayName("Total Leaves Allotted")]
        public decimal TotalLeaves { get; set; }

        [DisplayName("Total Leaves Available")]
        public decimal BalanceLeaves { get; set; }

        [DisplayName("Total Utilised Leaves")]
        public decimal UsedLeaves { get; set; }
        public int OrderID { get; set; }
    }
}
