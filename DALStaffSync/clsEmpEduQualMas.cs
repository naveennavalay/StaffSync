using dbStaffSync;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class clsEmpEduQualMas
    {
        dbStaffSync.clsEmpEduQualMas objEmpEduQualMas = new dbStaffSync.clsEmpEduQualMas();

        public clsEmpEduQualMas() { 

        }

        public DataTable GetEduQualListInfo(int EmployeeID)
        {
            DataTable dt = new DataTable();

            dt = objEmpEduQualMas.GetEduQualListInfo(EmployeeID);

            return dt;
        }

        public int InsertEmpEduQualInfo(int txtEmpID, int txtEduQualID, int txtExpertiseLevel)
        {
            int affectedRows = 0;
            
            affectedRows = objEmpEduQualMas.InsertEmpEduQualInfo(txtEmpID, txtEduQualID, txtExpertiseLevel);

            return affectedRows;
        }

        public int UpdatetEmpEduQualInfo(int txtEmpEduQualID, int txtEmpID, int txtEduQualID, int txtExpertiseLevel)
        {
            int affectedRows = 0;
            
            affectedRows = objEmpEduQualMas.UpdatetEmpEduQualInfo(txtEmpEduQualID, txtEmpID, txtEduQualID, txtExpertiseLevel);

            return affectedRows;
        }

        public int DeleteEmpEduQualInfo(int txtEmpID)
        {
            int affectedRows = 0;
            
            affectedRows = objEmpEduQualMas.DeleteEmpEduQualInfo(txtEmpID);

            return affectedRows;
        }
    }
}
