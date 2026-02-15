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

        public int InsertUserTask(DateTime txtTaskDate, int txtTaskSourceID, int txtTaskRaisedByID, int TaskOwnerID, string txtTaskDescription, string txtTaskStatus, DateTime txtTaskCompletionDate, string txtTaskType)
        {
            int affectedRows = 0;

            affectedRows = objAppUserTasks.InsertUserTask(txtTaskDate, txtTaskSourceID, txtTaskRaisedByID, TaskOwnerID, txtTaskDescription, txtTaskStatus, txtTaskCompletionDate, txtTaskType);

            return affectedRows;
        }
    }
}
