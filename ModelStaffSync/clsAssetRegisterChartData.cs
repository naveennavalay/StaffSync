using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class AssetRegisterChartData
    {
        public int AssetCatMasID { get; set; }
        public string AssetCatCode { get; set; }
        public string AssetCatName { get; set; }
        public string AssetCatDesc { get; set; }
        public int AssetID { get; set; }
        public string AssetCode { get; set; }
        public string AssetName { get; set; }
        public string CurrentAssetStatusName { get; set; }
        public string SerialNumber { get; set; }
        public string ModelNumber { get; set; }
        public string ManufacturerInfo { get; set; }
        public string AssetTag { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public decimal? PurchaseValue { get; set; } = 0;
        public string VendorName { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? WarrantyStartDate { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
        public bool HasWarranty { get; set; }
        public DateTime? NextServiceDate { get; set; }
        public string Location { get; set; }
        public decimal? TotalQuantity { get; set; } = 0;
        public decimal? OutstandingQuantity { get; set; } = 0;
        public int EmpID { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string DesignationTitle { get; set; }
        public string DepartmentTitle { get; set; }
        public int ClientID { get; set; }
    }
}
