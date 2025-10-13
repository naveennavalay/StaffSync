using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    //public class EmployeeInfo
    //{
    //    public int EmpID { get; set; }
    //    public string EmpCode { get; set; }
    //    public string EmpName { get; set; }
    //    public int EmpDesignationID { get; set; }
    //    public string DesignationTitle { get; set; }
    //    public int EmpRepManID { get; set; }
    //    public int DepartmentID { get; set; }
    //    public string DepartmentTitle { get; set; }
    //    public int BloodGroupID { get; set; }
    //    public string BloodGroupTitle { get; set; }
    //    public bool IsActive { get; set; }
    //    public bool IsDeleted { get; set; }
    //}

    public class EmployeeInfo
    {
        public int EmpID { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public int EmpDesignationID { get; set; }
        public string DesignationTitle { get; set; }
        public int EmpRepManID { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentTitle { get; set; }
        public int BloodGroupID { get; set; }
        public string BloodGroupTitle { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
