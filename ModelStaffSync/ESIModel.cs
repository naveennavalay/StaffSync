using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class ESIModel
    {
        public int ESIMasID { get; set; }
        public string ESIMasTitle { get; set; }
        public int ESIDetID { get; set; }
        public string EmpESIPercentageOrAmount { get; set; }
        public decimal EmpESIPercentage { get; set; }
        public decimal EmpESIAmount { get; set; }
        public string EmprESIPercentageOrAmount { get; set; }
        public decimal EmprPercentage { get; set; }
        public decimal EmprESIAmount { get; set; }
        public DateTime EffectiveDate { get; set; }
        public int OrderID { get; set; }
        public int DedID { get; set; }
        public int ClientID { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public decimal MaxESIAmount { get; set; }
    }
}
