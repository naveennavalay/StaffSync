using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace dbStaffSync
{
    public class clsAssetsCategory
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();


        public List<AssetsCategory> getAssetsCategoryList(int txtClient)
        {
            List<AssetsCategory> objAssetsCategoryList = new List<AssetsCategory>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT * FROM AssetCategoryMas WHERE IsActive = true AND IsDeleted = false AND ClientID = " + txtClient + " ORDER BY AssetCatMasID ASC";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAssetsCategoryList = JsonConvert.DeserializeObject<List<AssetsCategory>>(DataTableToJSon);
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

            return objAssetsCategoryList;
        }

        public AssetsCategory getAssetsCategoryInfo(int txtAssetCatMasID, int txtClient)
        {
            AssetsCategory objAssetsCategoryInfo = new AssetsCategory();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT * FROM AssetCategoryMas WHERE AssetCatMasID = " + txtAssetCatMasID + " AND ClientID = " + txtClient + " AND IsActive = true AND IsDeleted = false ORDER BY AssetCatMasID ASC";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAssetsCategoryInfo = JsonConvert.DeserializeObject<AssetsCategory>(DataTableToJSon);
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

            return objAssetsCategoryInfo;
        }

        public AssetsCategory getAssetsCategoryInfo(string txtAssetName, int txtClient)
        {
            AssetsCategory objAssetsCategoryInfo = new AssetsCategory();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT * FROM AssetCategoryMas WHERE AssetName = '" + txtAssetName + "' AND ClientID = " + txtClient + " AND IsActive = true AND IsDeleted = false ORDER BY AssetCatMasID ASC";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAssetsCategoryInfo = JsonConvert.DeserializeObject<AssetsCategory>(DataTableToJSon);
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

            return objAssetsCategoryInfo;
        }

        public List<AssetsCategory> getAssetsCategoryInfoFilter(string txtAssetName, int txtClientID)
        {
            List<AssetsCategory> objAssetsCategoryList = new List<AssetsCategory>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " +
                        " AssetCategoryMas.AssetCatMasID, " +
                        " AssetCategoryMas.AssetCode, " +
                        " AssetCategoryMas.AssetName, " +
                        " AssetCategoryMas.AssetDescription, " +
                        " AssetCategoryMas.IsActive, " +
                        " AssetCategoryMas.IsDeleted, " +
                        " AssetCategoryMas.AssetNote, " +
                        " AssetCategoryMas.ClientID " +
                    " FROM " +
                        " AssetCategoryMas " +
                    " WHERE " +
                        " AssetCategoryMas.AssetName LIKE '*" + txtAssetName + " *' AND IsActive = true and IsDeleted = false AND ClientiD = " + txtClientID + " ORDER BY AssetCatMasID ASC";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAssetsCategoryList = JsonConvert.DeserializeObject<List<AssetsCategory>>(DataTableToJSon);
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

            return objAssetsCategoryList;
        }

        public int InsertAssetCategoryInfo(string txtAssetCode, string txtAssetName, string txtAssetDescription, string txtAssetNote, bool IsActive, bool IsDeleted, int txtClientID)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("AssetCategoryMas", "AssetCatMasID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO AssetCategoryMas (AssetCatMasID, AssetCode, AssetName, AssetDescription, AssetNote, IsActive, IsDeleted, ClientID) VALUES " +
                 "(" + maxRowCount.Data + ",'" + txtAssetCode + "', '" + txtAssetName + "', '" + txtAssetDescription + "','" + txtAssetNote + "'," + IsActive + ", " + IsDeleted + ", " + txtClientID + ")";

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

        public int UpdateAssetCategoryInfo(int intAssetCatMasID, string txtAssetCode, string txtAssetName, string txtAssetDescription, string txtAssetNote, bool IsActive, bool IsDeleted, int txtClientID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE AssetCategoryMas SET " + 
                                        " AssetCode = '" + txtAssetCode + "', AssetName = '" + txtAssetName + "', AssetDescription = '" + txtAssetDescription + "', IsActive = " + IsActive + ", AssetNote = '" + txtAssetNote + "', ClientID = " + txtClientID + 
                                  " WHERE " +
                                        " AssetCatMasID = " + intAssetCatMasID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = intAssetCatMasID;
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

        public int DeleteAssetCategoryInfo(int intAssetCatMasID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE * FROM AssetCategoryMas " +
                                  " WHERE " +
                                        " AssetCatMasID = " + intAssetCatMasID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = intAssetCatMasID;
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
