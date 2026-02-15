using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace DALStaffSync
{
    public class clsProfessionalTaxCalculation
    {
        dbStaffSync.clsProfessionalTaxCalculation objAuditLog = new dbStaffSync.clsProfessionalTaxCalculation();

        public decimal? CalculateProfessionalTax(int PTMasID, int StateID, decimal grossIncome, int monthNo, int SexID)
        {
            return objAuditLog.CalculateProfessionalTax(PTMasID, StateID, grossIncome, monthNo, SexID);
        }
    }
}
