using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StaffSyncJobs
{
    public class StaffSyncLeaveJobs : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            // Retrieve the JobDataMap
            JobDataMap dataMap = context.MergedJobDataMap;

            string EmpID = "";
            string LeaveTypeID = "";
            string LeaveActionType = "";    //Approved/Rejected/Pending
            string LeaveDateFrom = "";
            string LeaveDateTo = "";
            string LeaveDuration = "";
            string LeaveDurationType = "";


            if (dataMap.ContainsKey("EmpID"))
            {
                EmpID = dataMap.GetString("EmpID");
            }
            if (dataMap.ContainsKey("LeaveTypeID"))
            {
                LeaveTypeID = dataMap.GetString("LeaveTypeID");
            }
            if (dataMap.ContainsKey("LeaveActionType"))
            {
                LeaveActionType = dataMap.GetString("LeaveActionType");
            }
            if (dataMap.ContainsKey("LeaveDateFrom"))
            {
                LeaveDateFrom = dataMap.GetString("LeaveDateFrom");
            }
            if (dataMap.ContainsKey("LeaveDateTo"))
            {
                LeaveDateTo = dataMap.GetString("LeaveDateTo");
            }
            if (dataMap.ContainsKey("LeaveDuration"))
            {
                LeaveDuration = dataMap.GetString("LeaveDuration");
            }
            if (dataMap.ContainsKey("LeaveDurationType"))
            {
                LeaveDurationType = dataMap.GetString("LeaveDurationType");
            }



            // Access data passed to the job
            //string message = dataMap.GetString("message");
            //float value = dataMap.GetFloat("value");
            //IList<DateTimeOffset> timestamps = (IList<DateTimeOffset>)dataMap["timestamps"];
            //timestamps.Add(DateTimeOffset.UtcNow);

            // Output the retrieved data
            //Console.WriteLine($"Job says: {message}, Value: {value}");
            //Console.WriteLine("Timestamps:");
            //foreach (var timestamp in timestamps)
            //{
            //    Console.WriteLine(timestamp);
            //}

            // Simulate async work (optional)

            Debug.WriteLine("Leave Updated for the EmpID : " + EmpID);

            Console.WriteLine("Leave Updated for the EmpID : " + EmpID);
            await Task.CompletedTask;
        }
    }
}
