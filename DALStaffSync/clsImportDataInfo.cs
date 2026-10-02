using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class clsImportDataInfo
    {
        dbStaffSync.clsImportDataInfo objImportDataInfo = new dbStaffSync.clsImportDataInfo();

        public List<ImportDataInfoModel> getImportInfoData()
        {
            List<ImportDataInfoModel> objImportDataInfoModelList = new List<ImportDataInfoModel>();

            objImportDataInfoModelList = objImportDataInfo.getImportInfoData();

            return objImportDataInfoModelList;
        }
    }
}
