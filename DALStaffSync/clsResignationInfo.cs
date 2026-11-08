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
    public class clsResignationInfo
    {
        dbStaffSync.clsResignationInfo objResignationInfo = new dbStaffSync.clsResignationInfo();

        public List<ResignationInfo> getResignationInfotatements()
        {
            return objResignationInfo.getResignationInfotatements();
        }

        public IsResignationAppliedInfo getIsResignationAppliedInfotatements(int txtEmpID)
        {
            return objResignationInfo.getIsResignationAppliedInfotatements(txtEmpID);
        }

        public int InsertEmployeeResignationInfo(int PersonalInfoID, int ResigMasID, string EmpResignMoreInfo, DateTime EmpResignDate,
            DateTime EmpResignApprovalDate1, int EmpResignApprovedBy1, bool EmpResignApprovalStatus1,
            DateTime EmpResignApprovalDate2, int EmpResignApprovedBy2, bool EmpResignApprovalStatus2,
            int EmpResignFinalStatus, DateTime EmpFinalResignApprovalDate,
            int EmpNoticePeriod, DateTime EmpLastWorkingDay, string EmpNoticePeriodServeStatus)
        {
            return objResignationInfo.InsertEmployeeResignationInfo(PersonalInfoID, ResigMasID, EmpResignMoreInfo, EmpResignDate,
                EmpResignApprovalDate1, EmpResignApprovedBy1, EmpResignApprovalStatus1,
                EmpResignApprovalDate2, EmpResignApprovedBy2, EmpResignApprovalStatus2,
                EmpResignFinalStatus, EmpFinalResignApprovalDate,
                EmpNoticePeriod, EmpLastWorkingDay, EmpNoticePeriodServeStatus);
        }
    }
}
