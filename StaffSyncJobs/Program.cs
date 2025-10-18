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
            DALStaffSync.clsEmpMnthlyAttdInfo objEmpMnthlyAttdInfo = new DALStaffSync.clsEmpMnthlyAttdInfo();



            while (true)
            {
                Console.Write("Enter Attendance Date (or type 'exit' to close): ");
                string input = Console.ReadLine();

                if (input.Trim().ToLower() == "exit")
                {
                    Console.WriteLine("Exiting the application. Goodbye!");
                    break;
                }

                if (DateTime.TryParse(input, out DateTime parsedDate))
                {
                    Console.WriteLine($"You entered a valid date: {parsedDate.ToShortDateString()}");

                    int PresentCounter = 0;
                    int LeaveCounter = 0;
                    int FirstHalfLeaveCounter = 0;
                    int SecondHalfLeaveCounter = 0;

                    DateTime dtSelectedMonth = parsedDate; //DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 1, 1, 1);

                    int selectedMonth = parsedDate.Month;

                    int MonthlyAttendanceSlNumber = 0;

                    List<ReportingManagerInfo> lstEmployeesList = objEmployeeMaster.getCompleteEmployeesList();
                    foreach (ReportingManagerInfo indReportingManagerInfo in lstEmployeesList)
                    {
                        //Console.WriteLine($"Employee ID: { indReportingManagerInfo.EmpID }, Name: { indReportingManagerInfo.EmpName }, Designation : { indReportingManagerInfo.DesignationTitle }, Department : { indReportingManagerInfo.DepartmentTitle }");

                        List<EmployeeAttendanceInfo> objEmployeeAttendanceList = objAttendanceMas.GetDefaultEmployeeAttendanceInfo(Convert.ToInt16(indReportingManagerInfo.EmpID.ToString()), dtSelectedMonth);
                        if (objEmployeeAttendanceList.Count > 0)
                        {
                            foreach (EmployeeAttendanceInfo indEmployeeAttendanceInfo in objEmployeeAttendanceList)
                            {
                                string strDayAttendance = "";
                                //strDayAttendance = indEmployeeAttendanceInfo.AttStatus == "Present" ? "Present" : "Leave";

                                strDayAttendance = indEmployeeAttendanceInfo.AttStatus;
                                if (indEmployeeAttendanceInfo.AttStatus == "Present")
                                {
                                    strDayAttendance = "Present";
                                    PresentCounter = PresentCounter + 1;
                                }
                                else if (indEmployeeAttendanceInfo.AttStatus == "Leave : Full Day" || indEmployeeAttendanceInfo.AttStatus == "Leave")
                                {
                                    strDayAttendance = indEmployeeAttendanceInfo.AttStatus; //"Leave : Full Day";
                                    LeaveCounter = LeaveCounter + 1;
                                }
                                else if (indEmployeeAttendanceInfo.AttStatus == "Leave : First Half")
                                {
                                    strDayAttendance = indEmployeeAttendanceInfo.AttStatus;
                                    FirstHalfLeaveCounter = FirstHalfLeaveCounter + 1;
                                }
                                else if (indEmployeeAttendanceInfo.AttStatus == "Leave : Second Half")
                                {
                                    strDayAttendance = indEmployeeAttendanceInfo.AttStatus;
                                    SecondHalfLeaveCounter = SecondHalfLeaveCounter + 1;
                                }

                                MonthlyAttendanceSlNumber = objEmpMnthlyAttdInfo.getMonthlyAttendanceInfo(indEmployeeAttendanceInfo.EmpID, dtSelectedMonth);
                                if (MonthlyAttendanceSlNumber == 0)
                                {
                                    MonthlyAttendanceSlNumber = objEmpMnthlyAttdInfo.InsertMonthlyAttendanceInfo(indEmployeeAttendanceInfo.EmpID, dtSelectedMonth);
                                }
                                else
                                {
                                    MonthlyAttendanceSlNumber = objEmpMnthlyAttdInfo.UpdateMonthlyAttendanceInfo(MonthlyAttendanceSlNumber, indEmployeeAttendanceInfo.EmpID, dtSelectedMonth, "Day" + indEmployeeAttendanceInfo.AttDate.Day, strDayAttendance);
                                    int DailyAttendanceID = objAttendanceInfo.UpdateDailyAttendance(indReportingManagerInfo.EmpID, Convert.ToDateTime(dtSelectedMonth.Date.ToString()), strDayAttendance, Convert.ToInt16(indEmployeeAttendanceInfo.LeaveTRID));
                                    if (DailyAttendanceID == 0)
                                        DailyAttendanceID = objAttendanceInfo.InsertDailyAttendance(indReportingManagerInfo.EmpID, Convert.ToDateTime(dtSelectedMonth.Date.ToString()), strDayAttendance, Convert.ToInt16(indEmployeeAttendanceInfo.LeaveTRID));
                                }
                            }
                        }
                        else
                        {
                            MonthlyAttendanceSlNumber = objEmpMnthlyAttdInfo.getMonthlyAttendanceInfo(Convert.ToInt16(indReportingManagerInfo.EmpID.ToString()), dtSelectedMonth);
                            if (MonthlyAttendanceSlNumber == 0)
                            {
                                MonthlyAttendanceSlNumber = objEmpMnthlyAttdInfo.InsertMonthlyAttendanceInfo(indReportingManagerInfo.EmpID, dtSelectedMonth);
                                objEmpMnthlyAttdInfo.UpdateMonthlyAttendanceInfo(MonthlyAttendanceSlNumber, indReportingManagerInfo.EmpID, dtSelectedMonth, "Day" + dtSelectedMonth.Day, "Present");
                                objAttendanceInfo.InsertDailyAttendance(indReportingManagerInfo.EmpID, Convert.ToDateTime(dtSelectedMonth.Date.ToString()), "Present", 0);
                            }
                            else
                            {
                                objEmpMnthlyAttdInfo.UpdateMonthlyAttendanceInfo(MonthlyAttendanceSlNumber, indReportingManagerInfo.EmpID, dtSelectedMonth, "Day" + dtSelectedMonth.Day, "Present");
                                int DailyAttendanceID = objAttendanceInfo.UpdateDailyAttendance(indReportingManagerInfo.EmpID, Convert.ToDateTime(dtSelectedMonth.Date.ToString()), "Present", 0);
                                if (DailyAttendanceID == 0)
                                    objAttendanceInfo.InsertDailyAttendance(indReportingManagerInfo.EmpID, Convert.ToDateTime(dtSelectedMonth.Date.ToString()), "Present", 0);
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please try again.");
                }

                Console.WriteLine();
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
