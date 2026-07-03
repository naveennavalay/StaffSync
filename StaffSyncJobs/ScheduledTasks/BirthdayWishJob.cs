using Quartz;
using StaffSyncJobs.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffSyncJobs.ScheduledTasks
{
    public class BirthdayWishJob : BaseScheduledJob
    {
        protected override async Task ExecuteJob(IJobExecutionContext context)
        {
            Console.WriteLine("Payroll Job Executed");

            await Task.CompletedTask;
        }
    }
}
