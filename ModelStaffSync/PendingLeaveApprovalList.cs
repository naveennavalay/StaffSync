using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class PendingLeaveApprovalList
    {
        //[DisplayName("Select")]
        //public bool Select { get; set; }

        public int EmpID { get; set; }

        [DisplayName("Employee Code")]
        public string EmpCode { get; set; }

        [DisplayName("Employee Name")]
        public string EmpName { get; set; }

        [DisplayName("Designation")]
        public string DesignationTitle { get; set; }

        [DisplayName("Department")]
        public string DepartmentTitle { get; set; }

        public int LeaveTRID { get; set; }

        public int LeaveTypeID { get; set; }

        [DisplayName("Leave Type")]
        public string LeaveTypeTitle { get; set; }

        [DisplayName("Applied Date")]
        public DateTime LeaveAppliedDate { get; set; }

        [DisplayName("Reason")]
        public string LeaveComments { get; set; }

        [DisplayName("Leave From")]
        public DateTime ActualLeaveDateFrom { get; set; }

        [DisplayName("Leave Till")]
        public DateTime ActualLeaveDateTo { get; set; }

        [DisplayName("Duration")]
        public string LeaveDuration { get; set; }

        [DisplayName("Leave Mode")]
        public string LeaveMode { get; set; }
    }
}
