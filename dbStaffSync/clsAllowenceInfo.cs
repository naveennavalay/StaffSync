using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace dbStaffSync
{
    public class clsAllowenceInfo
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public DataTable GetAllowenceList()
        {
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT * FROM AllowanceHeaderMas WHERE IsActive = true AND IsDeleted = false";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

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

            return dt;
        }

        public DataTable GetAllowenceList(string filterText)
        {
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT * FROM AllowanceHeaderMas WHERE IsDeleted = false AND AllTitle LIKE '" + filterText + "%'";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

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

            return dt;
        }

        public string GetAllowenceTitleByID(int AllowenceID)
        {
            string selectedAllowenceTitle = "";
            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT AllowenceTitle FROM AllowanceHeaderMas WHERE AllowenceID = " + AllowenceID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                selectedAllowenceTitle = (string)cmd.ExecuteScalar();
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

            return selectedAllowenceTitle;
        }

        public int GetAllowenceTitleByTitle(string AllowenceTitle)
        {
            int selectedAllowenceID = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT AllID FROM AllowanceHeaderMas WHERE AllTitle = '" + AllowenceTitle + "'";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                object a = cmd.ExecuteScalar();
                if (a != null)
                    selectedAllowenceID = (int)a;
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

            return selectedAllowenceID;
        }

        public int InsertAllowence(string txtAllCode, string txtAllTitle, string txtAllDescription, bool IsFixed, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            try
            {

                Response<int> maxRowCount = objGenFunc.getMaxRowCount("AllowanceHeaderMas", "AllID");
                
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO AllowanceHeaderMas (AllID, AllCode, AllTitle, AllDescription, IsFixed, IsActive, IsDeleted, OrderID, CalcFormula) VALUES " +
                 "(" + maxRowCount.Data + ",'" + "ALL-" + (maxRowCount.Data).ToString().PadLeft(4, '0').Trim() + "','" + txtAllTitle.Trim() + "','" + txtAllDescription.Trim() + "'," + IsFixed  + "," + IsActive + "," + IsDeleted + "," + maxRowCount.Data + ",'')";

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

        public int UpdateAllowence(int txtAllID, string txtAllCode, string txtAllTitle, string txtAllDescription, bool IsFixed, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE AllowanceHeaderMas SET " +
                 "AllCode = '" + txtAllCode.Trim() + "', AllTitle = '" + txtAllTitle.Trim() + "', AllDescription = '" + txtAllDescription.Trim() + "', IsFixed = " + IsFixed + ", IsActive = " + IsActive +
                 " WHERE AllID = " + txtAllID.ToString().Trim();

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

        public int DeleteAllowence(int txtAllID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE * FROM AllowanceHeaderMas WHERE AllID = " + txtAllID.ToString().Trim();

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
