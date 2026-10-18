using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class clsEmployeeWeeklyOffInfo
    {

    }

    public class EmployeeWeeklyOffInfo
    {
        public int WklyOffDay { get; set; }

        [DisplayName("Day Name")]
        public string WeeklyOffDayName { get; set; }

        public int EmpID { get; set; }
    }

}
