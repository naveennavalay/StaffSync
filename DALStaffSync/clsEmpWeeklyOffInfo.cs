using ModelStaffSync;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace DALStaffSync
{
    public class clsEmpWeeklyOffInfo
    {
        dbStaffSync.clsEmpWeeklyOffInfo objEmpWeeklyOffInfo = new dbStaffSync.clsEmpWeeklyOffInfo();

        public List<EmployeeWeeklyOffInfo> getEmployeeSpecificWeeklyOffInfo(int intEmpID)
        {
            return objEmpWeeklyOffInfo.getEmployeeSpecificWeeklyOffInfo(intEmpID);
        }
    }
}
