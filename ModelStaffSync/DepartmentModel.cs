using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class DepartmentModel
    {
        public int DepartmentID { get; set; }
        public string DepCode { get; set; }

        public string DepartmentTitle { get; set; }
        public string DepartmentInitial { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class EmpListDeaprtmentWise
    {
        public int EmpID { get; set; }
        public string EmpName { get; set; }
    }
}
