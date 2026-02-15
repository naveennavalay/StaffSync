using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class ClientStatutory
    {
        public int ClientStatutoryID { get; set; }

        public int ClientID { get; set; }

        public DateTime EffectiveDate { get; set; }

        public bool EnableClientStatutory { get; set; }

        public bool EnablePF { get; set; }

        public string PFCode { get; set; }

        public bool EnablePT { get; set; }

        public string PTCode { get; set; }

        public bool EnableESI { get; set; }

        public string ESICode { get; set; }

        public bool EnableNPS { get; set; }

        public string NPSCode { get; set; }

        public int OrderID { get; set; }
    }
}
