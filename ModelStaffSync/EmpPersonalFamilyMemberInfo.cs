using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class EmpPersonalIDInfo
    {
        public int EmpGovtID { get; set; }
        public int PersonalInfoID { get; set; }
        public string AadhaarCardNumber { get; set; }
        public string VoterCardNumber { get; set; }
        public string PANNumber { get; set; }
        public string PassportNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime RenewalDate { get; set; }
        public string ID1 { get; set; }
        public string ID2 { get; set; }
        public string ID3 { get; set; }
        public string ID4 { get; set; }
        public string ID5 { get; set; }
    }
}
