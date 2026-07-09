using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Models
{
    public static class SampleData
    {
        public static List<SampleEmployee> GetEmployees()
        {
            return new List<SampleEmployee>
            {
                new SampleEmployee{SrNo=1,EmployeeCode="EMP-0001",EmployeeName="Naveen Navale",Department="Development",Designation="Technical Specialist"},
                new SampleEmployee{SrNo=2,EmployeeCode="EMP-0002",EmployeeName="John Smith",Department="HR",Designation="HR Manager"},
                new SampleEmployee{SrNo=3,EmployeeCode="EMP-0003",EmployeeName="David Miller",Department="Finance",Designation="Accountant"},
                new SampleEmployee{SrNo=4,EmployeeCode="EMP-0004",EmployeeName="Rahul Patil",Department="Testing",Designation="QA Engineer"}
            };
        }
    }
}
