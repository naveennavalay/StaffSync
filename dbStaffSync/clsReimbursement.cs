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
    public class clsReimbursement
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset = null;
        clsGenFunc objGenFunc = new clsGenFunc();

        public DataTable GetReimbursementList()
        {
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT * FROM ReimbursementHeaderMas WHERE IsActive = true AND IsDeleted = false";

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

        public DataTable GetReimbursementList(string filterText)
        {
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT * FROM ReimbursementHeaderMas WHERE IsDeleted = false AND ReimbTitle LIKE '" + filterText + "%'";

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

        public string GetReimbursementTitleByID(int ReimbuctionID)
        {
            string selectedReimbuctionTitle = "";
            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT ReimbTitle FROM ReimbursementHeaderMas WHERE ReimbID = " + ReimbuctionID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                selectedReimbuctionTitle = (string)cmd.ExecuteScalar();
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

            return selectedReimbuctionTitle;
        }

        public int GetReimbursementTitleByTitle(string ReimbursementTitle)
        {
            int selectedReimbursementID = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT ReimbID FROM ReimbursementHeaderMas WHERE ReimbTitle = '" + ReimbursementTitle + "'";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                object a = cmd.ExecuteScalar();
                if (a != null)
                    selectedReimbursementID = (int)a;
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

            return selectedReimbursementID;
        }

        public ReimbursementModel getSelectedDeductionInfo(int txtReimbursementID)
        {
            List<ReimbursementModel> objReimbursementModel = new List<ReimbursementModel>();

            try
            {
                DataTable dt = new DataTable();

                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " + 
                                        " ReimbursementHeaderMas.ReimbID, " + 
                                        " ReimbursementHeaderMas.ReimbCode, " + 
                                        " ReimbursementHeaderMas.ReimbTitle, " + 
                                        " ReimbursementHeaderMas.ReimbDescription, " + 
                                        " ReimbursementHeaderMas.IsActive, " + 
                                        " ReimbursementHeaderMas.IsDeleted, " + 
                                        " ReimbursementHeaderMas.OrderID, " + 
                                        " ReimbursementHeaderMas.CalcFormula, " + 
                                        " ReimbursementHeaderMas.IsFixed, " + 
                                        " ReimbursementHeaderMas.MaxCap, " + 
                                        " ReimbursementHeaderMas.VisibleInPayslip, " + 
                                        " ReimbursementHeaderMas.ProrataBasis " +
                                    " FROM " + 
                                        " ReimbursementHeaderMas " + 
                                    " WHERE " + 
                                        " ReimbursementHeaderMas.ReimbID = " + txtReimbursementID ;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objReimbursementModel = JsonConvert.DeserializeObject<List<ReimbursementModel>>(DataTableToJSon);
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

            if (objReimbursementModel.Count > 0)
                return objReimbursementModel[0];
            else
                return new ReimbursementModel();
        }

        public int InsertReimbursement(string txtReimbCode, string txtReimbTitle, string txtReimbDescription, bool IsFixed, bool IsActive, bool IsDeleted, decimal txtMaxCap, bool ShowInPayslip, bool ConsiderProrataBasis)
        {
            int affectedRows = 0;
            try
            {

                Response<int> maxRowCount = objGenFunc.getMaxRowCount("ReimbursementHeaderMas", "ReimbID");
                
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO ReimbursementHeaderMas (ReimbID, ReimbCode, ReimbTitle, ReimbDescription, IsFixed, IsActive, IsDeleted, OrderID, CalcFormula, MaxCap, VisibleInPayslip, ProrataBasis) VALUES " +
                 "(" + maxRowCount.Data + ",'" + "RIM-" + (maxRowCount.Data).ToString().PadLeft(4, '0').Trim() + "','" + txtReimbTitle.Trim() + "','" + txtReimbDescription.Trim() + "'," + IsFixed + "," + IsActive + "," + IsDeleted + "," + maxRowCount.Data + ",'', " + txtMaxCap + ", " + ShowInPayslip + "," + ConsiderProrataBasis + ")";

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

        public int UpdateReimbursement(int txtReimbID, string txtReimbCode, string txtReimbTitle, string txtReimbDescription, bool IsFixed, bool IsActive, bool IsDeleted, decimal txtMaxCap, bool ShowInPayslip, bool ConsiderProrataBasis)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE ReimbursementHeaderMas SET " +
                 "ReimbCode = '" + txtReimbCode.Trim() + "', ReimbTitle = '" + txtReimbTitle.Trim() + "', ReimbDescription = '" + txtReimbDescription.Trim() + "', IsFixed = " + IsFixed + ", IsActive = " + IsActive + ", MaxCap = " + txtMaxCap + ", VisibleInPayslip = " + ShowInPayslip + ", ProrataBasis = " + ConsiderProrataBasis + 
                 " WHERE ReimbID = " + txtReimbID.ToString().Trim();

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

        public int DeleteReimbursement(int txtReimbID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE * FROM ReimbursementHeaderMas WHERE ReimbID = " + txtReimbID.ToString().Trim();

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
