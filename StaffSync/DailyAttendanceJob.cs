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
            //MessageBox.Show($"Daily Attendance Job executed at: {DateTime.Now}", "Quartz Job", MessageBoxButtons.OK, MessageBoxIcon.Information);




            return Task.CompletedTask;
        }
    }
}
