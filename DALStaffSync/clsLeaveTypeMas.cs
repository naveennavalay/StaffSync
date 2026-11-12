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
    public class clsLeaveTypeMas
    {
        dbStaffSync.clsLeaveTypeMas objLeaveTypeMas = new dbStaffSync.clsLeaveTypeMas();

        public clsLeaveTypeMas() { 

        }

        public DataTable GetLeaveTypeList()
        {
            DataTable dt = new DataTable();

            dt = objLeaveTypeMas.GetLeaveTypeList();

            return dt;
        }

        public DataTable GetLeaveTypeList(string FilterText)
        {
            DataTable dt = new DataTable();

            dt = objLeaveTypeMas.GetLeaveTypeList(FilterText);

            return dt;
        }

        public List<LeaveTypeInfoModel> GetLeaveTypeInfoList()
        {
            List<LeaveTypeInfoModel> LeaveTypeInfoList = new List<LeaveTypeInfoModel>();

            LeaveTypeInfoList = objLeaveTypeMas.GetLeaveTypeInfoList();

            return LeaveTypeInfoList;
        }

        public List<LeaveTypeInfoModel> GetLeaveTypeInfo(int LeaveTypeID)
        {
            List<LeaveTypeInfoModel> LeaveTypeInfoList = new List<LeaveTypeInfoModel>(); 
            
            LeaveTypeInfoList = objLeaveTypeMas.GetLeaveTypeInfo(LeaveTypeID);

            return LeaveTypeInfoList;
        }

        public int GetLeaveTypeMasInfoByName(string LeaveTypeTitle)
        {
            int LeaveTypeID = 0;

            LeaveTypeID = objLeaveTypeMas.GetLeaveTypeMasInfoByName(LeaveTypeTitle);

            return LeaveTypeID;
        }

        public int InsertLeaveTypeInfo(string txtLeaveCode, string txtLeaveTypeTitle, bool IsPaid, bool IsActive, bool IsDeleted,
            bool IsCarryForwardAllowed, int MaxCarryForwardAllowed, int CarryForwardExpiryMonths, bool IsCarryForwardExpiryApplicable,
            bool IsEncashmentAllowed, int MaxLeaveDaysAllowedToEncash, bool IsEncashmentDuringEmploymentAllowed, bool IsEncashmentAtResignationAllowed,
            bool IsLeaveAccrualApplicable, int AccrualFrequency, bool IsApplicableDuringProbation, bool IsApplicableDuringNoticePeriod)
        {
            int affectedRows = 0;

            affectedRows = objLeaveTypeMas.InsertLeaveTypeInfo(txtLeaveCode, txtLeaveTypeTitle, IsPaid, IsActive, IsDeleted, 
                IsCarryForwardAllowed, MaxCarryForwardAllowed, CarryForwardExpiryMonths, IsCarryForwardExpiryApplicable,
                IsEncashmentAllowed, MaxLeaveDaysAllowedToEncash, IsEncashmentDuringEmploymentAllowed, IsEncashmentAtResignationAllowed, 
                IsLeaveAccrualApplicable, AccrualFrequency, IsApplicableDuringProbation, IsApplicableDuringNoticePeriod);

            return affectedRows;
        }

        public int UpadateLeaveTypeInfo(int txtLeaveTypeID, string txtLeaveCode, string txtLeaveTypeTitle, bool IsPaid, bool IsActive, bool IsDeleted,
            bool IsCarryForwardAllowed, int MaxCarryForwardAllowed, int CarryForwardExpiryMonths, bool IsCarryForwardExpiryApplicable,
            bool IsEncashmentAllowed, int MaxLeaveDaysAllowedToEncash, bool IsEncashmentDuringEmploymentAllowed, bool IsEncashmentAtResignationAllowed,
            bool IsLeaveAccrualApplicable, int AccrualFrequency, bool IsApplicableDuringProbation, bool IsApplicableDuringNoticePeriod)
        {
            int affectedRows = 0;
            
            affectedRows = objLeaveTypeMas.UpadateLeaveTypeInfo(txtLeaveTypeID, txtLeaveCode, txtLeaveTypeTitle, IsPaid, IsActive, IsDeleted,
                IsCarryForwardAllowed, MaxCarryForwardAllowed, CarryForwardExpiryMonths, IsCarryForwardExpiryApplicable,
                IsEncashmentAllowed, MaxLeaveDaysAllowedToEncash, IsEncashmentDuringEmploymentAllowed, IsEncashmentAtResignationAllowed,
                IsLeaveAccrualApplicable, AccrualFrequency, IsApplicableDuringProbation, IsApplicableDuringNoticePeriod);

            return affectedRows;
        }

        public int DeleteListTypeInfo(int txtLeaveTypeID)
        {
            int affectedRows = 0;

            affectedRows = objLeaveTypeMas.DeleteListTypeInfo(txtLeaveTypeID);

            return affectedRows;
        }
    }
}
