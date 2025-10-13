using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DALStaffSync
{
    public class clsReimbursement
    {

        dbStaffSync.clsReimbursement objReimbursement = new dbStaffSync.clsReimbursement();


        public DataTable GetReimbursementList()
        {
            DataTable dt = new DataTable();

            dt = objReimbursement.GetReimbursementList();

            return dt;
        }

        public DataTable GetReimbursementList(string filterText)
        {
            DataTable dt = new DataTable();

            dt = objReimbursement.GetReimbursementList(filterText);

            return dt;
        }

        public string GetReimbursementTitleByID(int ReimbuctionID)
        {
            string selectedReimbuctionTitle = "";
            
            selectedReimbuctionTitle = objReimbursement.GetReimbursementTitleByID(ReimbuctionID);

            return selectedReimbuctionTitle;
        }

        public int InsertReimbursement(string txtReimbCode, string txtReimbTitle, string txtReimbDescription, bool IsFixed, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            
            affectedRows = objReimbursement.InsertReimbursement( txtReimbCode, txtReimbTitle, txtReimbDescription, IsFixed, IsActive, IsDeleted);

            return affectedRows;
        }

        public int UpdateReimbursement(int txtReimbID, string txtReimbCode, string txtReimbTitle, string txtReimbDescription, bool IsFixed, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            
            affectedRows = objReimbursement.UpdateReimbursement(txtReimbID, txtReimbCode, txtReimbTitle, txtReimbDescription, IsFixed, IsActive, IsDeleted);

            return affectedRows;
        }

        public int DeleteReimbursement(int txtReimbID)
        {
            int affectedRows = 0;
            
            affectedRows = objReimbursement.DeleteReimbursement(txtReimbID);

            return affectedRows;
        }
    }
}
