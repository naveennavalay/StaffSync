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
    public class clsEmployeePersonalInfo
    {
        dbStaffSync.clsEmployeePersonalInfo objEmployeePersonalInfo = new dbStaffSync.clsEmployeePersonalInfo();

        public clsEmployeePersonalInfo() { 

        }

        public int InsertEmployeePersonalInfo(int txtEmployeeID, DateTime txtEmployeeDOB, DateTime txtEmployeeDOJ, int txtEmployeeQualID, int txtPermanentAddressID, int txtCurrentAddressID, string ContactNumber1, string ContactNumber2, int txtContactID1, int txtContactID2, int txtEmployeeSexID, int txtEmployeeLastCompayID, int txtClientBranchID)
        {
            int affectedRows = 0;

            affectedRows = objEmployeePersonalInfo.InsertEmployeePersonalInfo(txtEmployeeID, txtEmployeeDOB, txtEmployeeDOJ, txtEmployeeQualID, txtPermanentAddressID, txtCurrentAddressID, ContactNumber1, ContactNumber2, txtContactID1, txtContactID2, txtEmployeeSexID, txtEmployeeLastCompayID, txtClientBranchID);

            return affectedRows;
        }

        public int UpdateEmployeePersonalInfo(int txtEmployeeID, DateTime txtEmployeeDOB, DateTime txtEmployeeDOJ, int txtEmployeeQualID, int txtPermanentAddressID, int txtCurrentAddressID, string ContactNumber1, string ContactNumber2, int txtContactID1, int txtContactID2, int txtEmployeeSexID, int txtEmployeeLastCompayID, int txtClientBranchID)
        {
            int affectedRows = 0;

            affectedRows = objEmployeePersonalInfo.UpdateEmployeePersonalInfo(txtEmployeeID, txtEmployeeDOB, txtEmployeeDOJ, txtEmployeeQualID, txtPermanentAddressID, txtCurrentAddressID, ContactNumber1, ContactNumber2, txtContactID1, txtContactID2, txtEmployeeSexID, txtEmployeeLastCompayID, txtClientBranchID);

            return affectedRows;
        }

        public EmpPersonalPersonalInfo GetEmpPersonalPersonalInfo(int EmployeeID)
        {
            EmpPersonalPersonalInfo empPersonalPersonalInfo = new EmpPersonalPersonalInfo();
            
            empPersonalPersonalInfo = objEmployeePersonalInfo.GetEmpPersonalPersonalInfo(EmployeeID);

            return empPersonalPersonalInfo;
        }
    }
}
