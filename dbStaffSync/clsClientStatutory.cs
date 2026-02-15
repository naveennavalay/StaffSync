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
    public class clsClientStatutory
    {

        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public ProvidentFund GetCompanyProvidentFundSettings(int ClientID)
        {
            ProvidentFund tmpProvidentFund = new ProvidentFund();
            List<ProvidentFund> objProvidentFund = new List<ProvidentFund>();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT " +
                                        "ProvFundMas.PFMasID, " +
                                        "ProvFundMas.PFMasTitle, " +
                                        "ProvFundDetails.PFDetID, " +
                                        "ProvFundDetails.PFMasID, " +
                                        "ProvFundDetails.EmpPFPercentageOrAmount, " +
                                        "ProvFundDetails.EmpPFPercentage, " +
                                        "ProvFundDetails.EmpPFAmount, " +
                                        "ProvFundDetails.EmprPFPercentageOrAmount, " +
                                        "ProvFundDetails.EmprPFPercentage, " +
                                        "ProvFundDetails.EmprPFAmount, " +
                                        "ProvFundDetails.EmprPSPercentageOrAmount, " +
                                        "ProvFundDetails.EmprPSPercentage, " +
                                        "ProvFundDetails.EmprPSAmount, " +
                                        "ProvFundDetails.EffectiveDate, " +
                                        "ProvFundDetails.OrderID " +
                                    " FROM " +
                                        " ProvFundMas INNER JOIN ProvFundDetails ON ProvFundMas.PFMasID = ProvFundDetails.PFMasID " +
                                    " WHERE " +
                                        " (" +
                                            " ( " +
                                                " (ProvFundDetails.OrderID) IN ( " +
                                                    " SELECT " +
                                                        " MAX(OrderID) " +
                                                    " FROM " +
                                                        " ProvFundDetails " +
                                                    " WHERE " +
                                                        " ProvFundMas.ClientID = " + ClientID + " " +
                                                " ) " +
                                            " ) " +
                                        " ) " +
                                    " GROUP BY " +
                                        "ProvFundMas.PFMasID, " +
                                        "ProvFundMas.PFMasTitle, " +
                                        "ProvFundDetails.PFDetID, " +
                                        "ProvFundDetails.PFMasID, " +
                                        "ProvFundDetails.EmpPFPercentageOrAmount, " +
                                        "ProvFundDetails.EmpPFPercentage, " +
                                        "ProvFundDetails.EmpPFAmount, " +
                                        "ProvFundDetails.EmprPFPercentageOrAmount, " +
                                        "ProvFundDetails.EmprPFPercentage, " +
                                        "ProvFundDetails.EmprPFAmount, " +
                                        "ProvFundDetails.EmprPSPercentageOrAmount, " +
                                        "ProvFundDetails.EmprPSPercentage, " +
                                        "ProvFundDetails.EmprPSAmount, " +
                                        "ProvFundDetails.EffectiveDate, " +
                                        "ProvFundDetails.OrderID, " +
                                        "ProvFundMas.ClientID, " +
                                        "ProvFundMas.IsActive, " +
                                        "ProvFundMas.IsDeleted " +
                                    " HAVING " +
                                        " ( " +
                                            " ((ProvFundMas.ClientID) = " + ClientID + ") " +
                                            " AND ((ProvFundMas.IsActive) = True) " +
                                            " AND ((ProvFundMas.IsDeleted) = False) " +
                                        ");";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objProvidentFund = JsonConvert.DeserializeObject<List<ProvidentFund>>(DataTableToJSon);
                if (objProvidentFund.Count > 0)
                {
                    tmpProvidentFund = objProvidentFund[0];
                }
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

            return tmpProvidentFund;
        }

        public int InsertClientProvidentFundSettings(int txtPFMasID, string txtEmpPFPercentageOrAmount, decimal txtEmpPFPercentage, decimal txtEmpPFAmount, string txtEmprPFPercentageOrAmount, decimal txtEmprPFPercentage, decimal txtEmprPFAmount, string txtEmprPSPercentageOrAmount, decimal txtEmprPSPercentage, decimal txtEmprPSAmount, DateTime txtEffectiveDate)
        {
            int affectedRows = 0;

            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("ProvFundDetails", "PFDetID");
                Response<int> OrderID = objGenFunc.getMaxRowCount("ProvFundDetails", "OrderID");

                conn = dbStaffSync.openDBConnection();

                string strQuery = "INSERT INTO ProvFundDetails (PFDetID, PFMasID, EmpPFPercentageOrAmount, EmpPFPercentage, EmpPFAmount, EmprPFPercentageOrAmount, EmprPFPercentage, EmprPFAmount, EmprPSPercentageOrAmount, EmprPSPercentage, EmprPSAmount, EffectiveDate, OrderID) VALUES " +
                "(" + maxRowCount.Data + "," + txtPFMasID + ",'" + txtEmpPFPercentageOrAmount + "'," + txtEmpPFPercentage + "," + txtEmpPFAmount + ",'" + txtEmprPFPercentageOrAmount + "'," + txtEmprPFPercentage + "," + txtEmprPFAmount + ",'" + txtEmprPSPercentageOrAmount + "'," + txtEmprPSPercentage + "," + txtEmprPSAmount + ",'" + txtEffectiveDate.ToString("dd-MMM-yyyy") + "'," + OrderID.Data + ")";

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


        public ClientStatutory getClientStatutory(int txtClientID)
        {
            ClientStatutory tmpClientStatutory = new ClientStatutory();
            List<ClientStatutory> objClientStatutory = new List<ClientStatutory>();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT " + 
                                        " Max(clientstatutoryinfo.ClientStatutoryID) AS ClientStatutoryID, Max(clientstatutoryinfo.ClientID) AS ClientID, Max(clientstatutoryinfo.EffectiveDate) AS EffectiveDate, Max(clientstatutoryinfo.EnableClientStatutory) AS EnableClientStatutory, Max(clientstatutoryinfo.EnablePF) AS EnablePF, Max(clientstatutoryinfo.PFCode) AS PFCode, Max(clientstatutoryinfo.EnablePT) AS EnablePT, Max(clientstatutoryinfo.PTCode) AS PTCode, Max(clientstatutoryinfo.EnableESI) AS EnableESI, Max(clientstatutoryinfo.ESICode) AS ESICode, Max(clientstatutoryinfo.EnableNPS) AS EnableNPS, Max(clientstatutoryinfo.NPSCode) AS NPSCode, Max(clientstatutoryinfo.OrderID) AS OrderID " + 
                                        " FROM clientstatutoryinfo " + 
                                  " WHERE " + 
                                        " clientstatutoryinfo.ClientID = " + txtClientID + " ORDER BY Max(clientstatutoryinfo.EffectiveDate), Max(clientstatutoryinfo.OrderID);";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objClientStatutory = JsonConvert.DeserializeObject<List<ClientStatutory>>(DataTableToJSon);
                if (objClientStatutory.Count > 0)
                {
                    tmpClientStatutory = objClientStatutory[0];
                }
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

            return tmpClientStatutory;
        }

        public int InsertClientStatutory(int txtClientID, DateTime txtEffectiveDate, bool EnableClientStatutory, bool EnablePF, string txtPFCode, bool EnablePT, string txtPTCode, bool EnableESI, string txtESICode, bool EnableNPS, string txtNPSCode)
        {
            int affectedRows = 0;

            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("ClientStatutoryInfo", "ClientStatutoryID");
                Response<int> OrderID = objGenFunc.getMaxRowCount("ClientStatutoryInfo", "OrderID");

                conn = dbStaffSync.openDBConnection();

                string strQuery = "INSERT INTO ClientStatutoryInfo (ClientStatutoryID, ClientID, EffectiveDate, EnableClientStatutory, EnablePF, PFCode, EnablePT, PTCode, EnableESI, ESICode, EnableNPS, NPSCode, OrderID) VALUES " +
                "(" + maxRowCount.Data + "," + txtClientID + ",'" + txtEffectiveDate.ToString("dd-MMM-yyyy") + "'," + EnableClientStatutory + "," + EnablePF + ",'" + txtPFCode + "'," + EnablePT + ",'" + txtPTCode + "'," + EnableESI + ",'" + txtESICode + "'," + EnableNPS + ",'" + txtNPSCode + "'," + OrderID.Data + ")";

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

        public int UpdateClientStatutory(int txtClientStatutoryID, int txtClientID, DateTime txtEffectiveDate, bool EnableClientStatutory, bool EnablePF, string txtPFCode, bool EnablePT, string txtPTCode, bool EnableESI, string txtESICode, bool EnableNPS, string txtNPSCode)
        {
            int affectedRows = 0;

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "UPDATE ClientStatutoryInfo SET " + 
                                          " ClientStatutoryInfo.ClientID = " + txtClientID + ", ClientStatutoryInfo.EffectiveDate = '" + txtEffectiveDate + "', ClientStatutoryInfo.EnableClientStatutory = " + EnableClientStatutory + ", ClientStatutoryInfo.EnablePF = " + EnablePF + ", ClientStatutoryInfo.PFCode = '" + txtPFCode + "', ClientStatutoryInfo.EnablePT = " + EnablePT + ", ClientStatutoryInfo.PTCode = '" + txtPTCode + "', ClientStatutoryInfo.EnableESI = " + EnableESI + ", ClientStatutoryInfo.ESICode = '" + txtESICode + "', ClientStatutoryInfo.EnableNPS = " + EnableNPS + ", ClientStatutoryInfo.NPSCode = '" + txtNPSCode + "'" +
                                  " WHERE " + 
                                          " ClientStatutoryInfo.ClientStatutoryID = " + txtClientStatutoryID;

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
