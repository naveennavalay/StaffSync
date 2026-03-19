using ModelStaffSync;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace DALStaffSync
{
    public class clsAssetsInfo
    {
        dbStaffSync.clsAssetsInfo objAssetsInfo = new dbStaffSync.clsAssetsInfo();


        public AssetInfo getAssetInfo(int txtAssetInfoID)
        {
            return objAssetsInfo.getAssetInfo(txtAssetInfoID);
        }

        public AssetMoreInfo getAssetMoreInfo(int txtAssetInfoID)
        {
            return objAssetsInfo.getAssetMoreInfo(txtAssetInfoID);
        }

        public List<AssetInfoListing> getAssetsInfoList(int txtClientID)
        {
            return objAssetsInfo.getAssetsInfoList(txtClientID);
        }

        //public AssetInfoListing getAssetInfo(int txtAssetInfoID, int txtClientID)
        //{
        //    return objAssetsInfo.getAssetInfo(txtAssetInfoID, txtClientID);
        //}

        public AssetInfoListing getAssetInfo(string txtAssetName, int txtClientID)
        {
            return objAssetsInfo.getAssetInfo(txtAssetName, txtClientID);
        }

        public List<AssetInfoListing> getAssetsInfoFilter(string txtAssetName, int txtClientID)
        {
            return objAssetsInfo.getAssetsInfoFilter(txtAssetName, txtClientID);
        }

        public int InsertAssetInfo(string txtAssetCode, string txtAssetName, string txtAssetDescription, bool IsActive, bool IsDeleted, int AssetCatMasID, bool IsRecoverable, bool IsRequireReturn, bool IsCriticalAsset, int RecoveryTypeID, bool AffectsPayroll, string PayrollImpact, int PayrollHeaderID, int CurrentAssetStatusID)
        {
            return objAssetsInfo.InsertAssetInfo(txtAssetCode, txtAssetName, txtAssetDescription, IsActive, IsDeleted, AssetCatMasID, IsRecoverable, IsRequireReturn, IsCriticalAsset, RecoveryTypeID, AffectsPayroll, PayrollImpact, PayrollHeaderID, CurrentAssetStatusID);
        }

        public int UpdateAssetInfo(int AssetID, string txtAssetCode, string txtAssetName, string txtAssetDescription, bool IsActive, bool IsDeleted, int txtAssetCatMasID, bool IsRecoverable, bool IsRequireReturn, bool IsCriticalAsset, int RecoveryTypeID, bool AffectsPayroll, string txtPayrollImpact, int PayrollHeaderID, int CurrentAssetStatusID)
        {
            return objAssetsInfo.UpdateAssetInfo(AssetID, txtAssetCode, txtAssetName, txtAssetDescription, IsActive, IsDeleted, txtAssetCatMasID, IsRecoverable, IsRequireReturn, IsCriticalAsset, RecoveryTypeID, AffectsPayroll, txtPayrollImpact, PayrollHeaderID, CurrentAssetStatusID);
        }

        public int DeleteAssetInfo(int AssetID)
        {
            return objAssetsInfo.DeleteAssetInfo(AssetID);
        }

        public int InsertAssetMoreInfo(int AssetID, string txtSerialNumber, string txtModelNumber, string txtManufacturerInfo, string txtAssetTag, DateTime dtPurchaseDate, decimal txtPurchaseValue, string txtVendorName, string txtInvoiceNumber, string txtLocation, bool HasWarranty, DateTime dtWarrantyStartDate, DateTime dtWarrantyEndDate, DateTime dtLastServiceDate, DateTime dtNextServiceDate)
        {
            return objAssetsInfo.InsertAssetMoreInfo(AssetID, txtSerialNumber, txtModelNumber, txtManufacturerInfo, txtAssetTag, dtPurchaseDate, txtPurchaseValue, txtVendorName, txtInvoiceNumber, txtLocation, HasWarranty, dtWarrantyStartDate, dtWarrantyEndDate, dtLastServiceDate, dtNextServiceDate);
        }

        public int UpdateAssetMoreInfo(int AssetMasMoreDetID, int AssetID, string txtSerialNumber, string txtModelNumber, string txtManufacturerInfo, string txtAssetTag, DateTime dtPurchaseDate, decimal txtPurchaseValue, string txtVendorName, string txtInvoiceNumber, string txtLocation, bool HasWarranty, DateTime dtWarrantyStartDate, DateTime dtWarrantyEndDate, DateTime dtLastServiceDate, DateTime dtNextServiceDate)
        {
            return objAssetsInfo.UpdateAssetMoreInfo(AssetMasMoreDetID, AssetID, txtSerialNumber, txtModelNumber, txtManufacturerInfo, "", dtPurchaseDate, 0, txtVendorName, txtInvoiceNumber, "", HasWarranty, dtWarrantyStartDate, dtWarrantyEndDate, dtLastServiceDate, dtNextServiceDate);
        }

        public int DeleteAssetMoreInfo(int AssetID)
        {
            return objAssetsInfo.DeleteAssetMoreInfo(AssetID);
        }
    }
}
