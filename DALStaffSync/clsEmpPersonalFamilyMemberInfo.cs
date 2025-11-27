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
    public class clsEmpPersonalFamilyMemberInfo
    {
        dbStaffSync.clsEmployeeFamilyMemberInfo objEmpPersonalFamilyMemberInfo = new dbStaffSync.clsEmployeeFamilyMemberInfo();

        public clsEmpPersonalFamilyMemberInfo() { 

        }

        public int InsertEmployeePersonalFamilyMemberInfo(int txtEmpPerFamInfoID, int txtPersonalInfoID, string txtFamMemName, DateTime txtFamMemDOB, int txtFamMemAge, string txtFamMemRelationship, string txtFamMemAddr1, string txtFamMemAddr2, string txtFamMemArea, string txtFamMemCity, string txtFamMemState, string txtFamMemPIN, string txtFamMemCountry, string txtFamMemContactNumber, string txtFamMemMailID, string txtFamMemBloodGroup)
        {
            int affectedRows = 0;

            affectedRows = objEmpPersonalFamilyMemberInfo.InsertEmployeePersonalFamilyMemberInfo(txtEmpPerFamInfoID, txtPersonalInfoID, txtFamMemName, txtFamMemDOB, txtFamMemAge, txtFamMemRelationship, txtFamMemAddr1, txtFamMemAddr2, txtFamMemArea, txtFamMemCity, txtFamMemState, txtFamMemPIN, txtFamMemCountry, txtFamMemContactNumber, txtFamMemMailID, txtFamMemBloodGroup);

            return affectedRows;
        }

        public int UpdateEmployeePersonalFamilyMemberInfo(int txtEmpPerFamInfoID, int txtPersonalInfoID, string txtFamMemName, DateTime txtFamMemDOB, int txtFamMemAge, string txtFamMemRelationship, string txtFamMemAddr1, string txtFamMemAddr2, string txtFamMemArea, string txtFamMemCity, string txtFamMemState, string txtFamMemPIN, string txtFamMemCountry, string txtFamMemContactNumber, string txtFamMemMailID, string txtFamMemBloodGroup)
        {
            int affectedRows = 0;

            affectedRows = objEmpPersonalFamilyMemberInfo.UpdateEmployeePersonalFamilyMemberInfo(txtEmpPerFamInfoID, txtPersonalInfoID, txtFamMemName, txtFamMemDOB, txtFamMemAge, txtFamMemRelationship, txtFamMemAddr1, txtFamMemAddr2, txtFamMemArea, txtFamMemCity, txtFamMemState, txtFamMemPIN, txtFamMemCountry, txtFamMemContactNumber, txtFamMemMailID, txtFamMemBloodGroup);

            return affectedRows;
        }

        public List<EmpPersonalFamilyMemberInfo> GetEmpPersonalFamilyMemberInfo(int txtPersonalInfoID)
        {
            List<EmpPersonalFamilyMemberInfo> lstEmpPersonalFamilyMemberInfo = new List<EmpPersonalFamilyMemberInfo>();

            lstEmpPersonalFamilyMemberInfo = objEmpPersonalFamilyMemberInfo.GetEmpPersonalFamilyMemberInfo(txtPersonalInfoID);

            return lstEmpPersonalFamilyMemberInfo;
        }
    }
}
