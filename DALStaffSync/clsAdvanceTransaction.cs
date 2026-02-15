using ModelStaffSync;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Net;

namespace DALStaffSync
{
    public class clsAdvanceTransaction
    {
        dbStaffSync.clsAdvanceTransaction objAdvanceTransaction = new dbStaffSync.clsAdvanceTransaction();

        public bool IsAdvanceAlreadyExist(int txtEmpPersonalInfoID)
        { 
            return objAdvanceTransaction.IsAdvanceAlreadyExist(txtEmpPersonalInfoID);
        }

        public List<AdvanceApprovalPendingList> AdvanceFirstApprovalPendingList(int txtClientID)
        {
            return objAdvanceTransaction.AdvanceFirstApprovalPendingList(txtClientID);
        }

        public List<AdvanceApprovalPendingList> AdvanceSecondApprovalPendingList(int txtClientID)
        {
            return objAdvanceTransaction.AdvanceSecondApprovalPendingList(txtClientID);
        }

        public int InsertEmpAdvanceRequestMas(int txtEmpPersonalInfoID, int txtAdvanceTypeID, bool IsActive, bool IsDeleted, DateTime txtEmpAdvanceRequestDate, string txtEmpAdvanceRequestComments, int txtRequestedTo, DateTime txtRequestedToDate, 
            bool RequestedToStatus, string txtRequestedToComments, int RequestMovedTo, DateTime txtRequestMovedToDate, bool txtRequestMovedToStatus, string txtRequestMovedToComments, DateTime txtRequestApprovalDate, 
            bool AdvanceRequestStatus, decimal txtAdvanceAmount, decimal txtAdvanceTenure, decimal AdvanceInstallment, DateTime txtAdvanceStartDate, DateTime AdvanceEndDate)
        {
                return objAdvanceTransaction.InsertEmpAdvanceRequestMas(txtEmpPersonalInfoID, txtAdvanceTypeID, IsActive, IsDeleted, txtEmpAdvanceRequestDate, txtEmpAdvanceRequestComments, txtRequestedTo, txtRequestedToDate,
                RequestedToStatus, txtRequestedToComments, RequestMovedTo, txtRequestMovedToDate, txtRequestMovedToStatus, txtRequestMovedToComments, txtRequestApprovalDate,
                AdvanceRequestStatus, txtAdvanceAmount, txtAdvanceTenure, AdvanceInstallment, txtAdvanceStartDate, AdvanceEndDate);
        }

        public int UpdateEmpAdvanceRequestMas(int EmpAdvanceRequestID, int txtEmpPersonalInfoID, bool IsActive, bool IsDeleted, int txtAdvanceTypeID, DateTime txtEmpAdvanceRequestDate, string txtEmpAdvanceRequestComments, int txtRequestedTo, DateTime txtRequestedToDate,
        bool RequestedToStatus, string txtRequestedToComments, int RequestMovedTo, DateTime txtRequestMovedToDate, bool txtRequestMovedToStatus, string txtRequestMovedToComments, DateTime txtRequestApprovalDate,
        bool AdvanceRequestStatus, decimal txtAdvanceAmount, decimal txtAdvanceTenure, decimal txtAdvanceInstallment, DateTime txtAdvanceStartDate, DateTime txtAdvanceEndDate)
        {
            return objAdvanceTransaction.UpdateEmpAdvanceRequestMas(EmpAdvanceRequestID, txtEmpPersonalInfoID, IsActive, IsDeleted, txtAdvanceTypeID, txtEmpAdvanceRequestDate, txtEmpAdvanceRequestComments, txtRequestedTo, txtRequestedToDate,
                RequestedToStatus, txtRequestedToComments, RequestMovedTo, txtRequestMovedToDate, txtRequestMovedToStatus, txtRequestMovedToComments, txtRequestApprovalDate,
                AdvanceRequestStatus, txtAdvanceAmount, txtAdvanceTenure, txtAdvanceInstallment, txtAdvanceStartDate, txtAdvanceEndDate);
        }

        public int DeleteEmpAdvanceRequestMas(int txtEmpAdvanceRequestID)
        {
            return objAdvanceTransaction.DeleteEmpAdvanceRequestMas(txtEmpAdvanceRequestID);
        }

        public int UpdateFirstApproverStatus(int txtEmpAdvanceRequestID, string txtRequestedToComments, DateTime RequestedToDate)
        {
            return objAdvanceTransaction.UpdateFirstApproverStatus(txtEmpAdvanceRequestID, txtRequestedToComments, RequestedToDate);
        }

        public int UpdateSecondApproverStatus(int txtEmpAdvanceRequestID, string txtRequestMovedToComments, DateTime RequestMovedToDate)
        {
            return objAdvanceTransaction.UpdateSecondApproverStatus(txtEmpAdvanceRequestID, txtRequestMovedToComments, RequestMovedToDate);
        }

        public int RejectOrCancleApproverStatus(int txtEmpAdvanceRequestID, string CancelOrRejectedComments, DateTime CancelOrRejectionDate)
        {
            return objAdvanceTransaction.RejectOrCancleApproverStatus(txtEmpAdvanceRequestID, CancelOrRejectedComments, CancelOrRejectionDate);
        }

        public int CloseEmployeeSpecificAdvanceRequest(int txtEmpAdvanceRequestID)
        {
            return objAdvanceTransaction.CloseEmployeeSpecificAdvanceRequest(txtEmpAdvanceRequestID);
        }

        public int InsertAdvanceTransaction(string txtEmpAdvReqCode, int EmpAdvanceRequestID, DateTime txtAdvanceDate, decimal txtOBalance, decimal txtCrBalance, decimal txtDrBalance, decimal txtCBalance, string txtTRType, string txtComments, int txtEmpSalID)
        {
            return objAdvanceTransaction.InsertAdvanceTransaction(txtEmpAdvReqCode, EmpAdvanceRequestID, txtAdvanceDate, txtOBalance, txtCrBalance, txtDrBalance, txtCBalance, txtTRType, txtComments, txtEmpSalID);
        }

        public int DeleteAdvanceTransaction(int txtEmpAdvanceRecoveryID)
        {
            return objAdvanceTransaction.DeleteAdvanceTransaction(txtEmpAdvanceRecoveryID);
        }

        public List<EmployeeSpecificAdvanceInformation> EmployeeSpecificAdvanceInformation(int txtEmpID)
        {
            return objAdvanceTransaction.EmployeeSpecificAdvanceInformation(txtEmpID);
        }

        public List<EmployeeSpecificAdvanceStatemetns> EmployeeSpecificAdvanceStatemetns(int txtEmpAdvanceRequestID)
        {
            return objAdvanceTransaction.EmployeeSpecificAdvanceStatemetns(txtEmpAdvanceRequestID);
        }
    }
}
