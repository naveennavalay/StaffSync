using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class EmployeeSpecificLeaveInfo
    {
        public int LeaveTRID { get; set; }
        public int EmpID { get; set; }
        public int LeaveTypeID { get; set; }
        public DateTime LeaveAppliedDate { get; set; }
        public string LeaveComments { get; set; }
        public DateTime LeaveApprovedDate { get; set; }
        public string LeaveApprovalComments { get; set; }
        public DateTime ActualLeaveDateFrom { get; set; }
        public DateTime ActualLeaveDateTo { get; set; }
        public double LeaveDuration { get; set; }
        public string LeaveMode { get; set; }
        public DateTime LeaveRejectedDate { get; set; }
        public string LeaveRejectionComments { get; set; }
        public int ApprovedOrRejectedByEmpID { get; set; }
        public int OrderID { get; set; }
    }
}
