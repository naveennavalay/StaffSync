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
    public class clsAllowenceInfo
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

        public DataTable GetAllowenceList()
        {
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return dt;
        }

        public DataTable GetAllowenceList(string filterText)
        {
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "SELECT * FROM AllowanceHeaderMas WHERE IsDeleted = false AND AllowenceTitle LIKE '" + filterText + "%'";

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

        public string GetAllowenceTitleByID(int AllowenceID)
        {
            string selectedAllowenceTitle = "";
            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "SELECT AllowenceTitle FROM AllowanceHeaderMas WHERE AllowenceID = " + AllowenceID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                selectedAllowenceTitle = (string)cmd.ExecuteScalar();
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

            return selectedAllowenceTitle;
        }

        public int GetBloodGroupByTitle(string AllowenceTitle)
        {
            int selectedAllowenceID = 0;
            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "SELECT AllowenceID FROM AllowanceHeaderMas WHERE AllowenceTitle = '" + AllowenceTitle + "'";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                selectedAllowenceID = (int)cmd.ExecuteScalar();
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

            return selectedAllowenceID;
        }

        public int InsertAllowence(string txtAllCode, string txtAllTitle, string txtAllDescription, bool IsFixed, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            try
            {

                int maxRowCount = getMaxRowCount("AllowanceHeaderMas", "AllID");
                
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO AllowanceHeaderMas (AllID, AllCode, AllTitle, AllDescription, IsFixed, IsActive, IsDeleted, OrderID, CalcFormula) VALUES " +
                 "(" + maxRowCount + ",'" + "ALL-" + (maxRowCount).ToString().PadLeft(4, '0').Trim() + "','" + txtAllTitle.Trim() + "','" + txtAllDescription.Trim() + "'," + IsFixed  + "," + IsActive + "," + IsDeleted + "," + maxRowCount + ",'')";

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

        public int UpdateAllowence(int txtAllID, string txtAllCode, string txtAllTitle, string txtAllDescription, bool IsFixed, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return affectedRows;
        }

        public int DeleteAllowence(int txtAllID)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE * FROM AllowanceHeaderMas WHERE AllID = " + txtAllID.ToString().Trim();

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

    public class AllowenceModel
    {
        public int AllID { get; set; }

        [DisplayName("Allowance Code")] 
        public string AllCode { get; set; }

        [DisplayName("Allowance Title")]
        public string AllTitle { get; set; }

        [DisplayName("Allowance Description")] 
        public string AllDescription { get; set; }

        public bool IsFixed { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int OrderID { get; set; }

        [DisplayName("Calculation Formula")]
        public string CalcFormula { get; set; }
    }
}
