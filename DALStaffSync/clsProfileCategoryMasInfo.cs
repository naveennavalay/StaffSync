using dbStaffSync;
using ModelStaffSync;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class clsProfileCategoryMasInfo
    {
        dbStaffSync.clsProfileCategoryMasInfo objProfileCategoryMasInfo = new dbStaffSync.clsProfileCategoryMasInfo();

        public DataTable GetProfileCategoryList()
        {
            DataTable dt = new DataTable();

            dt = objProfileCategoryMasInfo.GetProfileCategoryList();

            return dt;
        }
    }
}
