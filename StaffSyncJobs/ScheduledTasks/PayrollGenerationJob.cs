using Quartz;
using StaffSyncJobs.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffSyncJobs.ScheduledTasks
{
    public class PayrollGenerationJob : BaseScheduledJob
    {
        protected override async Task ExecuteJob(IJobExecutionContext context)
        {
            Console.WriteLine("Birthday Job Executed");

            await Task.CompletedTask;
        }
    }
}
