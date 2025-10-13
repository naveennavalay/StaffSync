//using C1.Framework;
using dbStaffSync;
using ModelStaffSync;
using Newtonsoft.Json;
//using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace DALStaffSync
{
    public class clsEmployeeSalaryProfileInfo
    {
        dbStaffSync.clsEmployeeSalaryProfileInfo objEmployeeSalaryProfileInfo = new dbStaffSync.clsEmployeeSalaryProfileInfo();

        public List<EmployeeSalaryProfileInfo> GetDefaultEmployeeSalaryProfileInfo(int SalaryProfileID)
        {
            List<EmployeeSalaryProfileInfo> objReturnEmployeeSalaryProfileInfoList = new List<EmployeeSalaryProfileInfo>();
            DataTable dt = new DataTable();

            objReturnEmployeeSalaryProfileInfoList = objEmployeeSalaryProfileInfo.GetDefaultEmployeeSalaryProfileInfo(SalaryProfileID);

            return objReturnEmployeeSalaryProfileInfoList;
        }

        public int InsertEmployeeEmployeeSalaryProfileInfo(int txtEmpID, int SalaryProfileID, DateTime EffectiveDate)
        {
            int affectedRows = 0;
            
            affectedRows = objEmployeeSalaryProfileInfo.InsertEmployeeEmployeeSalaryProfileInfo(txtEmpID, SalaryProfileID, EffectiveDate);

            return affectedRows;
        }
    }
}
