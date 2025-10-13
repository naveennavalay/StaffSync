using ModelStaffSync;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class clsEmployeeMaster
    {
        dbStaffSync.clsEmployeeMaster objEmployeeMaster = new dbStaffSync.clsEmployeeMaster();

        public List<ReportingManagerInfo> objReportingManagerInfo { get; set; }

        public clsEmployeeMaster() { 

        }


        public List<ReportingManagerInfo> getCompleteEmployeesList()
        {
            List<ReportingManagerInfo> employeesList = new List<ReportingManagerInfo>();

            employeesList = objEmployeeMaster.getCompleteEmployeesList();

            return employeesList;
        }

        public DataTable GetEmployeeList()
        {
            DataTable dt = new DataTable();

            dt = objEmployeeMaster.GetEmployeeList();

            return dt;
        }

        public DataTable GetEmployeeList(string filterText)
        {
            DataTable dt = new DataTable();

            dt = objEmployeeMaster.GetEmployeeList(filterText);

            return dt;
        }


        public List<LoggedInUser> getMyEmployeeInformation(int txtEmpID)
        {
            List<LoggedInUser> objEmployeePaySlipList = new List<LoggedInUser>();

            objEmployeePaySlipList = objEmployeeMaster.getMyEmployeeInformation(txtEmpID);

            return objEmployeePaySlipList;
        }

        public EmployeeInfo GetSelectedEmployeeInfo(int EmployeeID)
        {
            EmployeeInfo employeeInfo = new EmployeeInfo();

            employeeInfo = objEmployeeMaster.GetSelectedEmployeeInfo(EmployeeID);

            return employeeInfo;
        }

        public ReportingManagerInfo GetEmployeesList()
        {
            ReportingManagerInfo reportingManagerInfo = new ReportingManagerInfo();

            reportingManagerInfo = objEmployeeMaster.GetEmployeesList();

            return reportingManagerInfo;
        }

        public ReportingManagerInfo GetReportingManagerInfo(int EmployeeID)
        {
            ReportingManagerInfo reportingManagerInfo = new ReportingManagerInfo();

            reportingManagerInfo = objEmployeeMaster.GetReportingManagerInfo(EmployeeID);

            return reportingManagerInfo;
        }

        public int InsertEmployeeMaster(int txtEmployeeID, string txtEmployeeCode, string txtEmployeeTitle, int txtEmployeeDesignationID, int txtReportingManagerID, int txtEmployeeDepartmentID, int txtEmployeeBloodGroupID, bool IsActive, bool IsDeleted, int txtClientID)
        {
            int affectedRows = 0;

            affectedRows = objEmployeeMaster.InsertEmployeeMaster(txtEmployeeID, txtEmployeeCode, txtEmployeeTitle, txtEmployeeDesignationID, txtReportingManagerID, txtEmployeeDepartmentID, txtEmployeeBloodGroupID, IsActive, IsDeleted, txtClientID);

            return affectedRows;
        }

        public int UpdateEmployeeMaster(int txtEmployeeID, string txtEmployeeCode, string txtEmployeeTitle, int txtEmployeeDesignationID, int txtReportingManagerID, int txtEmployeeDepartmentID, int txtEmployeeBloodGroupID, bool IsActive, bool IsDeleted, int txtClientID)
        {
            int affectedRows = 0;

            affectedRows = objEmployeeMaster.UpdateEmployeeMaster(txtEmployeeID, txtEmployeeCode, txtEmployeeTitle, txtEmployeeDesignationID, txtReportingManagerID, txtEmployeeDepartmentID, txtEmployeeBloodGroupID, IsActive, IsDeleted, txtClientID);

            return affectedRows;
        }

        public int DeleteEmployeeMaster(int txtEmployeeID)
        {
            int affectedRows = 0;

            affectedRows = objEmployeeMaster.DeleteEmployeeMaster(txtEmployeeID);

            return affectedRows;
        }
    }

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

    //public class ReportingManagerInfo
    //{
    //    public int EmpID { get; set; }

    //    [DisplayName("Employee Code")]
    //    public string EmpCode { get; set; }

    //    [DisplayName("Employee Name")]
    //    public string EmpName { get; set; }

    //    [DisplayName("Designation")]
    //    public string DesignationTitle { get; set; }

    //    [DisplayName("Department")]
    //    public string DepartmentTitle { get; set; }

    //    [DisplayName("Contact Number")]
    //    public string ContactNumber1 { get; set; }

    //    [DisplayName("Mail ID")]
    //    public string ContactNumber2 { get; set; }
    //}
}
