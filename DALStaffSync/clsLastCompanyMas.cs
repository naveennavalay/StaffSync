using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class clsLastCompanyMas
    {
        dbStaffSync.clsLastCompanyMas objLastCompanyMas = new dbStaffSync.clsLastCompanyMas();

        public DataTable GetLastCompDetMasList()
        {
            DataTable dt = new DataTable();

            dt = objLastCompanyMas.GetLastCompDetMasList();

            return dt;
        }

        public DataTable GetLastCompDetMasList(string filterText)
        {
            DataTable dt = new DataTable();

            dt = objLastCompanyMas.GetLastCompDetMasList(filterText);

            return dt;
        }

        public int InsertLastCompDetMas(string txtLastCompanyCode, string txtLastCompanyTitle, string txtLastCompanyAddress, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;

            affectedRows = objLastCompanyMas.InsertLastCompDetMas(txtLastCompanyCode, txtLastCompanyTitle, txtLastCompanyAddress, IsActive, IsDeleted);

            return affectedRows;
        }

        public int UpdateLastCompDetMas(int txtLastCompanyInfoID, string txtLastCompanyCode, string txtLastCompanyTitle, string txtLastCompanyAddress, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            
            affectedRows = objLastCompanyMas.UpdateLastCompDetMas(txtLastCompanyInfoID, txtLastCompanyCode, txtLastCompanyTitle, txtLastCompanyAddress, IsActive, IsDeleted);

            return affectedRows;
        }

        public int DeleteLastCompDetMas(int txtLastCompanyInfoID)
        {
            int affectedRows = 0;
            
            affectedRows = objLastCompanyMas.DeleteLastCompDetMas(txtLastCompanyInfoID);

            return affectedRows;
        }
    }
}
