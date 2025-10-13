using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class clsEduQalification
    {
        dbStaffSync.clsEduQalification objEduQalification = new dbStaffSync.clsEduQalification();

        public DataTable GetEduQualMasList()
        {
            DataTable dt = new DataTable();

            dt = objEduQalification.GetEduQualMasList();

            return dt;
        }

        public DataTable GetEduQualMasList(string filterText)
        {
            DataTable dt = new DataTable();

            dt = objEduQalification.GetEduQualMasList(filterText);

            return dt;
        }

        public int InsertEduQual(string txtEduQualCode, string txtEduQualTitle, string txtEduQualInitial, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            
            affectedRows = objEduQalification.InsertEduQual(txtEduQualCode, txtEduQualTitle, txtEduQualInitial, IsActive, IsDeleted);

            return affectedRows;
        }

        public int UpdateEduQual(int txtEduQualID, string txtEduQualCode, string txtEduQualTitle, string txtEduQualInitial, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            
            affectedRows = objEduQalification.UpdateEduQual(txtEduQualID, txtEduQualCode, txtEduQualTitle, txtEduQualInitial, IsActive, IsDeleted);

            return affectedRows;
        }

        public int DeleteEduQual(int txtEduQualID)
        {
            int affectedRows = 0;
            
            affectedRows = objEduQalification.DeleteEduQual(txtEduQualID);

            return affectedRows;
        }
    }
}
