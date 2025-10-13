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
    public class clsEmpLeaveEntitlementInfo
    {
        dbStaffSync.clsEmpLeaveEntitlementInfo objEmpLeaveEntitlementInfo = new dbStaffSync.clsEmpLeaveEntitlementInfo();

        public clsEmpLeaveEntitlementInfo() { 

        }

        public List<LeaveEntitlementInfo> getDefaultLeaveEntitilementList()
        {
            List<LeaveEntitlementInfo> LeaveEntitlementInfoList = new List<LeaveEntitlementInfo>();
            
            LeaveEntitlementInfoList = objEmpLeaveEntitlementInfo.getDefaultLeaveEntitilementList();

            return LeaveEntitlementInfoList;
        }

        public List<LeaveEntitlementInfo> getEmployeeLeaveEntitilementList(int txtEmpID, int txtLeaveMasID)
        {
            List<LeaveEntitlementInfo> LeaveEntitlementInfoList = new List<LeaveEntitlementInfo>();
            
            LeaveEntitlementInfoList = objEmpLeaveEntitlementInfo.getEmployeeLeaveEntitilementList(txtEmpID, txtLeaveMasID);

            return LeaveEntitlementInfoList;
        }

        public List<LeaveEntitlementInfo> AddNewEntryOnGridEmployeeLeaveEntitilementList(int txtEmpID, int[] intLeaveTypeIDs, int txtNewLeaveTypeID)
        {
            List<LeaveEntitlementInfo> LeaveEntitlementInfoList = new List<LeaveEntitlementInfo>();

            LeaveEntitlementInfoList = objEmpLeaveEntitlementInfo.AddNewEntryOnGridEmployeeLeaveEntitilementList(txtEmpID, intLeaveTypeIDs, txtNewLeaveTypeID);

            return LeaveEntitlementInfoList;
        }

        public DataTable GetLeaveEntitlementInfo(int LeaveEntitlementID)
        {
            DataTable dt = new DataTable();

            dt = objEmpLeaveEntitlementInfo.GetLeaveEntitlementInfo(LeaveEntitlementID);

            return dt;
        }

        public int InsertLeaveEntitlementInfo(int txtEmpID, int txtLeaveMasID, int txtLeaveTypeID, decimal txtTotalLeaves, decimal txtBalanceLeaves, int txtOrderID)
        {
            int affectedRows = 0;
            
            affectedRows = objEmpLeaveEntitlementInfo.InsertLeaveEntitlementInfo(txtEmpID, txtLeaveMasID, txtLeaveTypeID, txtTotalLeaves, txtBalanceLeaves, txtOrderID);

            return affectedRows;
        }

        public int UpadateLeaveEntitlementInfo(int txtLeaveEntmtID, int txtEmpID, int txtLeaveMasID, int txtLeaveTypeID, decimal txtTotalLeaves, decimal txtBalanceLeaves, int txtOrderID)
        {
            int affectedRows = 0;
            
            affectedRows = objEmpLeaveEntitlementInfo.UpadateLeaveEntitlementInfo(txtLeaveEntmtID, txtEmpID, txtLeaveMasID, txtLeaveTypeID, txtTotalLeaves, txtBalanceLeaves, txtOrderID);

            return affectedRows;
        }

        public int DeleteLeaveEntitlementInfo(int txtLeaveEntmtID)
        {
            int affectedRows = 0;
            
            affectedRows = objEmpLeaveEntitlementInfo.DeleteLeaveEntitlementInfo(txtLeaveEntmtID);

            return affectedRows;
        }
    }
}
