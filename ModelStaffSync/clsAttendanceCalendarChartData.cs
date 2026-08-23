using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class AttendanceCalendarMonthData
    {
        public string AttendanceMonth { get; set; }

        public DateTime? AttendanceDate { get; set; }

        public string Department { get; set; }

        public double TotalPresent { get; set; }

        public double TotalLeave { get; set; }

        public double TotalLOP { get; set; }

        public double TotalAttendance
        {
            get
            {
                return TotalPresent + TotalLeave + TotalLOP;
            }
        }

        public double PresentPercentage { get; set; }

        public double LeavePercentage { get; set; }

        public double LOPPercentage { get; set; }
    }


    public class AttendanceCalendarChartData
    {
        public string AttendanceMonth { get; set; }

        public DateTime? AttendanceDate { get; set; }

        public string Department { get; set; }

        public double TotalPresent { get; set; }

        public double TotalLeave { get; set; }

        public double TotalLOP { get; set; }

        public double TotalAttendance
        {
            get
            {
                return TotalPresent + TotalLeave + TotalLOP;
            }
        }

        public double PresentPercentage { get; set; }

        public double LeavePercentage { get; set; }

        public double LOPPercentage { get; set; }
    }

    public class AttendanceCalendarDateData
    {
        public string AttendanceMonth { get; set; }

        public DateTime? AttendanceDate { get; set; }

        public string Department { get; set; }

        public double TotalPresent { get; set; }

        public double TotalLeave { get; set; }

        public double TotalLOP { get; set; }

        public double TotalAttendance
        {
            get
            {
                return TotalPresent + TotalLeave + TotalLOP;
            }
        }

        public double PresentPercentage { get; set; }

        public double LeavePercentage { get; set; }

        public double LOPPercentage { get; set; }
    }


    public class AttendanceCalendarDepartmentData
    {
        public string AttendanceMonth { get; set; }

        public DateTime? AttendanceDate { get; set; }

        public string Department { get; set; }

        public double TotalPresent { get; set; }

        public double TotalLeave { get; set; }

        public double TotalLOP { get; set; }

        public double TotalAttendance
        {
            get
            {
                return TotalPresent + TotalLeave + TotalLOP;
            }
        }

        public double PresentPercentage { get; set; }

        public double LeavePercentage { get; set; }

        public double LOPPercentage { get; set; }
    }
}
