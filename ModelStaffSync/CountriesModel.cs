using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class CountriesModel
    {
        public int CountryID { get; set; }
        public string CountryCode { get; set; }

        public string CountryTitle { get; set; }
        public string CountryInitial { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
