using ModelStaffSync;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class clsAdvanceTypeConfigInfo
    {
        dbStaffSync.clsAdvanceTypeConfigInfo objAdvanceTypeConfigInfo = new dbStaffSync.clsAdvanceTypeConfigInfo();

        public AdvanceTypeConfigModel GetAdvanceTypeConfigByID(int txtAdvanceTypeConfigID)
        {
            return objAdvanceTypeConfigInfo.GetAdvanceTypeConfigByID(txtAdvanceTypeConfigID);
        }

        public int InsertAdvanceTypeConfig(int txtAdvanceTypeID, bool AutoDeductFromSalary, string txtBasedOnNetOrGross, string txtMaxPerOfNetOrGross, decimal txtMaxPercentage, decimal txtMaxFixed, bool IncludeInSalary, bool RecoveryRequired, bool AutoRecoveryFromNextSalary, bool InterestRequired, bool ApprovalRequired, bool AllowPause, bool WaiverAllowed, decimal MaxTenure)
        {
            return objAdvanceTypeConfigInfo.InsertAdvanceTypeConfig(txtAdvanceTypeID, AutoDeductFromSalary, txtBasedOnNetOrGross, txtMaxPerOfNetOrGross, txtMaxPercentage, txtMaxFixed, IncludeInSalary, RecoveryRequired, AutoRecoveryFromNextSalary, InterestRequired, ApprovalRequired, AllowPause, WaiverAllowed, MaxTenure);
        }

        public int UpdateAdvanceTypeConfig(int txtAdvanceTypeConfigID, int txtAdvanceTypeID, bool AutoDeductFromSalary, string txtBasedOnNetOrGross, string txtMaxPerOfNetOrGross, decimal txtMaxPercentage, decimal txtMaxFixed, bool IncludeInSalary, bool RecoveryRequired, bool AutoRecoveryFromNextSalary, bool InterestRequired, bool ApprovalRequired, bool AllowPause, bool WaiverAllowed, decimal MaxTenure)
        {
            return objAdvanceTypeConfigInfo.UpdateAdvanceTypeConfig(txtAdvanceTypeConfigID, txtAdvanceTypeID, AutoDeductFromSalary, txtBasedOnNetOrGross, txtMaxPerOfNetOrGross, txtMaxPercentage, txtMaxFixed, IncludeInSalary, RecoveryRequired, AutoRecoveryFromNextSalary, InterestRequired, ApprovalRequired, AllowPause, WaiverAllowed, MaxTenure);
        }

        public int DeleteAdvanceTypeConfig(int txtAdvanceTypeConfigID)
        {
            return objAdvanceTypeConfigInfo.DeleteAdvanceTypeConfig(txtAdvanceTypeConfigID);
        }

    }
}
