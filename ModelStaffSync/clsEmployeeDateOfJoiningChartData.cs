using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class EmployeeDateOfJoiningChartData
    {
        public int EmpID { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string DesignationTitle { get; set; }
        public string DepartmentTitle { get; set; }
        public DateTime? DOJ { get; set; }
        public int PhotoID { get; set; }
        public string EmpPhoto { get; set; }
        public string EmpPhotoBase64 { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int ClientID { get; set; }
    }
}
