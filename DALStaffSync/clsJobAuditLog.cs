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
    public class clsJobAuditLog
    {
        dbStaffSync.clsJobAuditLog objJobAuditLog = new dbStaffSync.clsJobAuditLog();

        public List<SchedulerJobHistory> getClientSpecificSchedulerJobHistoryStatements(int txtClientiD)
        {
            return objJobAuditLog.getClientSpecificSchedulerJobHistoryStatements(txtClientiD);
        }

        public List<SchedulerJobHistory> getClientSpecificSchedulerJobSpecificHistoryStatements(int txtClientiD, int txtJobID)
        {
            return objJobAuditLog.getClientSpecificSchedulerJobSpecificHistoryStatements(txtClientiD, txtJobID);
        }

        public int InsertSchedulerJobHistoryInfo(int JobSchedulerSettingsID, DateTime? StartTime, DateTime? EndTime, int DurationSeconds, string Status, string Message, string TriggeredBy)
        {
            int affectedRows = 0;
            affectedRows = objJobAuditLog.InsertSchedulerJobHistoryInfo(JobSchedulerSettingsID, StartTime, EndTime, DurationSeconds, Status, Message, TriggeredBy);
            return affectedRows;
        }

    }
}
