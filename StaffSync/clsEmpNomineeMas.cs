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
    public class clsEmpNomineeMas
    {
        myDBClass objDBClass = new myDBClass();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsStates objState = new clsStates();
        clsCountries objCountry = new clsCountries();

        public clsEmpNomineeMas() { 

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

        public NomineeInfo GetNomineeInfo(int EmployeeID)
        {
            NomineeInfo empNomineeInfo = new NomineeInfo();

            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "SELECT * FROM NomineeMas WHERE EmpID = " + EmployeeID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                List<NomineeInfo> objEmpNomineeInfo = JsonConvert.DeserializeObject<List<NomineeInfo>>(DataTableToJSon);
                if (objEmpNomineeInfo.Count > 0)
                {
                    empNomineeInfo.NomineeID = objEmpNomineeInfo[0].NomineeID;
                    empNomineeInfo.NomineePerson = objEmpNomineeInfo[0].NomineePerson;
                    empNomineeInfo.EmpID = objEmpNomineeInfo[0].EmpID;
                    empNomineeInfo.RelationshipID = objEmpNomineeInfo[0].RelationshipID;
                    empNomineeInfo.ContactNumber = objEmpNomineeInfo[0].ContactNumber;
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

            return empNomineeInfo;
        }


        public int InsertNomineeIfo(string txtNomineePerson, int EmpID, int txtNomineeRelationship, string txtContactNumber)
        {
            int affectedRows = 0;
            try
            {

                int maxRowCount = getMaxRowCount("NomineeMas", "NomineeID");

                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO NomineeMas (NomineeID, NomineePerson, EmpID, RelationShipID, ContactNumber) VALUES " +
                 "(" + maxRowCount + ",'" + txtNomineePerson + "'," + EmpID + "," + txtNomineeRelationship + ",'" + txtContactNumber + "')";

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

        public int UdpateNomineeInfo(int txtNomineeID, string txtNomineePerson, int EmpID, int txtNomineeRelationship, string txtContactNumber)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE NomineeMas SET " +
                "NomineePerson = '" + txtNomineePerson + "', RelationShipID = " + txtNomineeRelationship + ", ContactNumber = '" + txtContactNumber + "'" +
                " WHERE NomineeID = " + txtNomineeID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtNomineeID;
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

    public class NomineeInfo
    {
        public int NomineeID { get; set; }
        public string NomineePerson { get; set; }
        public int EmpID { get; set; }
        public int RelationshipID { get; set; }
        public string ContactNumber { get; set; }
    }
}
