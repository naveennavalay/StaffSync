using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class LeaveEntitlementInfo
    {
        public int LeaveEntmtID { get; set; }
        public int EmpID { get; set; }
        public int LeaveMasID { get; set; }
        public int LeaveTypeID { get; set; }

        [DisplayName("Leave Type")]
        public string LeaveTypeTitle { get; set; }

        [DisplayName("Total Leaves Allotted")]
        public decimal TotalLeaves { get; set; }

        [DisplayName("Total Leaves Available")]
        public decimal BalanceLeaves { get; set; }

        [DisplayName("Total Utilised Leaves")]
        public decimal UsedLeaves { get; set; }
        public int OrderID { get; set; }
    }

    public class ConsolidatedLeaveOutStandingStatement
    {
        public int EmpID { get; set; }
        
        [DisplayName("Employee Code")] 
        public string EmpCode { get; set; }

        [DisplayName("Employee Name")] 
        public string EmpName { get; set; }

        [DisplayName("Department")] 
        public string DesignationTitle { get; set; }

        [DisplayName("Department")] 
        public string DepartmentTitle { get; set; }

        [JsonProperty("01 - Paid Leave")]
        [DisplayName("Paid Leave")]
        public double _01PaidLeave { get; set; }

        [JsonProperty("02 - Compensatory Off")]
        [DisplayName("Compensatory Off")]
        public double _02CompensatoryOff { get; set; }

        [JsonProperty("03 - Unpaid Leave")]
        [DisplayName("Unpaid Leave")]
        public double _03UnpaidLeave { get; set; }

        [JsonProperty("04 - Loss of Pay (LOP) / Leave Without Pay (LWP)")]
        [DisplayName("Loss of Pay (LOP)")]
        public double _04LossofPayLOPLeaveWithoutPayLWP { get; set; }

        [JsonProperty("05 - Sick Leave")]
        [DisplayName("Sick Leave")]
        public double _05SickLeave { get; set; }

        [JsonProperty("06 - Privilege Leave/Earned Leave")]
        [DisplayName("Privilege Leave/Earned Leave")] 
        public double _06PrivilegeLeaveEarnedLeave { get; set; }

        [JsonProperty("07 - Casual Leave")]
        [DisplayName("Casual Leave")] 
        public double _07CasualLeave { get; set; }

        [JsonProperty("08 - Maternity Leave")]
        [DisplayName("Maternity Leave")] 
        public double _08MaternityLeave { get; set; }

        [JsonProperty("09 - Marriage Leave")]
        [DisplayName("Marriage Leave")] 
        public double _09MarriageLeave { get; set; }

        [JsonProperty("10 - Paternity Leave")]
        [DisplayName("Paternity Leave")]
        public double _10PaternityLeave { get; set; }

        [JsonProperty("11 - Bereavement Leave")]
        [DisplayName("Bereavement Leave")] 
        public double _11BereavementLeave { get; set; }

        [JsonProperty("12 - Public Holiday")]
        [DisplayName("Public Holiday")] 
        public double _12PublicHoliday { get; set; }

        [JsonProperty("13 - Birthday Leave")]
        [DisplayName("Birthday Leave")] 
        public double _13BirthdayLeave { get; set; }
    }

    public class LeaveOutStandingSummary
    {
        public int EmpID { get; set; }
        
        [DisplayName("Employee Code")] 
        public string EmpCode { get; set; }
        
        [DisplayName("Employee Name")] 
        public string EmpName { get; set; }

        [DisplayName("Designation")] 
        public string DesignationTitle { get; set; }

        [DisplayName("Department")] 
        public string DepartmentTitle { get; set; }

        [DisplayName("Leave Balance")] 
        public double LeaveBalance { get; set; }
    }
}
