using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class ProfessionalTax
    {
        public int? PTDSlabID { get; set; }        
        public decimal? GrossFrom { get; set; }
        public decimal? GrossTo { get; set; }
        public decimal? PTAmount { get; set; }
        public int? MonthNumber { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string PTNote { get; set; }
        public decimal? MaxPTAmount { get; set; }
        public int OrderID { get; set; }
        public int StateID { get; set; }
        public int DedID { get; set; }
        public int ClientID { get; set; }
    }
}
