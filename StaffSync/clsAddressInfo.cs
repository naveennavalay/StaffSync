using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaffSync
{
    public class clsAddressInfo
    {
        myDBClass objDBClass = new myDBClass();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsStates objState = new clsStates();
        clsCountries objCountry = new clsCountries();

        public clsAddressInfo() { 

        }

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

        public DataTable GetAddressList(string ColumnName)
        {
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "SELECT DISTINCT " + ColumnName + " FROM AddressMas";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

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

            return dt;
        }

        public int InsertAddressInfo(string txtAddress1, string txtAddress2, string txtArea, string txtCity, string txtPIN, string txtState, string txtCountry)
        {
            int affectedRows = 0;
            try
            {
                int maxRowCount = getMaxRowCount("AddressMas", "AddressID");

                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO AddressMas (AddressID, Address1, Address2, Area, City, PIN, StateID, CountryID) VALUES " +
                 "(" + maxRowCount + ",'" + txtAddress1 + "','" + txtAddress2 + "','" + txtArea + "','" + txtCity + "','"+ txtPIN + "',"+ objState.GetStateByTitle(txtState) + "," + objCountry.GetCountryByTitle(txtCountry) + ")";

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

        public int UpdateAddressInfo(int txtAddressInfoID, string txtAddress1, string txtAddress2, string txtArea, string txtCity, string txtPIN, string txtState, string txtCountry)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE AddressMas SET " +
                "Address1 = '" + txtAddress1 + "', Address2 = '" + txtAddress2 + "', Area = '" + txtArea + "', City = '" + txtCity + "', PIN = '" + txtPIN + "', StateID = " + objState.GetStateByTitle(txtState) + ", CountryID = " + objCountry.GetCountryByTitle(txtCountry) + 
                " WHERE AddressID = " + txtAddressInfoID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtAddressInfoID;
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

        public AddressInfo GetAddressInfo(int AddressID)
        {
            AddressInfo addressInfo = new AddressInfo();

            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT * FROM qryAddressInfo WHERE AddressID = " + AddressID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                List<AddressInfo> objAddressInfo = JsonConvert.DeserializeObject<List<AddressInfo>>(DataTableToJSon);
                if (objAddressInfo.Count > 0)
                {
                    addressInfo.AddressID = objAddressInfo[0].AddressID;
                    addressInfo.Address1 = objAddressInfo[0].Address1;
                    addressInfo.Address2 = objAddressInfo[0].Address2;
                    addressInfo.Area = objAddressInfo[0].Area;
                    addressInfo.City = objAddressInfo[0].City;
                    addressInfo.StateID = objAddressInfo[0].StateID;
                    addressInfo.State = objState.GetStateByID(objAddressInfo[0].StateID);
                    addressInfo.PIN = objAddressInfo[0].PIN;
                    addressInfo.CountryID = objAddressInfo[0].CountryID;
                    addressInfo.Country = objCountry.GetCountryByID(objAddressInfo[0].CountryID);
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

            return addressInfo;
        }
    }

    //public class AddressInfo
    //{
    //    public int AddressID { get; set; }
    //    public string Address1 { get; set; }
    //    public string Address2 { get; set; }
    //    public string Area { get; set; }
    //    public string City { get; set; }
    //    public string PIN { get; set; }
    //    public int StateID { get; set; }
    //    public string State{ get; set; }
    //    public int CountryID { get; set; }
    //    public string Country { get; set; }
    //}
}
