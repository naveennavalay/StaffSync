using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaffSync
{
    public class DailyAttendanceJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            //Mehttps://www.tickertape.in/screener/equityssageBox.Show($"Daily Attendance Job executed at: {DateTime.Now}", "Quartz Job", MessageBoxButtons.OK, MessageBoxIcon.Information);

            DALStaffSync.clsLeaveTRList objLeaveTRList = new DALStaffSync.clsLeaveTRList();

            return Task.CompletedTask;
        }
    }
}
