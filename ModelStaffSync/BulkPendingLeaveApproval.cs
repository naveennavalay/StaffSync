using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class BulkPendingLeaveApproval
    {
        public bool Select { get; set; }
        public int EmpID { get; set; }

        [DisplayName("Employee Code")]
        public string EmpCode { get; set; }

        [DisplayName("Employee Name")]
        public string EmpName { get; set; }

        [DisplayName("Designation")]
        public string DesignationTitle { get; set; }

        [DisplayName("Department")]
        public string DepartmentTitle { get; set; }

        [DisplayName("Leave Type ID")]
        public int LeaveTypeID { get; set; }

        [DisplayName("Leave Type")]
        public string LeaveTypeTitle { get; set; }

        public int LeaveEntmtID { get; set; }

        [DisplayName("Leave Trans ID")]
        public int LeaveTRID { get; set; }

        [DisplayName("Leave From")]
        public DateTime ActualLeaveDateFrom { get; set; }

        [DisplayName("Leave To")]
        public DateTime ActualLeaveDateTo { get; set; }

        [DisplayName("Leave Duration")]
        public double LeaveDuration { get; set; }

        [DisplayName("Leave Mode")]
        public string LeaveMode { get; set; }

        [DisplayName("Leave Comments")]
        public string LeaveComments { get; set; }

        [DisplayName("Leave Approval Comments")]
        public string LeaveApprovalComments { get; set; }

        [DisplayName("Leave Rejection Comments")]
        public string LeaveRejectionComments { get; set; }

        [DisplayName("Leave Cancelled")]
        public bool Canceled { get; set; }

        [DisplayName("Leave Cancelled Date")]
        public DateTime CanceledDate { get; set; }

        [DisplayName("OrderID")]
        public int OrderID { get; set; }
    }
}
