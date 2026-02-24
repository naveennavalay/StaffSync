using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

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

        public AllowenceModel getSelectedAllowenceInfo(int txtAllowenceID)
        {
            List<AllowenceModel> objAllowenceModelInfo = new List<AllowenceModel>();

            try
            {
                DataTable dt = new DataTable();

                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " + 
                                        " AllowanceHeaderMas.AllID, " + 
                                        " AllowanceHeaderMas.AllCode, " + 
                                        " AllowanceHeaderMas.AllTitle, " + 
                                        " AllowanceHeaderMas.AllDescription, " + 
                                        " AllowanceHeaderMas.IsActive, " + 
                                        " AllowanceHeaderMas.IsDeleted, " + 
                                        " AllowanceHeaderMas.OrderID, " + 
                                        " AllowanceHeaderMas.CalcFormula, " + 
                                        " AllowanceHeaderMas.IsFixed, " + 
                                        " AllowanceHeaderMas.MaxCap, " + 
                                        " AllowanceHeaderMas.VisibleInPayslip, " + 
                                        " AllowanceHeaderMas.ProrataBasis " + 
                                    " FROM " + 
                                        " AllowanceHeaderMas " + 
                                    " WHERE " + 
                                        " AllowanceHeaderMas.AllID = " + txtAllowenceID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAllowenceModelInfo = JsonConvert.DeserializeObject<List<AllowenceModel>>(DataTableToJSon);
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

            if (objAllowenceModelInfo.Count > 0)
                return objAllowenceModelInfo[0];
            else
                return new AllowenceModel();
        }

        public int InsertAllowence(string txtAllCode, string txtAllTitle, string txtAllDescription, bool IsFixed, bool IsActive, bool IsDeleted, decimal txtMaxCap, bool ShowInPayslip, bool ConsiderProrataBasis)
        {
            int affectedRows = 0;
            try
            {

                Response<int> maxRowCount = objGenFunc.getMaxRowCount("AllowanceHeaderMas", "AllID");
                
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO AllowanceHeaderMas (AllID, AllCode, AllTitle, AllDescription, IsFixed, IsActive, IsDeleted, OrderID, CalcFormula, MaxCap, VisibleInPayslip, ProrataBasis) VALUES " +
                 "(" + maxRowCount.Data + ",'" + "ALL-" + (maxRowCount.Data).ToString().PadLeft(4, '0').Trim() + "','" + txtAllTitle.Trim() + "','" + txtAllDescription.Trim() + "'," + IsFixed  + "," + IsActive + "," + IsDeleted + "," + maxRowCount.Data + ",''," + txtMaxCap + ", " + ShowInPayslip + ", " + ConsiderProrataBasis + ")";

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

        public int UpdateAllowence(int txtAllID, string txtAllCode, string txtAllTitle, string txtAllDescription, bool IsFixed, bool IsActive, bool IsDeleted, decimal txtMaxCap, bool ShowInPayslip, bool ConsiderProrataBasis)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE AllowanceHeaderMas SET " +
                 "AllCode = '" + txtAllCode.Trim() + "', AllTitle = '" + txtAllTitle.Trim() + "', AllDescription = '" + txtAllDescription.Trim() + "', IsFixed = " + IsFixed + ", IsActive = " + IsActive + ", MaxCap = " + txtMaxCap  + ", VisibleInPayslip = " + ShowInPayslip + ", ProrataBasis = " + ConsiderProrataBasis +  
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
