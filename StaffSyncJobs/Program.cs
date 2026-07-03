using DALStaffSync;
using ModelStaffSync;
using Quartz;
using Quartz.Impl;
using StaffSyncJobs;
using StaffSyncJobs.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffSyncJobs
{
    public class Program
    {
        static void Main(string[] args)
        {
            SchedulerManager.Start().Wait();

            Console.WriteLine("Quartz Scheduler Started...");
            Console.WriteLine("Press ENTER to exit.");

            Console.ReadLine();

            SchedulerManager.Stop().Wait();
        }
    }
}


