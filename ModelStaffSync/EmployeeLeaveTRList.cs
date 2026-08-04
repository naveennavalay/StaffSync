using ModelStaffSync.Enum;
using ModelStaffSync.Enums;
using ModelStaffSync.Reports.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
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

    public class LeaveRegister
    {

        [DisplayName("Employee ID")]
        public int EmpID { get; set; }

        [DisplayName("Employee Code")]
        [ReportColumnAttribute(Header = "Employee Code", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        public string EmpCode { get; set; }

        [DisplayName("Employee Name")]
        [ReportColumnAttribute(Header = "Employee Name", Width = 5, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)] 
        public string EmpName { get; set; }

        [DisplayName("Designation")]
        [ReportColumnAttribute(Header = "Designation", Width = 6, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        public string DesignationTitle { get; set; }

        [DisplayName("Department")]
        [ReportColumnAttribute(Header = "Department", Width = 6, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        public string DepartmentTitle { get; set; }

        [DisplayName("Leave Type")]
        [ReportColumnAttribute(Header = "Leave Type", Width = 6, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        public string LeaveTypeTitle { get; set; }

        [DisplayName("Date From")]
        [ReportColumnAttribute(Header = "Date From", Width = 3, Format = "Date", Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        public DateTime? ActualLeaveDateFrom { get; set; }

        [DisplayName("Date To")]
        [ReportColumnAttribute(Header = "Date To", Width = 3, Format = "Date", Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        public DateTime? ActualLeaveDateTo { get; set; }

        [DisplayName("Duration")]
        [ReportColumnAttribute(Header = "Duration", Width = 2, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        public double LeaveDuration { get; set; }

        [DisplayName("Leave Mode")]
        [ReportColumnAttribute(Header = "Leave Mode", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        public string LeaveMode { get; set; }

        [DisplayName("Leave Status")]
        [ReportColumnAttribute(Header = "Leave Status", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        public string LeaveStatus { get; set; }

        [DisplayName("Order ID")]
        [ReportColumnAttribute(Header = "Order ID", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = false, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        public int OrderID { get; set; }
    }

    public class PivotLeaveTrendSummary
    {
        [DisplayName("Month Name")]
        [ReportColumnAttribute(Header = "Month Name", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        public string MonthName { get; set; }

        [DisplayName("Leave Year")]
        [ReportColumnAttribute(Header = "Leave Year", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        public int LeaveYear { get; set; }

        [DisplayName("Leave Month")]
        [ReportColumnAttribute(Header = "Leave Month", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        public int LeaveMonth { get; set; }

        [DisplayName("Total Application")]
        [ReportColumnAttribute(Header = "Total Application", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        public double TotalApplication { get; set; }

        [DisplayName("Total Approved")]
        [ReportColumnAttribute(Header = "Total Approved", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        public double Approved { get; set; }

        [DisplayName("Total Rejected")]
        [ReportColumnAttribute(Header = "Total Rejected", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        public double Rejected { get; set; }

        [DisplayName("Total Pending")]
        [ReportColumnAttribute(Header = "Total Pending", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        public double Pending { get; set; }

        [DisplayName("Total Cancelled")]
        [ReportColumnAttribute(Header = "Total Cancelled", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        public double Cancelled { get; set; }

        [DisplayName("Total Leave Days")]
        [ReportColumnAttribute(Header = "Total Leave Days", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        public double TotalLeaveDays { get; set; }
    }
}
