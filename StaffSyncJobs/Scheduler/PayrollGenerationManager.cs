using ModelStaffSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffSyncJobs.Scheduler
{
    public class PayrollGenerationManager
    {
        public async Task Execute()
        {
            //DALStaffSync.clsEmployeeMaster objEmployeeMaster = new DALStaffSync.clsEmployeeMaster();

            //List<EmployeeBirthdayInfo> objEmployeeBirthdayList = objEmployeeMaster.GetEmployeeBirthdayList();
            //if (objEmployeeBirthdayList.Count > 0)
            //{
            //    DALStaffSync.clsAppMailTemplate objMailTemplate = new DALStaffSync.clsAppMailTemplate();
            //    AppMailTemplateModel objTemplate = objMailTemplate.GetSpecificAppMailTemplateByName("EmployeeBirthday");

            //    foreach (EmployeeBirthdayInfo indEmployeeBirthdayInfo in objEmployeeBirthdayList)
            //    {
            //        objTemplate.AppMailTempBodyHTML = objTemplate.AppMailTempBodyHTML.Replace("{EmployeeCode}", indEmployeeBirthdayInfo.EmpCode);
            //        objTemplate.AppMailTempBodyHTML = objTemplate.AppMailTempBodyHTML.Replace("{EmployeeName}", indEmployeeBirthdayInfo.EmpName);
            //        objTemplate.AppMailTempBodyHTML = objTemplate.AppMailTempBodyHTML.Replace("{Designation}", indEmployeeBirthdayInfo.DesignationTitle);
            //        objTemplate.AppMailTempBodyHTML = objTemplate.AppMailTempBodyHTML.Replace("{Department}", indEmployeeBirthdayInfo.DepartmentTitle);

            //        Console.WriteLine($"Sending birthday wish to {indEmployeeBirthdayInfo.EmpName} ({indEmployeeBirthdayInfo.EmpCode})");
            //    }
            //}

            await Task.CompletedTask;
        }
    }
}
