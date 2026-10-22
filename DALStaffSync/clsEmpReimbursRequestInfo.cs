using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class clsEmpReimbursRequestInfo
    {
        dbStaffSync.clsEmpReimbursRequestInfo objEmpReimbursRequestInfo = new dbStaffSync.clsEmpReimbursRequestInfo();

        public clsEmpReimbursRequestInfo() { 

        }

        public List<ReimbursementPaymentsPendingList> getSelectedReimbursmentPaymentPendingList(int txtRequestID)
        {
            return objEmpReimbursRequestInfo.getSelectedReimbursmentPaymentPendingList(txtRequestID);
        }

        public List<ReimbursementPaymentsPendingList> GetReimbursmentPaymentPendingList(int txtClientID)
        {
            return objEmpReimbursRequestInfo.GetReimbursmentPaymentPendingList(txtClientID);
        }

        public List<ReimbursementInfoPendingList> FirstApprovalPendingReimbursmentApprovalList(int txtClientID, int txtApproverID)
        {
            List<ReimbursementInfoPendingList> objReimbursementInfoPendingListInfo = new List<ReimbursementInfoPendingList>();

            objReimbursementInfoPendingListInfo = objEmpReimbursRequestInfo.FirstApprovalPendingReimbursmentApprovalList(txtClientID, txtApproverID);

            return objReimbursementInfoPendingListInfo;
        }

        public List<ReimbursementInfoPendingList> SecondApprovalPendingReimbursmentApprovalList(int txtClientID, int txtApproverID)
        {
            List<ReimbursementInfoPendingList> objReimbursementInfoPendingListInfo = new List<ReimbursementInfoPendingList>();

            objReimbursementInfoPendingListInfo = objEmpReimbursRequestInfo.SecondApprovalPendingReimbursmentApprovalList(txtClientID, txtApproverID);

            return objReimbursementInfoPendingListInfo;
        }

        public int InsertEmployeeReimbursmentRequestInfo(int PersonalInfoID, bool IsActive, bool IsDeleted, int ReimbID, DateTime? ReimbursRequestDate, decimal ReimbursRequestAmount,
            bool ReimbursAproveStatus1, DateTime? ReimbursAproveDate1, int ReimbursAproveBy1, string ReimbursAproveComments1, bool ReimbursAproveStatus2, DateTime? ReimbursApproveDate2, int ReimbursAproveBy2, string ReimbursAproveComments2, bool ReimbursRequestFinalStatus, bool IncludeInNextPayslipProcess)
        {
            return objEmpReimbursRequestInfo.InsertEmployeeReimbursmentRequestInfo(PersonalInfoID, IsActive, IsDeleted, ReimbID, ReimbursRequestDate, ReimbursRequestAmount,
                ReimbursAproveStatus1, ReimbursAproveDate1, ReimbursAproveBy1, ReimbursAproveComments1, ReimbursAproveStatus2, ReimbursApproveDate2, ReimbursAproveBy2, ReimbursAproveComments2, ReimbursRequestFinalStatus, IncludeInNextPayslipProcess);
        }

        public int UpdateReimbursmentRequestFirstApprover(int ReimbursRequestID, string FirstApproverStatus)
        {
            return objEmpReimbursRequestInfo.UpdateReimbursmentRequestFirstApprover(ReimbursRequestID, FirstApproverStatus);
        }

        public int UpdateReimbursmentRequestSecondApprover(int ReimbursRequestID, string FirstApproverStatus)
        {
            return objEmpReimbursRequestInfo.UpdateReimbursmentRequestSecondApprover(ReimbursRequestID, FirstApproverStatus);
        }

        public int InsertEmployeeReimbursmentRequestMasInfo(DateTime ReimbRequestDate, string ReimbRequestDescription, string ReimbRequestStatus, bool IsActive, bool IsDeleted, int ReimbursRequestID)
        {
            return objEmpReimbursRequestInfo.InsertEmployeeReimbursmentRequestMasInfo(ReimbRequestDate, ReimbRequestDescription.ToString(), ReimbRequestStatus, IsActive, IsDeleted, ReimbursRequestID);
        }

        public int InsertEmployeeReimbursmentRequestDetailedInfo(int ReimbRequestID, DateTime ReimbRequestTRDate, decimal OBalance, decimal CrAmount, decimal DrAmount, decimal CBalance, string TRType, string Comments)
        {
            return objEmpReimbursRequestInfo.InsertEmployeeReimbursmentRequestDetailedInfo(ReimbRequestID, ReimbRequestTRDate, OBalance, CrAmount, DrAmount, CBalance, TRType, Comments);
        }
    }
}
