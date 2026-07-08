using ModelStaffSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffSyncJobs.Scheduler
{
    public class WorkAnniversaryManager
    {
        public async Task Execute()
        {
            DALStaffSync.clsEmployeeMaster objEmployeeMaster = new DALStaffSync.clsEmployeeMaster();

            List<EmployeeWorkAnniversaryInfo> objEmployeeWorkAnniversaryList = objEmployeeMaster.GetEmployeeAnniversaryList();
            if (objEmployeeWorkAnniversaryList != null || objEmployeeWorkAnniversaryList.Count > 0)
            {
                DALStaffSync.clsAppMailTemplate objMailTemplate = new DALStaffSync.clsAppMailTemplate();
                AppMailTemplateModel objTemplate = objMailTemplate.GetSpecificAppMailTemplateByName("HappyWorkAnniversary");

                foreach (EmployeeWorkAnniversaryInfo indEmployeeWorkAnniversaryInfo in objEmployeeWorkAnniversaryList)
                {
                    objTemplate.AppMailTempBodyHTML = objTemplate.AppMailTempBodyHTML.Replace("{EmployeeCode}", indEmployeeWorkAnniversaryInfo.EmpCode);
                    objTemplate.AppMailTempBodyHTML = objTemplate.AppMailTempBodyHTML.Replace("{EmpName}", indEmployeeWorkAnniversaryInfo.EmpName);
                    objTemplate.AppMailTempBodyHTML = objTemplate.AppMailTempBodyHTML.Replace("{DOJ}", indEmployeeWorkAnniversaryInfo.DOJ.ToString("dd-MMM-yyyy"));
                    objTemplate.AppMailTempBodyHTML = objTemplate.AppMailTempBodyHTML.Replace("{Designation}", indEmployeeWorkAnniversaryInfo.DesignationTitle);
                    objTemplate.AppMailTempBodyHTML = objTemplate.AppMailTempBodyHTML.Replace("{Department}", indEmployeeWorkAnniversaryInfo.DepartmentTitle);

                    DateTime joiningDate = indEmployeeWorkAnniversaryInfo.DOJ;
                    DateTime today = DateTime.Today;

                    int yearsCompleted = today.Year - joiningDate.Year;

                    if (joiningDate.Date > today.AddYears(-yearsCompleted))
                    {
                        yearsCompleted--;
                    }

                    objTemplate.AppMailTempBodyHTML = objTemplate.AppMailTempBodyHTML.Replace("{YearsCompleted}", yearsCompleted.ToString());

                    Console.WriteLine($"Sending work anniversary email to {indEmployeeWorkAnniversaryInfo.EmpName} ({indEmployeeWorkAnniversaryInfo.EmpCode})");
                }
            }

            await Task.CompletedTask;
        }
    }
}
