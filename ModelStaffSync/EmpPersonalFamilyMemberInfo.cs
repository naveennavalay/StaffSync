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
        public bool PFApplicable { get; set; }
        public string PFAccNumber { get; set; }
        public DateTime PFJoiningDate { get; set; }
        public DateTime PFRelievingDate { get; set; }
        public bool PTApplicable { get; set; }
        public string PTAccNumber { get; set; }
        public bool ESIApplicable { get; set; }
        public string ESIAccNumber { get; set; }
        public string ESIDispensary { get; set; }
        public bool NPSApplicable { get; set; }
        public string NPSAccNumber { get; set; }
    }
}
