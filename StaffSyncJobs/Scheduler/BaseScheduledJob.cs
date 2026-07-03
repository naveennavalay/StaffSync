using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffSyncJobs.Scheduler
{
    public abstract class BaseScheduledJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Stopwatch sw = new Stopwatch();

            SchedulerExecutionManager executionManager = new SchedulerExecutionManager();

            sw.Start();

            try
            {
                executionManager.JobStarted(context);

                await ExecuteJob(context);

                sw.Stop();

                executionManager.JobCompleted(context);

                Console.WriteLine("Execution Time : " + sw.Elapsed);
            }
            catch (Exception ex)
            {
                sw.Stop();

                executionManager.JobFailed(context, ex);

                Console.WriteLine("Execution Time : " + sw.Elapsed);

                throw;
            }
            finally
            {
                Console.ResetColor();

                Console.WriteLine("======================================================");
            }
        }

        protected abstract Task ExecuteJob(IJobExecutionContext context);
    }
}
