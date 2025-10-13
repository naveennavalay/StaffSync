using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class clsEmpNomineeMas
    {
        dbStaffSync.clsEmpNomineeMas objEmpNomineeMas = new dbStaffSync.clsEmpNomineeMas();
        public clsEmpNomineeMas() { 

        }

        public NomineeInfo GetNomineeInfo(int EmployeeID)
        {
            NomineeInfo empNomineeInfo = new NomineeInfo();

            empNomineeInfo = objEmpNomineeMas.GetNomineeInfo(EmployeeID);

            return empNomineeInfo;
        }


        public int InsertNomineeIfo(string txtNomineePerson, int EmpID, int txtNomineeRelationship, string txtContactNumber)
        {
            int affectedRows = 0;
            
            affectedRows = objEmpNomineeMas.InsertNomineeIfo( txtNomineePerson, EmpID, txtNomineeRelationship, txtContactNumber);

            return affectedRows;
        }

        public int UdpateNomineeInfo(int txtNomineeID, string txtNomineePerson, int EmpID, int txtNomineeRelationship, string txtContactNumber)
        {
            int affectedRows = 0;

            affectedRows = objEmpNomineeMas.UdpateNomineeInfo(txtNomineeID, txtNomineePerson, EmpID, txtNomineeRelationship, txtContactNumber);

            return affectedRows;
        } 
    }
}
