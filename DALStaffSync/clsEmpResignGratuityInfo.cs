using ModelStaffSync;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace DALStaffSync
{
    public class clsEmpResignGratuityInfo
    {
        dbStaffSync.clsEmpResignGratuityInfo objEmpResignGratuityInfo = new dbStaffSync.clsEmpResignGratuityInfo();

        public EmpResignGratuityInfo getEmpResignResignGratuityInfotatements(int EmpResignID)
        {
            return objEmpResignGratuityInfo.getEmpResignResignGratuityInfotatements(EmpResignID);
        }

        public int InsertEmpResignResignGratuityInfo(int EmpResignID, DateTime DateOfJoining, DateTime ResignationDate, DateTime LastWorkingDate, int ActualServiceYears, int ActualServiceMonths, int ActualServiceDays, 
            decimal GratuityServiceYears, bool IsGratuityEligible, string EligibilityStatus, string GratuityFormulaTitle,
            decimal LastDrawnBasicSalary, decimal EligibleAllowances, decimal WageAdjustmentAmount, decimal GratuitySalaryBasis, int GratuityDays, int WageDivisor,
            decimal GrossGratuityAmount, decimal StatutoryGratuityLimit, decimal GratuityCeilingAdjustment, decimal TaxOrDeductionAmount, decimal NetGratuityAmount,
            int HRVerifiedBy, DateTime HRVerificationDate, int HRVerificationStatus, string HRRemarks,
            int FinanceVerifiedBy, DateTime FinanceVerificationDate, int FinanceVerificationStatus, string FinanceRemarks,
            int PaymentStatus, DateTime PaymentDate, string PaymentReferenceNo, string PaymentRemarks)
        {
            return objEmpResignGratuityInfo.InsertEmpResignResignGratuityInfo(EmpResignID, DateOfJoining, ResignationDate, LastWorkingDate, ActualServiceYears, ActualServiceMonths, ActualServiceDays,
                GratuityServiceYears, IsGratuityEligible, EligibilityStatus, GratuityFormulaTitle,
                LastDrawnBasicSalary, EligibleAllowances, WageAdjustmentAmount, GratuitySalaryBasis, GratuityDays, WageDivisor,
                GrossGratuityAmount, StatutoryGratuityLimit, GratuityCeilingAdjustment, TaxOrDeductionAmount, NetGratuityAmount,
                HRVerifiedBy, HRVerificationDate, HRVerificationStatus, HRRemarks,
                FinanceVerifiedBy, FinanceVerificationDate, FinanceVerificationStatus, FinanceRemarks,
                PaymentStatus, PaymentDate, PaymentReferenceNo, PaymentRemarks);
        }

        public int UpdateEmpResignResignGratuityInfo(int EmpResignGratuityID, int EmpResignID, DateTime DateOfJoining, DateTime ResignationDate, DateTime LastWorkingDate, int ActualServiceYears, int ActualServiceMonths, int ActualServiceDays,
            decimal GratuityServiceYears, bool IsGratuityEligible, string EligibilityStatus, string GratuityFormulaTitle,
            decimal LastDrawnBasicSalary, decimal EligibleAllowances, decimal WageAdjustmentAmount, decimal GratuitySalaryBasis, int GratuityDays, int WageDivisor,
            decimal GrossGratuityAmount, decimal StatutoryGratuityLimit, decimal GratuityCeilingAdjustment, decimal TaxOrDeductionAmount, decimal NetGratuityAmount,
            int HRVerifiedBy, DateTime HRVerificationDate, int HRVerificationStatus, string HRRemarks,
            int FinanceVerifiedBy, DateTime FinanceVerificationDate, int FinanceVerificationStatus, string FinanceRemarks,
            int PaymentStatus, DateTime PaymentDate, string PaymentReferenceNo, string PaymentRemarks)
        {
            return objEmpResignGratuityInfo.UpdateEmpResignResignGratuityInfo(EmpResignGratuityID, EmpResignID, DateOfJoining, ResignationDate, LastWorkingDate, ActualServiceYears, ActualServiceMonths, ActualServiceDays,
                GratuityServiceYears, IsGratuityEligible, EligibilityStatus, GratuityFormulaTitle,
                LastDrawnBasicSalary, EligibleAllowances, WageAdjustmentAmount, GratuitySalaryBasis, GratuityDays, WageDivisor,
                GrossGratuityAmount, StatutoryGratuityLimit, GratuityCeilingAdjustment, TaxOrDeductionAmount, NetGratuityAmount,
                HRVerifiedBy, HRVerificationDate, HRVerificationStatus, HRRemarks,
                FinanceVerifiedBy, FinanceVerificationDate, FinanceVerificationStatus, FinanceRemarks,
                PaymentStatus, PaymentDate, PaymentReferenceNo, PaymentRemarks);
        }
    }
}
