using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net;
using System.Text;

namespace DALStaffSync
{
    public class clsAppUserTasks
    {
        dbStaffSync.clsAppUserTasks objAppUserTasks = new dbStaffSync.clsAppUserTasks();

        public AppUserTasks getSpecificTaskInfo(int TaskSourceID, string TaskStatus, string TaskType)
        {
            return objAppUserTasks.getSpecificTaskInfo(TaskSourceID, TaskStatus, TaskType);
        }

        public int InsertUserTask(DateTime txtTaskDate, int txtTaskSourceID, int txtTaskRaisedByID, int TaskOwnerID, string txtTaskDescription, string txtTaskStatus, DateTime txtTaskCompletionDate, string txtTaskType)
        {
            int affectedRows = 0;

            affectedRows = objAppUserTasks.InsertUserTask(txtTaskDate, txtTaskSourceID, txtTaskRaisedByID, TaskOwnerID, txtTaskDescription, txtTaskStatus, txtTaskCompletionDate, txtTaskType);

            return affectedRows;
        }

        public int UpdateUserTask(int TaskID, int txtTaskSourceID, DateTime txtTaskDate, int txtTaskRaisedByID, int TaskOwnerID, string txtTaskDescription, string txtTaskStatus, DateTime txtTaskCompletionDate, string txtTaskType)
        {
            return objAppUserTasks.UpdateUserTask(TaskID, txtTaskSourceID, txtTaskDate, txtTaskRaisedByID, TaskOwnerID, txtTaskDescription, txtTaskStatus, txtTaskCompletionDate, txtTaskType);
        }
    }
}
