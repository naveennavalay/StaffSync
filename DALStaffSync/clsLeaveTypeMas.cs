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

        public List<LeaveTypeInfoModel> GetLeaveTypeInfo(int LeaveTypeID)
        {
            List<LeaveTypeInfoModel> LeaveTypeInfoList = new List<LeaveTypeInfoModel>(); 
            
            LeaveTypeInfoList = objLeaveTypeMas.GetLeaveTypeInfo(LeaveTypeID);

            return LeaveTypeInfoList;
        }

        public int InsertLeaveTypeInfo(string txtLeaveCode, string txtLeaveTypeTitle, bool IsPaid, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            
            affectedRows = objLeaveTypeMas.InsertLeaveTypeInfo(txtLeaveCode, txtLeaveTypeTitle, IsPaid, IsActive, IsDeleted);

            return affectedRows;
        }

        public int UpadateLeaveTypeInfo(int txtLeaveTypeID, string txtLeaveCode, string txtLeaveTypeTitle, bool IsPaid, bool IsActive)
        {
            int affectedRows = 0;
            
            affectedRows = objLeaveTypeMas.UpadateLeaveTypeInfo(txtLeaveTypeID, txtLeaveCode, txtLeaveTypeTitle, IsPaid, IsActive);

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
