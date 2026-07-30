using Common.Attibutes;
using ModelStaffSync.Enum;
using ModelStaffSync.Enums;
using ModelStaffSync.Reports.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class OutstandingLeaveStatement
    {
        [ReportColumnAttribute(Header = "Select", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = false, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        [ReportIgnore] 
        public bool Select { get; set; }

        [ReportColumnAttribute(Header = "EmpID", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = false, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        [ReportIgnore]
        public int EmpID { get; set; }

        [ReportColumnAttribute(Header = "Employee Code", Width = 5, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        [DisplayName("Employee Code")]
        public string EmpCode { get; set; }

        [ReportColumnAttribute(Header = "Employee Name", Width = 5, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]        
        [DisplayName("Employee Name")]
        public string EmpName { get; set; }

        [ReportColumnAttribute(Header = "Designation", Width = 5, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        [DisplayName("Designation")]
        public string DesignationTitle { get; set; }

        [ReportColumnAttribute(Header = "Department", Width = 5, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]        
        [DisplayName("Department")]
        public string DepartmentTitle { get; set; }

        [ReportColumnAttribute(Header = "Total Leaves", Width = 5, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]        
        [DisplayName("Total Leaves")]
        public float TotalLeaves { get; set; }

        [ReportColumnAttribute(Header = "Balance Leaves", Width = 5, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        [DisplayName("Balance Leaves")]
        public float BalanceLeaves { get; set; }

        [ReportColumnAttribute(Header = "Utilised Leaves", Width = 5, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        [DisplayName("Utilised Leaves")]
        public float UtilisedLeaves { get; set; }
    }
}
