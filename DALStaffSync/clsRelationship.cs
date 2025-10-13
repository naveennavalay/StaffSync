using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class clsRelationship
    {

        dbStaffSync.clsRelationship objRelationship = new dbStaffSync.clsRelationship();

        public DataTable GetRelationshipList()
        {
            DataTable dt = new DataTable();

            dt = objRelationship.GetRelationshipList();

            return dt;
        }

        public DataTable GetRelationshipList(string filterText)
        {
            DataTable dt = new DataTable();
            
            dt = objRelationship.GetRelationshipList(filterText);

            return dt;
        }

        public int InsertRelationship(string txtRelationshipCode, string txtRelationshipTitle, string txtRelationshipInitial, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            
            affectedRows = objRelationship.InsertRelationship(txtRelationshipCode, txtRelationshipTitle, txtRelationshipInitial, IsActive, IsDeleted);

            return affectedRows;
        }

        public int UpdateRelationship(int txtRelationshipID, string txtRelationshipCode, string txtRelationshipTitle, string txtRelationshipInitial, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;

            affectedRows = objRelationship.UpdateRelationship(txtRelationshipID, txtRelationshipCode, txtRelationshipTitle, txtRelationshipInitial, IsActive, IsDeleted);

            return affectedRows;
        }

        public int DeleteRelationship(int txtRelationshipID)
        {
            int affectedRows = 0;

            affectedRows = objRelationship.DeleteRelationship(txtRelationshipID);

            return affectedRows;
        }
    }
}
