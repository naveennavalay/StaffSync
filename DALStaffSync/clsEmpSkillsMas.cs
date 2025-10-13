using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace DALStaffSync
{
    public class clsEmpSkillsMas
    {

        dbStaffSync.clsEmpSkillsMas objEmpSkillsMas = new dbStaffSync.clsEmpSkillsMas();

        public clsEmpSkillsMas() { 

        }

        public DataTable GetSkillsListInfo(int EmployeeID)
        {
            DataTable dt = new DataTable();

            dt = objEmpSkillsMas.GetSkillsListInfo(EmployeeID);

            return dt;
        }

        public int InsertEmpSkillsInfo(int txtEmpID, int txtSkillID, int txtExpertiseLevel)
        {
            int affectedRows = 0;

            affectedRows = objEmpSkillsMas.InsertEmpSkillsInfo(txtEmpID, txtSkillID, txtExpertiseLevel);

            return affectedRows;
        }

        public int UpadateEmpSkillsInfo(int txtEmpSkillID, int txtEmpID, int txtSkillID, int txtExpertiseLevel)
        {
            int affectedRows = 0;
            
            affectedRows = objEmpSkillsMas.UpadateEmpSkillsInfo(txtEmpSkillID, txtEmpID, txtSkillID, txtExpertiseLevel);

            return affectedRows;
        }

        public int DeleteSkillsInfo(int txtEmpID)
        {
            int affectedRows = 0;
            
            affectedRows = objEmpSkillsMas.DeleteSkillsInfo(txtEmpID);

            return affectedRows;
        }
    }
}
