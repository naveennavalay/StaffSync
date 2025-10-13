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
    public class clsEmpWorkExperienceInfo
    {
        dbStaffSync.clsEmpWorkExperienceInfo objEmpWorkExperienceInfo = new dbStaffSync.clsEmpWorkExperienceInfo();

        public clsEmpWorkExperienceInfo() { 

        }

        public List<EmpWorkExpInfo> GetWorkExpDefaultList()
        {
            List<EmpWorkExpInfo> objEmpWorkExpInfoList = new List<EmpWorkExpInfo>();

            objEmpWorkExpInfoList = objEmpWorkExperienceInfo.GetWorkExpDefaultList();

            return objEmpWorkExpInfoList;
        }

        public List<EmpWorkExpInfo> GetWorkExpListInfo(int txtEmployeeID)
        {
            List<EmpWorkExpInfo> objEmpWorkExpInfoList = new List<EmpWorkExpInfo>();

            objEmpWorkExpInfoList = objEmpWorkExperienceInfo.GetWorkExpListInfo(txtEmployeeID);
            
            return objEmpWorkExpInfoList;
        }

        public List<EmpWorkExpInfo> UpdateWorkExpListInfo(int txtEmployeeID, EmpWorkExpInfo objNewRow)
        {
            List<EmpWorkExpInfo> objEmpWorkExpInfoList = new List<EmpWorkExpInfo>();

            objEmpWorkExpInfoList = objEmpWorkExperienceInfo.UpdateWorkExpListInfo(txtEmployeeID, objNewRow);

            return objEmpWorkExpInfoList;
        }

        public int InsertEmpWorkExpInfo(int txtEmpID, int txtLastCompanyInfoID, DateTime txtStartDate, DateTime txtEndDate, string txtComments)
        {
            int affectedRows = 0;

            affectedRows = objEmpWorkExperienceInfo.InsertEmpWorkExpInfo(txtEmpID, txtLastCompanyInfoID, txtStartDate, txtEndDate, txtComments);

            return affectedRows;
        }

        public int UpdatetEmpWorkExpInfo(int txtLastCompanyID, int txtEmpID, int txtLastCompanyInfoID, DateTime txtStartDate, DateTime txtEndDate, string txtComments)
        {
            int affectedRows = 0;

            affectedRows = objEmpWorkExperienceInfo.UpdatetEmpWorkExpInfo(txtLastCompanyID, txtEmpID, txtLastCompanyInfoID, txtStartDate, txtEndDate, txtComments);

            return affectedRows;
        }

        public int DeleteEmpWorkExpInfo(int txtLastCompanyID)
        {
            int affectedRows = 0;

            affectedRows = objEmpWorkExperienceInfo.DeleteEmpWorkExpInfo(txtLastCompanyID);

            return affectedRows;
        }
    }
}
