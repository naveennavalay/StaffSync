using ModelStaffSync.Enum;
using ModelStaffSync.Enums;
using ModelStaffSync.Reports.Attributes;
using ReportingEngine.Enum;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    //public class EmployeeInfo
    //{
    //    public int EmpID { get; set; }
    //    public string EmpCode { get; set; }
    //    public string EmpName { get; set; }
    //    public int EmpDesignationID { get; set; }
    //    public string DesignationTitle { get; set; }
    //    public int EmpRepManID { get; set; }
    //    public int DepartmentID { get; set; }
    //    public string DepartmentTitle { get; set; }
    //    public int BloodGroupID { get; set; }
    //    public string BloodGroupTitle { get; set; }
    //    public bool IsActive { get; set; }
    //    public bool IsDeleted { get; set; }
    //}

    public class EmployeeInfo
    {
        public int EmpID { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public int EmpDesignationID { get; set; }
        public string DesignationTitle { get; set; }
        public int EmpRepManID { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentTitle { get; set; }
        public int BloodGroupID { get; set; }
        public string BloodGroupTitle { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class EmpStateAndGenderInfo
    {
        public int EmpID { get; set; }
        public int StateID { get; set; }
        public int SexID { get; set; }
    }


    public class  ActiveEmployeeListReport
    {
        public string FinYearFromTo { get; set; }
        public int EmpID { get; set; }

        //[ReportColumnAttribute(Header = "Emp Code", Width = 6, Alignment = ReportAlignment.Left, Format = "Currency", ShowTotal = true, Visible = true)]
        [ReportColumnAttribute(Header = "Emp Code", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto )]
        public string EmpCode { get; set; }

        [ReportColumnAttribute(Header = "Emp Name", Width = 5, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)] 
        public string EmpName { get; set; }
        
        [ReportColumnAttribute(Header = "Designation", Width = 5, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)] 
        public string DesignationTitle { get; set; }

        [ReportColumnAttribute(Header = "Department", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)] 
        public string DepartmentTitle { get; set; }

        [ReportColumnAttribute(Header = "Contact Number", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string ContactNumber1 { get; set; }

        [ReportColumnAttribute(Header = "Mail ID", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string ContactNumber2 { get; set; }

        [ReportColumnAttribute(Header = "Status", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string Status { get; set; }

        [ReportColumnAttribute(Header = "Date Of Joining", Width = 3, Alignment = ReportAlignment.Left, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public DateTime DOJ { get; set; }

        [ReportColumnAttribute(Header = "Probation Last Date", Width = 3, Alignment = ReportAlignment.Left, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public DateTime LastDateOfProbation { get; set; }

        [ReportColumnAttribute(Header = "Date Of Confirm", Width = 3, Alignment = ReportAlignment.Left, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public DateTime DateOfConfirmation { get; set; }

        [ReportColumnAttribute(Header = "Gender", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string SexTitle { get; set; }

        [ReportColumnAttribute(Header = "Blood Group", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string BloodGroupTitle { get; set; }

        [ReportColumnAttribute(Header = "Branch Code", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string ClientBranchCode { get; set; }

        [ReportColumnAttribute(Header = "Branch Name", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string ClientBranchName { get; set; }

        [ReportColumnAttribute(Header = "Nominee", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string NomineePerson { get; set; }

        [ReportColumnAttribute(Header = "Contact Number", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string ContactNumber { get; set; }

        [ReportColumnAttribute(Header = "Relationship", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string RelationShipTitle { get; set; }
    }
}
