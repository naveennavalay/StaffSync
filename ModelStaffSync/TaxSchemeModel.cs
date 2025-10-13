using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class TaxSchemeModel
    {
        public int TaxSchemeID { get; set; }
        public string TaxCode { get; set; }
        public string TaxTitle { get; set; }
        public string TaxInitial { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class EmpTaxSchemeInfo
    {
        public int EmpTaxSchemeID { get; set; }
        public int EmpID { get; set; }
        public int TaxSchemeID { get; set; }
        public DateTime EffectiveDate { get; set; }

    }

    public class TaxSchemeInfo
    {
        public int TaxSchemeID { get; set; }
        public string TaxCode { get; set; }
        public string TaxTitle { get; set; }
        public string TaxInitial { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }


    }
}
