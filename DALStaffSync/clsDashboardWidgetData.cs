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

        public int InsertAuditLog(int txtEmpID, int txtSourceID, string txtAuditLogStatement, string txtUserName, string txtEventGroup)
        {
            int affectedRows = 0;
            
            affectedRows = objAuditLog.InsertAuditLog(txtEmpID, txtSourceID, txtAuditLogStatement, txtUserName, txtEventGroup);

            return affectedRows;
        }
    }
}
