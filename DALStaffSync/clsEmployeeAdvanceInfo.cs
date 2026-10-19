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
    public class clsEmployeeAdvanceInfo
    {
        dbStaffSync.clsEmployeeAdvanceInfo objEmployeeAdvanceInfo = new dbStaffSync.clsEmployeeAdvanceInfo();

        public List<EmpAdvanceInfoModel> getEmployeeAdvanceInformationList(int empID)
        {
            List<EmpAdvanceInfoModel> objEmpAdvanceInfoList = new List<EmpAdvanceInfoModel>();

            objEmpAdvanceInfoList = objEmployeeAdvanceInfo.getEmployeeAdvanceInformationList(empID);

            return objEmpAdvanceInfoList;
        }
    }
}
