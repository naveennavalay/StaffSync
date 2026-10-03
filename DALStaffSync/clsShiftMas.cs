using dbStaffSync;
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
    public class clsShiftMas
    {
        dbStaffSync.clsShiftMas objShiftMasInfo = new dbStaffSync.clsShiftMas();

        public List<ShiftInfo> GetShiftList()
        {
            List<ShiftInfo> objShiftInfoList = new List<ShiftInfo>();

            objShiftInfoList = objShiftMasInfo.GetShiftList();

            return objShiftInfoList;
        }

        public EmpShiftInfo getEmployeeSpecificShiftInfo(int txtEmpID)
        {
            EmpShiftInfo objSpecificShiftInfo = new EmpShiftInfo();

            objSpecificShiftInfo = objShiftMasInfo.getEmployeeSpecificShiftInfo(txtEmpID);

            return objSpecificShiftInfo;
        }

        public int GetShiftTitleByTitle(string ShiftTitle)
        {
            int ShiftID = 0;

            ShiftID = objShiftMasInfo.GetShiftTitleByTitle(ShiftTitle);

            return ShiftID;
        }

        public int InsertShiftMasInfo(string txtShiftCode, string txtShiftTitle, string txtShiftInitial, DateTime ShiftStart, DateTime ShiftEnd, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;

            affectedRows = objShiftMasInfo.InsertShiftMasInfo(txtShiftCode, txtShiftTitle, txtShiftInitial, ShiftStart, ShiftEnd, IsActive, IsDeleted);

            return affectedRows;
        }

        public int UpdateShiftMasInfo(int txtShiftID, string txtShiftCode, string txtShiftTitle, string txtShiftInitial, DateTime ShiftStart, DateTime ShiftEnd, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;

            affectedRows = objShiftMasInfo.UpdateShiftMas(txtShiftID, txtShiftCode, txtShiftTitle, txtShiftInitial, ShiftStart, ShiftEnd, IsActive, IsDeleted);

            return affectedRows;
        }

        public int DeleteShiftMasInfo(int ShiftID)
        {
            int affectedRows = 0;

            affectedRows = objShiftMasInfo.DeleteShiftMasInfo(ShiftID);

            return affectedRows;
        }

        public int InsertEmployeeShiftInfo(int txtEmpID, int txtShiftID, DateTime txtEffectiveDate)
        {
            int affectedRows = 0;

            affectedRows = objShiftMasInfo.InsertEmployeeShiftInfo(txtEmpID, txtShiftID, txtEffectiveDate);

            return affectedRows;
        }

        public int UpdateEmployeeShiftInfo(int txtEmpShiftInfoID, int txtEmpID, int txtShiftID, DateTime txtEffectiveDate)
        {
            int affectedRows = 0;

            affectedRows = objShiftMasInfo.UpdateEmployeeShiftInfo(txtEmpShiftInfoID, txtEmpID, txtShiftID, txtEffectiveDate);

            return affectedRows;
        }

        public int DeleteEmployeeShiftInfo(int txtEmpShiftInfoID)
        {
            int affectedRows = 0;

            affectedRows = objShiftMasInfo.DeleteEmployeeShiftInfo(txtEmpShiftInfoID);

            return affectedRows;
        }
    }
}
