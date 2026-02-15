using ModelStaffSync;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class clsAdvanceTypeMas
    {
        dbStaffSync.clsAdvanceTypeMas objAdvanceTypeMas = new dbStaffSync.clsAdvanceTypeMas();

        public AdvanceTypesModel GetAdvanceTypeConfigByID(int txtAdvanceTypeID)
        {
            AdvanceTypesModel objAdvanceTypeConfig = new AdvanceTypesModel();

            objAdvanceTypeConfig = objAdvanceTypeMas.GetAdvanceTypeConfigByID(txtAdvanceTypeID);

            return objAdvanceTypeConfig;
        }
        public DataTable GetAdvanceTypeList(int txtCompanyID)
        {
            DataTable dt = new DataTable();

            dt = objAdvanceTypeMas.GetAdvanceTypeList(txtCompanyID);

            return dt;
        }

        public DataTable GetAdvanceTypeList(int txtCompanyID, string filterText)
        {
            DataTable dt = new DataTable();

            dt = objAdvanceTypeMas.GetAdvanceTypeList(1, filterText);

            return dt;
        }

        public string GetAdvanceTypeByID(int txtAdvanceTypeID)
        {
            string selectedBloodGroupTitle = "";

            selectedBloodGroupTitle = objAdvanceTypeMas.GetAdvanceTypeByID(1, txtAdvanceTypeID);

            return selectedBloodGroupTitle;
        }

        public int GetAdvanceTypeByTitle(string txtAdvanceTypeTitle)
        {
            int selectedBloodGroupID = 0;
            
            selectedBloodGroupID = objAdvanceTypeMas.GetAdvanceTypeByTitle(1, txtAdvanceTypeTitle);

            return selectedBloodGroupID;
        }

        public int InsertAdvanceType(string txtAdvanceTypeCode, string txtAdvanceTypeTitle, bool IsActive, bool IsDeleted, int txtClientID)
        {
            int affectedRows = 0;
            
            affectedRows = objAdvanceTypeMas.InsertAdvanceType(txtAdvanceTypeCode, txtAdvanceTypeTitle, IsActive, IsDeleted, txtClientID);

            return affectedRows;
        }

        public int UpdateAdvanceType(int txtAdvanceTypeID, string txtAdvanceTypeCode, string txtAdvanceTypeTitle, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            
            affectedRows = objAdvanceTypeMas.UpdateAdvanceType(txtAdvanceTypeID, txtAdvanceTypeCode, txtAdvanceTypeTitle, IsActive, IsDeleted);

            return affectedRows;
        }

        public int DeleteAdvanceType(int txtAdvanceTypeID)
        {
            int affectedRows = 0;
            
            affectedRows = objAdvanceTypeMas.DeleteAdvanceType(txtAdvanceTypeID);

            return affectedRows;
        }
    }
}
