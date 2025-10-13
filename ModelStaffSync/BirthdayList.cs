using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class BirthdayList
    {
        public int EmpID { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string DepartmentTitle { get; set; }
        public string DesignationTitle { get; set; }
        public DateTime DOB { get; set; }
        public byte[] EmpPhoto { get; set; }
    }
}
