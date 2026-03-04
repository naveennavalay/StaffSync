using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace dbStaffSync
{
    public class clsClientSignatoryInfo
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public List<ClientSigningPerson> getClientSpecificSignatoryInfo(int txtClientID)
        {
            List<ClientSigningPerson> objClientSigningPerson = new List<ClientSigningPerson>();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT " + 
                                        " ClientSigningPerson.ClientSigningPersonID, " + 
                                        " ClientSigningPerson.ClientID, " + 
                                        " ClientSigningPerson.ClientSigningPersonName, " + 
                                        " ClientSigningPerson.ClientSigningPersonDesignation, " + 
                                        " ClientSigningPerson.ClientSigningPersonFatherName, " + 
                                        " ClientSigningPerson.ClientSigningPersonPANNumber, " + 
                                        " ClientSigningPerson.ClientSigningPersonSex, " + 
                                        " ClientSigningPerson.ClientSigningPersonDOB, " + 
                                        " ClientSigningPerson.IsActive, " + 
                                        " ClientSigningPerson.IsDeleted " + 
                                    " FROM " + 
                                        " ClientSigningPerson " + 
                                    " WHERE " + 
                                            " ClientSigningPerson.ClientID = " + txtClientID +
                                            " AND ClientSigningPerson.IsActive = True " + 
                                            " AND ClientSigningPerson.IsDeleted = False";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objClientSigningPerson = JsonConvert.DeserializeObject<List<ClientSigningPerson>>(DataTableToJSon);
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
            if(objClientSigningPerson == null)
                objClientSigningPerson = new List<ClientSigningPerson>();

            return objClientSigningPerson;
        }


        public int InsertSignatoryInfo(int txtClientID, string txtClientSigningPersonName, string ClientSigningPersonDesignation, string ClientSigningPersonFatherName, string ClientSigningPersonPANNumber, string ClientSigningPersonSex, DateTime ClientSigningPersonDOB, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("ClientSigningPerson", "ClientSigningPersonID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO ClientSigningPerson (ClientSigningPersonID, ClientID, ClientSigningPersonName, ClientSigningPersonDesignation, ClientSigningPersonFatherName, ClientSigningPersonPANNumber, ClientSigningPersonSex, ClientSigningPersonDOB, IsActive, IsDeleted, OrderID) VALUES " +
                 "(" + maxRowCount.Data + "," + txtClientID + ", '" + txtClientSigningPersonName + "', '" + ClientSigningPersonDesignation + "', '" + ClientSigningPersonFatherName + "', '" + ClientSigningPersonPANNumber + "', '" + ClientSigningPersonSex + "','" + ClientSigningPersonDOB.ToString("dd-MMM-yyyy") + "', " + IsActive + ", " + IsDeleted + ", " + maxRowCount.Data + ")";

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

        public int UpdateSignatoryInfo(int txtClientSigningPersonID, int txtClientID, string txtClientSigningPersonName, string ClientSigningPersonDesignation, string ClientSigningPersonFatherName, string ClientSigningPersonPANNumber, string ClientSigningPersonSex, DateTime ClientSigningPersonDOB, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("ClientSigningPerson", "ClientSigningPersonID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE ClientSigningPerson SET " + 
                                          " ClientSigningPersonID = " + txtClientSigningPersonID + ", " + 
                                          " ClientID = " + txtClientID + ", " + 
                                          " ClientSigningPersonName = '" + txtClientSigningPersonName + "', " +
                                          " ClientSigningPersonDesignation = '" + ClientSigningPersonDesignation + "', " +
                                          " ClientSigningPersonFatherName = '" + ClientSigningPersonFatherName + "', " +
                                          " ClientSigningPersonPANNumber = '" + ClientSigningPersonPANNumber + "', " +
                                          " ClientSigningPersonSex = '" + ClientSigningPersonSex + "', " +
                                          " ClientSigningPersonDOB = #" + ClientSigningPersonDOB.ToString("dd-MMM-yyyy") + "#, " +
                                          " IsActive = " + IsActive + ", " +
                                          " IsDeleted = " + IsDeleted +
                                  " WHERE " +
                                            " ClientSigningPersonID = " + txtClientSigningPersonID;

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
    }
}
