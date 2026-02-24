using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using System.ComponentModel;

namespace ModelStaffSync
{
    public class EmployeeAttendanceInfo
    {
        public int AttID { get; set; }
        public DateTime AttDate { get; set; }
        public int EmpID { get; set; }
        public string AttStatus { get; set; }

        //[JsonProperty("EmpDailyAttendanceInfo.LeaveTRID")]
        public int? LeaveTRID { get; set; }

        public string LeaveComments { get; set; }
    }

    public class MonthlyAttendanceInfo
    {
        public int ClientID { get; set; }        
        public int EmpID { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string DesignationTitle { get; set; }
        public string DepartmentTitle { get; set; }
        public int SlNo { get; set; }
        public DateTime AttdMonth { get; set; }
        public string Day1 { get; set; }
        public string Day2 { get; set; }
        public string Day3 { get; set; }
        public string Day4 { get; set; }
        public string Day5 { get; set; }
        public string Day6 { get; set; }
        public string Day7 { get; set; }
        public string Day8 { get; set; }
        public string Day9 { get; set; }
        public string Day10 { get; set; }
        public object Day11 { get; set; }
        public object Day12 { get; set; }
        public object Day13 { get; set; }
        public object Day14 { get; set; }
        public object Day15 { get; set; }
        public object Day16 { get; set; }
        public object Day17 { get; set; }
        public object Day18 { get; set; }
        public object Day19 { get; set; }
        public object Day20 { get; set; }
        public object Day21 { get; set; }
        public object Day22 { get; set; }
        public object Day23 { get; set; }
        public object Day24 { get; set; }
        public object Day25 { get; set; }
        public object Day26 { get; set; }
        public object Day27 { get; set; }
        public object Day28 { get; set; }
        public object Day29 { get; set; }
        public object Day30 { get; set; }
        public object Day31 { get; set; }
        public object Day32 { get; set; }
    }

    public class UnProcessedAttendanceInfo
    {
        public int BatchAttndEntrNotProcID { get; set; }
        public int EmpID { get; set; }

        [DisplayName("Employee Code")]
        public string EmpCode { get; set; }
        
        [DisplayName("Employee Name")]         
        public string EmpName { get; set; }
        
        [DisplayName("Designation")]        
        public string DesignationTitle { get; set; }
        
        [DisplayName("Department")]
        public string DepartmentTitle { get; set; }
        
        [DisplayName("Processed Date")] 
        public DateTime BatchAttndEntrNotProcDate { get; set; }

        [DisplayName("Comments")]
        public string Comments { get; set; }
        
        public int OrderID { get; set; }

        public int BatchNumber { get; set; }
    }

    public class EmployeeTotalWorkingInfo
    {
        public int EmpID { get; set; }
        public DateTime AttDate { get; set; }
        public decimal TotalDaysInMonth { get; set; }
        public decimal TotalPresent { get; set; }
        public decimal TotalPaidLeave { get; set; }
        public decimal TotalLossOfPay { get; set; }
        public decimal TotalPayableDays { get; set; }
        public decimal BasicPay { get; set; }
        public decimal BasicPerDay { get; set; }
        public decimal BasicPerHour { get; set; }
    }

    public class DaySpecificLeaveCheck
    {
        public string LeaveMode { get; set; }
        public decimal LeaveDuration { get; set; }
        public decimal LeaveDuration1 { get; set; }
    }

}
