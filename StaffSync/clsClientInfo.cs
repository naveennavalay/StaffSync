using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.MonthCalendar;

namespace StaffSync
{
    public class clsClientInfo
    {
        myDBClass objDBClass = new myDBClass();
        OleDbConnection conn = null;
        DataSet dtDataset;

        public int getMaxRowCount(string tableName, string ColumnName)
        {
            int rowCount = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT MAX(" + ColumnName.ToString().Trim() + ") FROM " + tableName;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                int maxRow = (Int32)cmd.ExecuteScalar();
                if (maxRow == 0)
                    rowCount = 1;
                else if (maxRow > 0)
                    rowCount = maxRow + 1;

            }
            catch (Exception ex)
            {
                if (ex.Message.ToString().ToLower() == "Specified cast is not valid.".ToLower())
                {
                    rowCount = 1;
                }
                else
                {
                    MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return rowCount;
        }

        public List<ClientInfo> getAllCompanyList()
        {
            List<ClientInfo> companyList = new List<ClientInfo>();

            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();
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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return companyList;
        }

        public List<ClientInfo> getAllCompanyList(string filterText)
        {
            List<ClientInfo> companyList = new List<ClientInfo>();

            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();
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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return companyList;
        }

        public List<ClientInfo> getClientInfo(int txtClientID)
        {
            List<ClientInfo> objClientInfoList = new List<ClientInfo>();

            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();
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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return objClientInfoList;
        }

        public int InsertClientInfo(string txtClientCode, string txtClientName, string txtClientAddress1, string txtClientAddress2, string txtClientArea, string txtClientCity, string txtClientState, string txtClientPIN, string txtClientCountry, string txtClientPhone, string txtClientMailID, string txtClientContactPerson, string txtClientContactNumber, string txtClientContactMail, string txtClientWebSite, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;

            try
            {
                int maxRowCount = getMaxRowCount("ClientMas", "ClientID");

                conn = objDBClass.openDBConnection();

                string strQuery = "INSERT INTO ClientMas (ClientID, ClientCode, ClientName, ClientAddress1, ClientAddress2, ClientArea, ClientCity, ClientState, ClientPIN, ClientCountry, ClientPhone, ClientMailID, ClientContactPerson, ClientContactNumber, ClientContactMail, ClientWebSite, IsActive, IsDeleted) VALUES " +
                "(" + maxRowCount + ",'" + "CNT-" + (maxRowCount).ToString().PadLeft(4, '0').Trim() + "','" + txtClientName.Trim() + "','" + txtClientAddress1.Trim() + "','" + txtClientAddress2.Trim() + "','" + txtClientArea.Trim() + "','" + txtClientCity.Trim() + "','" + txtClientState.Trim() + "','" + txtClientPIN + "','" + txtClientCountry.Trim() + "','" + txtClientPhone.Trim() + "','" + txtClientMailID.Trim() + "','" + txtClientContactPerson.Trim() + "','" + txtClientContactNumber.Trim() + "','" + txtClientContactMail.Trim() + "','" + txtClientWebSite.Trim() + "'," + IsActive + "," + IsDeleted + ")";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = maxRowCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }
            return affectedRows;
        }

        public int UpdateClientInfo(int txtClientID, string txtClientCode, string txtClientName, string txtClientAddress1, string txtClientAddress2, string txtClientArea, string txtClientCity, string txtClientState, string txtClientPIN, string txtClientCountry, string txtClientPhone, string txtClientMailID, string txtClientContactPerson, string txtClientContactNumber, string txtClientContactMail, string txtClientWebSite, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;

            try
            {
                conn = objDBClass.openDBConnection();

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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }
            return affectedRows;
        }

        public int DeleteClientInfo(int txtClientID)
        {
            int affectedRows = 0;

            try
            {
                conn = objDBClass.openDBConnection();

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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }
            return affectedRows;
        }
    }

    public class ClientInfo
    {
        public int ClientID { get; set; }

        [DisplayName("Client Code")]
        public string ClientCode { get; set; }

        [DisplayName("Client Name")]
        public string ClientName { get; set; }

        [DisplayName("Address1")]
        public string ClientAddress1 { get; set; }

        [DisplayName("Address2")]
        public string ClientAddress2 { get; set; }

        [DisplayName("Area")]
        public string ClientArea { get; set; }

        [DisplayName("City")]
        public string ClientCity { get; set; }

        [DisplayName("State")]
        public string ClientState { get; set; }

        [DisplayName("PIN")]
        public string ClientPIN { get; set; }

        [DisplayName("Country")]
        public string ClientCountry { get; set; }

        [DisplayName("Phone")]
        public string ClientPhone { get; set; }

        [DisplayName("Mail ID")]
        public string ClientMailID { get; set; }

        [DisplayName("Contact Person")]
        public string ClientContactPerson { get; set; }

        [DisplayName("Contact Number")]
        public string ClientContactNumber { get; set; }

        [DisplayName("Contact Mail ID")]
        public string ClientContactMail { get; set; }

        [DisplayName("WebSite")]
        public string ClientWebSite { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

    }
}
