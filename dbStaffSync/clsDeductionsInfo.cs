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
    public class clsDeductionsInfo
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public DataTable GetDeductionList()
        {
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT * FROM DeductionHeaderMas WHERE IsActive = true AND IsDeleted = false";

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

        public DataTable GetDeductionList(string filterText)
        {
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT * FROM DeductionHeaderMas WHERE IsDeleted = false AND DedTitle LIKE '" + filterText + "%'";

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

        public string GetDeductionTitleByID(int DeductionID)
        {
            string selectedDeductionTitle = "";
            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT DedTitle FROM DeductionHeaderMas WHERE DedID = " + DeductionID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                selectedDeductionTitle = (string)cmd.ExecuteScalar();
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

            return selectedDeductionTitle;
        }

        public int GetDeductionTitleByTitle(string DeductionTitle)
        {
            int selectedDeductionID = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT DedID FROM DeductionHeaderMas WHERE DedTitle = '" + DeductionTitle + "'";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                object a = cmd.ExecuteScalar();
                if (a != null)
                    selectedDeductionID = (int)a;
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

            return selectedDeductionID;
        }

        public DeductionModel getSelectedDeductionInfo(int txtDeductionID)
        {
            List<DeductionModel> objDeductionModelInfo = new List<DeductionModel>();

            try
            {
                DataTable dt = new DataTable();

                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " + 
                                        " DeductionHeaderMas.DedID, " + 
                                        " DeductionHeaderMas.DedCode, " + 
                                        " DeductionHeaderMas.DedTitle, " + 
                                        " DeductionHeaderMas.DedDescription, " + 
                                        " DeductionHeaderMas.IsActive, " + 
                                        " DeductionHeaderMas.IsDeleted, " + 
                                        " DeductionHeaderMas.OrderID, " + 
                                        " DeductionHeaderMas.CalcFormula, " + 
                                        " DeductionHeaderMas.IsFixed, " + 
                                        " DeductionHeaderMas.MaxCap, " + 
                                        " DeductionHeaderMas.VisibleInPayslip, " + 
                                        " DeductionHeaderMas.ProrataBasis " + 
                                    " FROM " + 
                                        " DeductionHeaderMas " + 
                                    " WHERE " + 
                                        " DeductionHeaderMas.DedID = " + txtDeductionID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objDeductionModelInfo = JsonConvert.DeserializeObject<List<DeductionModel>>(DataTableToJSon);
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

            if (objDeductionModelInfo.Count > 0)
                return objDeductionModelInfo[0];
            else
                return new DeductionModel();
        }

        public decimal getDeductionMaxCap(int txtDeductionID)
        {
            decimal selectedDeductionMaxCap = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT MaxCap FROM DeductionHeaderMas WHERE DedID = " + txtDeductionID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                selectedDeductionMaxCap = (decimal)cmd.ExecuteScalar();
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

            return selectedDeductionMaxCap;
        }

        public int InsertDeduction(string txtDedCode, string txtDedTitle, string txtDedDescription, bool IsFixed, bool IsActive, bool IsDeleted, decimal txtMaxCap, bool ShowInPayslip, bool ConsiderProrataBasis)
        {
            int affectedRows = 0;
            try
            {

                Response<int> maxRowCount = objGenFunc.getMaxRowCount("DeductionHeaderMas", "DedID");
                
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO DeductionHeaderMas (DedID, DedCode, DedTitle, DedDescription, IsFixed, IsActive, IsDeleted, OrderID, CalcFormula, MaxCap, VisibleInPayslip, ProrataBasis) VALUES " +
                 "(" + maxRowCount.Data + ",'" + "DDN-" + (maxRowCount.Data).ToString().PadLeft(4, '0').Trim() + "','" + txtDedTitle.Trim() + "','" + txtDedDescription.Trim() + "'," + IsFixed  + "," + IsActive + "," + IsDeleted + "," + maxRowCount.Data + ",'', " + txtMaxCap + ", " + ShowInPayslip + ", " + ConsiderProrataBasis + ")";

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

        public int UpdateDeduction(int txtDedID, string txtDedCode, string txtDedTitle, string txtDedDescription, bool IsFixed, bool IsActive, bool IsDeleted, decimal txtMaxCap, bool ShowInPayslip, bool ConsiderProrataBasis)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE DeductionHeaderMas SET " +
                 "DedCode = '" + txtDedCode.Trim() + "', DedTitle = '" + txtDedTitle.Trim() + "', DedDescription = '" + txtDedDescription.Trim() + "', IsFixed = " + IsFixed + ", IsActive = " + IsActive + ", MaxCap = " + txtMaxCap + ", VisibleInPayslip = " + ShowInPayslip + ", ProrataBasis = " + ConsiderProrataBasis+
                 " WHERE DedID = " + txtDedID.ToString().Trim();

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

        public int DeleteDeduction(int txtDedID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE * FROM DeductionHeaderMas WHERE DedID = " + txtDedID.ToString().Trim();

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
