using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class clsDesignation
    {

        dbStaffSync.clsDesignation objDesignation = new dbStaffSync.clsDesignation();

        public DataTable GetDesignationList()
        {
            DataTable dt = new DataTable();

            dt = objDesignation.GetDesignationList();

            return dt;
        }

        public DataTable GetDesignationList(string filterText)
        {
            DataTable dt = new DataTable();

            dt = objDesignation.GetDesignationList(filterText);

            return dt;
        }

        public string GetDesignationByID(int DesignationID)
        {
            string selectedDesignationTitle = "";
            
            selectedDesignationTitle = objDesignation.GetDesignationByID(DesignationID);

            return selectedDesignationTitle;
        }

        public int GetDesignationByTitle(string DesignationTitle)
        {
            int selectedDesignationID = 0;
            
            selectedDesignationID = objDesignation.GetDesignationByTitle(DesignationTitle);

            return selectedDesignationID;
        }

        public int InsertDesignation(string txtDesignationCode, string txtDesignationTitle, string txtDesignationInitial, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            
            affectedRows = objDesignation.InsertDesignation(txtDesignationCode, txtDesignationTitle, txtDesignationInitial, IsActive, IsDeleted);

            return affectedRows;
        }

        public int UpdateDesignation(int txtDesignationID, string txtDesignationCode, string txtDesignationTitle, string txtDesignationInitial, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            
            affectedRows = objDesignation.UpdateDesignation(txtDesignationID, txtDesignationCode, txtDesignationTitle, txtDesignationInitial, IsActive, IsDeleted);

            return affectedRows;
        }

        public int DeleteDesignation(int txtDesignationID)
        {
            int affectedRows = 0;
           
            affectedRows = objDesignation.DeleteDesignation(txtDesignationID);

            return affectedRows;
        }
    }
}
