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
    public class clsEmploymentTypeInfo
    {
        dbStaffSync.clsEmploymentTypeInfo objEmploymentTypeInfo = new dbStaffSync.clsEmploymentTypeInfo();

        public List<EmploymentTypeInfo> GetEmploymentTypeList()
        {
            List<EmploymentTypeInfo> objEmploymentTypeInfoList = new List<EmploymentTypeInfo>();

            objEmploymentTypeInfoList = objEmploymentTypeInfo.GetEmploymentTypeList();

            return objEmploymentTypeInfoList;
        }

        public EmpTypeInfo getEmployeeSpecificEmploymentTypeInfo(int txtEmpID)
        {
            EmpTypeInfo objTempEmpTypeInfo = new EmpTypeInfo();

            objTempEmpTypeInfo = objEmploymentTypeInfo.getEmployeeSpecificEmploymentTypeInfo(txtEmpID);

            return objTempEmpTypeInfo;
        }

        public int GetEmployeeTypeTitleByTitle(string EmpTypeTitle)
        {
            int EmpTypeMasID = 0;

            EmpTypeMasID = objEmploymentTypeInfo.GetEmployeeTypeTitleByTitle(EmpTypeTitle);

            return EmpTypeMasID;
        }

        public int InsertEmploymentTypeMasInfo(string txtEmpTypeCode, string txtEmpTypeTitle, string txtEmpTypeInitial, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;

            affectedRows = objEmploymentTypeInfo.InsertEmploymentTypeMasInfo(txtEmpTypeCode, txtEmpTypeTitle, txtEmpTypeInitial, IsActive, IsDeleted);

            return affectedRows;
        }

        public int InsertEmploymentTypeInfo(int txtEmpID, int txtEmpTypeMasID, DateTime txtEffectiveDate)
        {
            int affectedRows = 0;

            affectedRows = objEmploymentTypeInfo.InsertEmploymentTypeInfo(txtEmpID, txtEmpTypeMasID, txtEffectiveDate);

            return affectedRows;
        }

        public int UpdateEmploymentTypeInfo(int txtEmpTypeInfoID, int txtEmpID, int txtEmpTypeMasID, DateTime txtEffectiveDate)
        {
            int affectedRows = 0;

            affectedRows = objEmploymentTypeInfo.UpdateEmploymentTypeInfo(txtEmpTypeInfoID, txtEmpID, txtEmpTypeMasID, txtEffectiveDate);

            return affectedRows;
        }

        public int DeleteEmployeeShiftInfo(int txtEmpTypeInfoID)
        {
            int affectedRows = 0;

            affectedRows = objEmploymentTypeInfo.DeleteEmployeeShiftInfo(txtEmpTypeInfoID);

            return affectedRows;
        }
    }
}
