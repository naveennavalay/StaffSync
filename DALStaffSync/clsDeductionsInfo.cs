using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DALStaffSync
{
    public class clsDeductionsInfo
    {
        dbStaffSync.clsDeductionsInfo objDeductionsInfo = new dbStaffSync.clsDeductionsInfo();

        public DataTable GetDeductionList()
        {
            DataTable dt = new DataTable();

            dt = objDeductionsInfo.GetDeductionList();

            return dt;
        }

        public DataTable GetDeductionList(string filterText)
        {
            DataTable dt = new DataTable();

            dt = objDeductionsInfo.GetDeductionList(filterText);

            return dt;
        }

        public string GetDeductionTitleByID(int DeductionID)
        {
            string selectedDeductionTitle = "";
            
            selectedDeductionTitle = objDeductionsInfo.GetDeductionTitleByID(DeductionID);

            return selectedDeductionTitle;
        }

        public int GetDeductionTitleByTitle(string AllowenceTitle)
        {
            int selectedDeductionID = 0;

            selectedDeductionID = objDeductionsInfo.GetDeductionTitleByTitle(AllowenceTitle);

            return selectedDeductionID;
        }

        public int InsertDeduction(string txtDedCode, string txtDedTitle, string txtDedDescription, bool IsFixed, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            
            affectedRows = objDeductionsInfo.InsertDeduction(txtDedCode, txtDedTitle, txtDedDescription, IsFixed, IsActive, IsDeleted);

            return affectedRows;
        }

        public int UpdateDeduction(int txtDedID, string txtDedCode, string txtDedTitle, string txtDedDescription, bool IsFixed, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            
            affectedRows = objDeductionsInfo.UpdateDeduction(txtDedID, txtDedCode, txtDedTitle, txtDedDescription, IsFixed, IsActive, IsDeleted);

            return affectedRows;
        }

        public int DeleteDeduction(int txtDedID)
        {
            int affectedRows = 0;
            
            affectedRows = objDeductionsInfo.DeleteDeduction(txtDedID);

            return affectedRows;
        }
    }
}
