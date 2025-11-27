using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace dbStaffSync
{
    public class clsClientInfo
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public List<ClientInfo> getAllCompanyList()
        {
            List<ClientInfo> companyList = new List<ClientInfo>();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT ClientMas.ClientID, ClientMas.ClientCode, ClientMas.ClientName, ClientMas.ClientAddress1, ClientMas.ClientAddress2, ClientMas.ClientArea, ClientMas.ClientCity, ClientMas.ClientState, ClientMas.ClientPIN, ClientMas.ClientCountry, ClientMas.ClientPhone, ClientMas.ClientMailID, ClientMas.ClientContactPerson, ClientMas.ClientContactNumber, ClientMas.ClientContactMail, ClientMas.ClientWebSite, ClientMas.IsActive, ClientMas.IsDeleted " +
                                          " FROM ClientMas " + 
                                  " WHERE " + 
                                          " (((ClientMas.IsActive)=True) AND ((ClientMas.IsDeleted)=False))";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                companyList = JsonConvert.DeserializeObject<List<ClientInfo>>(DataTableToJSon);
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

            return companyList;
        }

        public List<ClientInfo> getAllCompanyList(string filterText)
        {
            List<ClientInfo> companyList = new List<ClientInfo>();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT ClientMas.ClientID, ClientMas.ClientCode, ClientMas.ClientName, ClientMas.ClientAddress1, ClientMas.ClientAddress2, ClientMas.ClientArea, ClientMas.ClientCity, ClientMas.ClientState, ClientMas.ClientPIN, ClientMas.ClientCountry, ClientMas.ClientPhone, ClientMas.ClientMailID, ClientMas.ClientContactPerson, ClientMas.ClientContactNumber, ClientMas.ClientContactMail, ClientMas.ClientWebSite, ClientMas.IsActive, ClientMas.IsDeleted " +
                                        " FROM ClientMas " +
                                  " WHERE ClientMas.IsActive = true AND ClientMas.IsDeleted = false AND ClientMas.ClientName LIKE '" + filterText + "%'";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                companyList = JsonConvert.DeserializeObject<List<ClientInfo>>(DataTableToJSon);
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

            return companyList;
        }

        public List<ClientInfo> getClientInfo(int txtClientID)
        {
            List<ClientInfo> objClientInfoList = new List<ClientInfo>();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT * FROM ClientMas WHERE ClientID = " + txtClientID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objClientInfoList = JsonConvert.DeserializeObject<List<ClientInfo>>(DataTableToJSon);
                if(objClientInfoList.Count > 0)
                {
                    AppVariables.CompanyID = objClientInfoList[0].ClientID;
                    AppVariables.CompanyCode = objClientInfoList[0].ClientCode.ToString();
                    AppVariables.CompanyName = objClientInfoList[0].ClientName.ToString();
                    AppVariables.CompanyAddress = objClientInfoList[0].ClientAddress1.ToString() + "," + objClientInfoList[0].ClientAddress1.ToString() + "," + objClientInfoList[0].ClientArea.ToString() + "," + objClientInfoList[0].ClientCity.ToString() + "," + objClientInfoList[0].ClientState.ToString() + "," + objClientInfoList[0].ClientCountry.ToString();
                    AppVariables.CompanyPhone = objClientInfoList[0].ClientPhone.ToString();
                    AppVariables.CompanyEmail = objClientInfoList[0].ClientMailID.ToString();
                    AppVariables.CompanyContactPerson = objClientInfoList[0].ClientContactPerson.ToString();
                    AppVariables.ClientContactNumber = objClientInfoList[0].ClientContactNumber.ToString();
                    AppVariables.ClientContactMail = objClientInfoList[0].ClientContactMail.ToString();
                    AppVariables.ClientWebSite = objClientInfoList[0].ClientWebSite.ToString();
                    AppVariables.IsActive = Convert.ToBoolean(objClientInfoList[0].IsActive.ToString()); 
                    AppVariables.IsDeleted = Convert.ToBoolean(objClientInfoList[0].IsDeleted.ToString());
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

            return objClientInfoList;
        }

        public List<ClientInfo> getClientInfoByEmpID(int txtEmpID)
        {
            List<ClientInfo> objClientInfoList = new List<ClientInfo>();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT " +
                                    "EmpMas.EmpID, " +
                                    "ClientMas.ClientID, " +
                                    "ClientMas.ClientCode, " +
                                    "ClientMas.ClientName, " +
                                    "ClientMas.ClientAddress1, " +
                                    "ClientMas.ClientAddress2, " +
                                    "ClientMas.ClientArea, " +
                                    "ClientMas.ClientCity, " +
                                    "ClientMas.ClientState, " +
                                    "ClientMas.ClientPIN, " +
                                    "ClientMas.ClientCountry, " +
                                    "ClientMas.ClientPhone, " +
                                    "ClientMas.ClientMailID, " +
                                    "ClientMas.ClientContactPerson, " +
                                    "ClientMas.ClientContactNumber, " +
                                    "ClientMas.ClientContactMail, " +
                                    "ClientMas.ClientWebSite, " +
                                    "ClientMas.ClientLogo, " +
                                    "ClientMas.IsActive, " +
                                    "ClientMas.IsDeleted " +
                                "FROM " +
                                    "ClientMas " +
                                    "INNER JOIN EmpMas ON ClientMas.ClientID = EmpMas.ClientID " +
                                "WHERE " +
                                    "(" +
                                        "((EmpMas.EmpID) = " + txtEmpID + ") " +
                                        "AND ((ClientMas.IsActive) = True) " +
                                        "AND ((ClientMas.IsDeleted) = False) " +
                                    ");";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objClientInfoList = JsonConvert.DeserializeObject<List<ClientInfo>>(DataTableToJSon);
                if (objClientInfoList.Count > 0)
                {
                    AppVariables.CompanyID = objClientInfoList[0].ClientID;
                    AppVariables.CompanyCode = objClientInfoList[0].ClientCode.ToString();
                    AppVariables.CompanyName = objClientInfoList[0].ClientName.ToString();
                    AppVariables.CompanyAddress = objClientInfoList[0].ClientAddress1.ToString() + "," + objClientInfoList[0].ClientAddress1.ToString() + "," + objClientInfoList[0].ClientArea.ToString() + "," + objClientInfoList[0].ClientCity.ToString() + "," + objClientInfoList[0].ClientState.ToString() + "," + objClientInfoList[0].ClientCountry.ToString();
                    AppVariables.CompanyPhone = objClientInfoList[0].ClientPhone.ToString();
                    AppVariables.CompanyEmail = objClientInfoList[0].ClientMailID.ToString();
                    AppVariables.CompanyContactPerson = objClientInfoList[0].ClientContactPerson.ToString();
                    AppVariables.ClientContactNumber = objClientInfoList[0].ClientContactNumber.ToString();
                    AppVariables.ClientContactMail = objClientInfoList[0].ClientContactMail.ToString();
                    AppVariables.ClientWebSite = objClientInfoList[0].ClientWebSite.ToString();
                    AppVariables.IsActive = Convert.ToBoolean(objClientInfoList[0].IsActive.ToString());
                    AppVariables.IsDeleted = Convert.ToBoolean(objClientInfoList[0].IsDeleted.ToString());
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

            return objClientInfoList;
        }

        public int InsertClientInfo(string txtClientCode, string txtClientName, string txtClientAddress1, string txtClientAddress2, string txtClientArea, string txtClientCity, string txtClientState, string txtClientPIN, string txtClientCountry, string txtClientPhone, string txtClientMailID, string txtClientContactPerson, string txtClientContactNumber, string txtClientContactMail, string txtClientWebSite, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;

            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("ClientMas", "ClientID");

                conn = dbStaffSync.openDBConnection();

                string strQuery = "INSERT INTO ClientMas (ClientID, ClientCode, ClientName, ClientAddress1, ClientAddress2, ClientArea, ClientCity, ClientState, ClientPIN, ClientCountry, ClientPhone, ClientMailID, ClientContactPerson, ClientContactNumber, ClientContactMail, ClientWebSite, IsActive, IsDeleted) VALUES " +
                "(" + maxRowCount.Data + ",'" + "CNT-" + (maxRowCount.Data).ToString().PadLeft(4, '0').Trim() + "','" + txtClientName.Trim() + "','" + txtClientAddress1.Trim() + "','" + txtClientAddress2.Trim() + "','" + txtClientArea.Trim() + "','" + txtClientCity.Trim() + "','" + txtClientState.Trim() + "','" + txtClientPIN + "','" + txtClientCountry.Trim() + "','" + txtClientPhone.Trim() + "','" + txtClientMailID.Trim() + "','" + txtClientContactPerson.Trim() + "','" + txtClientContactNumber.Trim() + "','" + txtClientContactMail.Trim() + "','" + txtClientWebSite.Trim() + "'," + IsActive + "," + IsDeleted + ")";

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

        public int UpdateClientInfo(int txtClientID, string txtClientCode, string txtClientName, string txtClientAddress1, string txtClientAddress2, string txtClientArea, string txtClientCity, string txtClientState, string txtClientPIN, string txtClientCountry, string txtClientPhone, string txtClientMailID, string txtClientContactPerson, string txtClientContactNumber, string txtClientContactMail, string txtClientWebSite, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "UPDATE ClientMas SET ClientCode = '" + txtClientCode + "', ClientName = '" + txtClientName + "', ClientAddress1 = '" + txtClientAddress1 + "', ClientAddress2 = '" + txtClientAddress2 + "', ClientArea = '" + txtClientArea + "', ClientCity = '" + txtClientCity + "', ClientState = '" + 
                txtClientState + "', ClientPIN = '" + txtClientPIN + "', ClientCountry = '" + txtClientCountry + "', ClientPhone = '" + txtClientPhone + "', ClientMailID = '" + txtClientMailID + "', ClientContactPerson = '" + txtClientContactPerson + "', ClientContactNumber = '" + 
                txtClientContactNumber + "', ClientContactMail = '" + txtClientContactMail + "', ClientWebSite = '" + txtClientWebSite + "', IsActive = " + IsActive + ", IsDeleted = " + IsDeleted + 
                " WHERE ClientID = " + txtClientID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtClientID;
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

        public int DeleteClientInfo(int txtClientID)
        {
            int affectedRows = 0;

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "UPDATE ClientMas SET IsDeleted = true WHERE ClientID = " + txtClientID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtClientID;
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
