using ModelStaffSync;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace DALStaffSync
{
    public class clsClientSignatoryInfo
    {
        dbStaffSync.clsClientSignatoryInfo objClientSignatoryInfo = new dbStaffSync.clsClientSignatoryInfo();

        public List<ClientSigningPerson> getClientSpecificSignatoryInfo(int txtClientID)
        {
            List<ClientSigningPerson> objClientSigningPerson = new List<ClientSigningPerson>();

            objClientSigningPerson = objClientSignatoryInfo.getClientSpecificSignatoryInfo(txtClientID);

            return objClientSigningPerson;
        }

        public int InsertSignatoryInfo(int txtClientID, string txtClientSigningPersonName, string ClientSigningPersonDesignation, string ClientSigningPersonFatherName, string ClientSigningPersonPANNumber, string ClientSigningPersonSex, DateTime ClientSigningPersonDOB, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;

            affectedRows = objClientSignatoryInfo.InsertSignatoryInfo(txtClientID, txtClientSigningPersonName, ClientSigningPersonDesignation, ClientSigningPersonFatherName, ClientSigningPersonPANNumber, ClientSigningPersonSex, ClientSigningPersonDOB, IsActive, IsDeleted);

            return affectedRows;
        }

        public int UpdateSignatoryInfo(int txtClientSigningPersonID, int txtClientID, string txtClientSigningPersonName, string ClientSigningPersonDesignation, string ClientSigningPersonFatherName, string ClientSigningPersonPANNumber, string ClientSigningPersonSex, DateTime ClientSigningPersonDOB, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;

            affectedRows = objClientSignatoryInfo.UpdateSignatoryInfo(txtClientSigningPersonID, txtClientID, txtClientSigningPersonName, ClientSigningPersonDesignation, ClientSigningPersonFatherName, ClientSigningPersonPANNumber, ClientSigningPersonSex, ClientSigningPersonDOB, IsActive, IsDeleted);

            return affectedRows;
        }
    }
}
