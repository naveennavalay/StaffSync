using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class EmployeeDashboardConfigModel
    {
        [DisplayName("DBChartID")]
        public int DBChartID { get; set; }

        [DisplayName("PersonalInfoID")]
        public int PersonalInfoID { get; set; }

        [DisplayName("Dashboard Chart Title")]
        public string DBChartTitle { get; set; }

        [DisplayName("Dashboard Short Title")] 
        public string DBChartShortTitle { get; set; }

        [DisplayName("UIChartID")] 
        public string UIChartID { get; set; }

        [DisplayName("EmpDBChartID")] 
        public int EmpDBChartID { get; set; }

        [DisplayName("Show / Hide")]
        public bool DBChartEnabled { get; set; }

        [DisplayName("OrderID")]
        public int OrderID { get; set; }
    }
}
