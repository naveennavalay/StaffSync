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
    public class clsProvidentFundCalculation
    {
        dbStaffSync.clsProvidentFundCalculation objAuditLog = new dbStaffSync.clsProvidentFundCalculation();

        public ProvidentFund CalculatePF(int PFMasID)
        {
            return objAuditLog.CalculatePF(PFMasID);
        }
    }
}
