using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class BankDetailsInfo
    {
        public int BankID { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public string IFSCCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class EmployeeBankInfo
    {
        public int EmpBankID { get; set; }
        public int EmpID { get; set; }
        public int BankID { get; set; }
        public string EmpACNumber { get; set; }
        public bool IsDefault { get; set; }
    }
}
