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
        public int EmpID { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string DesignationTitle { get; set; }
        public string DepartmentTitle { get; set; }
        public string ReimbursRequestCode { get; set; }
        public string ReimbTitle { get; set; }
        public DateTime ReimbursRequestDate { get; set; }
        public decimal ReimbursRequestAmount { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool ReimbursAproveStatus1 { get; set; }
        public bool ReimbursAproveStatus2 { get; set; }
    }
}
