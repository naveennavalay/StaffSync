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
    public class clsEmpAssetAllocationInfo
    {
        dbStaffSync.clsEmpAssetAllocationInfo objEmpAssetAllocationInfo = new dbStaffSync.clsEmpAssetAllocationInfo();
        public clsEmpAssetAllocationInfo() { 

        }

        public void getEmployeeAssetRequestInfoList(int EmployeeID)
        {
            objEmpAssetAllocationInfo.getEmployeeAssetRequestInfoList(EmployeeID);
        }

        public int InsertEmployeeAssetAllocationShortInformation(int EmpAssetAllocationID, int PersonalInfoID, int AssetID, DateTime TransDate, string Description, int AssetRequestID)
        {
            return objEmpAssetAllocationInfo.InsertEmployeeAssetAllocationShortInformation(EmpAssetAllocationID, PersonalInfoID, AssetID, TransDate, Description, AssetRequestID);
        }
    }
}
