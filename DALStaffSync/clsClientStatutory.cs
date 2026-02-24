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
    public class clsClientStatutory
    {
        dbStaffSync.clsClientStatutory objClientStatutory = new dbStaffSync.clsClientStatutory();


        public ProvidentFund GetCompanyProvidentFundSettings(int ClientID)
        {
            return objClientStatutory.GetCompanyProvidentFundSettings(ClientID);
        }

        public ESIModel GetCompanyESISettings(int ClientID)
        {
            return objClientStatutory.GetCompanyESISettings(ClientID);
        }

        public int InsertClientProvidentFundSettings(int txtPFMasID, string txtEmpPFPercentageOrAmount, decimal txtEmpPFPercentage, decimal txtEmpPFAmount, string txtEmprPFPercentageOrAmount, decimal txtEmprPFPercentage, decimal txtEmprPFAmount, string txtEmprPSPercentageOrAmount, decimal txtEmprPSPercentage, decimal txtEmprPSAmount, DateTime txtEffectiveDate)
        {
            return objClientStatutory.InsertClientProvidentFundSettings(txtPFMasID, txtEmpPFPercentageOrAmount, txtEmpPFPercentage, txtEmpPFAmount, txtEmprPFPercentageOrAmount, txtEmprPFPercentage, txtEmprPFAmount, txtEmprPSPercentageOrAmount, txtEmprPSPercentage, txtEmprPSAmount, txtEffectiveDate);
        }

        public ClientStatutory getClientStatutory(int txtClientID)
        {
            return objClientStatutory.getClientStatutory(txtClientID);
        }
        

        public int InsertClientStatutory(int txtClientID, DateTime txtEffectiveDate, bool EnableClientStatutory, bool EnablePF, string txtPFCode, bool EnablePT, string txtPTCode, bool EnableESI, string txtESICode, bool EnableNPS, string txtNPSCode)
        {
            return objClientStatutory.InsertClientStatutory(txtClientID, txtEffectiveDate, EnableClientStatutory, EnablePF, txtPFCode, EnablePT, txtPTCode, EnableESI, txtESICode, EnableNPS, txtNPSCode);
        }

        public int UpdateClientStatutory(int txtClientStatutoryID, int txtClientID, DateTime txtEffectiveDate, bool EnableClientStatutory, bool EnablePF, string txtPFCode, bool EnablePT, string txtPTCode, bool EnableESI, string txtESICode, bool EnableNPS, string txtNPSCode)
        {
            return objClientStatutory.UpdateClientStatutory(txtClientStatutoryID, txtClientID, txtEffectiveDate, EnableClientStatutory, EnablePF, txtPFCode, EnablePT, txtPTCode, EnableESI, txtESICode, EnableNPS, txtNPSCode);
        }
    }
}
