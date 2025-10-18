//using C1.Framework;
using ModelStaffSync;
using Newtonsoft.Json;
//using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
using System.Data;
//using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.OleDb;
//using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class clsLeaveTRList
    {
        dbStaffSync.clsLeaveTRList objLeaveTRList = new dbStaffSync.clsLeaveTRList();

        public clsLeaveTRList() { 

        }

        public int getEmployeeSpecificOrderID(string tableName, string ColumnName, int EmpID)
        {
            int rowCount = 0;
            
            rowCount = objLeaveTRList.getEmployeeSpecificOrderID(tableName, ColumnName, EmpID);

            return rowCount;
        }

        public int getMaxLeaveMasID(int txtEmpID)
        {
            int MaxLeaveMasID = 0;
            
            MaxLeaveMasID = objLeaveTRList.getMaxLeaveMasID(txtEmpID);

            return Convert.ToInt16(MaxLeaveMasID.ToString());
        }

        public decimal getBalanceLeave(int EmpID)
        {
            string BalanceLeave = "0.00";
            
            BalanceLeave = objLeaveTRList.getBalanceLeave(EmpID).ToString();

            return Convert.ToDecimal(BalanceLeave.ToString());
        }

        public decimal getSpecificLeaveTypeBalance(int txtLeaveMasID, int txtLeaveTypeID)
        {
            string BalanceLeave = "0.00";
            
            BalanceLeave = objLeaveTRList.getSpecificLeaveTypeBalance(txtLeaveMasID, txtLeaveTypeID).ToString();

            return Convert.ToDecimal(BalanceLeave.ToString());
        }

        public List<PendingLeaveApprovalList> getPendingLeaveApprovalList()
        {
            List<PendingLeaveApprovalList> empPendingLeaveApprovalList = new List<PendingLeaveApprovalList>();

            empPendingLeaveApprovalList = objLeaveTRList.getPendingLeaveApprovalList();

            return empPendingLeaveApprovalList;
        }

        public List<BulkPendingLeaveApproval> getBulkPendingLeaveApprovalList()
        {
            List<BulkPendingLeaveApproval> empPendingLeaveApprovalList = new List<BulkPendingLeaveApproval>();
            
            empPendingLeaveApprovalList = objLeaveTRList.getBulkPendingLeaveApprovalList();

            return empPendingLeaveApprovalList;
        }

        public List<BulkPendingLeaveApproval> getRejectedLeavelList()
        {
            List<BulkPendingLeaveApproval> empPendingLeaveApprovalList = new List<BulkPendingLeaveApproval>();
            
            empPendingLeaveApprovalList = objLeaveTRList.getRejectedLeavelList();

            return empPendingLeaveApprovalList;
        }

        public List<BulkPendingLeaveApproval> getConsolidatedLeaveStatement()
        {
            List<BulkPendingLeaveApproval> empPendingLeaveApprovalList = new List<BulkPendingLeaveApproval>();

            empPendingLeaveApprovalList = objLeaveTRList.getConsolidatedLeaveStatement();

            return empPendingLeaveApprovalList;
        }

        public List<OutstandingLeaveStatement> getOutStandingLeaveStaetment()
        {
            List<OutstandingLeaveStatement> empOutStandingLeaveStatement = new List<OutstandingLeaveStatement>();

            empOutStandingLeaveStatement = objLeaveTRList.getOutStandingLeaveStaetment();

            return empOutStandingLeaveStatement;
        }

        public List<EmployeeSpecificLeaveInfo> getSpecificEmployeeSpecificLeaveInfo(int LeaveTRID)
        {
            List<EmployeeSpecificLeaveInfo> employeeLeaveTRList = new List<EmployeeSpecificLeaveInfo>();
            
            employeeLeaveTRList = objLeaveTRList.getSpecificEmployeeSpecificLeaveInfo(LeaveTRID);

            return employeeLeaveTRList;
        }

        public List<EmployeeLeaveTRList> getEmployeeLeaveTRList(int txtEmpID)
        {
            List<EmployeeLeaveTRList> employeeLeaveTRList = new List<EmployeeLeaveTRList>();

            employeeLeaveTRList = objLeaveTRList.getEmployeeLeaveTRList(txtEmpID);

            return employeeLeaveTRList;
        }

        public List<EmployeeLeaveTRList> getAllEmployeesLeaveStatement(int txtEmpID)
        {
            List<EmployeeLeaveTRList> EmployeeLeaveStatements = new List<EmployeeLeaveTRList>();

            //EmployeeLeaveStatements = objLeaveTRList.getAllEmployeesLeaveStatement(txtEmpID);

            return EmployeeLeaveStatements;
        }

        public List<EmployeeOOOList> GetEmployeeOOOList()
        {
            List<EmployeeOOOList> EmpOOOList = new List<EmployeeOOOList>();
            
            EmpOOOList = objLeaveTRList.GetEmployeeOOOList();

            return EmpOOOList;
        }

        public bool AttendanceExistsForToday(int txtEmpID, DateTime dtDate)
        {
            bool attendanceExists = false;
            
            attendanceExists = objLeaveTRList.AttendanceExistsForToday(txtEmpID, dtDate);

            return attendanceExists;
        }

        public int InsertDefaultLeaveAllotment(int txtEmpID, decimal TotalLeaves, decimal TotalBalanceLeave, DateTime txtEffectiveDate)
        {
            int affectedRows = 0;

            affectedRows = objLeaveTRList.InsertDefaultLeaveAllotment(txtEmpID, TotalLeaves, TotalBalanceLeave, txtEffectiveDate);

            return affectedRows;
        }

        public int UpdateEmployeeLeaveBalance(int txtLeaveMasID, int txtEmpID, decimal TotalLeaves, decimal TotalBalanceLeave, DateTime txtEffectiveDate)
        {
            int affectedRows = 0;

            affectedRows = objLeaveTRList.UpdateEmployeeLeaveBalance(txtLeaveMasID, txtEmpID, TotalLeaves, TotalBalanceLeave, txtEffectiveDate);

            return affectedRows;
        }

        public int UpdateSpecificLeaveTypeBalance(int txtLeaveMasID, int txtLeaveTypeID, decimal TotalBalanceLeave)
        {
            int affectedRows = 0;

            affectedRows = objLeaveTRList.UpdateSpecificLeaveTypeBalance(txtLeaveMasID, txtLeaveTypeID, TotalBalanceLeave);

            return affectedRows;
        }

        public int InsertLeaveTransaction(int txtEmpID, int txtLeaveTypeID, DateTime txtLeaveAppliedDate, string txtLeaveComments, DateTime txtLeaveFromDate, DateTime txtLeaveToDate, decimal txtLeaveDuration, string txtLeaveMode, DateTime txtLeaveApprovedDate, string txtLeaveApprovalComments, DateTime txtLeaveRejectedDate, string txtLeaveRejectionComment, int txtApproverID)
        {
            int affectedRows = 0;

            affectedRows = objLeaveTRList.InsertLeaveTransaction(txtEmpID, txtLeaveTypeID, txtLeaveAppliedDate, txtLeaveComments, txtLeaveFromDate, txtLeaveToDate, txtLeaveDuration, txtLeaveMode, txtLeaveApprovedDate, txtLeaveApprovalComments, txtLeaveRejectedDate, txtLeaveRejectionComment, txtApproverID);

            return affectedRows;
        }

        public int UpdateLeaveTransaction(int txtLeaveTRID, int txtEmpID, int txtLeaveTypeID, DateTime txtLeaveAppliedDate, string txtLeaveComments, DateTime txtLeaveFromDate, DateTime txtLeaveToDate, decimal txtLeaveDuration, string txtLeaveMode, DateTime txtLeaveApprovedDate, string txtLeaveApprovalComments, DateTime txtLeaveRejectedDate, string txtLeaveRejectionComment, int txtApproverID)
        {
            int affectedRows = 0;

            affectedRows = objLeaveTRList.UpdateLeaveTransaction(txtLeaveTRID, txtEmpID, txtLeaveTypeID, txtLeaveAppliedDate, txtLeaveComments, txtLeaveFromDate, txtLeaveToDate, txtLeaveDuration, txtLeaveMode, txtLeaveApprovedDate, txtLeaveApprovalComments, txtLeaveRejectedDate, txtLeaveRejectionComment, txtApproverID);

            return affectedRows;
        }

        public int CancelLeaveTransaction(int txtLeaveTRID, int txtEmpID, string txtLeaveComments)
        {
            int affectedRows = 0;

            affectedRows = objLeaveTRList.CancelLeaveTransaction(txtLeaveTRID, txtEmpID, txtLeaveComments);

            return affectedRows;
        }

        public int RejectLeaveTransaction(int txtLeaveTRID, int txtEmpID, string txtLeaveComments)
        {
            int affectedRows = 0;

            affectedRows = objLeaveTRList.RejectLeaveTransaction(txtLeaveTRID, txtEmpID, txtLeaveComments);

            return affectedRows;
        }

        public int ApproveLeave(int txtLeaveTRID, int txtEmpID, string txtLeaveApprovalComments, int txtApproverID)
        {
            int affectedRows = 0;

            affectedRows = objLeaveTRList.ApproveLeave(txtLeaveTRID, txtEmpID, txtLeaveApprovalComments, txtApproverID);

            return affectedRows;
        }

        public int RejectLeave(int txtLeaveTRID, int txtEmpID, string txtLeaveRejectionComments, int txtApproverID)
        {
            int affectedRows = 0;

            affectedRows = objLeaveTRList.RejectLeave(txtLeaveTRID, txtEmpID, txtLeaveRejectionComments, txtApproverID);

            return affectedRows;
        }

        public int ApproveLeaveCancellation(int txtLeaveTRID, int txtEmpID, string txtLeaveRejectionComments, int txtApproverID)
        {
            int affectedRows = 0;

            affectedRows = objLeaveTRList.ApproveLeaveCancellation(txtLeaveTRID, txtEmpID, txtLeaveRejectionComments, txtApproverID);

            return affectedRows;
        }

    }
}
