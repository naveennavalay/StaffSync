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
    public class EmpResignGratuityInfo
    {
        public int EmpResignGratuityID { get; set; }
        public int EmpResignID { get; set; }
        public DateTime DateOfJoining { get; set; }
        public DateTime ResignationDate { get; set; }
        public DateTime LastWorkingDate { get; set; }
        public int ActualServiceYears { get; set; }
        public int ActualServiceMonths { get; set; }
        public int ActualServiceDays { get; set; }
        public decimal GratuityServiceYears { get; set; }
        public bool IsGratuityEligible { get; set; }
        public string EligibilityStatus { get; set; }
        public string GratuityFormulaTitle { get; set; }
        public decimal LastDrawnBasicSalary { get; set; }
        public decimal EligibleAllowances { get; set; }
        public decimal WageAdjustmentAmount { get; set; }
        public decimal GratuitySalaryBasis { get; set; }
        public int GratuityDays { get; set; }
        public int WageDivisorratuityDays { get; set; } = 26;
        public decimal GrossGratuityAmount { get; set; }
        public decimal StatutoryGratuityLimit { get; set; } = 2000000;
        public decimal GratuityCeilingAdjustment { get; set; }
        public decimal TaxOrDeductionAmount { get; set; }
        public decimal NetGratuityAmount { get; set; }
        public int HRVerifiedBy { get; set; }
        public DateTime HRVerificationDate { get; set; }
        public int HRVerificationStatus { get; set; }
        public string HRRemarks { get; set; }
        public int FinanceVerifiedBy { get; set; }
        public DateTime FinanceVerificationDate { get; set; }
        public int FinanceVerificationStatus {  get; set; }
        public string FinanceRemarks { get; set; }
        public int PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentReferenceNo { get; set; }
        public string PaymentRemarks { get; set; }
    }
}
