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
    public class clsBankMas
    {
        dbStaffSync.clsBankMas objBankMas = new dbStaffSync.clsBankMas();

        public clsBankMas() { 

        }

        public EmployeeBankInfo GetEmployeeSpecificBankInfo(int txtEmpID)
        {
            EmployeeBankInfo empBankDetailsInfo = new EmployeeBankInfo();
            
            empBankDetailsInfo = objBankMas.GetEmployeeSpecificBankInfo(txtEmpID);

            return empBankDetailsInfo;
        }

        public List<BankDetailsInfo> GetFullBanksList()
        {
            List<BankDetailsInfo> bankDetailsInfoList = new List<BankDetailsInfo>();
            
            bankDetailsInfoList = objBankMas.GetFullBanksList();

            return bankDetailsInfoList;
        }


        public int InsertEmployeeBankReference(int txtEmpID, string txtEmpACNumber, int txtBankID, bool IsDefault)
        {
            int affectedRows = 0;

            affectedRows = objBankMas.InsertEmployeeBankReference(txtEmpID, txtEmpACNumber, txtBankID, IsDefault);

            return affectedRows;
        }

        public int UpdateEmployeeBankReference(int txtEmpBankID, int txtEmpID, string txtEmpACNumber, int txtBankID, bool IsDefault)
        {
            int affectedRows = 0;
            
            affectedRows = objBankMas.UpdateEmployeeBankReference(txtEmpBankID, txtEmpID, txtEmpACNumber, txtBankID, IsDefault);

            return affectedRows;
        }
    }
}
