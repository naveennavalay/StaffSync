using ModelStaffSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffSyncJobs.Scheduler
{
    public class ProbationCompletionManager
    {
        public async Task Execute()
        {
            DALStaffSync.clsEmployeeMaster objEmployeeMaster = new DALStaffSync.clsEmployeeMaster();

            List<EmployeeWorkProbationCompletionInfo> objEmployeeWorkProbationCompletionList = objEmployeeMaster.GetEmployeeProbationCompletionList();
            if (objEmployeeWorkProbationCompletionList != null || objEmployeeWorkProbationCompletionList.Count > 0)
            {
                DALStaffSync.clsAppMailTemplate objMailTemplate = new DALStaffSync.clsAppMailTemplate();
                AppMailTemplateModel objTemplate = objMailTemplate.GetSpecificAppMailTemplateByName("ProbationCompletion");

                foreach (EmployeeWorkProbationCompletionInfo indEmployeeWorkProbationCompletionInfo in objEmployeeWorkProbationCompletionList)
                {
                    objTemplate.AppMailTempBodyHTML = objTemplate.AppMailTempBodyHTML.Replace("{EmployeeCode}", indEmployeeWorkProbationCompletionInfo.EmpCode);
                    objTemplate.AppMailTempBodyHTML = objTemplate.AppMailTempBodyHTML.Replace("{EmpName}", indEmployeeWorkProbationCompletionInfo.EmpName);
                    objTemplate.AppMailTempBodyHTML = objTemplate.AppMailTempBodyHTML.Replace("{Designation}", indEmployeeWorkProbationCompletionInfo.DesignationTitle);
                    objTemplate.AppMailTempBodyHTML = objTemplate.AppMailTempBodyHTML.Replace("{Department}", indEmployeeWorkProbationCompletionInfo.DepartmentTitle);
                    objTemplate.AppMailTempBodyHTML = objTemplate.AppMailTempBodyHTML.Replace("{DOJ}", indEmployeeWorkProbationCompletionInfo.DOJ.ToString("dd-MMM-yyyy"));
                    objTemplate.AppMailTempBodyHTML = objTemplate.AppMailTempBodyHTML.Replace("{ConfirmationDate}", indEmployeeWorkProbationCompletionInfo.DOJ.ToString("dd-MMM-yyyy"));

                    Console.WriteLine($"Sending Probation Completion email to {indEmployeeWorkProbationCompletionInfo.EmpName} ({indEmployeeWorkProbationCompletionInfo.EmpCode})");
                }
            }

            await Task.CompletedTask;
        }
    }
}
