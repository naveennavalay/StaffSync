using ModelStaffSync;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace DALStaffSync
{
    public class clsEmpResignLeaveEncashmentInfo
    {
        dbStaffSync.clsEmpResignLeaveEncashmentInfo objEmpResignLeaveEncashmentInfo = new dbStaffSync.clsEmpResignLeaveEncashmentInfo();

        public EmpResignLeaveEncashmentInfo getEmpResignLeaveEncashmentInfotatements(int EmpResignID)
        {
            return objEmpResignLeaveEncashmentInfo.getEmpResignLeaveEncashmentInfotatements(EmpResignID);
        }

        public int InsertEmpResignLeaveEncashmentInfo(int EmpResignID, DateTime EncashmentDate, decimal TotalLeaveAllotted, decimal TotalLeaveUtilised, decimal TotalLeaveBalance, decimal TotalEncashableLeave,
            decimal DaysToEncash, decimal EligibleSalary, decimal DailySalaryRate, string SalaryCalculationBasis, decimal EncashmentAmount, decimal TaxDeduction, decimal NetEncashmentAmount, string MoreDetails, bool EncashmentProcessStatus)
        {
            return objEmpResignLeaveEncashmentInfo.InsertEmpResignLeaveEncashmentInfo(EmpResignID, EncashmentDate, TotalLeaveAllotted, TotalLeaveUtilised, TotalLeaveBalance, TotalEncashableLeave,
                DaysToEncash, EligibleSalary, DailySalaryRate, SalaryCalculationBasis, EncashmentAmount, TaxDeduction, NetEncashmentAmount, MoreDetails, EncashmentProcessStatus);
        }

        public int UpdateEmpResignLeaveEncashmentInfo(int LeaveEncashmentID, int EmpResignID, DateTime EncashmentDate, decimal TotalLeaveAllotted, decimal TotalLeaveUtilised, decimal TotalLeaveBalance, decimal TotalEncashableLeave,
            decimal DaysToEncash, decimal EligibleSalary, decimal DailySalaryRate, string SalaryCalculationBasis, decimal EncashmentAmount, decimal TaxDeduction, decimal NetEncashmentAmount, string MoreDetails, bool EncashmentProcessStatus)
        {
            return objEmpResignLeaveEncashmentInfo.UpdateEmpResignLeaveEncashmentInfo(LeaveEncashmentID, EmpResignID, EncashmentDate, TotalLeaveAllotted, TotalLeaveUtilised, TotalLeaveBalance, TotalEncashableLeave,
                DaysToEncash, EligibleSalary, DailySalaryRate, SalaryCalculationBasis, EncashmentAmount, TaxDeduction, NetEncashmentAmount, MoreDetails, EncashmentProcessStatus);
        }
    }
}
