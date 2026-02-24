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

        public int GetReimbursementTitleByTitle(string AllowenceTitle)
        {
            int selectedReimbursementID = 0;

            selectedReimbursementID = objReimbursement.GetReimbursementTitleByTitle(AllowenceTitle);

            return selectedReimbursementID;
        }

        public ReimbursementModel getSelectedDeductionInfo(int txtReimbursementID)
        {
            return objReimbursement.getSelectedDeductionInfo(txtReimbursementID);
        }

        public int InsertReimbursement(string txtReimbCode, string txtReimbTitle, string txtReimbDescription, bool IsFixed, bool IsActive, bool IsDeleted, decimal txtMaxCap, bool ShowInPayslip, bool ConsiderProrataBasis)
        {
            int affectedRows = 0;
            
            affectedRows = objReimbursement.InsertReimbursement( txtReimbCode, txtReimbTitle, txtReimbDescription, IsFixed, IsActive, IsDeleted, txtMaxCap, ShowInPayslip, ConsiderProrataBasis);

            return affectedRows;
        }

        public int UpdateReimbursement(int txtReimbID, string txtReimbCode, string txtReimbTitle, string txtReimbDescription, bool IsFixed, bool IsActive, bool IsDeleted, decimal txtMaxCap, bool ShowInPayslip, bool ConsiderProrataBasis)
        {
            int affectedRows = 0;
            
            affectedRows = objReimbursement.UpdateReimbursement(txtReimbID, txtReimbCode, txtReimbTitle, txtReimbDescription, IsFixed, IsActive, IsDeleted, txtMaxCap, ShowInPayslip, ConsiderProrataBasis);

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
