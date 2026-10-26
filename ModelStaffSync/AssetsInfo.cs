using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
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
        public int ParentAssetCatMasID { get; set; }
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

        [DisplayName("Quantity")] 
        public string OutstandingQuantity { get; set; }

        [DisplayName("Asset Current Status")]
        public string CurrentAssetStatusName { get; set; }

        [DisplayName("Asset Current Status Description")] 
        public string CurrentAssetDescription { get; set; }

        public int AssetCatMasID { get; set; }
        public int ParentAssetCatMasID { get; set; }
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

    public class SpecificAssetRegisterInfo
    {
        public int AssetRegID { get; set; }
        public int AssetID { get; set; }
        public decimal CBalance { get; set; }
        public int EmpAdvanceRequestID { get; set; }
    }

    public class AssetEditingList
    {
        public int AssetID { get; set; }
        [DisplayName("Asset Code")]
        public string AssetCode { get; set; }

        [DisplayName("Asset Name")] 
        public string AssetName { get; set; }
        public int AssetRequestID { get; set; }
        
        [DisplayName("Request Code")] 
        public string AssetRequestCode { get; set; }
        
        [DisplayName("Request Date")] 
        public DateTime AssetRequestDate { get; set; }

        [DisplayName("Request Comments")]
        public string AssetRequestComments { get; set; }
        
        [DisplayName("Request Status")] 
        public bool AssetRequestStatus { get; set; }

        public int EmpID { get; set; }

        [DisplayName("Employee Code")] 
        public string EmpCode { get; set; }


        [DisplayName("Employee Name")] 
        public string EmpName { get; set; }

        [DisplayName("Designation")]
        public string DesignationTitle { get; set; }

        [DisplayName("Department")] 
        public string DepartmentTitle { get; set; }
        
        [DisplayName("Contact Number")] 
        public string ContactNumber1 { get; set; }

        [DisplayName("Mail ID")] 
        public string ContactNumber2 { get; set; }
    }

    public class AssetRequestInfo
    {
        public int AssetRequestID { get; set; }
        public int AssetID { get; set; }
        public int AssetRequestByID { get; set; }
        public int RequestedTo { get; set; }
        public DateTime AssetRequestDate { get; set; }
        public string AssetRequestComments { get; set; }
    }

    public class AssetApproverPendingList
    {        
        public bool Select {  get; set; }
        public int EmpID { get; set; }

        [DisplayName("Employee Code")]
        public string EmpCode { get; set; }
        
        [DisplayName("Employee Name")] 
        public string EmpName { get; set; }
        
        [DisplayName("Designation")] 
        public string DesignationTitle { get; set; }
        
        [DisplayName("Department")] 
        public string DepartmentTitle { get; set; }

        public int AssetCatMasID { get; set; }

        [DisplayName("Asset CategoryName")]
        public string AssetCategoryName { get; set; }

        public int AssetID { get; set; }
        
        [DisplayName("Asset Code")] 
        public string AssetCode { get; set; }

        [DisplayName("Asset Name")] 
        public string AssetName { get; set; }
        public int AssetRequestID {  set; get; }

        [DisplayName("Asset Request Code")] 
        public string AssetRequestCode { set; get; }

        [DisplayName("Asset Request Date")]
        public DateTime AssetRequestDate { set; get; }
        
        public int AssetRequestByID { set; get; }

        [DisplayName("Comments")] 
        public string AssetRequestComments { set; get; }
        
        [DisplayName("First Approver Status")] 
        public bool AssetRequestByStatus { get; set; }
        public string RequestedTo { set; get; }
        public string RequestedDate { set; get; }
        public string RequestedToComments { set; get; }
        
        [DisplayName("Second Approver Status")] 
        public string AssetRequestStatus { set; get; }

        [DisplayName("Status")] 
        public string RequestStatus { set; get; }
        public int ClientID { get; set; }
    }

    public class  SelectedAssetInfo
    {
        public int AssetID { get; set; }
        
        [DisplayName("Asset Code")] 
        public string AssetCode { get; set; }
        
        [DisplayName("Asset Name")] 
        public string AssetName { get; set; }

        [DisplayName("Quantity")] 
        public decimal TotalQuantity { get; set; }

        [DisplayName("Balance Quantity")] 
        public decimal OutstandingQuantity { get; set; }
    }

    public class ApprovedAssetInformationRequestList
    {
        
        public int AssetRequestID { get; set; }
        public int AssetCatMasID { get; set; }
        
        [DisplayName("Category Code")] 
        public string CatAssetCode { get; set; }
        
        [DisplayName("Category Name")] 
        public string CatAssetName { get; set; }
        public int AssetID { get; set; }
        
        [DisplayName("Asset Code")]
        public string AssetCode { get; set; }

        [DisplayName("Asset Name")]
        public string AssetName { get; set; }

        [DisplayName("Asset Request Code")] 
        public string AssetRequestCode { set; get; }

        [DisplayName("Request Date")] 
        public DateTime AssetRequestDate { set; get; }

        [DisplayName("Description")] 
        public string AssetRequestComments { set; get;}

        [DisplayName("Request Status")] 
        public string RequestStatus {  get; set; }
        public int EmpID { get; set; }

        [DisplayName("Employee Code")]
        public string EmpCode { get; set; }

        [DisplayName("Employee Name")]
        public string EmpName { get; set; }

        [DisplayName("Designation")]
        public string DesignationTitle { get; set; }

        [DisplayName("Department")]
        public string DepartmentTitle { get; set; }

        public int PersonalInfoID { get; set; }
    }

    public class AssetInformationOfAnAssetRequest
    {
        public int EmpAssetAllocationID { get; set; }
        public int AssetCatMasID { get; set; }
        
        [DisplayName("Category")] 
        public string AssetCategoryName { get; set; }
        public int AssetID { get; set; }
        
        [DisplayName("Asset Code")] 
        public string AssetCode { get; set; }

        [DisplayName("Asset Name")] 
        public string AssetName { get; set; }
        public int AssetMasMoreDetID { get; set; }

        [DisplayName("Serial Number")] 
        public string SerialNumber { get; set; }

        [DisplayName("Asset Model Number")] 
        public string ModelNumber { get; set; }

        [DisplayName("Asset Tag")] 
        public string AssetTag { get; set; }

        [DisplayName("Asset Status")] 
        public string CurrentAssetStatusName { get; set; }
        public int AssetRequestID { get; set; }
        public int AssetRegID { get; set; }
        
        [DisplayName("Available Quantity")] 
        public decimal OBalance { get; set; }

        [DisplayName("Requested Quantity")] 
        public decimal DrBalance { get; set; }

        [DisplayName("Balance Quantity")] 
        public decimal CBalance { get; set; }

        [DisplayName("Asset TR Type")]
        public string TRType { get; set; }

        [DisplayName("Request Description")] 
        public string Comments { get; set; }

        [DisplayName("Issue Date")] 
        public DateTime IssueDate { get; set; }

        [DisplayName("Issue Condition")] 
        public string IssueCondition { get; set; }

        [DisplayName("Remarks")]
        public string IssueRemarks { get; set; }
    }

    public class AssetAnalyticsInformation
    {
        public int EmpID { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string DepartmentTitle { get; set; }
        public string DesignationTitle { get; set; }
        public int AssetCatMasID { get; set; }
        public string CatAssetCode { get; set; }
        public string CatAssetName { get; set; }
        public int AssetID { get; set; }
        public string AssetCode { get; set; }
        public string AssetName { get; set; }
        public decimal TotalQuantity {  get; set; }
        public decimal OutstandingQuantity { get; set; }
        public decimal IssuedQuantity { get; set; }
        public DateTime IssueDate { get; set; }
        public string IssueCondition { get; set; }
        public string IssueRemarks { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
