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
    public class clsAssetRegister
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        //public AssetInfo getAssetRegisterInfo(int txtAssetID)
        //{
        //    List<AssetInfo> objAssetInfoList = new List<AssetInfo>();
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
        //                                " AssetMas.CurrentAssetStatusID " + 
        //                            " FROM " + 
        //                                " AssetMas " + 
        //                            " WHERE " + 
        //                                " AssetMas.AssetID = " + txtAssetInfoID;

        //        OleDbCommand cmd = conn.CreateCommand();
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = strQuery;
        //        cmd.ExecuteNonQuery();

        //        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        //        da.Fill(dt);

        //        string DataTableToJSon = "";
        //        DataTableToJSon = JsonConvert.SerializeObject(dt);
        //        objAssetInfoList = JsonConvert.DeserializeObject<List<AssetInfo>>(DataTableToJSon);
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

        //    if (objAssetInfoList.Count > 0)
        //        return objAssetInfoList[0];
        //    else
        //        return new AssetInfo();
        //}

        public int InsertAssetRegisterInfo(int txtAssetID, DateTime dtAssetTRDate, decimal txtOBalance, decimal txtCrBalance, decimal txtDrBalance, decimal txtCBalance, string txtTRType, string txtComments, int txtEmpAdvanceRequestID)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("AssetRegister", "AssetRegID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO AssetRegister (AssetRegID, AssetID, AssetTRDate, OBalance, CrBalance, DrBalance, CBalance, TRType, Comments, OrderID, EmpAdvanceRequestID) VALUES " +
                 "(" + maxRowCount.Data + "," + txtAssetID + ", '" + dtAssetTRDate.ToString("dd-MMM-yyyy") + "', " + txtOBalance + "," + txtCrBalance + ", " + txtDrBalance + ", " + txtCBalance + ", '" + txtTRType + "', '" + txtComments + "', " + maxRowCount.Data  + ", " + txtEmpAdvanceRequestID + ")";

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

        public int UpdateAssetRegisterInfo(int txtAssetRegID, int txtAssetID, DateTime dtAssetTRDate, decimal txtOBalance, decimal txtCrBalance, decimal txtDrBalance, decimal txtCBalance, string txtTRType, string txtComments, int txtEmpAdvanceRequestID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE AssetRegister SET " +
                                        " AssetID = " + txtAssetID + ", AssetTRDate = '" + dtAssetTRDate.ToString("dd-MMM-yyyy") + "', OBalance = " + txtOBalance + ", CrBalance = " + txtCrBalance + ", DrBalance = " + txtDrBalance + ", CBalance = " + txtCBalance + ", TRType = '" + txtTRType + "', Comments = '" + txtComments + "'" +
                                  " WHERE " +
                                        " AssetRegID = " + txtAssetRegID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtAssetRegID;
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

        public int DeleteAssetRegisterInfo(int txtAssetRegID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE * FROM AssetRegister " +
                                  " WHERE " +
                                        " AssetID = " + txtAssetRegID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtAssetRegID;
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
