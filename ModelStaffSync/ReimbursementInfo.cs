using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class ReimbursementInfoPendingList
    {
        public bool Select { get; set; }
        public int EmpID { get; set; }
        
        [DisplayName("Employee Code")]
        public string EmpCode { get; set; }
        
        [DisplayName("Employee Name")] 
        public string EmpName { get; set; }
        
        [DisplayName("Designation")] 
        public string DesignationTitle { get; set; }

        [DisplayName("Department")]
        public string DepartmentTitle { get; set; }
        public int ReimbursRequestID { get; set; }
        
        [DisplayName("Reimb. Code")] 
        public string ReimbursRequestCode { get; set; }
        
        public int ReimbID { get; set; }

        [DisplayName("Reimb. Header")] 
        public string ReimbTitle { get; set; }

        [DisplayName("Reimb. Date")] 
        public DateTime ReimbursRequestDate { get; set; }

        [DisplayName("Reimb. Amount")] 
        public decimal ReimbursRequestAmount { get; set; }
        
        [DisplayName("Reimb. Comment")] 
        public string ReimbursAproveComments1 { get; set; }
        
        //[DisplayName("Reimb. Status")] 
        public bool ReimbursRequestFinalStatus { get; set; }

        [DisplayName("Status")]
        public string RequestStatus { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int ReimbursAproveBy1 { get; set; }
        public int ReimbursAproveBy2 { get; set; }
        public bool ReimbursAproveStatus1 { get; set; }
        public bool ReimbursAproveStatus2 { get; set; }
    }

    public class ReimbursementPaymentsPendingList
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

        public int ReimbID { get; set; }

        [DisplayName("Reimbursment Type")]
        public string ReimbTitle { get; set; }
        
        public int ReimbRequestID { get; set; }

        [DisplayName("Request Code")]
        public string ReimbRequestCode { get; set; }

        [DisplayName("Description")] 
        public string ReimbRequestDescription { get; set; }

        [DisplayName("Request Date")]
        public DateTime ReimbRequestDate { get; set; }

        [DisplayName("Request Status")]
        public string ReimbRequestStatus { get; set; }

        public int ReimbRequestDetailID { get; set; }

        [DisplayName("Payment Date")] 
        public DateTime ReimbRequestTRDate { get; set; }

        [DisplayName("Balance Amount")]
        public decimal CBalance { get; set; }

        [DisplayName("Comments")] 
        public string Comments { get; set; }

        public int ReimbursRequestID { get; set; }

        [DisplayName("Status")]
        public string RequestStatus { get; set; }
        
        public bool EmpIsActive { get; set; }

        public bool EmpIsDeleted { get; set; }

        public bool RequestIsActive { get; set; }

        public bool RequestIsDeleted { get; set; }

        public int ClientID { get; set; }

        public bool IncludeInNextPayslipProcess { get; set; }
    }

}
