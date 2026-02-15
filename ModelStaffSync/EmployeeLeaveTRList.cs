using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class EmployeeLeaveTRList
    {
        public int LeaveTRID { get; set; }

        [DisplayName("Employee ID")]
        public int EmpID { get; set; }

        [DisplayName("Employee Code")]
        public string EmpCode { get; set; }

        [DisplayName("Employee Name")]
        public string EmpName { get; set; }

        [DisplayName("Designation")]
        public string DesignationTitle { get; set; }

        [DisplayName("Department")]
        public string DepartmentTitle { get; set; }

        [DisplayName("Attendance Date")]
        public DateTime? AttDate { get; set; }

        [DisplayName("Attendance Status")]
        public string AttStatus { get; set; }

        [DisplayName("Leave Type ID")]
        public int LeaveTypeID { get; set; }

        [DisplayName("Leave Type")]
        public string LeaveTypeTitle { get; set; }

        [DisplayName("Leave Applied Date")]
        public DateTime LeaveAppliedDate { get; set; }

        [DisplayName("Leave Comments")]
        public string LeaveComments { get; set; }

        [DisplayName("Leave From")]
        public DateTime? ActualLeaveDateFrom { get; set; }

        [DisplayName("Leave To")]
        public DateTime? ActualLeaveDateTo { get; set; }

        [DisplayName("Leave Duration")]
        public float LeaveDuration { get; set; }

        [DisplayName("Leave Mode")]
        public string LeaveMode { get; set; }

        public DateTime? LeaveApprovedDate { get; set; }

        [DisplayName("Approval Comments")]
        public string LeaveApprovalComments { get; set; }
        public DateTime? LeaveRejectedDate { get; set; }

        [DisplayName("Rejection Comments")]
        public string LeaveRejectionComments { get; set; }
        public int OrderID { get; set; }
        public int ApprovedOrRejectedByEmpID { get; set; }
        public string LeaveStatus { get; set; }

        [DisplayName("Cancelled")]
        public bool Canceled { get; set; }

        [DisplayName("Cancelled Date")]
        public DateTime? CanceledDate { get; set; }
    }
}
