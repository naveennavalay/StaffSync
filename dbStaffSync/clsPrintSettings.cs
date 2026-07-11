using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace dbStaffSync
{
    public class clsPrintSettings
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public PrintSettings GetPrintSettings(int ClientID)
        {
            List<PrintSettings> objPrintSettingList = new List<PrintSettings>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " + 
                                        "ClientMas.ClientCode, " + 
                                        "ClientMas.ClientName, " + 
                                        "PrintSettings.PRNTSettingID, " + 
                                        "PrintSettings.PrntReportGeneratedBy, " + 
                                        "PrintSettings.PrntReportGeneratedOn, " + 
                                        "PrintSettings.PrntLogoInReport, " + 
                                        "PrintSettings.PrntHeaderInReport, " + 
                                        "PrintSettings.PrntFooterInReport, " + 
                                        "PrintSettings.PnrtShowWatermark, " + 
                                        "ClientMas.ClientID " + 
                                    " FROM " + 
                                        " ClientMas " + 
                                        " INNER JOIN PrintSettings ON ClientMas.ClientID = PrintSettings.ClientID " + 
                                    " WHERE " + 
                                        " ClientMas.ClientID = " + ClientID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objPrintSettingList = JsonConvert.DeserializeObject<List<PrintSettings>>(DataTableToJSon);
                if(objPrintSettingList.Count > 0)
                    return objPrintSettingList[1];
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = dbStaffSync.closeDBConnection();
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }

            return objPrintSettingList[0];
        }

        public int InsertPrintSettings(int ClientID, bool PrntReportGeneratedBy, bool PrntReportGeneratedOn, bool PrntLogoInReport, bool PrntHeaderInReport, bool PrntFooterInReport, bool PnrtShowWatermark)
        {
            int affectedRows = 0;
            try
            {

                Response<int> maxRowCount = objGenFunc.getMaxRowCount("PrintSettings", "PRNTSettingID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO PrintSettings (PRNTSettingID, ClientID, PrntReportGeneratedBy, PrntReportGeneratedOn, PrntLogoInReport, PrntHeaderInReport, PrntFooterInReport, PnrtShowWatermark) VALUES " +
                 "(" + maxRowCount.Data + ", " + ClientID + ", " + PrntReportGeneratedBy + ", " + PrntReportGeneratedOn + ", " + PrntLogoInReport + ", " + PrntHeaderInReport + ", " + PrntFooterInReport + ", " + PnrtShowWatermark + ")";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = maxRowCount.Data;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = dbStaffSync.closeDBConnection();
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }

            return affectedRows;
        }

        public int UpdatePrintSettings(int PRNTSettingID, int ClientID, bool PrntReportGeneratedBy, bool PrntReportGeneratedOn, bool PrntLogoInReport, bool PrntHeaderInReport, bool PrntFooterInReport, bool PnrtShowWatermark)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE PrintSettings SET " +
                " PrntReportGeneratedBy = " + PrntReportGeneratedBy + ", PrntReportGeneratedOn = " + PrntReportGeneratedOn + ", " +
                " PrntLogoInReport = " + PrntLogoInReport + ", PrntHeaderInReport = " + PrntHeaderInReport + ", PrntFooterInReport = " + PrntFooterInReport + ", PnrtShowWatermark = " + PnrtShowWatermark +
                " WHERE PRNTSettingID = " + PRNTSettingID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = dbStaffSync.closeDBConnection();
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }

            return affectedRows;
        }
    }
}
