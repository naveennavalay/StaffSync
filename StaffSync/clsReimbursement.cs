using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;

namespace StaffSync
{
    public class clsReimbursement
    {
        myDBClass objDBClass = new myDBClass();
        OleDbConnection conn = null;
        DataSet dtDataset;

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

        public DataTable GetReimbursementList()
        {
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return dt;
        }

        public DataTable GetReimbursementList(string filterText)
        {
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return dt;
        }

        public string GetReimbursementTitleByID(int ReimbuctionID)
        {
            string selectedReimbuctionTitle = "";
            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "SELECT ReimbTitle FROM ReimbursementHeaderMas WHERE ReimbID = " + ReimbuctionID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                selectedReimbuctionTitle = (string)cmd.ExecuteScalar();
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

            return selectedReimbuctionTitle;
        }

        public int InsertReimbursement(string txtReimbCode, string txtReimbTitle, string txtReimbDescription, bool IsFixed, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            try
            {

                int maxRowCount = getMaxRowCount("ReimbursementHeaderMas", "ReimbID");
                
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO ReimbursementHeaderMas (ReimbID, ReimbCode, ReimbTitle, ReimbDescription, IsFixed, IsActive, IsDeleted, OrderID, CalcFormula) VALUES " +
                 "(" + maxRowCount + ",'" + "RIM-" + (maxRowCount).ToString().PadLeft(4, '0').Trim() + "','" + txtReimbTitle.Trim() + "','" + txtReimbDescription.Trim() + "'," + IsFixed + "," + IsActive + "," + IsDeleted + "," + maxRowCount + ",'')";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
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

        public int UpdateReimbursement(int txtReimbID, string txtReimbCode, string txtReimbTitle, string txtReimbDescription, bool IsFixed, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE ReimbursementHeaderMas SET " +
                 "ReimbCode = '" + txtReimbCode.Trim() + "', ReimbTitle = '" + txtReimbTitle.Trim() + "', ReimbDescription = '" + txtReimbDescription.Trim() + "', IsFixed = " + IsFixed + ", IsActive = " + IsActive +
                 " WHERE ReimbID = " + txtReimbID.ToString().Trim();

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
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

        public int DeleteReimbursement(int txtReimbID)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE * FROM ReimbursementHeaderMas WHERE ReimbID = " + txtReimbID.ToString().Trim();

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
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

    public class ReimbursementModel
    {
        public int ReimbID { get; set; }

        [DisplayName("Reimbursement Code")] 
        public string ReimbCode { get; set; }

        [DisplayName("Reimbursement Title")]
        public string ReimbTitle { get; set; }

        [DisplayName("Reimbursement Description")] 
        public string ReimbDescription { get; set; }
        public bool IsFixed { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int OrderID { get; set; }

        [DisplayName("Calculation Formula")]
        public string CalcFormula { get; set; }
    }
}
