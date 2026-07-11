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
    public class clsPrintSettings
    {
        dbStaffSync.clsPrintSettings objPrintSettings = new dbStaffSync.clsPrintSettings();

        public PrintSettings GetPrintSettings(int ClientID)
        {
            PrintSettings objPrintSetting = new PrintSettings();

            objPrintSetting = objPrintSettings.GetPrintSettings(ClientID);

            return objPrintSetting;
        }

        public int InsertPrintSettings(int ClientID, bool PrntReportGeneratedBy, bool PrntReportGeneratedOn, bool PrntLogoInReport, bool PrntHeaderInReport, bool PrntFooterInReport, bool PnrtShowWatermark)
        {
            int affectedRows = 0;
            affectedRows = objPrintSettings.InsertPrintSettings(ClientID, PrntReportGeneratedBy, PrntReportGeneratedOn, PrntLogoInReport, PrntHeaderInReport, PrntFooterInReport, PnrtShowWatermark);
            return affectedRows;
        }

        public int UpdatePrintSettings(int PRNTSettingID, int ClientID, bool PrntReportGeneratedBy, bool PrntReportGeneratedOn, bool PrntLogoInReport, bool PrntHeaderInReport, bool PrntFooterInReport, bool PnrtShowWatermark)
        {
            int affectedRows = 0;

            affectedRows = objPrintSettings.UpdatePrintSettings(PRNTSettingID, ClientID, PrntReportGeneratedBy, PrntReportGeneratedOn, PrntLogoInReport, PrntHeaderInReport, PrntFooterInReport, PnrtShowWatermark);

            return affectedRows;
        }
    }
}
