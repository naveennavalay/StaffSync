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
    public class clsAssetsInfo
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public AssetInfo getAssetInfo(int txtAssetInfoID)
        {
            List<AssetInfo> objAssetInfoList = new List<AssetInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " + 
                                        " AssetMas.AssetID, " + 
                                        " AssetMas.AssetCode, " + 
                                        " AssetMas.AssetName, " + 
                                        " AssetMas.AssetDescription, " + 
                                        " AssetMas.IsActive, " + 
                                        " AssetMas.IsDeleted, " + 
                                        " AssetMas.AssetCatMasID, " + 
                                        " AssetMas.IsRecoverable, " + 
                                        " AssetMas.IsRequireReturn, " + 
                                        " AssetMas.IsCriticalAsset, " + 
                                        " AssetMas.RecoveryTypeID, " + 
                                        " AssetMas.AffectsPayroll, " + 
                                        " AssetMas.PayrollImpact, " + 
                                        " AssetMas.PayrollHeaderID, " + 
                                        " AssetMas.CurrentAssetStatusID " + 
                                    " FROM " + 
                                        " AssetMas " + 
                                    " WHERE " + 
                                        " AssetMas.AssetID = " + txtAssetInfoID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAssetInfoList = JsonConvert.DeserializeObject<List<AssetInfo>>(DataTableToJSon);
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

            if (objAssetInfoList.Count > 0)
                return objAssetInfoList[0];
            else
                return new AssetInfo();
        }

        public AssetMoreInfo getAssetMoreInfo(int txtAssetInfoID)
        {
            List<AssetMoreInfo> objAssetMoreInfo = new List<AssetMoreInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " + 
                                        " AssetMasMoreDetails.AssetMasMoreDetID, " + 
                                        " AssetMasMoreDetails.AssetID, " + 
                                        " AssetMasMoreDetails.SerialNumber, " + 
                                        " AssetMasMoreDetails.ModelNumber, " + 
                                        " AssetMasMoreDetails.ManufacturerInfo, " + 
                                        " AssetMasMoreDetails.AssetTag, " + 
                                        " AssetMasMoreDetails.PurchaseDate, " + 
                                        " AssetMasMoreDetails.PurchaseValue, " + 
                                        " AssetMasMoreDetails.VendorName, " + 
                                        " AssetMasMoreDetails.InvoiceNumber, " + 
                                        " AssetMasMoreDetails.Location, " + 
                                        " AssetMasMoreDetails.WarrantyStartDate, " + 
                                        " AssetMasMoreDetails.WarrantyEndDate, " + 
                                        " AssetMasMoreDetails.HasWarranty, " + 
                                        " AssetMasMoreDetails.LastServiceDate, " + 
                                        " AssetMasMoreDetails.NextServiceDate " + 
                                    " FROM " + 
                                        " AssetMasMoreDetails " + 
                                    " WHERE " + 
                                        " AssetMasMoreDetails.AssetID = " + txtAssetInfoID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAssetMoreInfo = JsonConvert.DeserializeObject<List<AssetMoreInfo>>(DataTableToJSon);
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

            if (objAssetMoreInfo.Count > 0)
                return objAssetMoreInfo[0];
            else
                return new AssetMoreInfo();
        }

        public List<AssetInfoListing> getAssetsInfoList(int txtClientID)
        {
            List<AssetInfoListing> objAssetInfoListingList = new List<AssetInfoListing>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " +
                                        " AssetMas.AssetID, " +
                                        " AssetMas.AssetCode, " +
                                        " AssetMas.AssetName, " +
                                        " AssetMas.AssetDescription, " + 
                                        " AssetMas.IsActive, " + 
                                        " AssetMas.IsDeleted, " + 
                                        " AssetMas.AssetCatMasID, " + 
                                        " AssetMas.IsRecoverable, " + 
                                        " AssetMas.IsRequireReturn, " + 
                                        " AssetMas.IsCriticalAsset, " + 
                                        " AssetMas.RecoveryTypeID, " + 
                                        " AssetMas.AffectsPayroll, " + 
                                        " AssetMas.PayrollImpact, " + 
                                        " AssetMas.PayrollHeaderID, " + 
                                        " AssetMas.CurrentAssetStatusID, " + 
                                        " AssetCategoryMas.AssetCode, " + 
                                        " AssetCategoryMas.AssetName, " + 
                                        " AssetCategoryMas.AssetDescription, " + 
                                        " RecoveryTypeMas.RecoveryTypeCode, " + 
                                        " RecoveryTypeMas.RecoveryTypeName, " + 
                                        " RecoveryTypeMas.RecoveryTypeDescription, " + 
                                        " CurrentAssetStatus.CurrentAssetStatusCode, " + 
                                        " CurrentAssetStatus.CurrentAssetStatusName, " + 
                                        " CurrentAssetStatus.CurrentAssetDescription, " + 
                                        " AssetCategoryMas.ClientID " + 
                                    " FROM " + 
                                        " CurrentAssetStatus INNER JOIN (RecoveryTypeMas INNER JOIN ( AssetCategoryMas INNER JOIN AssetMas ON AssetCategoryMas.AssetCatMasID = AssetMas.AssetCatMasID) ON RecoveryTypeMas.RecoveryTypeID = AssetMas.RecoveryTypeID) ON CurrentAssetStatus.CurrentAssetStatusID = AssetMas.PayrollHeaderID " + 
                                    " WHERE " +
                                        " ( " + 
                                            " ((AssetMas.IsActive) = True) " + 
                                            " AND ((AssetMas.IsDeleted) = False) " + 
                                            " AND ((AssetCategoryMas.ClientID) = " + txtClientID + ") " + 
                                        " ) " + 
                                    " ORDER BY " + 
                                        " AssetMas.AssetID;";


                strQuery = "SELECT " +
                                " AssetMas.AssetID, " +
                                " AssetMas.AssetCode, " +
                                " AssetMas.AssetName, " +
                                " AssetMas.AssetDescription, " +
                                " AssetCategoryMas.AssetName AS AssetCategoryName, " +
                                " AssetMas.IsActive, " +
                                " AssetMas.IsDeleted, " +
                                " AssetMas.AssetCatMasID, " +
                                " AssetMas.IsRecoverable, " +
                                " AssetMas.IsRequireReturn, " +
                                " AssetMas.IsCriticalAsset, " +
                                " AssetMas.RecoveryTypeID, " +
                                " AssetMas.AffectsPayroll, " +
                                " AssetMas.PayrollImpact, " +
                                " AssetMas.PayrollHeaderID, " +
                                " AssetMas.CurrentAssetStatusID, " +
                                " RecoveryTypeMas.RecoveryTypeCode, " +
                                " RecoveryTypeMas.RecoveryTypeName, " +
                                " RecoveryTypeMas.RecoveryTypeDescription, " +
                                " CurrentAssetStatus.CurrentAssetStatusCode, " +
                                " CurrentAssetStatus.CurrentAssetStatusName, " +
                                " CurrentAssetStatus.CurrentAssetDescription, " +
                                " AssetCategoryMas.ClientID " +
                            " FROM " +
                                " AssetCategoryMas " +
                                " INNER JOIN ( " +
                                    " RecoveryTypeMas " +
                                    " INNER JOIN ( " +
                                        " CurrentAssetStatus " +
                                        " INNER JOIN AssetMas ON CurrentAssetStatus.CurrentAssetStatusID = AssetMas.PayrollHeaderID " +
                                    " ) ON RecoveryTypeMas.RecoveryTypeID = AssetMas.RecoveryTypeID " +
                                " ) ON AssetCategoryMas.AssetCatMasID = AssetMas.AssetCatMasID " +
                            " WHERE " +
                                " ( " +
                                    " ((AssetMas.IsActive) = True) " +
                                    " AND ((AssetMas.IsDeleted) = False) " +
                                    " AND ((AssetCategoryMas.ClientID) = " + txtClientID + ") " +
                                " ) " +
                            " ORDER BY " +
                                " AssetMas.AssetID;";

                strQuery = "SELECT " +
                                        " AssetMas.AssetID, " +
                                        " AssetMas.AssetCode, " +
                                        " AssetMas.AssetName, " +
                                        " AssetMas.AssetDescription, " +
                                        " AssetCategoryMas.AssetName AS AssetCategoryName, " +
                                        " CurrentAssetStatus.CurrentAssetStatusName, " +
                                        " AssetMas.IsActive, " +
                                        " AssetMas.IsDeleted, " +
                                        " AssetCategoryMas.ClientID " +
                                    " FROM " +
                                        " CurrentAssetStatus " +
                                        " INNER JOIN ( " +
                                            " AssetCategoryMas " +
                                            " INNER JOIN AssetMas ON AssetCategoryMas.AssetCatMasID = AssetMas.AssetCatMasID " +
                                        " ) ON CurrentAssetStatus.CurrentAssetStatusID = AssetMas.CurrentAssetStatusID " +
                                    " WHERE " +
                                        " ( " +
                                            " ((AssetMas.IsActive) = True) " +
                                            " AND ((AssetMas.IsDeleted) = False) " +
                                            " AND ((AssetCategoryMas.ClientID) = " + txtClientID + ") " +
                                        " ) " +
                                    " ORDER BY " +
                                        " AssetMas.AssetID Asc";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAssetInfoListingList = JsonConvert.DeserializeObject<List<AssetInfoListing>>(DataTableToJSon);
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

            return objAssetInfoListingList;
        }

        //public AssetInfoListing getAssetInfo(int txtAssetInfoID, int txtClientID)
        //{
        //    AssetInfoListing objAssetInfoListing = new AssetInfoListing();
        //    DataTable dt = new DataTable();

        //    try
        //    {
        //        conn = dbStaffSync.openDBConnection();

        //        string strQuery = "SELECT " + 
        //                                " AssetMas.AssetID, " + 
        //                                " AssetMas.AssetCode, " + 
        //                                " AssetMas.AssetName, " + 
        //                                " AssetMas.AssetDescription, " + 
        //                                " AssetMas.IsActive, " + 
        //                                " AssetMas.IsDeleted, " + 
        //                                " AssetMas.AssetCatMasID, " + 
        //                                " AssetMas.IsRecoverable, " + 
        //                                " AssetMas.IsRequireReturn, " + 
        //                                " AssetMas.IsCriticalAsset, " + 
        //                                " AssetMas.RecoveryTypeID, " + 
        //                                " AssetMas.AffectsPayroll, " + 
        //                                " AssetMas.PayrollImpact, " + 
        //                                " AssetMas.PayrollHeaderID, " + 
        //                                " AssetMas.CurrentAssetStatusID, " + 
        //                                " AssetCategoryMas.AssetCode, " + 
        //                                " AssetCategoryMas.AssetName, " + 
        //                                " AssetCategoryMas.AssetDescription, " + 
        //                                " RecoveryTypeMas.RecoveryTypeCode, " + 
        //                                " RecoveryTypeMas.RecoveryTypeName, " + 
        //                                " RecoveryTypeMas.RecoveryTypeDescription, " + 
        //                                " CurrentAssetStatus.CurrentAssetStatusCode, " + 
        //                                " CurrentAssetStatus.CurrentAssetStatusName, " + 
        //                                " CurrentAssetStatus.CurrentAssetDescription, " + 
        //                                " AssetCategoryMas.ClientID " + 
        //                            " FROM " + 
        //                                " RecoveryTypeMas INNER JOIN (CurrentAssetStatus INNER JOIN (AssetCategoryMas INNER JOIN AssetMas ON AssetCategoryMas.AssetCatMasID = AssetMas.AssetCatMasID) ON " + 
        //                                " CurrentAssetStatus.CurrentAssetStatusID = AssetMas.PayrollHeaderID) ON RecoveryTypeMas.RecoveryTypeID = AssetMas.RecoveryTypeID " + 
        //                            " WHERE " + 
        //                                " ( " + 
        //                                    " ((AssetMas.AssetID) = " + txtAssetInfoID + ") " +
        //                                    " AND((AssetMas.IsActive) = True) " +
        //                                    " AND((AssetMas.IsDeleted) = False) " +
        //                                    " AND((AssetCategoryMas.ClientID) = " + txtClientID + ") " +
        //                                " ) " +
        //                            " ORDER BY " +
        //                                " AssetMas.AssetID;";

        //        OleDbCommand cmd = conn.CreateCommand();
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = strQuery;
        //        cmd.ExecuteNonQuery();

        //        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        //        da.Fill(dt);

        //        string DataTableToJSon = "";
        //        DataTableToJSon = JsonConvert.SerializeObject(dt);
        //        objAssetInfoListing = JsonConvert.DeserializeObject<AssetInfoListing>(DataTableToJSon);
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        conn = dbStaffSync.closeDBConnection();
        //    }
        //    finally
        //    {
        //        conn = dbStaffSync.closeDBConnection();
        //    }

        //    return objAssetInfoListing;
        //}

        public AssetInfoListing getAssetInfo(string txtAssetName, int txtClientID)
        {
            AssetInfoListing objAssetInfoListing = new AssetInfoListing();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " +
                                        " AssetMas.AssetID, " +
                                        " AssetMas.AssetCode, " +
                                        " AssetMas.AssetName, " +
                                        " AssetMas.AssetDescription, " +
                                        " AssetMas.IsActive, " +
                                        " AssetMas.IsDeleted, " +
                                        " AssetMas.AssetCatMasID, " +
                                        " AssetMas.IsRecoverable, " +
                                        " AssetMas.IsRequireReturn, " +
                                        " AssetMas.IsCriticalAsset, " +
                                        " AssetMas.RecoveryTypeID, " +
                                        " AssetMas.AffectsPayroll, " +
                                        " AssetMas.PayrollImpact, " +
                                        " AssetMas.PayrollHeaderID, " +
                                        " AssetMas.CurrentAssetStatusID, " +
                                        " AssetCategoryMas.AssetCode, " +
                                        " AssetCategoryMas.AssetName, " +
                                        " AssetCategoryMas.AssetDescription, " +
                                        " RecoveryTypeMas.RecoveryTypeCode, " +
                                        " RecoveryTypeMas.RecoveryTypeName, " +
                                        " RecoveryTypeMas.RecoveryTypeDescription, " +
                                        " CurrentAssetStatus.CurrentAssetStatusCode, " +
                                        " CurrentAssetStatus.CurrentAssetStatusName, " +
                                        " CurrentAssetStatus.CurrentAssetDescription, " +
                                        " AssetCategoryMas.ClientID " +
                                    " FROM " +
                                        " RecoveryTypeMas INNER JOIN (CurrentAssetStatus INNER JOIN (AssetCategoryMas INNER JOIN AssetMas ON AssetCategoryMas.AssetCatMasID = AssetMas.AssetCatMasID) ON " +
                                        " CurrentAssetStatus.CurrentAssetStatusID = AssetMas.PayrollHeaderID) ON RecoveryTypeMas.RecoveryTypeID = AssetMas.RecoveryTypeID " +
                                    " WHERE " +
                                        " ( " +
                                            " ((AssetMas.AssetName) = '" + txtAssetName + "') " + 
                                            " AND((AssetMas.IsActive) = True) " +
                                            " AND((AssetMas.IsDeleted) = False) " +
                                            " AND((AssetCategoryMas.ClientID) = " + txtClientID + ") " +
                                        " ) " +
                                    " ORDER BY " +
                                        " AssetMas.AssetID;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAssetInfoListing = JsonConvert.DeserializeObject<AssetInfoListing>(DataTableToJSon);
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

            return objAssetInfoListing;
        }

        public List<AssetInfoListing> getAssetsInfoFilter(string txtAssetName, int txtClientID)
        {
            List<AssetInfoListing> objAssetInfoListingList = new List<AssetInfoListing>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                //string strQuery = "SELECT " +
                //                        " AssetMas.AssetID, " +
                //                        " AssetMas.AssetCode, " +
                //                        " AssetMas.AssetName, " +
                //                        " AssetMas.AssetDescription, " +
                //                        " AssetMas.IsActive, " +
                //                        " AssetMas.IsDeleted, " +
                //                        " AssetMas.AssetCatMasID, " +
                //                        " AssetMas.IsRecoverable, " +
                //                        " AssetMas.IsRequireReturn, " +
                //                        " AssetMas.IsCriticalAsset, " +
                //                        " AssetMas.RecoveryTypeID, " +
                //                        " AssetMas.AffectsPayroll, " +
                //                        " AssetMas.PayrollImpact, " +
                //                        " AssetMas.PayrollHeaderID, " +
                //                        " AssetMas.CurrentAssetStatusID, " +
                //                        " AssetCategoryMas.AssetCode, " +
                //                        " AssetCategoryMas.AssetName, " +
                //                        " AssetCategoryMas.AssetDescription, " +
                //                        " RecoveryTypeMas.RecoveryTypeCode, " +
                //                        " RecoveryTypeMas.RecoveryTypeName, " +
                //                        " RecoveryTypeMas.RecoveryTypeDescription, " +
                //                        " CurrentAssetStatus.CurrentAssetStatusCode, " +
                //                        " CurrentAssetStatus.CurrentAssetStatusName, " +
                //                        " CurrentAssetStatus.CurrentAssetDescription, " +
                //                        " AssetCategoryMas.ClientID " +
                //                    " FROM " +
                //                        " RecoveryTypeMas INNER JOIN (CurrentAssetStatus INNER JOIN (AssetCategoryMas INNER JOIN AssetMas ON AssetCategoryMas.AssetCatMasID = AssetMas.AssetCatMasID) ON " +
                //                        " CurrentAssetStatus.CurrentAssetStatusID = AssetMas.PayrollHeaderID) ON RecoveryTypeMas.RecoveryTypeID = AssetMas.RecoveryTypeID " +
                //                    " WHERE " +
                //                        " ( " +
                //                            " ((AssetMas.AssetName) LIKE '*" + txtAssetName + "*') " +
                //                            " AND((AssetMas.IsActive) = True) " +
                //                            " AND((AssetMas.IsDeleted) = False) " +
                //                            " AND((AssetCategoryMas.ClientID) = " + txtClientID + ") " +
                //                        " ) " +
                //                    " ORDER BY " +
                //                        " AssetMas.AssetID;";

                string strQuery = "SELECT " + 
                                        " AssetMas.AssetID, " + 
                                        " AssetMas.AssetCode, " + 
                                        " AssetMas.AssetName, " + 
                                        " AssetMas.AssetDescription, " + 
                                        " AssetCategoryMas.AssetName AS AssetCategoryName, " + 
                                        " CurrentAssetStatus.CurrentAssetStatusName, " + 
                                        " AssetMas.IsActive, " + 
                                        " AssetMas.IsDeleted, " + 
                                        " AssetCategoryMas.ClientID " + 
                                    " FROM " + 
                                        " CurrentAssetStatus " + 
                                        " INNER JOIN ( " + 
                                            " AssetCategoryMas " + 
                                            " INNER JOIN AssetMas ON AssetCategoryMas.AssetCatMasID = AssetMas.AssetCatMasID " + 
                                        " ) ON CurrentAssetStatus.CurrentAssetStatusID = AssetMas.CurrentAssetStatusID " + 
                                    " WHERE " + 
                                        " ( " + 
                                            " ((AssetMas.AssetName) LIKE '%" + txtAssetName + "%') " + 
                                            " AND((AssetMas.IsActive) = True) " + 
                                            " AND((AssetMas.IsDeleted) = False) " + 
                                            " AND((AssetCategoryMas.ClientID) = " + txtClientID + ") " + 
                                        " ) " + 
                                    " ORDER BY " + 
                                        " AssetMas.AssetID Asc";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAssetInfoListingList = JsonConvert.DeserializeObject<List<AssetInfoListing>>(DataTableToJSon);
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

            return objAssetInfoListingList;
        }

        public int InsertAssetInfo(string txtAssetCode, string txtAssetName, string txtAssetDescription, bool IsActive, bool IsDeleted, int AssetCatMasID, bool IsRecoverable, bool IsRequireReturn, bool IsCriticalAsset, int RecoveryTypeID, bool AffectsPayroll, string PayrollImpact, int PayrollHeaderID, int CurrentAssetStatusID)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("AssetMas", "AssetID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO AssetMas (AssetID, AssetCode, AssetName, AssetDescription, IsActive, IsDeleted, AssetCatMasID, IsRecoverable, IsRequireReturn, IsCriticalAsset, RecoveryTypeID, AffectsPayroll, PayrollImpact, PayrollHeaderID, CurrentAssetStatusID) VALUES " +
                 "(" + maxRowCount.Data + ",'" + txtAssetCode + "', '" + txtAssetName + "', '" + txtAssetDescription + "'," + IsActive + ", " + IsDeleted + ", " + AssetCatMasID + ", " + IsRecoverable + ", " + IsRequireReturn + ", " + IsCriticalAsset + ", " + RecoveryTypeID + ", " + AffectsPayroll + ", '" + PayrollImpact + "', " + PayrollHeaderID + ", " + CurrentAssetStatusID + ")";

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

        public int UpdateAssetInfo(int AssetID, string txtAssetCode, string txtAssetName, string txtAssetDescription, bool IsActive, bool IsDeleted, int txtAssetCatMasID, bool IsRecoverable, bool IsRequireReturn, bool IsCriticalAsset, int RecoveryTypeID, bool AffectsPayroll, string txtPayrollImpact, int PayrollHeaderID, int CurrentAssetStatusID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE AssetMas SET " +
                                        " AssetCode = '" + txtAssetCode + "', AssetName = '" + txtAssetName + "', AssetDescription = '" + txtAssetDescription + "', IsActive = " + IsActive + ", AssetCatMasID = " + txtAssetCatMasID + ", IsRecoverable = " + IsRecoverable + ", IsRequireReturn = " + IsRequireReturn + ", IsCriticalAsset = " + IsCriticalAsset + ", RecoveryTypeID = " + RecoveryTypeID +
                                        ", AffectsPayroll = " + AffectsPayroll + ", PayrollImpact = '" + txtPayrollImpact + "', PayrollHeaderID = " + PayrollHeaderID + ", CurrentAssetStatusID = " + CurrentAssetStatusID +
                                  " WHERE " +
                                        " AssetID = " + AssetID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = AssetID;
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

        public int DeleteAssetInfo(int AssetID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE * FROM AssetMas " +
                                  " WHERE " +
                                        " AssetID = " + AssetID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = AssetID;
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

        public int InsertAssetMoreInfo(int AssetID, string txtSerialNumber, string txtModelNumber, string txtManufacturerInfo, string txtAssetTag, DateTime dtPurchaseDate, decimal txtPurchaseValue, string txtVendorName, string txtInvoiceNumber, string txtLocation, bool HasWarranty, DateTime dtWarrantyStartDate, DateTime dtWarrantyEndDate, DateTime dtLastServiceDate, DateTime dtNextServiceDate)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("AssetMasMoreDetails", "AssetMasMoreDetID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO AssetMasMoreDetails (AssetMasMoreDetID, AssetID, SerialNumber, ModelNumber, ManufacturerInfo, AssetTag, PurchaseDate, PurchaseValue, VendorName, InvoiceNumber, Location, WarrantyStartDate, WarrantyEndDate, HasWarranty, LastServiceDate, NextServiceDate) VALUES " +
                 "(" + maxRowCount.Data + ", " + AssetID + ", '" + txtSerialNumber + "', '" + txtModelNumber + "', '" + txtManufacturerInfo  + "', '" + txtAssetTag + "', '" + dtPurchaseDate.ToString("dd-MMM-yyyy") + "', " + txtPurchaseValue + ", '" + txtVendorName + "', '" + txtInvoiceNumber + "', '" + txtLocation +  "', '" + dtWarrantyStartDate.ToString("dd-MMM-yyyy") + "', '" + dtWarrantyEndDate.ToString("dd-MMM-yyyy") + "', " + HasWarranty + ", '" + dtLastServiceDate.ToString("dd-MMM-yyyy") + "', '" + dtWarrantyEndDate.ToString("dd-MMM-yyyy") + "')";

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

        public int UpdateAssetMoreInfo(int AssetMasMoreDetID, int AssetID, string txtSerialNumber, string txtModelNumber, string txtManufacturerInfo, string txtAssetTag, DateTime dtPurchaseDate, decimal txtPurchaseValue, string txtVendorName, string txtInvoiceNumber, string txtLocation, bool HasWarranty, DateTime dtWarrantyStartDate, DateTime dtWarrantyEndDate, DateTime dtLastServiceDate, DateTime dtNextServiceDate)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE AssetMasMoreDetails SET " + 
                                            " AssetID = " + AssetID + ", " + 
                                            " SerialNumber = '" + txtSerialNumber + "', " + 
                                            " ModelNumber = '" + txtModelNumber + "', " + 
                                            " ManufacturerInfo = '" + txtManufacturerInfo + "', " + 
                                            " AssetTag = '', " + 
                                            " PurchaseDate = #" + dtPurchaseDate.ToString("dd-MMM-yyyy") + "#, " + 
                                            " PurchaseValue = 0, " + 
                                            " VendorName = '" + txtVendorName + "', " +
                                            " InvoiceNumber = '" + txtInvoiceNumber + "', " +
                                            " Location = '', " +
                                            " WarrantyStartDate = #" + dtWarrantyStartDate.ToString("dd-MMM-yyyy") + "#, " +
                                            " WarrantyEndDate  = #" + dtWarrantyEndDate.ToString("dd-MMM-yyyy") + "#, " +
                                            " HasWarranty = " + HasWarranty + ", " +
                                            " LastServiceDate = #" + dtLastServiceDate.ToString("dd-MMM-yyyy") + "#, " +
                                            " NextServiceDate = #" + dtNextServiceDate.ToString("dd-MMM-yyyy") + "# " +
                                        " WHERE " +
                                            " AssetID = " + AssetID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = AssetID;
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

        public int DeleteAssetMoreInfo(int AssetID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE * FROM AssetMasMoreDetails " +
                                  " WHERE " +
                                        " AssetID = " + AssetID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = AssetID;
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
