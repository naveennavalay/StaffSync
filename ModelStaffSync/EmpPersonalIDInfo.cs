using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class EmpPersonalFamilyMemberInfo
    {
        public int EmpPerFamInfoID { get; set; }
        public int PersonalInfoID { get; set; }
        public string FamMemName { get; set; }
        public DateTime FamMemDOB { get; set; }
        public int FamMemAge { get; set; }
        public string FamMemRelationship { get; set; }
        public string FamMemAddr1 { get; set; }
        public string FamMemAddr2 { get; set; }
        public string FamMemArea { get; set; }
        public string FamMemCity { get; set; }
        public string FamMemState { get; set; }
        public string FamMemPIN { get; set; }
        public string FamMemCountry { get; set; }
        public string FamMemContactNumber { get; set; }
        public string FamMemMailID { get; set; }
        public string FamMemBloodGroup { get; set; }
    }
}
