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
    public class clsEmpActiveInactiveStatusInfo
    {
        dbStaffSync.clsEmpActiveInactiveStatusInfo objEmpActiveInactiveStatusInfo = new dbStaffSync.clsEmpActiveInactiveStatusInfo();

        public List<EmpActiveInactiveStatusInfoModel> getEmpActiveInactiveStatusInfo(int txtEmpID, string txtEmpActiveInactiveStatusComments)
        {
            return objEmpActiveInactiveStatusInfo.getEmpActiveInactiveStatusInfo(txtEmpID, txtEmpActiveInactiveStatusComments);
        }

        public int InsertEmpActiveInactiveStatus(int txtEmpPersonalID, bool ActiveInactiveStatus, DateTime txtActiveInactiveDate, string txtComments)
        {
            int affectedRows = 0;

            affectedRows = objEmpActiveInactiveStatusInfo.InsertEmpActiveInactiveStatus(txtEmpPersonalID, ActiveInactiveStatus, txtActiveInactiveDate, txtComments);

            return affectedRows;
        }
    }
}
