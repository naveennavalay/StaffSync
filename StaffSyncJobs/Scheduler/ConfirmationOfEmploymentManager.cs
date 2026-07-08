using ModelStaffSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffSyncJobs.Scheduler
{
    public class ConfirmationOfEmploymentManager
    {
        public async Task Execute()
        {
            DALStaffSync.clsEmployeeMaster objEmployeeMaster = new DALStaffSync.clsEmployeeMaster();

            List<ConfirmationOfEmploymentInfo> objConfirmationOfEmploymentList = objEmployeeMaster.GetEmployeeConfirmationOfEmploymentList();
            if (objConfirmationOfEmploymentList != null || objConfirmationOfEmploymentList.Count > 0)
            {
                DALStaffSync.clsAppMailTemplate objMailTemplate = new DALStaffSync.clsAppMailTemplate();
                AppMailTemplateModel objTemplate = objMailTemplate.GetSpecificAppMailTemplateByName("ConfirmationOfEmployment");

                foreach (ConfirmationOfEmploymentInfo indConfirmationOfEmploymentInfo in objConfirmationOfEmploymentList)
                {
                    objTemplate.AppMailTempBodyHTML = objTemplate.AppMailTempBodyHTML.Replace("{EmployeeCode}", indConfirmationOfEmploymentInfo.EmpCode);
                    objTemplate.AppMailTempBodyHTML = objTemplate.AppMailTempBodyHTML.Replace("{EmployeeName}", indConfirmationOfEmploymentInfo.EmpName);
                    objTemplate.AppMailTempBodyHTML = objTemplate.AppMailTempBodyHTML.Replace("{Designation}", indConfirmationOfEmploymentInfo.DesignationTitle);
                    objTemplate.AppMailTempBodyHTML = objTemplate.AppMailTempBodyHTML.Replace("{Department}", indConfirmationOfEmploymentInfo.DepartmentTitle);
                    objTemplate.AppMailTempBodyHTML = objTemplate.AppMailTempBodyHTML.Replace("{JoiningDate}", indConfirmationOfEmploymentInfo.DOJ.ToString("dd-MMM-yyyy"));
                    objTemplate.AppMailTempBodyHTML = objTemplate.AppMailTempBodyHTML.Replace("{ConfirmationDate}", indConfirmationOfEmploymentInfo.DOJ.ToString("dd-MMM-yyyy"));

                    Console.WriteLine($"Sending Confirmation Of Employment email to {indConfirmationOfEmploymentInfo.EmpName} ({indConfirmationOfEmploymentInfo.EmpCode})");
                }
            }

            await Task.CompletedTask;
        }
    }
}
