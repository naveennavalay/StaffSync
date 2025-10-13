using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class clsSexMas
    {
        dbStaffSync.clsSexMas objSexMas = new dbStaffSync.clsSexMas();

        public DataTable GetSexList()
        {
            DataTable dt = new DataTable();

            dt = objSexMas.GetSexList();

            return dt;
        }

        public DataTable GetSexList(string filterText)
        {
            DataTable dt = new DataTable();

            dt = objSexMas.GetSexList(filterText);

            return dt;
        }

        public int InsertSex(string txtSexCode, string txtSexTitle, string txtSexInitial, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            
            affectedRows = objSexMas.InsertSex(txtSexCode, txtSexTitle, txtSexInitial, IsActive, IsDeleted);

            return affectedRows;
        }

        public int UpdateSex(int txtSexID, string txtSexCode, string txtSexTitle, string txtSexInitial, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            
            affectedRows = objSexMas.UpdateSex(txtSexID, txtSexCode, txtSexTitle, txtSexInitial, IsActive, IsDeleted);

            return affectedRows;
        }

        public int DeleteSex(int txtSexID)
        {
            int affectedRows = 0;
            
            affectedRows = objSexMas.DeleteSex(txtSexID);

            return affectedRows;
        }

        public string GetSexByID(int SexID)
        {
            string selectedSexName = "";
            
            selectedSexName = objSexMas.GetSexByID(SexID);

            return selectedSexName;
        }

        public int GetSexByTitle(string SexTitle)
        {
            int selectedSexID = 0;
            
            selectedSexID = objSexMas.GetSexByTitle(SexTitle);

            return selectedSexID;
        }
    }
}
