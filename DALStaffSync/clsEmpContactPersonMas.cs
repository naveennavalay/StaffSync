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
    public class clsEmpContactPersonMas
    {
        dbStaffSync.clsEmpContactPersonMas objEmpContactPersonMas = new dbStaffSync.clsEmpContactPersonMas();

        public clsEmpContactPersonMas() { 

        }

        public int InsertContactInfo(string txtContactPerson, string txtContactPersonAddress, int txtContactPersonRelationship, int txtContactPersonSexID)
        {
            int affectedRows = 0;

            affectedRows = objEmpContactPersonMas.InsertContactInfo(txtContactPerson, txtContactPersonAddress, txtContactPersonRelationship, txtContactPersonSexID);

            return affectedRows;
        }

        public int UdpateContactInfo(int ContactID, string txtContactPerson, string txtContactPersonAddress, int txtContactPersonRelationship, int txtContactPersonSexID)
        {
            int affectedRows = 0;

            affectedRows = objEmpContactPersonMas.UdpateContactInfo(ContactID, txtContactPerson, txtContactPersonAddress, txtContactPersonRelationship, txtContactPersonSexID);

            return affectedRows;
        }

        public ContactInfo GetContactInfo(int ContactID)
        {
            ContactInfo contactInfo = new ContactInfo();

            contactInfo = objEmpContactPersonMas.GetContactInfo(ContactID);

            return contactInfo;
        }
    }
}
