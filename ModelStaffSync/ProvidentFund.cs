using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class ProvidentFund
    {
        public int PFMasID { get; set; }
        public string PFMasTitle { get; set; }
        public int PFDetID { get; set; }
        public string EmpPFPercentageOrAmount { get; set; }
        public decimal EmpPFPercentage { get; set; }
        public decimal EmpPFAmount { get; set; }
        public string EmprPFPercentageOrAmount { get; set; }
        public decimal EmprPFPercentage { get; set; }
        public decimal EmprPFAmount { get; set; }
        public string EmprPSPercentageOrAmount { get; set; }
        public decimal EmprPSPercentage { get; set; }
        public decimal EmprPSAmount { get; set; }
        public DateTime EffectiveDate { get; set; }
        public int OrderID { get; set; }
        public int DedID { get; set; }
        public int ClientID { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
