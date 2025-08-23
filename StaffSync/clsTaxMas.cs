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
    public class clsTaxMas
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

        public List<TaxSchemeInfo> GetTaxList()
        {

            List<TaxSchemeInfo> objTaxSchemeInfoList = new List<TaxSchemeInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "SELECT * FROM TaxSchemeInfo WHERE IsDeleted = false";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objTaxSchemeInfoList = JsonConvert.DeserializeObject<List<TaxSchemeInfo>>(DataTableToJSon);
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

            return objTaxSchemeInfoList;
        }

        public EmpTaxSchemeInfo getEmployeeSpecificTaxSchemeInfo(int txtEmpID)
        {
            List<EmpTaxSchemeInfo> objEmpTaxSchemeInfo = new List<EmpTaxSchemeInfo>();
            EmpTaxSchemeInfo objSpecificTaxSchemeInfo = new EmpTaxSchemeInfo();

            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();
                DataTable dt = new DataTable();

                string strQuery = "SELECT " + 
                                  " EmpTaxSchemeID, " +
                                  " EmpID, " +
                                  " TaxSchemeID, " +
                                  " EffectiveDate " +
                                " FROM " +
                                    " EmpTaxSchemeInfo " +
                                " WHERE " +
                                    " EmpID = " + txtEmpID + " AND EmpTaxSchemeID = (SELECT Max(EmpTaxSchemeID) AS MaxEmpTaxSchemeID " +
                                            " FROM EmpTaxSchemeInfo " + 
                                            " WHERE " + 
                                            " EmpID = " + txtEmpID +
                                    " )";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objEmpTaxSchemeInfo = JsonConvert.DeserializeObject<List<EmpTaxSchemeInfo>>(DataTableToJSon);
                if (objEmpTaxSchemeInfo.Count > 0)
                {
                    objSpecificTaxSchemeInfo = objEmpTaxSchemeInfo[0];
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

            return objSpecificTaxSchemeInfo;
        }

        public int InsertEmployeeTaxSchemeInfo(int txtEmpID, int txtTaxSchemeID, DateTime txtEffectiveDate)
        {
            int affectedRows = 0;
            try
            {

                int maxRowCount = getMaxRowCount("EmpTaxSchemeInfo", "EmpTaxSchemeID");

                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO EmpTaxSchemeInfo (EmpTaxSchemeID, EmpID, TaxSchemeID, EffectiveDate) VALUES " +
                 "(" + maxRowCount  + ", " + txtEmpID + ", " + txtTaxSchemeID + ", #" + txtEffectiveDate.ToString("dd-MMM-yyyy") + "#)";

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

        public int UpdateEmployeeTaxSchemeInfo(int txtEmpTaxSchemeID, int txtEmpID, int txtTaxSchemeID, DateTime txtEffectiveDate)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpTaxSchemeInfo SET " +
                " TaxSchemeID = " + txtTaxSchemeID + ", EffectiveDate = #" + txtEffectiveDate.ToString("dd-MMM-yyyy") + "# " +
                " WHERE EmpTaxSchemeID = (SELECT Max(EmpTaxSchemeID) AS MaxEmpTaxSchemeID FROM EmpTaxSchemeInfo WHERE EmpID = " + txtEmpID + ")";

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

        public int DeleteEmployeeTaxSchemeInfo(int txtEmpTaxSchemeID)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE * FROM EmpTaxSchemeInfo WHERE EmpTaxSchemeID = " + txtEmpTaxSchemeID;

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

    public class EmpTaxSchemeInfo
    {
        public int EmpTaxSchemeID { get; set; }
        public int EmpID { get; set; }
        public int TaxSchemeID { get; set; }
        public DateTime EffectiveDate { get; set; }

    }

    public class TaxSchemeInfo
    {
        public int TaxSchemeID { get; set; }
        public string TaxCode { get; set; }
        public string TaxTitle { get; set; }
        public string TaxInitial { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }


    }
}
