using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class LeaveTypeInfoModel
    {
        public int LeaveTypeID { get; set; }

        [DisplayName("Leave Type Code")]
        public string LeaveCode { get; set; }

        [DisplayName("Leave Type Title")]
        public string LeaveTypeTitle { get; set; }

        [DisplayName("Is Paid")]
        public bool IsPaid { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public int OrderID { get; set; }
    }

    public class LeaveIsPaid
    {
        public bool IsPaid { get; set; }
    }
}
