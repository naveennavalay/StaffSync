using ModelStaffSync.Enum;
using ModelStaffSync.Enums;
using ModelStaffSync.Reports.Attributes;
using Newtonsoft.Json;
using ReportingEngine.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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


    public class ActiveEmployeeListReport
    {
        public string FinYearFromTo { get; set; }
        public int EmpID { get; set; }

        //[ReportColumnAttribute(Header = "Emp Code", Width = 6, Alignment = ReportAlignment.Left, Format = "Currency", ShowTotal = true, Visible = true)]
        [ReportColumnAttribute(Header = "Emp Code", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        public string EmpCode { get; set; }

        [ReportColumnAttribute(Header = "Emp Name", Width = 5, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string EmpName { get; set; }

        [ReportColumnAttribute(Header = "Designation", Width = 5, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string DesignationTitle { get; set; }

        [ReportColumnAttribute(Header = "Department", Width = 4, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string DepartmentTitle { get; set; }

        [ReportColumnAttribute(Header = "Contact Number", Width = 3.5, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string ContactNumber1 { get; set; }

        [ReportColumnAttribute(Header = "Mail ID", Width = 5, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string ContactNumber2 { get; set; }

        [ReportColumnAttribute(Header = "Status", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = false, AutoFit = true)]
        public string Status { get; set; }

        [ReportColumnAttribute(Header = "Joining Date", Width = 3, Alignment = ReportAlignment.Left, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public DateTime DOJ { get; set; }

        [ReportColumnAttribute(Header = "Probation Last Date", Width = 3, Alignment = ReportAlignment.Left, Format = "Date", ShowTotal = false, Visible = false, AutoFit = false)]
        public DateTime LastDateOfProbation { get; set; }

        [ReportColumnAttribute(Header = "Date Of Confirm", Width = 3, Alignment = ReportAlignment.Left, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public DateTime DateOfConfirmation { get; set; }

        [ReportColumnAttribute(Header = "Gender", Width = 2, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string SexTitle { get; set; }

        [ReportColumnAttribute(Header = "Blood Group", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string BloodGroupTitle { get; set; }

        [ReportColumnAttribute(Header = "Branch Code", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string ClientBranchCode { get; set; }

        [ReportColumnAttribute(Header = "Branch Name", Width = 5, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string ClientBranchName { get; set; }

        [ReportColumnAttribute(Header = "Nominee", Width = 4, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string NomineePerson { get; set; }

        [ReportColumnAttribute(Header = "Contact Number", Width = 2.5, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string ContactNumber { get; set; }

        [ReportColumnAttribute(Header = "Relationship", Width = 2.5, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string RelationShipTitle { get; set; }
    }

    public class PersonalInformationListReport
    {
        public string FinYearFromTo { get; set; }
        public int EmpID { get; set; }

        //[ReportColumnAttribute(Header = "Emp Code", Width = 6, Alignment = ReportAlignment.Left, Format = "Currency", ShowTotal = true, Visible = true)]
        [ReportColumnAttribute(Header = "Emp Code", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        public string EmpCode { get; set; }

        [ReportColumnAttribute(Header = "Emp Name", Width = 5, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string EmpName { get; set; }

        [ReportColumnAttribute(Header = "Designation", Width = 5, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string DesignationTitle { get; set; }

        [ReportColumnAttribute(Header = "Department", Width = 5, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string DepartmentTitle { get; set; }

        [ReportColumnAttribute(Header = "Contact Number", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string ContactNumber1 { get; set; }

        [ReportColumnAttribute(Header = "Mail ID", Width = 6, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string ContactNumber2 { get; set; }

        [ReportColumnAttribute(Header = "Gender", Width = 2.5, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string SexTitle { get; set; }

        [ReportColumnAttribute(Header = "Current Address", Width = 8, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string CurrentAddress { get; set; }

        [ReportColumnAttribute(Header = "Permanent Address", Width = 8, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string PermanentAddress { get; set; }

        [ReportColumnAttribute(Header = "Contact Person Info", Width = 6, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string ContactPersonInfo { get; set; }

        [ReportColumnAttribute(Header = "Nominee Info", Width = 6, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string NomineeInfo { get; set; }
    }

    public class EmployeeActiveInactiveReport
    {
        public string FinYearFromTo { get; set; }
        public int EmpID { get; set; }

        //[ReportColumnAttribute(Header = "Emp Code", Width = 6, Alignment = ReportAlignment.Left, Format = "Currency", ShowTotal = true, Visible = true)]
        [ReportColumnAttribute(Header = "Emp Code", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        public string EmpCode { get; set; }

        [ReportColumnAttribute(Header = "Emp Name", Width = 5, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string EmpName { get; set; }

        [ReportColumnAttribute(Header = "Designation", Width = 5, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string DesignationTitle { get; set; }

        [ReportColumnAttribute(Header = "Department", Width = 4, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string DepartmentTitle { get; set; }

        [ReportColumnAttribute(Header = "Joining Date", Width = 2.75, Alignment = ReportAlignment.Left, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public DateTime DOJ { get; set; }

        [ReportColumnAttribute(Header = "Probation Date", Width = 3, Alignment = ReportAlignment.Left, Format = "Date", ShowTotal = false, Visible = true, AutoFit = false)]
        public DateTime LastDateOfProbation { get; set; }

        [ReportColumnAttribute(Header = "Confirmation Date", Width = 4, Alignment = ReportAlignment.Left, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public DateTime DateOfConfirmation { get; set; }

        [ReportColumnAttribute(Header = "Status ID", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = false, AutoFit = true)] 
        public int EmpActiveInactiveStatusID { get; set; }

        [ReportColumnAttribute(Header = "Status Date", Width = 3, Alignment = ReportAlignment.Left, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public DateTime ActiveInactiveStatusDate { get; set; }

        [ReportColumnAttribute(Header = "Status", Width =2.75, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string ActiveInactiveStatus { get; set; }

        [ReportColumnAttribute(Header = "Comments", Width = 10, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string Comments { get; set; }
    }

    public class MonthlyAttendanceReport
    {
        public string FinYearFromTo { get; set; }
        public int EmpID { get; set; }

        //[ReportColumnAttribute(Header = "Emp Code", Width = 6, Alignment = ReportAlignment.Left, Format = "Currency", ShowTotal = true, Visible = true)]
        [ReportColumnAttribute(Header = "Emp Code", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        public string EmpCode { get; set; }

        [ReportColumnAttribute(Header = "Emp Name", Width = 5, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string EmpName { get; set; }

        [ReportColumnAttribute(Header = "Designation", Width = 5, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string DesignationTitle { get; set; }

        [ReportColumnAttribute(Header = "Department", Width = 5, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string DepartmentTitle { get; set; }

        //[ReportColumnAttribute(Header = "Joining Date", Width = 2.5, Alignment = ReportAlignment.Left, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        //public DateTime DOJ { get; set; }

        [JsonProperty("1")]
        [ReportColumnAttribute(Header = "1", Width = 1, Alignment = ReportAlignment.Center, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public string _1 { get; set; }

        [JsonProperty("2")]
        [ReportColumnAttribute(Header = "2", Width = 1, Alignment = ReportAlignment.Center, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public string _2 { get; set; }

        [JsonProperty("3")]
        [ReportColumnAttribute(Header = "3", Width = 1, Alignment = ReportAlignment.Center, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public string _3 { get; set; }

        [JsonProperty("4")]
        [ReportColumnAttribute(Header = "4", Width = 1, Alignment = ReportAlignment.Center, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public string _4 { get; set; }

        [JsonProperty("5")]
        [ReportColumnAttribute(Header = "5", Width = 1, Alignment = ReportAlignment.Center, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public string _5 { get; set; }

        [JsonProperty("6")]
        [ReportColumnAttribute(Header = "6", Width = 1, Alignment = ReportAlignment.Center, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public string _6 { get; set; }

        [JsonProperty("7")]
        [ReportColumnAttribute(Header = "7", Width = 1, Alignment = ReportAlignment.Center, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public string _7 { get; set; }

        [JsonProperty("8")]
        [ReportColumnAttribute(Header = "8", Width = 1, Alignment = ReportAlignment.Center, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public string _8 { get; set; }

        [JsonProperty("9")]
        [ReportColumnAttribute(Header = "9", Width = 1, Alignment = ReportAlignment.Center, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public string _9 { get; set; }

        [JsonProperty("10")]
        [ReportColumnAttribute(Header = "10", Width = 1, Alignment = ReportAlignment.Center, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public string _10 { get; set; }

        [JsonProperty("11")]
        [ReportColumnAttribute(Header = "11", Width = 1, Alignment = ReportAlignment.Center, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public string _11 { get; set; }

        [JsonProperty("12")]
        [ReportColumnAttribute(Header = "12", Width = 1, Alignment = ReportAlignment.Center, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public string _12 { get; set; }

        [JsonProperty("13")]
        [ReportColumnAttribute(Header = "13", Width = 1, Alignment = ReportAlignment.Center, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public string _13 { get; set; }

        [JsonProperty("14")]
        [ReportColumnAttribute(Header = "14", Width = 1, Alignment = ReportAlignment.Center, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public string _14 { get; set; }

        [JsonProperty("15")]
        [ReportColumnAttribute(Header = "15", Width = 1, Alignment = ReportAlignment.Center, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public string _15 { get; set; }

        [JsonProperty("16")]
        [ReportColumnAttribute(Header = "16", Width = 1, Alignment = ReportAlignment.Center, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public string _16 { get; set; }

        [JsonProperty("17")]
        [ReportColumnAttribute(Header = "17", Width = 1, Alignment = ReportAlignment.Center, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public string _17 { get; set; }

        [JsonProperty("18")]
        [ReportColumnAttribute(Header = "18", Width = 1, Alignment = ReportAlignment.Center, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public string _18 { get; set; }

        [JsonProperty("19")]
        [ReportColumnAttribute(Header = "19", Width = 1, Alignment = ReportAlignment.Center, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public string _19 { get; set; }

        [JsonProperty("20")]
        [ReportColumnAttribute(Header = "20", Width = 1, Alignment = ReportAlignment.Center, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public string _20 { get; set; }

        [JsonProperty("21")]
        [ReportColumnAttribute(Header = "21", Width = 1, Alignment = ReportAlignment.Center, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public string _21 { get; set; }

        [JsonProperty("22")]
        [ReportColumnAttribute(Header = "22", Width = 1, Alignment = ReportAlignment.Center, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public string _22 { get; set; }

        [JsonProperty("23")]
        [ReportColumnAttribute(Header = "23", Width = 1, Alignment = ReportAlignment.Center, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public string _23 { get; set; }

        [JsonProperty("24")]
        [ReportColumnAttribute(Header = "24", Width = 1, Alignment = ReportAlignment.Center, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public string _24 { get; set; }

        [JsonProperty("25")]
        [ReportColumnAttribute(Header = "25", Width = 1, Alignment = ReportAlignment.Center, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public string _25 { get; set; }

        [JsonProperty("26")]
        [ReportColumnAttribute(Header = "26", Width = 1, Alignment = ReportAlignment.Center, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public string _26 { get; set; }

        [JsonProperty("27")]
        [ReportColumnAttribute(Header = "27", Width = 1, Alignment = ReportAlignment.Center, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public string _27 { get; set; }

        [JsonProperty("28")]
        [ReportColumnAttribute(Header = "28", Width = 1, Alignment = ReportAlignment.Center, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public string _28 { get; set; }

        [JsonProperty("29")]
        [ReportColumnAttribute(Header = "29", Width = 1, Alignment = ReportAlignment.Center, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public string _29 { get; set; }

        [JsonProperty("30")]
        [ReportColumnAttribute(Header = "30", Width = 1, Alignment = ReportAlignment.Center, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public string _30{ get; set; }

        [JsonProperty("31")]
        [ReportColumnAttribute(Header = "31", Width = 1, Alignment = ReportAlignment.Center, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        public string _31 { get; set; }

        private IEnumerable<string> GetAttendanceValues()
        {
            return GetType()
                .GetProperties()
                .Where(p => p.Name.StartsWith("_"))
                .OrderBy(p => int.Parse(p.Name.Substring(1)))
                .Select(p =>
                {
                    object value = p.GetValue(this, null);
                    return value == null ? string.Empty : value.ToString();
                });
        }

        [ReportColumnAttribute(Header = "P", Width = 1.2, Alignment = ReportAlignment.Center)]
        public int PresentCount
        {
            get
            {
                return GetAttendanceValues().Count(x => x == "P");
            }
        }

        [ReportColumnAttribute(Header = "L", Width = 1.2, Alignment = ReportAlignment.Center)]
        public int LeaveCount
        {
            get
            {
                return GetAttendanceValues().Count(x => x == "L");
            }
        }

        [ReportColumnAttribute(Header = "HL", Width = 1.2, Alignment = ReportAlignment.Center)]
        public int HalfLeaveCount
        {
            get
            {
                return GetAttendanceValues().Count(x =>
                    x == "L/P" ||
                    x == "P/L");
            }
        }
    }

    public class DailyAttendanceReport
    {
        public string FinYearFromTo { get; set; }
        public int EmpID { get; set; }

        //[ReportColumnAttribute(Header = "Emp Code", Width = 6, Alignment = ReportAlignment.Left, Format = "Currency", ShowTotal = true, Visible = true)]
        [ReportColumnAttribute(Header = "Emp Code", Width = 3, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        public string EmpCode { get; set; }

        [ReportColumnAttribute(Header = "Emp Name", Width = 5, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string EmpName { get; set; }

        [ReportColumnAttribute(Header = "Designation", Width = 5, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string DesignationTitle { get; set; }

        [ReportColumnAttribute(Header = "Department", Width = 5, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        public string DepartmentTitle { get; set; }

        //[ReportColumnAttribute(Header = "Joining Date", Width = 2.5, Alignment = ReportAlignment.Left, Format = "Date", ShowTotal = false, Visible = true, AutoFit = true)]
        //public DateTime DOJ { get; set; }

        [ReportColumnAttribute(Header = "Attendance Status", Width = 4, Alignment = ReportAlignment.Center, ShowTotal = false, Visible = true, AutoFit = true)]
        public string AttendanceStatus { get; set; }
    }

    public class MonthlyAttendanceSummary
    {
        [ReportColumnAttribute(Header = "Header", Width = 6, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true, SizeMode = ReportColumnSizeMode.Auto)]
        [DisplayName("Header")]
        public string RowHeader { get; set; }

        [ReportColumnAttribute(Header = "Value", Width = 6, Alignment = ReportAlignment.Left, ShowTotal = false, Visible = true, AutoFit = true)]
        [DisplayName("Value")]
        public string RowValue { get; set; }
    }
}
