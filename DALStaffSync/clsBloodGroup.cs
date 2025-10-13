using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class clsBloodGroup
    {
        dbStaffSync.clsBloodGroup objBloodGroup = new dbStaffSync.clsBloodGroup();

        public DataTable GetBloodGroupList()
        {
            DataTable dt = new DataTable();

            dt = objBloodGroup.GetBloodGroupList();

            return dt;
        }

        public DataTable GetBloodGroupList(string filterText)
        {
            DataTable dt = new DataTable();

            dt = objBloodGroup.GetBloodGroupList(filterText);

            return dt;
        }

        public string GetBloodGroupByID(int BloodGroupID)
        {
            string selectedBloodGroupTitle = "";

            selectedBloodGroupTitle = objBloodGroup.GetBloodGroupByID(BloodGroupID);

            return selectedBloodGroupTitle;
        }

        public int GetBloodGroupByTitle(string BloodGroupTitle)
        {
            int selectedBloodGroupID = 0;
            
            selectedBloodGroupID = objBloodGroup.GetBloodGroupByTitle(BloodGroupTitle);

            return selectedBloodGroupID;
        }

        public int InsertBloodGroup(string txtBloodGroupCode, string txtBloodGroupTitle, string txtBloodGroupInitial, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            
            affectedRows = objBloodGroup.InsertBloodGroup(txtBloodGroupCode, txtBloodGroupTitle, txtBloodGroupInitial, IsActive, IsDeleted);

            return affectedRows;
        }

        public int UpdateBloodGroup(int txtBloodGroupID, string txtBloodGroupCode, string txtBloodGroupTitle, string txtBloodGroupInitial, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            
            affectedRows = objBloodGroup.UpdateBloodGroup(txtBloodGroupID, txtBloodGroupCode, txtBloodGroupTitle, txtBloodGroupInitial, IsActive, IsDeleted);

            return affectedRows;
        }

        public int DeleteBloodGroup(int txtBloodGroupID)
        {
            int affectedRows = 0;
            
            affectedRows = objBloodGroup.DeleteBloodGroup(txtBloodGroupID);

            return affectedRows;
        }
    }
}
