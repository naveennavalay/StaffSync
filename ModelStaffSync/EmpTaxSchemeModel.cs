using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class EmpTaxSchemeModel
    {
        public int EmpTaxSchemeID { get; set; }
        public int EmpID { get; set; }
        public int TaxSchemeID { get; set; }
        public DateTime EffectiveDate { get; set; }

    }
}
