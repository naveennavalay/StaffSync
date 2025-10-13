using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class EmployeeOOOList
    {
        public int EmpID { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string DesignationTitle { get; set; }
        public string DepartmentTitle { get; set; }
        public string LeaveTypeTitle { get; set; }
        public DateTime ActualLeaveDateFrom { get; set; }
        public DateTime ActualLeaveDateTo { get; set; }
        public decimal LeaveDuration { get; set; }
        public string LeaveMode { get; set; }
        public int OrderID { get; set; }
    }
}
