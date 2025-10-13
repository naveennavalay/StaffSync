using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffSyncJobs
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            //// Create a scheduler factory and get a scheduler instance
            //IScheduler scheduler = await new StdSchedulerFactory().GetScheduler();
            //await scheduler.Start();

            //// Define the job and associate it with MyJob class
            //IJobDetail job = JobBuilder.Create<StaffSyncLeaveJobs>()
            //    .WithIdentity("myJob", "group1")
            //    .UsingJobData("EmpID", "0")
            //    .UsingJobData("LeaveTypeID", "0")
            //    .UsingJobData("LeaveActionType", "0")
            //    .UsingJobData("LeaveDateFrom", "")
            //    .UsingJobData("LeaveDateTo", "")
            //    .UsingJobData("LeaveDuration", "0")
            //    .UsingJobData("LeaveDurationType", "")

            //    //.UsingJobData("message", "Hello from Quartz.NET!")
            //    //.UsingJobData("value", 3.141f)
            //    .Build();

            //// Create a trigger that fires every 10 seconds
            //ITrigger trigger = TriggerBuilder.Create()
            //    .WithIdentity("myTrigger", "group1")
            //    .StartNow()
            //    .WithSimpleSchedule(x => x
            //        .WithIntervalInMinutes(1)
            //        .RepeatForever())
            //    .Build();

            //// Schedule the job with the trigger
            //await scheduler.ScheduleJob(job, trigger);

            //// Wait for user input to close the application
            //Console.WriteLine("Press any key to exit...");
            Console.ReadLine();

            //// Shutdown the scheduler
            //await scheduler.Shutdown();
        }
    }
}
