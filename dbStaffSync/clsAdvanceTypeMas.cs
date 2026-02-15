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
    public class clsAdvanceTypeMas
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public AdvanceTypesModel GetAdvanceTypeConfigByID(int txtAdvanceTypeID)
        {
            List<AdvanceTypesModel> objAdvanceTypeConfigList = new List<AdvanceTypesModel>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " +
                                        " AdvanceTypeID, AdvanceTypeCode, AdvanceTypeTitle, IsActive, IsDeleted " +
                                  " FROM AdvanceTypeMas " +
                                  " WHERE " +
                                        " AdvanceTypeID = " + txtAdvanceTypeID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAdvanceTypeConfigList = JsonConvert.DeserializeObject<List<AdvanceTypesModel>>(DataTableToJSon);
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

            return objAdvanceTypeConfigList[0];
        }

        public DataTable GetAdvanceTypeList(int txtCompanyID)
        {
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT * FROM AdvanceTypeMas WHERE IsDeleted = false AND ClientID = " + txtCompanyID + " Order By AdvanceTypeID, OrderID";

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

        public DataTable GetAdvanceTypeList(int txtCompanyID, string filterText)
        {
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT * FROM AdvanceTypeMas WHERE IsDeleted = false AND AdvanceTypeTitle LIKE '" + filterText + "%'";

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

        public string GetAdvanceTypeByID(int txtCompanyID, int txtAdvanceTypeID)
        {
            string selectedAdvanceTypeTitle = "";
            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT AdvanceTypeTitle FROM AdvanceTypeMas WHERE AdvanceTypeID = " + txtAdvanceTypeID + " AND ClientID = " + txtCompanyID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                selectedAdvanceTypeTitle = (string)cmd.ExecuteScalar();
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

            return selectedAdvanceTypeTitle;
        }

        public int GetAdvanceTypeByTitle(int txtCompanyID, string txtAdvanceTypeTitle)
        {
            int selectedAdvanceTypeID = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT AdvanceTypeID FROM AdvanceTypeMas WHERE AdvanceTypeTitle = '" + txtAdvanceTypeTitle + "' AND ClientID = " + txtCompanyID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                selectedAdvanceTypeID = (int)cmd.ExecuteScalar();
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

            return selectedAdvanceTypeID;
        }

        public int InsertAdvanceType(string txtAdvanceTypeCode, string txtAdvanceTypeTitle, bool IsActive, bool IsDeleted, int txtClientID)
        {
            int affectedRows = 0;
            try
            {

                Response<int> maxRowCount = objGenFunc.getMaxRowCount("AdvanceTypeMas", "AdvanceTypeID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO AdvanceTypeMas (AdvanceTypeID, AdvanceTypeCode, AdvanceTypeTitle, IsActive, IsDeleted, OrderID, ClientID) VALUES " +
                 "(" + maxRowCount.Data + ",'" + "ADV-" + (maxRowCount.Data).ToString().PadLeft(4, '0').Trim() + "','" + txtAdvanceTypeTitle.Trim() + "'," + IsActive + "," + IsDeleted + "," + maxRowCount.Data + "," + txtClientID + ")";

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

        public int UpdateAdvanceType(int txtAdvanceTypeID, string txtAdvanceTypeCode, string txtAdvanceTypeTitle, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE AdvanceTypeMas SET " +
                 "AdvanceTypeCode = '" + txtAdvanceTypeCode.Trim() + "', AdvanceTypeTitle = '" + txtAdvanceTypeTitle.Trim() + "', IsActive = " + IsActive +
                 " WHERE AdvanceTypeID = " + txtAdvanceTypeID.ToString().Trim();

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

        public int DeleteAdvanceType(int txtAdvanceTypeID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE * FROM AdvanceTypeMas WHERE AdvanceTypeID = " + txtAdvanceTypeID.ToString().Trim();

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
