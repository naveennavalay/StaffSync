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
    public class EmpResignLeaveEncashmentInfo
    {
        public int LeaveEncashmentID { get; set; }
        public int EmpResignID { get; set; }

        [DisplayName("Leave Encahsment Date")]
        public DateTime EncashmentDate { get; set; }

        [DisplayName("Total Leave Allotted")]
        public decimal TotalLeaveAllotted { get; set; }

        [DisplayName("Available Leaves")]
        public decimal TotalLeaveBalance { get; set; }

        [DisplayName("Total Leave Utilised")]
        public decimal TotalLeaveUtilised { get; set; }

        [DisplayName("Total Encashable Leave")]
        public decimal TotalEncashableLeave { get; set; }

        [DisplayName("Days To Encash")] 
        public decimal DaysToEncash { get; set; }

        [DisplayName("Eligible Salary")]
        public decimal EligibleSalary { get; set; }

        [DisplayName("Daily Salary Rate")]
        public decimal DailySalaryRate { get; set; }

        [DisplayName("Salary Calculation Basis")]
        public string SalaryCalculationBasis { get; set; }

        [DisplayName("Encashment Amount")]
        public decimal EncashmentAmount { get; set; }

        [DisplayName("Tax Deduction Amount")]
        public decimal TaxDeduction { get; set; }

        [DisplayName("Net Encashment Amount")]
        public decimal NetEncashmentAmount { get; set; }

        [DisplayName("More Details")]
        public string MoreDetails { get; set; }
        public bool EncashmentProcessStatus { get; set; }
    }
}
