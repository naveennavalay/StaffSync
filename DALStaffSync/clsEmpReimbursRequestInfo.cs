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

        public List<ReimbursementInfoPendingList> FirstApprovalPendingReimbursmentApprovalList(int txtClientID)
        {
            List<ReimbursementInfoPendingList> objReimbursementInfoPendingListInfo = new List<ReimbursementInfoPendingList>();

            objReimbursementInfoPendingListInfo = objEmpReimbursRequestInfo.FirstApprovalPendingReimbursmentApprovalList(txtClientID);

            return objReimbursementInfoPendingListInfo;
        }

        public List<ReimbursementInfoPendingList> SecondApprovalPendingReimbursmentApprovalList(int txtClientID)
        {
            List<ReimbursementInfoPendingList> objReimbursementInfoPendingListInfo = new List<ReimbursementInfoPendingList>();

            objReimbursementInfoPendingListInfo = objEmpReimbursRequestInfo.SecondApprovalPendingReimbursmentApprovalList(txtClientID);

            return objReimbursementInfoPendingListInfo;
        }

        public int InsertEmployeeReimbursmentRequestInfo(int PersonalInfoID, bool IsActive, bool IsDeleted, int ReimbID, DateTime? ReimbursRequestDate, decimal ReimbursRequestAmount,
            bool ReimbursAproveStatus1, DateTime? ReimbursAproveDate1, int ReimbursAproveBy1, string ReimbursAproveComments1, bool ReimbursAproveStatus2, DateTime? ReimbursApproveDate2, int ReimbursAproveBy2, string ReimbursAproveComments2, bool ReimbursRequestFinalStatus, bool IncludeInNextPayslipProcess)
        {
            return objEmpReimbursRequestInfo.InsertEmployeeReimbursmentRequestInfo(PersonalInfoID, IsActive, IsDeleted, ReimbID, ReimbursRequestDate, ReimbursRequestAmount,
                ReimbursAproveStatus1, ReimbursAproveDate1, ReimbursAproveBy1, ReimbursAproveComments1, ReimbursAproveStatus2, ReimbursApproveDate2, ReimbursAproveBy2, ReimbursAproveComments2, ReimbursRequestFinalStatus, IncludeInNextPayslipProcess);
        }   
    }
}
