using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class AssetInfo
    {
        public int AssetID { get; set; }
        public string AssetCode { get; set; }
        public string AssetName { get; set; }
        public string AssetDescription { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int AssetCatMasID { get; set; }

        public bool IsRecoverable { get; set; }
        public bool IsRequireReturn { get; set; }
        public bool IsCriticalAsset { get; set; }
        public int RecoveryTypeID { get; set; }
        public bool AffectsPayroll { get; set; }
        public string PayrollImpact { get; set; }
        public int PayrollHeaderID { get; set; }
        public int CurrentAssetStatusID { get; set; }
    }

    public class AssetMoreInfo
    {
        public int AssetMasMoreDetID { get; set; }
        public int AssetID { get; set; }
        public string SerialNumber { get; set; }
        public string ModelNumber { get; set; }
        public string ManufacturerInfo { get; set; }
        public string AssetTag { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PurchaseValue { get; set; }
        public string VendorName { get; set; }
        public string InvoiceNumber { get; set; }
        public string Location { get; set; }
        public DateTime WarrantyStartDate { get; set; }
        public DateTime WarrantyEndDate { get; set; }
        public bool HasWarranty { get; set; }
        public DateTime LastServiceDate { get; set; }
        public DateTime NextServiceDate { get; set; }
    }


    public class AssetInfoListing
    {
        public int AssetID { get; set; }

        [DisplayName("Asset Code")]
        public string AssetCode { get; set; }

        [DisplayName("Asset Name")]
        public string AssetName { get; set; }

        [DisplayName("Asset Description")]
        public string AssetDescription { get; set; }

        [DisplayName("Asset Category")] 
        public string AssetCategoryName { get; set; }

        [DisplayName("Asset Current Status")]
        public string CurrentAssetStatusName { get; set; }

        [DisplayName("Asset Current Status Description")] 
        public string CurrentAssetDescription { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class AssetCompleteInfo
    {
        public int AssetID { get; set; }
        
        [DisplayName("Asset Code")] 
        public string AssetCode { get; set; }
        
        [DisplayName("Asset Name")] 
        public string AssetName { get; set; }
        
        [DisplayName("Asset Description")] 
        public string AssetDescription { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int AssetCatMasID { get; set; }
        public bool IsRecoverable { get; set; }
        public bool IsRequireReturn { get; set; }
        public bool IsCriticalAsset { get; set; }
        public int RecoveryTypeID { get; set; }
        public bool AffectsPayroll { get; set; }
        public string PayrollImpact { get; set; }
        public int PayrollHeaderID { get; set; }
        public int CurrentAssetStatusID { get; set; }

        [DisplayName("Category Name")]
        public string AssetCategoryName { get; set; }

        [DisplayName("Recovery Type Code")] 
        public string RecoveryTypeCode { get; set; }

        [DisplayName("Recovery Type Name")] 
        public string RecoveryTypeName { get; set; }


        [DisplayName("Recovery Type Description")] 
        public string RecoveryTypeDescription { get; set; }

        [DisplayName("Asset Current Status Code")]
        public string CurrentAssetStatusCode { get; set; }


        [DisplayName("Asset Current Status Name")] 
        public string CurrentAssetStatusName { get; set; }

        [DisplayName("Asset Current Status Description")]
        public string CurrentAssetDescription { get; set; }
        public int ClientID { get; set; }
    }

    public class AssetRegister
    {
        public int AssetRegID { get; set; }
        public int AssetID { get; set; }

        [DisplayName("Date")]
        public DateTime AssetTRDate { get; set; }
        [DisplayName("Opening Balance")] 
        public decimal OBalance { get; set; }

        [DisplayName("Credit")] 
        public decimal CrBalance { get; set; }
        
        [DisplayName("Debit")]
        public decimal DrBalance { get; set; }
        
        [DisplayName("Closing Balance")] 
        public decimal CBalance { get; set; }

        [DisplayName("Operation Type")]
        public string TRType { get; set; }
        
        [DisplayName("Comments")] 
        public string Comments { get; set; }
        public int OrderID { get; set; }
        public int EmpAdvanceRequestID { get; set; }
    }

}
