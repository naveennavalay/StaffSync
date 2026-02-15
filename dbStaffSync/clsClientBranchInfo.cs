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
    public class clsClientBranchInfo
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public List<ClientBranchInfo> getBranchInfoList(int txtClientID)
        {
            List<ClientBranchInfo> companyList = new List<ClientBranchInfo>();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT " +
                                          " ClientBranchMas.ClientBranchID, ClientBranchMas.ClientBranchCode +" + "', '" + "+ ClientBranchMas.ClientBranchName+" + "', '" + "+ ClientBranchMas.ClientBranchAddress1+" + "', '" + "+ ClientBranchMas.ClientBranchAddress2+" + "', '" + "+ ClientBranchMas.ClientBranchArea+" + "', '" + "+ ClientBranchMas.ClientBranchCity+" + "', '" + "+ ClientBranchMas.ClientBranchState+" + "' - '" + "+ ClientBranchMas.ClientBranchPIN+" + "', '" + "+ ClientBranchMas.ClientBranchCountry AS ClientBranchName " + 
                                  "FROM " + 
                                        " ClientBranchMas " + 
                                  "WHERE " + 
                                         "ClientBranchMas.ClientID = " + txtClientID + " AND ClientBranchMas.IsDeleted = False " + 
                                  " ORDER BY " + 
                                         "  ClientBranchMas.OrderID;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                companyList = JsonConvert.DeserializeObject<List<ClientBranchInfo>>(DataTableToJSon);
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

        public List<ClientBranchInfo> getAllCompanyList(int txtClientID)
        {
            List<ClientBranchInfo> companyList = new List<ClientBranchInfo>();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT ClientBranchMas.ClientBranchID, ClientBranchMas.ClientBranchCode, ClientBranchMas.ClientBranchName, ClientBranchMas.ClientBranchAddress1, ClientBranchMas.ClientBranchAddress2, ClientBranchMas.ClientBranchArea, ClientBranchMas.ClientBranchCity, ClientBranchMas.ClientBranchState, ClientBranchMas.ClientBranchPIN, ClientBranchMas.ClientBranchCountry, ClientBranchMas.ClientBranchPhone, ClientBranchMas.ClientBranchMailID, ClientBranchMas.ClientBranchContactPerson, ClientBranchMas.ClientBranchContactNumber, ClientBranchMas.ClientBranchContactMail, ClientBranchMas.ClientBranchWebSite, ClientBranchMas.IsActive, ClientBranchMas.IsDeleted, ClientBranchMas.ClientID " +
                                          " FROM ClientBranchMas " + 
                                  " WHERE " +
                                          " (((ClientBranchMas.IsActive)=True) AND ((ClientBranchMas.IsDeleted)=False) AND ((ClientBranchMas.ClientID)=" + txtClientID + "))";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                companyList = JsonConvert.DeserializeObject<List<ClientBranchInfo>>(DataTableToJSon);
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

        public List<ClientBranchInfo> getAllCompanyList(int txtClientID, string filterText)
        {
            List<ClientBranchInfo> companyList = new List<ClientBranchInfo>();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT ClientBranchMas.ClientBranchID, ClientBranchMas.ClientBranchCode, ClientBranchMas.ClientBranchName, ClientBranchMas.ClientBranchAddress1, ClientBranchMas.ClientBranchAddress2, ClientBranchMas.ClientBranchArea, ClientBranchMas.ClientBranchCity, ClientBranchMas.ClientBranchState, ClientBranchMas.ClientBranchPIN, ClientBranchMas.ClientBranchCountry, ClientBranchMas.ClientBranchPhone, ClientBranchMas.ClientBranchMailID, ClientBranchMas.ClientBranchContactPerson, ClientBranchMas.ClientBranchContactNumber, ClientBranchMas.ClientBranchContactMail, ClientBranchMas.ClientBranchWebSite, ClientBranchMas.IsActive, ClientBranchMas.IsDeleted, ClientBranchMas.ClientID " +
                                        " FROM ClientBranchMas " +
                                  " WHERE ClientBranchMas.IsActive = true AND ClientBranchMas.IsDeleted = false AND ClientBranchMas.ClientID = " + txtClientID + " AND ClientBranchMas.ClientBranchName LIKE '" + filterText + "%'";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                companyList = JsonConvert.DeserializeObject<List<ClientBranchInfo>>(DataTableToJSon);
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

        public List<ClientBranchInfo> getClientBranchInfo(int txtClientBranchID)
        {
            List<ClientBranchInfo> objClientBranchInfoList = new List<ClientBranchInfo>();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT * FROM ClientBranchMas WHERE ClientBranchID = " + txtClientBranchID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objClientBranchInfoList = JsonConvert.DeserializeObject<List<ClientBranchInfo>>(DataTableToJSon);
                if(objClientBranchInfoList.Count > 0)
                {
                    //AppVariables.CompanyID = objClientBranchInfoList[0].ClientID;
                    //AppVariables.CompanyID = objClientBranchInfoList[0].ClientID;
                    //AppVariables.CompanyCode = objClientBranchInfoList[0].ClientBranchCode.ToString();
                    //AppVariables.CompanyName = objClientBranchInfoList[0].ClientBranchName.ToString();
                    //AppVariables.CompanyAddress = objClientBranchInfoList[0].ClientBranchAddress1.ToString() + "," + objClientBranchInfoList[0].ClientBranchAddress2.ToString() + "," + objClientBranchInfoList[0].ClientBranchArea.ToString() + "," + objClientBranchInfoList[0].ClientBranchCity.ToString() + "," + objClientBranchInfoList[0].ClientBranchState.ToString() + "," + objClientBranchInfoList[0].ClientBranchCountry.ToString();
                    //AppVariables.CompanyPhone = objClientBranchInfoList[0].ClientBranchPhone.ToString();
                    //AppVariables.CompanyEmail = objClientBranchInfoList[0].ClientBranchMailID.ToString();
                    //AppVariables.CompanyContactPerson = objClientBranchInfoList[0].ClientBranchContactPerson.ToString();
                    //AppVariables.ClientContactNumber = objClientBranchInfoList[0].ClientBranchContactNumber.ToString();
                    //AppVariables.ClientContactMail = objClientBranchInfoList[0].ClientBranchContactMail.ToString();
                    //AppVariables.ClientWebSite = objClientBranchInfoList[0].ClientBranchWebSite.ToString();
                    //AppVariables.IsActive = Convert.ToBoolean(objClientBranchInfoList[0].IsActive.ToString()); 
                    //AppVariables.IsDeleted = Convert.ToBoolean(objClientBranchInfoList[0].IsDeleted.ToString());
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

            return objClientBranchInfoList;
        }

        public List<ClientBranchInfo> getClientBranchInfoByEmpID(int txtEmpID)
        {
            List<ClientBranchInfo> objClientBranchInfoList = new List<ClientBranchInfo>();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT " +
                                    "EmpMas.EmpID, " +
                                    "ClientBranchMas.ClientID, " +
                                    "ClientBranchMas.ClientCode, " +
                                    "ClientBranchMas.ClientName, " +
                                    "ClientBranchMas.ClientAddress1, " +
                                    "ClientBranchMas.ClientAddress2, " +
                                    "ClientBranchMas.ClientArea, " +
                                    "ClientBranchMas.ClientCity, " +
                                    "ClientBranchMas.ClientState, " +
                                    "ClientBranchMas.ClientPIN, " +
                                    "ClientBranchMas.ClientCountry, " +
                                    "ClientBranchMas.ClientPhone, " +
                                    "ClientBranchMas.ClientMailID, " +
                                    "ClientBranchMas.ClientContactPerson, " +
                                    "ClientBranchMas.ClientContactNumber, " +
                                    "ClientBranchMas.ClientContactMail, " +
                                    "ClientBranchMas.ClientWebSite, " +
                                    "ClientBranchMas.ClientLogo, " +
                                    "ClientBranchMas.IsActive, " +
                                    "ClientBranchMas.IsDeleted " +
                                "FROM " +
                                    "ClientBranchMas " +
                                    "INNER JOIN EmpMas ON ClientBranchMas.ClientID = EmpMas.ClientID " +
                                "WHERE " +
                                    "(" +
                                        "((EmpMas.EmpID) = " + txtEmpID + ") " +
                                        "AND ((ClientBranchMas.IsActive) = True) " +
                                        "AND ((ClientBranchMas.IsDeleted) = False) " +
                                    ");";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objClientBranchInfoList = JsonConvert.DeserializeObject<List<ClientBranchInfo>>(DataTableToJSon);
                if (objClientBranchInfoList.Count > 0)
                {
                    //AppVariables.CompanyID = objClientBranchInfoList[0].ClientID;
                    //AppVariables.CompanyCode = objClientBranchInfoList[0].ClientCode.ToString();
                    //AppVariables.CompanyName = objClientBranchInfoList[0].ClientName.ToString();
                    //AppVariables.CompanyAddress = objClientBranchInfoList[0].ClientAddress1.ToString() + "," + objClientBranchInfoList[0].ClientAddress1.ToString() + "," + objClientBranchInfoList[0].ClientArea.ToString() + "," + objClientBranchInfoList[0].ClientCity.ToString() + "," + objClientBranchInfoList[0].ClientState.ToString() + "," + objClientBranchInfoList[0].ClientCountry.ToString();
                    //AppVariables.CompanyPhone = objClientBranchInfoList[0].ClientPhone.ToString();
                    //AppVariables.CompanyEmail = objClientBranchInfoList[0].ClientMailID.ToString();
                    //AppVariables.CompanyContactPerson = objClientBranchInfoList[0].ClientContactPerson.ToString();
                    //AppVariables.ClientContactNumber = objClientBranchInfoList[0].ClientContactNumber.ToString();
                    //AppVariables.ClientContactMail = objClientBranchInfoList[0].ClientContactMail.ToString();
                    //AppVariables.ClientWebSite = objClientBranchInfoList[0].ClientWebSite.ToString();
                    //AppVariables.IsActive = Convert.ToBoolean(objClientBranchInfoList[0].IsActive.ToString());
                    //AppVariables.IsDeleted = Convert.ToBoolean(objClientBranchInfoList[0].IsDeleted.ToString());
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

            return objClientBranchInfoList;
        }

        public int InsertClientBranchInfo(string txtClientBranchCode, string txtClientBranchName, string txtClientBranchAddress1, string txtClientBranchAddress2, string txtClientBranchArea, string txtClientBranchCity, string txtClientBranchState, string txtClientBranchPIN, string txtClientBranchCountry, string txtClientBranchPhone, string txtClientBranchMailID, string txtClientBranchContactPerson, string txtClientBranchContactNumber, string txtClientBranchContactMail, string txtClientBranchWebSite, bool IsActive, bool IsDeleted, int txtClientID)
        {
            int affectedRows = 0;

            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("ClientBranchMas", "ClientBranchID");
                Response<int> OrderID = objGenFunc.getMaxRowCount("ClientBranchMas", "OrderID");

                conn = dbStaffSync.openDBConnection();

                string strQuery = "INSERT INTO ClientBranchMas (ClientBranchID, ClientBranchCode, ClientBranchName, ClientBranchAddress1, ClientBranchAddress2, ClientBranchArea, ClientBranchCity, ClientBranchState, ClientBranchPIN, ClientBranchCountry, ClientBranchPhone, ClientBranchMailID, ClientBranchContactPerson, ClientBranchContactNumber, ClientBranchContactMail, ClientBranchWebSite,  IsActive, IsDeleted, ClientID, OrderID) VALUES " +
                "(" + maxRowCount.Data + ",'" + "BRN-" + (objGenFunc.getMaxRowCount("ClientBranchMas", "ClientBranchCode", txtClientID).Data).ToString().PadLeft(4, '0').Trim() + "','" + txtClientBranchName.Trim() + "','" + txtClientBranchAddress1.Trim() + "','" + txtClientBranchAddress2.Trim() + "','" + txtClientBranchArea.Trim() + "','" + txtClientBranchCity.Trim() + "','" + txtClientBranchState.Trim() + "','" + txtClientBranchPIN + "','" + txtClientBranchCountry.Trim() + "','" + txtClientBranchPhone.Trim() + "','" + txtClientBranchMailID.Trim() + "','" + txtClientBranchContactPerson.Trim() + "','" + txtClientBranchContactNumber.Trim() + "','" + txtClientBranchContactMail.Trim() + "','" + txtClientBranchWebSite.Trim() + "'," + IsActive + "," + IsDeleted + "," + txtClientID + "," + OrderID.Data + ")";

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

        public int UpdateClientBranchInfo(int txtClientBranchID, string txtClientBranchCode, string txtClientBranchName, string txtClientBranchAddress1, string txtClientBranchAddress2, string txtClientBranchArea, string txtClientBranchCity, string txtClientBranchState, string txtClientBranchPIN, string txtClientBranchCountry, string txtClientBranchPhone, string txtClientBranchMailID, string txtClientBranchContactPerson, string txtClientBranchContactNumber, string txtClientBranchContactMail, string txtClientBranchWebSite, bool IsActive, bool IsDeleted, int txtClientID)
        {
            int affectedRows = 0;

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "UPDATE ClientBranchMas SET ClientBranchCode = '" + txtClientBranchCode + "', ClientBranchName = '" + txtClientBranchName + "', ClientBranchAddress1 = '" + txtClientBranchAddress1 + "', ClientBranchAddress2 = '" + txtClientBranchAddress2 + "', ClientBranchArea = '" + txtClientBranchArea + "', ClientBranchCity = '" + txtClientBranchCity + "', ClientBranchState = '" + 
                txtClientBranchState + "', ClientBranchPIN = '" + txtClientBranchPIN + "', ClientBranchCountry = '" + txtClientBranchCountry + "', ClientBranchPhone = '" + txtClientBranchPhone + "', ClientBranchMailID = '" + txtClientBranchContactMail + "', ClientBranchContactPerson = '" + txtClientBranchContactPerson + "', ClientBranchContactNumber = '" + 
                txtClientBranchContactNumber + "', ClientBranchContactMail = '" + txtClientBranchContactMail + "', ClientBranchWebSite = '" + txtClientBranchWebSite + "', IsActive = " + IsActive + ", IsDeleted = " + IsDeleted + ", ClientID = " + txtClientID +
                " WHERE ClientBranchID = " + txtClientBranchID;

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

        public int DeleteClientBranchInfo(int txtClientID)
        {
            int affectedRows = 0;

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "UPDATE ClientBranchMas SET IsDeleted = true WHERE ClientBranchID = " + txtClientID;

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
