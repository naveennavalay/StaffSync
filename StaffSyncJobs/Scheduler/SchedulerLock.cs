using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffSyncJobs.Scheduler
{
    public static class SchedulerLock
    {
        public static readonly object DatabaseLock = new object();
    }
}
