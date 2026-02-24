using ModelStaffSync;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class clsAllowenceInfo
    {
        dbStaffSync.clsAllowenceInfo objAllowenceInfo = new dbStaffSync.clsAllowenceInfo();

        public DataTable GetAllowenceList()
        {
            DataTable dt = new DataTable();

            dt = objAllowenceInfo.GetAllowenceList();

            return dt;
        }

        public DataTable GetAllowenceList(string filterText)
        {
            DataTable dt = new DataTable();
            
            dt = objAllowenceInfo.GetAllowenceList(filterText);

            return dt;
        }

        public string GetAllowenceTitleByID(int AllowenceID)
        {
            string selectedAllowenceTitle = "";

            selectedAllowenceTitle = objAllowenceInfo.GetAllowenceTitleByID(AllowenceID);

            return selectedAllowenceTitle;
        }

        public int GetAllowenceTitleByTitle(string AllowenceTitle)
        {
            int selectedAllowenceID = 0;

            selectedAllowenceID = objAllowenceInfo.GetAllowenceTitleByTitle(AllowenceTitle);

            return selectedAllowenceID;
        }

        public AllowenceModel getSelectedAllowenceInfo(int txtAllowenceID)
        {
            return objAllowenceInfo.getSelectedAllowenceInfo(txtAllowenceID);
        }

        public int InsertAllowence(string txtAllCode, string txtAllTitle, string txtAllDescription, bool IsFixed, bool IsActive, bool IsDeleted, decimal txtMaxCap, bool ShowInPayslip, bool ConsiderProrataBasis)
        {
            int affectedRows = 0;
            
            affectedRows = objAllowenceInfo.InsertAllowence(txtAllCode, txtAllTitle, txtAllDescription, IsFixed, IsActive, IsDeleted, txtMaxCap, ShowInPayslip, ConsiderProrataBasis);

            return affectedRows;
        }

        public int UpdateAllowence(int txtAllID, string txtAllCode, string txtAllTitle, string txtAllDescription, bool IsFixed, bool IsActive, bool IsDeleted, decimal txtMaxCap, bool ShowInPayslip, bool ConsiderProrataBasis)
        {
            int affectedRows = 0;
            
            affectedRows = objAllowenceInfo.UpdateAllowence(txtAllID, txtAllCode, txtAllTitle, txtAllDescription, IsFixed, IsActive, IsDeleted, txtMaxCap, ShowInPayslip, ConsiderProrataBasis);

            return affectedRows;
        }

        public int DeleteAllowence(int txtAllID)
        {
            int affectedRows = 0;
            
            affectedRows = objAllowenceInfo.DeleteAllowence(txtAllID);

            return affectedRows;
        }
    }
}
