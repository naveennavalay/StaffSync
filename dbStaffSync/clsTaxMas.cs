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
    public class clsTaxMas
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public List<TaxSchemeModel> GetTaxList()
        {
            List<TaxSchemeModel> objTaxSchemeModelList = new List<TaxSchemeModel>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT * FROM TaxSchemeInfo WHERE IsActive = true AND IsDeleted = false";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objTaxSchemeModelList = JsonConvert.DeserializeObject<List<TaxSchemeModel>>(DataTableToJSon);
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

            return objTaxSchemeModelList;
        }

        public EmpTaxSchemeModel getEmployeeSpecificTaxSchemeModel(int txtEmpID)
        {
            List<EmpTaxSchemeModel> objEmpTaxSchemeModel = new List<EmpTaxSchemeModel>();
            EmpTaxSchemeModel objSpecificTaxSchemeModel = new EmpTaxSchemeModel();

            try
            {
                conn = dbStaffSync.openDBConnection();
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
                objEmpTaxSchemeModel = JsonConvert.DeserializeObject<List<EmpTaxSchemeModel>>(DataTableToJSon);
                if (objEmpTaxSchemeModel.Count > 0)
                {
                    objSpecificTaxSchemeModel = objEmpTaxSchemeModel[0];
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

            return objSpecificTaxSchemeModel;
        }

        public int InsertEmployeeTaxSchemeModel(int txtEmpID, int txtTaxSchemeID, DateTime txtEffectiveDate)
        {
            int affectedRows = 0;
            try
            {

                Response<int> maxRowCount = objGenFunc.getMaxRowCount("EmpTaxSchemeInfo", "EmpTaxSchemeID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO EmpTaxSchemeInfo (EmpTaxSchemeID, EmpID, TaxSchemeID, EffectiveDate) VALUES " +
                 "(" + maxRowCount.Data + ", " + txtEmpID + ", " + txtTaxSchemeID + ", #" + txtEffectiveDate.ToString("dd-MMM-yyyy") + "#)";

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

        public int UpdateEmployeeTaxSchemeModel(int txtEmpTaxSchemeID, int txtEmpID, int txtTaxSchemeID, DateTime txtEffectiveDate)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
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
                //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = dbStaffSync.closeDBConnection();
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }

            return affectedRows;
        }

        public int DeleteEmployeeTaxSchemeModel(int txtEmpTaxSchemeID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE * FROM EmpTaxSchemeInfo WHERE EmpTaxSchemeID = " + txtEmpTaxSchemeID;

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
