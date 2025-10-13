using dbStaffSync;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class clsGenFunc
    {

        dbStaffSync.clsGenFunc objGenFunc = new dbStaffSync.clsGenFunc();

        public Response<int> getMaxRowCount(string tableName, string ColumnName)
        {
            int rowCount = 0;
            
            rowCount = objGenFunc.getMaxRowCount(tableName, ColumnName).Data;

            return Response<int>.Success(rowCount);
        }

        public Response<int> getLeaveMasMaxRowCount(int txtLeaveMasID)
        {
            int rowCount = 0;

            rowCount = objGenFunc.getLeaveMasMaxRowCount(txtLeaveMasID).Data;

            return Response<int>.Success(rowCount);
        }

        public Response<int> getEmployeeSpecificOrderID(string tableName, string ColumnName, int EmpID)
        {
            int rowCount = 0;

            rowCount = objGenFunc.getEmployeeSpecificOrderID(tableName, ColumnName, EmpID).Data;

            return Response<int>.Success(rowCount);
        }

        public Response<int> getMaxRowCount(string tableName, string ColumnName, int CurrentCompanyID)
        {
            int rowCount = 0;

            rowCount = objGenFunc.getMaxRowCount(tableName, ColumnName, CurrentCompanyID).Data;

            return Response<int>.Success(rowCount);
        }

    }
}
