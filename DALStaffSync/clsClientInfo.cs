using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace DALStaffSync
{
    public class clsClientInfo
    {
        dbStaffSync.clsClientInfo objClientInfo = new dbStaffSync.clsClientInfo();

        public List<ClientInfo> getAllCompanyList()
        {
            List<ClientInfo> companyList = new List<ClientInfo>();

            companyList = objClientInfo.getAllCompanyList();

            return companyList;
        }

        public List<ClientInfo> getAllCompanyList(string filterText)
        {
            List<ClientInfo> companyList = new List<ClientInfo>();

            companyList = objClientInfo.getAllCompanyList(filterText);

            return companyList;
        }

        public List<ClientInfo> getClientInfo(int txtClientID)
        {
            List<ClientInfo> objClientInfoList = new List<ClientInfo>();

            objClientInfoList = objClientInfo.getClientInfo(txtClientID);

            return objClientInfoList;
        }

        public List<ClientInfo> getClientInfoByEmpID(int txtClientID)
        {
            List<ClientInfo> objClientInfoList = new List<ClientInfo>();

            objClientInfoList = objClientInfo.getClientInfoByEmpID(txtClientID);

            return objClientInfoList;
        }

        public int InsertClientInfo(string txtClientCode, string txtClientName, string txtClientAddress1, string txtClientAddress2, string txtClientArea, string txtClientCity, string txtClientState, string txtClientPIN, string txtClientCountry, string txtClientPhone, string txtClientMailID, string txtClientContactPerson, string txtClientContactNumber, string txtClientContactMail, string txtClientWebSite, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;

            affectedRows = objClientInfo.InsertClientInfo(txtClientCode, txtClientName, txtClientAddress1, txtClientAddress2, txtClientArea, txtClientCity, txtClientState, txtClientPIN, txtClientCountry, txtClientPhone, txtClientMailID, txtClientContactPerson, txtClientContactNumber, txtClientContactMail, txtClientWebSite, IsActive, IsDeleted);

            return affectedRows;
        }

        public int UpdateClientInfo(int txtClientID, string txtClientCode, string txtClientName, string txtClientAddress1, string txtClientAddress2, string txtClientArea, string txtClientCity, string txtClientState, string txtClientPIN, string txtClientCountry, string txtClientPhone, string txtClientMailID, string txtClientContactPerson, string txtClientContactNumber, string txtClientContactMail, string txtClientWebSite, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;

            affectedRows = objClientInfo.UpdateClientInfo(txtClientID, txtClientCode, txtClientName, txtClientAddress1, txtClientAddress2, txtClientArea, txtClientCity, txtClientState, txtClientPIN, txtClientCountry, txtClientPhone, txtClientMailID, txtClientContactPerson, txtClientContactNumber, txtClientContactMail, txtClientWebSite, IsActive, IsDeleted);

            return affectedRows;
        }

        public int DeleteClientInfo(int txtClientID)
        {
            int affectedRows = 0;

            affectedRows = objClientInfo.DeleteClientInfo(txtClientID);

            return affectedRows;
        }
    }
}
