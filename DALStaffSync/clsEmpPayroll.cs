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

        public List<SalaryProfileInfo> getSelectedSpecificMonthSalaryDetails(int txtEmpID, int txtEmpSalID)
        {
            List<SalaryProfileInfo> objEmployeePaySlipList = new List<SalaryProfileInfo>();

            objEmployeePaySlipList = objEmpPayroll.getSelectedSpecificMonthSalaryDetails(txtEmpID, txtEmpSalID);

            return objEmployeePaySlipList;
        }

        public bool IsMasterInfoFound(int txtEmpID, DateTime txtMasterDataDate)
        {
            return objEmpPayroll.IsMasterInfoFound(txtEmpID, txtMasterDataDate);
        }

        public int InsertEmployeeSalaryMasterInfo(int txtEmpID, DateTime txtSalaryDate, string txtSalaryMonthYear, decimal txtTotalWorkingDays, decimal txtTotalWorkedDays, decimal txtTotalLeavesTaken, decimal txtTotalUnpaidDaysLeave, decimal txtTotalPayableDays, decimal txtBasicPay, decimal txtBasicPerDay, decimal txtBasicPerHour, decimal txtTotalAllowance, decimal txtTotalDeduction, decimal txtTotalReimbursement, decimal txtNetPayableAmount, bool boolStructureEntry)
        {
            int affectedRows = 0;
            
            affectedRows = objEmpPayroll.InsertEmployeeSalaryMasterInfo(txtEmpID, txtSalaryDate, txtSalaryMonthYear, txtTotalWorkingDays, txtTotalWorkedDays, txtTotalLeavesTaken, txtTotalUnpaidDaysLeave, txtTotalPayableDays, txtBasicPay, txtBasicPerDay, txtBasicPerHour, txtTotalAllowance, txtTotalDeduction, txtTotalReimbursement, txtNetPayableAmount, boolStructureEntry);

            return affectedRows;
        }

        public bool IsSalaryAlreadyProcessed(int txtEmpID, DateTime txtMasterDataDate, string txtSalMonthYear)
        {
            return objEmpPayroll.IsSalaryAlreadyProcessed(txtEmpID, txtMasterDataDate, txtSalMonthYear);
        }

        public int UpdateEmployeeSalaryMasterInfo(int txtEmpSalID, int txtEmpID, DateTime txtSalaryDate, string txtSalaryMonthYear, decimal txtTotalWorkingDays, decimal txtTotalWorkedDays, decimal txtTotalUnpaidDaysLeave, decimal txtTotalPayableDays, decimal txtTotalLeavesTaken, decimal txtBasicPay, decimal txtBasicPerDay, decimal txtBasicPerHour, decimal txtTotalAllowance, decimal txtTotalDeduction, decimal txtTotalReimbursement, decimal txtNetPayableAmount, bool boolStructureEntry)
        {
            int affectedRows = 0;
            
            affectedRows = objEmpPayroll.UpdateEmployeeSalaryMasterInfo(txtEmpSalID, txtEmpID, txtSalaryDate, txtSalaryMonthYear, txtTotalWorkingDays, txtTotalWorkedDays, txtTotalLeavesTaken, txtTotalUnpaidDaysLeave, txtTotalPayableDays, txtBasicPay, txtBasicPerDay, txtBasicPerHour, txtTotalAllowance, txtTotalDeduction, txtTotalReimbursement, txtNetPayableAmount, boolStructureEntry);

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
