using ModelStaffSync;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class clsDepartment
    {
        dbStaffSync.clsDepartment objDeparment = new dbStaffSync.clsDepartment();

        public List<DepartmentModel> GetDepartmentList()
        {
            return objDeparment.GetDepartmentList();
        }

        public List<DepartmentModel> GetDepartmentList(string filterText)
        {
            return objDeparment.GetDepartmentList(filterText);
        }

        public string GetDepartmentTitleByID(int DepartmentID)
        {
            string selectedDepartmentTitle = "";
            
            selectedDepartmentTitle = objDeparment.GetDepartmentTitleByID(DepartmentID);

            return selectedDepartmentTitle;
        }

        public int GetBloodGroupByTitle(string DepartmentTitle)
        {
            int selectedDepartmentID = 0;
            
            selectedDepartmentID = objDeparment.GetDepartmentByTitle(DepartmentTitle);

            return selectedDepartmentID;
        }

        public int InsertDepartment(string txtDepCode, string txtDepTitle, string txtDepInitial, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            
            affectedRows = objDeparment.InsertDepartment(txtDepCode, txtDepTitle, txtDepInitial, IsActive, IsDeleted);

            return affectedRows;
        }

        public int UpdateDepartment(int txtDepID, string txtDepCode, string txtDepTitle, string txtDepInitial, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            
            affectedRows = objDeparment.UpdateDepartment(txtDepID, txtDepCode, txtDepTitle, txtDepInitial, IsActive, IsDeleted);

            return affectedRows;
        }

        public int DeleteDepartment(int txtDepID)
        {
            int affectedRows = 0;
            
            affectedRows = objDeparment.DeleteDepartment(txtDepID);

            return affectedRows;
        }
    }
}
