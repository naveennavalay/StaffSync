using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.ComponentModel;

namespace ModelStaffSync
{
    public class AdvanceTypesModel
    {
        public int AdvanceTypeID { get; set; }
        public string AdvanceTypeCode { get; set; }
        public string AdvanceTypeTitle { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int OrderID { get; set; }
        public int ClientID { get; set; }
    }

    public class AdvanceTypeConfigModel
    {
        public int AdvanceTypeConfigID { get; set; }
        public int AdvanceTypeID { get; set; }
        public bool AutoDeductFromSalary { get; set; }
        public string BasedOnNetOrGross { get; set; }
        public string MaxPerOfNetOrGross { get; set; }
        public decimal MaxPercentage { get; set; }
        public decimal MaxFixed { get; set; }
        public bool IncludeInSalary { get; set; }
        public bool RecoveryRequired { get; set; }
        public bool AutoRecoveryFromNextSalary { get; set; }
        public bool InterestRequired { get; set; }
        public bool ApprovalRequired { get; set; }
        public bool AllowPause { get; set; }
        public bool WaiverAllowed { get; set; }
        public decimal MaxTenure { get; set; }
    }

    public class AdvanceRequestStatusInfo
    {
        public bool AdvanceRequestStatus { get; set; }
    }

    public class AdvanceApprovalPendingList
    {
        public bool Select { get; set; }

        [DisplayName("Requester Employee ID")]
        public int RequesterEmpID { get; set; }
        
        [DisplayName("Employee Code")] 
        public string RequesterEmpCode { get; set; }
        
        [DisplayName("Employee Name")] 
        public string RequesterEmpName { get; set; }
        
        [DisplayName("Designation")] 
        public string RequesterDesignationTitle { get; set; }

        [DisplayName("Department")]
        public string RequesterDepartmentTitle { get; set; }
        
        [DisplayName("Advance Request ID")] 
        public int EmpAdvanceRequestID { get; set; }
        
        [DisplayName("Advance Type")] 
        public string AdvanceTypeTitle { get; set; }
        
        [DisplayName("Request Code")] 
        public string EmpAdvReqCode { get; set; }
        
        [DisplayName("Request Date")] 
        public DateTime EmpAdvanceRequestDate { get; set; }

        [DisplayName("Advance Amount")] 
        public decimal AdvanceAmount { get; set; }

        [DisplayName("Approver ID")] 
        public string ApproverEmpID1 { get; set; }
        
        [DisplayName("Approver Code")] 
        public string ApproverEmpCode1 { get; set; }
        
        [DisplayName("Approver Name")] 
        public string ApproverEmpName1 { get; set; }
        
        [DisplayName("Approver Mail ID")] 
        public string ApproverEmpMailID1 { get; set; }
        
        [DisplayName("Status")] 
        public string ApproverRequestedToComments1 { get; set; }

        [DisplayName("Approver ID")] 
        public string ApproverEmpID2 { get; set; }
        
        [DisplayName("Approver Code")] 
        public string ApproverEmpCode2 { get; set; }
        
        [DisplayName("Approver2 Name")] 
        public string ApproverEmpName2 { get; set; }
        
        [DisplayName("Approver Mail ID")] 
        public string ApproverEmpMailID2 { get; set; }
        
        [DisplayName("Status")] 
        public string RequestMovedToComments { get; set; }

        [DisplayName("Request Status")] 
        public bool AdvanceRequestStatus { get; set; }
    }

    public class EmployeeSpecificAdvanceInformation
    {
        public bool Select { get; set; }

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

        [DisplayName("Personal ID")]
        public int PersonalInfoID { get; set; }

        [DisplayName("Mail ID")] 
        public string ContactNumber2 { get; set; }

        [DisplayName("EmpAdvanceRequestID")]
        public int EmpAdvanceRequestID { get; set; }

        [DisplayName("Advance Request Code")] 
        public string EmpAdvReqCode { get; set; }

        public int AdvanceTypeID { get; set; }

        [DisplayName("Advance Type")] 
        public string AdvanceTypeTitle { get; set; }
        
        [DisplayName("Advance Amount")] 
        public decimal AdvanceAmount { get; set; }

        [DisplayName("Advance Installment")]
        public decimal AdvanceInstallment { get; set; }

        [DisplayName("Start Date")] 
        public DateTime AdvanceStartDate { get; set; }

        [DisplayName("End Date")] 
        public DateTime AdvanceEndDate { get; set; }

        [DisplayName("Request Status")]
        public bool AdvanceRequestStatus { get; set; }

        [DisplayName("Last Repay Date")] 
        public DateTime LastRepayDate { get; set; }

        [DisplayName("Last Repay ID")] 
        public int EmpAdvanceRecoveryID { get; set; }
        
        [DisplayName("Outstanding Balance")] 
        public decimal CBalance { get; set; }

        [DisplayName("RePay Amount")]
        public decimal RePaymentBalance { get; set; }
    }

    public class EmployeeSpecificAdvanceStatemetns
    {
        public int EmpAdvanceRequestID { get; set; }
        public int EmpAdvanceRecoveryID { get; set; }

        [DisplayName("Repayment Date")]
        public DateTime AdvanceDate { get; set; }

        [DisplayName("Opening Amount")] 
        public decimal OBalance { get; set; }

        [DisplayName("Credit Amount")] 
        public decimal CrBalance { get; set; }

        [DisplayName("Debit Amount")]
        public decimal DrBalance { get; set; }

        [DisplayName("Closing Amount")]
        public decimal CBalance { get; set; }

        [DisplayName("Transaction Type")]
        public string TRType { get; set; }

        [DisplayName("Comments")]
        public string Comments { get; set; }

        [DisplayName("OrderID")] 
        public int OrderID { get; set; }
    }

    public class AdvanceRiskBaseInfo
    {
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

        public int AdvanceTypeID { get; set; }

        [DisplayName("Advance Type")]
        public string AdvanceTypeTitle { get; set; }

        public int EmpAdvanceRequestID { get; set; }

        [DisplayName("Advance Request Code")]
        public string EmpAdvReqCode { get; set; }

        [DisplayName("Advance Amount")]
        public decimal AdvanceAmount { get; set; }

        [DisplayName("Start Date")]
        public DateTime AdvanceStartDate { get; set; }

        [DisplayName("End Date")]
        public DateTime AdvanceEndDate { get; set; }

        [DisplayName("Recovered Amount")]
        public decimal TotalRecovered { get; set; }

        [DisplayName("Balance Amount")]
        public decimal RemainingBalance { get; set; }

        [DisplayName("Age Days")]
        public decimal AdvanceAgeDays {  get; set; }
    }
}
