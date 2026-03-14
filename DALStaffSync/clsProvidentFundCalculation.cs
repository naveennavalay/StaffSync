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
        dbStaffSync.clsProvidentFundCalculation objProvidentFundCalculation = new dbStaffSync.clsProvidentFundCalculation();

        public ProvidentFund GetProvidentFundMasterInfo(int PFMasID)
        {
            return objProvidentFundCalculation.GetProvidentFundMasterInfo(PFMasID);
        }
    }
}
