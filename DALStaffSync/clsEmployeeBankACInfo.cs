using ModelStaffSync;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace DALStaffSync
{
    public class clsEmployeeBankACInfo
    {
        dbStaffSync.clsEmployeeBankACInfo objEmployeeBankACInfo = new dbStaffSync.clsEmployeeBankACInfo();

        public List<EmployeeSpecificBankInfo> getEmployeeSpecificBankAccountInfo(int empID)
        {
            List<EmployeeSpecificBankInfo> objEmployeeSpecificBankInfoList = new List<EmployeeSpecificBankInfo>();

            objEmployeeSpecificBankInfoList = objEmployeeBankACInfo.getEmployeeSpecificBankAccountInfo(empID);

            return objEmployeeSpecificBankInfoList;
        }
    }
}
