//using C1.Framework;
using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class clsEmpPayroll
    {
        dbStaffSync.clsEmpPayroll objEmpPayroll = new dbStaffSync.clsEmpPayroll();

        public clsEmpPayroll() { 

        }

        public List<EmployeePaySlipList> getAllEmployeePayslipList()
        {
            List<EmployeePaySlipList> objEmployeePaySlipList = new List<EmployeePaySlipList>();
            
            objEmployeePaySlipList = objEmpPayroll.getAllEmployeePayslipList();

            return objEmployeePaySlipList;
        }

        public List<EmployeePayslipMasterDetails> getSelectedSpecificMonthSalaryMasterDetails(int txtEmpID, int txtSelectedSalMonthID)
        {
            List<EmployeePayslipMasterDetails> objEmployeePaySlipList = new List<EmployeePayslipMasterDetails>();

            objEmployeePaySlipList = objEmpPayroll.getSelectedSpecificMonthSalaryMasterDetails(txtEmpID, txtSelectedSalMonthID);

            return objEmployeePaySlipList;
        }

        public List<EmployeePayslipDetails> getSelectedSpecificMonthSalaryDetails(int txtEmpSalID)
        {
            List<EmployeePayslipDetails> objEmployeePaySlipList = new List<EmployeePayslipDetails>();

            objEmployeePaySlipList = objEmpPayroll.getSelectedSpecificMonthSalaryDetails(txtEmpSalID);

            return objEmployeePaySlipList;
        }


        public int InsertEmployeeSalaryMasterInfo(int txtEmpID, DateTime txtSalaryDate, string txtSalaryMonthYear, decimal txtTotalWorkingDays, decimal txtTotalWorkedDays, decimal txtTotalLeavesTaken, decimal txtTotalAllowance, decimal txtTotalDeduction, decimal txtTotalReimbursement, decimal txtNetPayableAmount)
        {
            int affectedRows = 0;
            
            affectedRows = objEmpPayroll.InsertEmployeeSalaryMasterInfo(txtEmpID, txtSalaryDate, txtSalaryMonthYear, txtTotalWorkingDays, txtTotalWorkedDays, txtTotalLeavesTaken, txtTotalAllowance, txtTotalDeduction, txtTotalReimbursement, txtNetPayableAmount);

            return affectedRows;
        }

        public int UpdateEmployeeSalaryMasterInfo(int txtEmpSalID, int txtEmpID, DateTime txtSalaryDate, string txtSalaryMonthYear, decimal txtTotalWorkingDays, decimal txtTotalWorkedDays, decimal txtTotalLeavesTaken, decimal txtTotalAllowance, decimal txtTotalDeduction, decimal txtTotalReimbursement, decimal txtNetPayableAmount)
        {
            int affectedRows = 0;
            
            affectedRows = objEmpPayroll.UpdateEmployeeSalaryMasterInfo(txtEmpSalID, txtEmpID, txtSalaryDate, txtSalaryMonthYear, txtTotalWorkingDays, txtTotalWorkedDays, txtTotalLeavesTaken, txtTotalAllowance, txtTotalDeduction, txtTotalReimbursement, txtNetPayableAmount);

            return affectedRows;
        }

        public int InsertEmployeeSalaryDetailsInfo(int txtEmpSalID, int txtSalProDetID, int txtSalHeaderID, string txtSalHeaderTitle, string txtSalSalHeaderType, string txtCalcFormula, decimal txtAllAmount, decimal txtDedAmount, decimal txtReimbAmount, int txtOrderID)
        {
            int affectedRows = 0;

            affectedRows = objEmpPayroll.InsertEmployeeSalaryDetailsInfo( txtEmpSalID, txtSalProDetID, txtSalHeaderID, txtSalHeaderTitle, txtSalSalHeaderType, txtCalcFormula, txtAllAmount, txtDedAmount, txtReimbAmount, txtOrderID);

            return affectedRows;
        }


        public int UpdateEmployeeSalaryDetailsInfo(int txtEmpSalDetID, int txtEmpSalID, int txtSalProDetID, int txtSalHeaderID, string txtSalHeaderTitle, string txtSalSalHeaderType, string txtCalcFormula, decimal txtAllAmount, decimal txtDedAmount, decimal txtReimbAmount, int txtOrderID)
        {
            int affectedRows = 0;

            affectedRows = objEmpPayroll.UpdateEmployeeSalaryDetailsInfo(txtEmpSalDetID, txtEmpSalID, txtSalProDetID, txtSalHeaderID, txtSalHeaderTitle, txtSalSalHeaderType, txtCalcFormula, txtAllAmount, txtDedAmount, txtReimbAmount, txtOrderID);

            return affectedRows;
        }
    }
}
