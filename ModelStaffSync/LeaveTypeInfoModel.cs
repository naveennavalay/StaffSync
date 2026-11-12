using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class LeaveTypeInfoModel
    {
        public int LeaveTypeID { get; set; }

        [DisplayName("Leave Type Code")]
        public string LeaveCode { get; set; }

        [DisplayName("Leave Type Title")]
        public string LeaveTypeTitle { get; set; }

        [DisplayName("Is Paid")]
        public bool IsPaid { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public int OrderID { get; set; }

        [DisplayName("Carry Forward Allowed")] 
        public bool IsCarryForwardAllowed { get; set; }
        
        [DisplayName("Max. Days Carry Forward Allowed")] 
        public int MaxCarryForwardAllowed { get; set; }

        [DisplayName("Carry Forward Expiry Applicable")]
        public bool IsCarryForwardExpiryApplicable { get; set; }

        [DisplayName("Carry Forward Expiry Months")]
        public int CarryForwardExpiryMonths { get; set; }

        [DisplayName("Encashment Allowed")]
        public bool IsEncashmentAllowed { get; set; }
        
        [DisplayName("Max. Leave Days Allowed to Encash")]
        public int MaxLeaveDaysAllowedToEncash { get; set; }
        
        [DisplayName("Encashment During Employment Allowed")]
        public bool IsEncashmentDuringEmploymentAllowed { get; set; }

        [DisplayName("Encashment at Resignation Allowed")]
        public bool IsEncashmentAtResignationAllowed { get; set; }

        [DisplayName("Leave Accrual Applicable")]
        public bool IsLeaveAccrualApplicable { get; set; }
        
        [DisplayName("Accrual Frequency")]
        public int AccrualFrequency { get; set; }
        
        [DisplayName("Applicable During Probation")]
        public bool IsApplicableDuringProbation { get; set; }
        
        [DisplayName("Applicable During Notice Period")]
        public bool IsApplicableDuringNoticePeriod { get; set; }
    }

    public class LeaveIsPaid
    {
        public bool IsPaid { get; set; }
    }
}
