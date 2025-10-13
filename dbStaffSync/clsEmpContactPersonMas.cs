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
    public class clsEmpContactPersonMas
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();
        clsStates objState = new clsStates();
        clsCountries objCountry = new clsCountries();

        public clsEmpContactPersonMas() { 

        }

        public int InsertContactInfo(string txtContactPerson, string txtContactPersonAddress, int txtContactPersonRelationship, int txtContactPersonSexID)
        {
            int affectedRows = 0;
            try
            {

                Response<int> maxRowCount = objGenFunc.getMaxRowCount("ContactPersonMas", "ContactID");
                
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO ContactPersonMas (ContactID, ContactPerson, ContactPersonAddressInfo, RelationShipID, SexID) VALUES " +
                 "(" + maxRowCount.Data + ",'" + txtContactPerson + "','" + txtContactPersonAddress + "'," + txtContactPersonRelationship + "," + txtContactPersonSexID + ")";

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

        public int UdpateContactInfo(int ContactID, string txtContactPerson, string txtContactPersonAddress, int txtContactPersonRelationship, int txtContactPersonSexID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE ContactPersonMas SET " +
                "ContactPerson = '" + txtContactPerson + "', ContactPersonAddressInfo = '" + txtContactPersonAddress + "', RelationShipID = " + txtContactPersonRelationship + ", SexID = " + txtContactPersonSexID +
                " WHERE ContactID = " + ContactID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = ContactID;
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

        public ContactInfo GetContactInfo(int ContactID)
        {
            ContactInfo contactInfo = new ContactInfo();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT * FROM ContactPersonMas WHERE ContactID = " + ContactID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                List<ContactInfo> objContactInfo = JsonConvert.DeserializeObject<List<ContactInfo>>(DataTableToJSon);
                if (objContactInfo.Count > 0)
                {
                    contactInfo.ContactID = objContactInfo[0].ContactID;
                    contactInfo.ContactPerson = objContactInfo[0].ContactPerson;
                    contactInfo.ContactPersonAddressInfo = objContactInfo[0].ContactPersonAddressInfo;
                    contactInfo.RelationShipID = objContactInfo[0].RelationShipID;
                    contactInfo.SexID = objContactInfo[0].SexID;
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

            return contactInfo;
        }
    }
}
