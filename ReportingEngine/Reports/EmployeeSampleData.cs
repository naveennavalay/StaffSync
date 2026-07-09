using ReportingEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Reports
{
    public static class EmployeeSampleData
    {
        public static List<EmployeeInfo> Get()
        {
            return new List<EmployeeInfo>
            {
                new EmployeeInfo{EmployeeCode="EMP001",EmployeeName="Naveen Navale",Department="Development",Designation="Technical Specialist",Status="Active"},
                new EmployeeInfo{EmployeeCode="EMP002",EmployeeName="Amit Sharma",Department="HR",Designation="HR Executive",Status="Active"},
                new EmployeeInfo{EmployeeCode="EMP003",EmployeeName="Sneha Rao",Department="Finance",Designation="Accountant",Status="Active"}
            };
        }
    }
}
