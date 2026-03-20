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
    public class AuditLogs
    {
        [DisplayName("Audit Log Statement ID")]
        public int UserAuditLogID { get; set; }

        [DisplayName("Source ID")]
        public int SourceID { get; set; }

        [DisplayName("Audit Date")]
        public DateTime EventDateTime { get; set; }

        [DisplayName("Audit Statements")]
        public string AuditLogStatement { get; set; }

        [DisplayName("Change Type")]
        public string ActionType { get; set; }

        [DisplayName("Updated by")]
        public string UserName { get; set; }

        [DisplayName("Audit Event Group Type")]
        public string EventGroup { get; set; }

        [DisplayName("ClientID")]
        public int ClientID { get; set; }
    }

}
