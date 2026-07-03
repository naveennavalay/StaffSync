using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class SchedulerJobMaster
    {
        public int JobID { get; set; }
        public string JobCode { get; set; }
        public string JobName { get; set; }
        public string ClassName { get; set; }
        public string Description { get; set; }
        public bool IsSystemJob { get; set; }
        public bool IsActive { get; set; }
    }
}
