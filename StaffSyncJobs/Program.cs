using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALStaffSync;
using ModelStaffSync;

namespace StaffSyncJobs
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            DALStaffSync.clsEmployeeMaster objEmployeeMaster = new DALStaffSync.clsEmployeeMaster();
            DALStaffSync.clsAttendanceMas objAttendanceMas = new DALStaffSync.clsAttendanceMas();
            DALStaffSync.clsLeaveTRList objLeaveTRList = new DALStaffSync.clsLeaveTRList();
            DALStaffSync.clsAttendanceMas objAttendanceInfo = new DALStaffSync.clsAttendanceMas();

            int PresentCounter = 0;
            int LeaveCounter = 0;
            int FirstHalfLeaveCounter = 0;
            int SecondHalfLeaveCounter = 0;

            DateTime dtSelectedMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 1, 1, 1);

            int selectedMonth = DateTime.Now.Month;


            List<ReportingManagerInfo> lstEmployeesList = objEmployeeMaster.getCompleteEmployeesList();
            foreach (ReportingManagerInfo indReportingManagerInfo in lstEmployeesList)
            {
                Console.WriteLine($"Employee ID: { indReportingManagerInfo.EmpID }, Name: { indReportingManagerInfo.EmpName }, Designation : { indReportingManagerInfo.DesignationTitle }, Department : { indReportingManagerInfo.DepartmentTitle }");

                List<EmployeeAttendanceInfo> objEmployeeAttendanceList = objAttendanceMas.GetDefaultEmployeeAttendanceInfo(Convert.ToInt16(indReportingManagerInfo.EmpID.ToString()), dtSelectedMonth.Month);
                if (objEmployeeAttendanceList.Count > 0)
                {
                    foreach (EmployeeAttendanceInfo indEmployeeAttendanceInfo in objEmployeeAttendanceList)
                    {

                        string strDayAttendance = "";
                        //strDayAttendance = indEmployeeAttendanceInfo.AttStatus == "Present" ? "Present" : "Leave";

                        strDayAttendance = indEmployeeAttendanceInfo.AttStatus;
                        if (indEmployeeAttendanceInfo.AttStatus == "Present")
                        {
                            PresentCounter = PresentCounter + 1;
                        }
                        else if (indEmployeeAttendanceInfo.AttStatus == "Leave : Full Day" || indEmployeeAttendanceInfo.AttStatus == "Leave")
                        {
                            strDayAttendance = "Leave";
                            LeaveCounter = LeaveCounter + 1;
                        }
                        else if (indEmployeeAttendanceInfo.AttStatus == "Leave : First Half")
                        {
                            strDayAttendance = "First Half";
                            FirstHalfLeaveCounter = FirstHalfLeaveCounter + 1;
                        }
                        else if (indEmployeeAttendanceInfo.AttStatus == "Leave : Second Half")
                        {
                            strDayAttendance = "Second Half";
                            SecondHalfLeaveCounter = SecondHalfLeaveCounter + 1;
                        }
                    }
                }
                else
                {
                    //objLeaveTRList.UpdateEmployeeLeaveBalance(Convert.ToInt16(lblLeaveMasID.Text.ToString()), Convert.ToInt16(lblEmpID.Text.ToString()), Convert.ToDecimal(txtAvailableLeave.Text), (Convert.ToDecimal(txtBalanceLeave.Text) + Convert.ToDecimal(txtActualLeaveDays.Text)), DateTime.Now);
                    //objAttendanceInfo.InsertDailyAttendance(Convert.ToInt16(lblEmpID.Text.ToString()), Convert.ToDateTime(txtLeaveDateFrom.Text.ToString()), "Present", Convert.ToInt16(lblLeaveTRID.Text.ToString()));
                }
            }



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
