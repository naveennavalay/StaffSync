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
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class clsWeeklyOffInfo
    {
        dbStaffSync.clsWeeklyOffInfo objWeeklyOffInfo = new dbStaffSync.clsWeeklyOffInfo();

        public clsWeeklyOffInfo() { 

        }

        public List<WklyOffProfileMasInfo> getWklyOffProfileMasInfoList(string txtWklyOffTitle)
        {
            List<WklyOffProfileMasInfo> objWklyOffProfileMasInfoList = new List<WklyOffProfileMasInfo>();

            objWklyOffProfileMasInfoList = objWeeklyOffInfo.getWklyOffProfileMasInfoList(txtWklyOffTitle);

            return objWklyOffProfileMasInfoList;
        }

        public int InsertWeeklyOffInfo(string txtWklyOffCode, string txtWklyOffTitle, DateTime txtWklyOffEffectiveDate, bool IsActive, bool IsDelete)
        {
            int affectedRows = 0;

            affectedRows = objWeeklyOffInfo.InsertWeeklyOffInfo(txtWklyOffCode, txtWklyOffTitle, txtWklyOffEffectiveDate, IsActive, IsDelete);

            return affectedRows;
        }

        public int UpdateWeeklyOffInfo(int txtWeeklyOffMasID, string txtWklyOffCode, string txtWklyOffTitle, DateTime txtWklyOffEffectiveDate, bool IsActive, bool IsDelete)
        {
            int affectedRows = 0;

            affectedRows = objWeeklyOffInfo.UpdateWeeklyOffInfo(txtWeeklyOffMasID, txtWklyOffCode, txtWklyOffTitle, txtWklyOffEffectiveDate, IsActive, IsDelete);

            return affectedRows;
        }

        public int DeleteWeeklyOffInfo(int txtWeeklyOffMasID)
        {
            int affectedRows = 0;

            affectedRows = objWeeklyOffInfo.DeleteWeeklyOffInfo(txtWeeklyOffMasID);

            return affectedRows;
        }

        public List<WklyOffProfileDetailsInfo> getWeeklyOffDetailsInfo(int txtWklyOffMasID)
        {
            List<WklyOffProfileDetailsInfo> objWklyOffProfileDetailsInfoList = new List<WklyOffProfileDetailsInfo>();

            objWklyOffProfileDetailsInfoList = objWeeklyOffInfo.getWeeklyOffDetailsInfo(txtWklyOffMasID);

            return objWklyOffProfileDetailsInfoList;
        }

        public List<EmployeeWklyOffInfo> getEmployeeSpecificWeeklyOffMasterInfo(int txtEmpID)
        {
            List<EmployeeWklyOffInfo> objEmployeeWklyOffInfoList = new List<EmployeeWklyOffInfo>();

            objEmployeeWklyOffInfoList = objWeeklyOffInfo.getEmployeeSpecificWeeklyOffMasterInfo(txtEmpID);

            return objEmployeeWklyOffInfoList;
        }

        public List<EmpSpecificWklyOffInfo> getEmployeeSpecificWeeklyOffInfo(int txtEmpID)
        {
            List<EmpSpecificWklyOffInfo> objEmpSpecificWklyOffInfoList = new List<EmpSpecificWklyOffInfo>();

            objEmpSpecificWklyOffInfoList = objWeeklyOffInfo.getEmployeeSpecificWeeklyOffInfo(txtEmpID);

            return objEmpSpecificWklyOffInfoList;
        }

        public int InsertEmployeeSpecificWeeklyInfo(int txtEmpID, int txtWklyOffMasID, DateTime txtEffectDateFrom)
        {
            int affectedRows = 0;

            affectedRows = objWeeklyOffInfo.InsertEmployeeSpecificWeeklyInfo(txtEmpID, txtWklyOffMasID, txtEffectDateFrom);

            return affectedRows;
        }

        public int UpdateEmployeeSpecificWeeklyInfo(int txtWeeklyOffID, int txtEmpID, int txtWklyOffMasID, DateTime txtEffectDateFrom)
        {
            int affectedRows = 0;

            affectedRows = objWeeklyOffInfo.UpdateEmployeeSpecificWeeklyInfo(txtWeeklyOffID, txtEmpID, txtWklyOffMasID, txtEffectDateFrom);

            return affectedRows;
        }

        public int DeleteEmployeeSpecificWeeklyInfo(int txtWeeklyOffID)
        {
            int affectedRows = 0;

            affectedRows = objWeeklyOffInfo.DeleteEmployeeSpecificWeeklyInfo(txtWeeklyOffID);

            return affectedRows;
        }

        public int InsertWeeklyOffDetailInfo(int txtWklyOffMasID, int txtWklyOffDay, int txtWklyOffOrderID)
        {
            int affectedRows = 0;

            affectedRows = objWeeklyOffInfo.InsertWeeklyOffDetailInfo(txtWklyOffMasID, txtWklyOffDay, txtWklyOffOrderID);

            return affectedRows;
        }

        public int UpdateWeeklyOffDetailInfo(int txtWklyOffDetID, int txtWklyOffMasID, int txtWklyOffDay, int txtWklyOffOrderID)
        {
            int affectedRows = 0;

            affectedRows = objWeeklyOffInfo.UpdateWeeklyOffDetailInfo(txtWklyOffDetID, txtWklyOffMasID, txtWklyOffDay, txtWklyOffOrderID);

            return affectedRows;
        }

        public int DeleteWeeklyOffDetailInfo(int txtWklyOffDetID)
        {
            int affectedRows = 0;

            affectedRows = objWeeklyOffInfo.DeleteWeeklyOffDetailInfo(txtWklyOffDetID);

            return affectedRows;
        }
    }
}
