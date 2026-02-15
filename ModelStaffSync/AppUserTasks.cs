using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class AppUserTasks
    {
        public int TaskID { get; set; }
        public int TaskSourceID { get; set; }
        public DateTime TaskDate { get; set; }
        public int TaskRaisedByID { get; set; }
        public int TaskOwnerID { get; set; }
        public string TaskDescription { get; set; }
        public string TaskStatus { get; set; }
        public DateTime TaskCompletionDate { get; set; }
        public int OrderID { get; set; }
        public string TaskType { get; set; }
    }
}
