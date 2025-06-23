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
    public class clsEmpContactPersonMas
    {
        myDBClass objDBClass = new myDBClass();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsStates objState = new clsStates();
        clsCountries objCountry = new clsCountries();

        public clsEmpContactPersonMas() { 

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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return rowCount;
        }

        public int InsertContactInfo(string txtContactPerson, string txtContactPersonAddress, int txtContactPersonRelationship, int txtContactPersonSexID)
        {
            int affectedRows = 0;
            try
            {

                int maxRowCount = getMaxRowCount("ContactPersonMas", "ContactID");
                
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO ContactPersonMas (ContactID, ContactPerson, ContactPersonAddressInfo, RelationShipID, SexID) VALUES " +
                 "(" + maxRowCount + ",'" + txtContactPerson + "','" + txtContactPersonAddress + "'," + txtContactPersonRelationship + "," + txtContactPersonSexID + ")";

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

        public int UdpateContactInfo(int ContactID, string txtContactPerson, string txtContactPersonAddress, int txtContactPersonRelationship, int txtContactPersonSexID)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return affectedRows;
        }

        public ContactInfo GetContactInfo(int ContactID)
        {
            ContactInfo contactInfo = new ContactInfo();

            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return contactInfo;
        }
    }

    public class ContactInfo
    {
        public int ContactID { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonAddressInfo { get; set; }
        public int RelationShipID { get; set; }
        public int SexID { get; set; }
    }
}
