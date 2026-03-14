using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class clsEmployerContributionInfo
    {
        dbStaffSync.clsEmployerContributionInfo objclsEmployerContributionInfo = new dbStaffSync.clsEmployerContributionInfo();

        public int InsertEmployerContribution(int txtEmpSalDetID, decimal txtEmprPFAmount, decimal txtEmprPFAdminCharges, decimal txtEmprNPSAmount, decimal txtEmprESIAmount, decimal txtEmprGratuityAmount, decimal txtEmprBonusAmount)
        {
            int affectedRows = 0;
            affectedRows = objclsEmployerContributionInfo.InsertEmployerContribution(txtEmpSalDetID, txtEmprPFAmount, txtEmprPFAdminCharges, txtEmprNPSAmount, txtEmprESIAmount, txtEmprGratuityAmount, txtEmprBonusAmount);
            return affectedRows;
        }

        public int UpdateEmployerContribution(int txtEmpSalDetID, decimal txtEmprPFAmount, decimal txtEmprPFAdminCharges, decimal txtEmprNPSAmount, decimal txtEmprESIAmount, decimal txtEmprGratuityAmount, decimal txtEmprBonusAmount, int txtOrderID)
        {
            int affectedRows = 0;
            affectedRows = objclsEmployerContributionInfo.UpdateEmployerContribution(txtEmpSalDetID, txtEmprPFAmount, txtEmprPFAdminCharges, txtEmprNPSAmount, txtEmprESIAmount, txtEmprGratuityAmount, txtEmprBonusAmount, txtOrderID);
            return affectedRows;
        }

        public int DeleteEmployerContribution(int txtEmpSalDetID)
        {
            int affectedRows = 0;
            affectedRows = objclsEmployerContributionInfo.DeleteEmployerContribution(txtEmpSalDetID);
            return affectedRows;
        }
    }
}
