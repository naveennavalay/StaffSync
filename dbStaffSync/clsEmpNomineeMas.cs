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
    public class clsEmpNomineeMas
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset = null;
        clsGenFunc objGenFunc = new clsGenFunc();

        public clsEmpNomineeMas() { 

        }

        public NomineeInfo GetNomineeInfo(int EmployeeID)
        {
            NomineeInfo empNomineeInfo = new NomineeInfo();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

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
                //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = dbStaffSync.closeDBConnection();
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }

            return empNomineeInfo;
        }


        public int InsertNomineeIfo(string txtNomineePerson, int EmpID, int txtNomineeRelationship, string txtContactNumber)
        {
            int affectedRows = 0;
            try
            {

                Response<int> maxRowCount = objGenFunc.getMaxRowCount("NomineeMas", "NomineeID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO NomineeMas (NomineeID, NomineePerson, EmpID, RelationShipID, ContactNumber) VALUES " +
                 "(" + maxRowCount.Data + ",'" + txtNomineePerson + "'," + EmpID + "," + txtNomineeRelationship + ",'" + txtContactNumber + "')";

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

        public int UdpateNomineeInfo(int txtNomineeID, string txtNomineePerson, int EmpID, int txtNomineeRelationship, string txtContactNumber)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
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
