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
    public class clsEmployeeStateInsurance
    {
        dbStaffSync.clsEmployeeStateInsurance objEmployeeStateInsurance = new dbStaffSync.clsEmployeeStateInsurance();

        public ESIModel GetEmployeeStateInsuranceMasterInfo(int PFMasID)
        {
            return objEmployeeStateInsurance.GetEmployeeStateInsuranceMasterInfo(PFMasID);
        }
    }
}
