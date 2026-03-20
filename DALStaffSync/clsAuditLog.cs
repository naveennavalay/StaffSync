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
    public class clsAuditLog
    {
        dbStaffSync.clsAuditLog objAuditLog = new dbStaffSync.clsAuditLog();

        public List<AuditLogs> getAuditLogStatements(int txtSourceID, string txtAuditLogStatementFilter, string txtEventGroup, int txtClientiD)
        {
            return objAuditLog.getAuditLogStatements(txtSourceID, txtAuditLogStatementFilter, txtEventGroup, txtClientiD);
        }

        public List<AuditLogs> getAuditLogStatements(int txtSourceID, string txtEventGroup, int txtClientiD)
        {
            return objAuditLog.getAuditLogStatements(txtSourceID, txtEventGroup, txtClientiD);
        }

        public int InsertAuditLog(int txtEmpID, int txtSourceID, string txtAuditLogStatement, string txtActionType, string txtUserName, string txtEventGroup, int txtClientID)
        {
            int affectedRows = 0;

            affectedRows = objAuditLog.InsertAuditLog(txtEmpID, txtSourceID, txtAuditLogStatement, txtActionType, txtUserName, txtEventGroup, txtClientID);

            return affectedRows;
        }
    }
}
