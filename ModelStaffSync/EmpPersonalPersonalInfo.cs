using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class EmpPersonalPersonalInfo
    {
        public int PersonalInfoID { get; set; }
        public int EmpID { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DOJ { get; set; }
        public int EduQualID { get; set; }
        public int PerAddressID { get; set; }
        public int CurAddressID { get; set; }
        public string ContactNumber1 { get; set; }
        public string ContactNumber2 { get; set; }
        public int ContactID1 { get; set; }
        public int ContactID2 { get; set; }
        public int SexID { get; set; }
        public int LastCompanyInfoID { get; set; }
        public int ClientBranchID { get; set; }
    }
}
