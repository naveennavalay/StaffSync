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
    public class clsClientBranchInfo
    {
        dbStaffSync.clsClientBranchInfo objClientBranchInfo = new dbStaffSync.clsClientBranchInfo();

        public List<ClientBranchInfo> getBranchInfoList(int txtClientID)
        {
            List<ClientBranchInfo> branchInfoList = new List<ClientBranchInfo>();

            branchInfoList = objClientBranchInfo.getBranchInfoList(txtClientID);

            return branchInfoList;
        }

        public List<ClientBranchInfo> getAllCompanyList(int txtClientID)
        {
            List<ClientBranchInfo> companyList = new List<ClientBranchInfo>();

            companyList = objClientBranchInfo.getAllCompanyList(txtClientID);

            return companyList;
        }

        public List<ClientBranchInfo> getAllCompanyList(int txtClientID, string filterText)
        {
            List<ClientBranchInfo> companyList = new List<ClientBranchInfo>();

            companyList = objClientBranchInfo.getAllCompanyList(txtClientID, filterText);

            return companyList;
        }

        public List<ClientBranchInfo> getClientBranchInfo(int txtClientBranchID)
        {
            List<ClientBranchInfo> objClientBranchInfoList = new List<ClientBranchInfo>();

            objClientBranchInfoList = objClientBranchInfo.getClientBranchInfo(txtClientBranchID);

            return objClientBranchInfoList;
        }

        public List<ClientBranchInfo> getClientBranchInfoByEmpID(int txtClientBranchID)
        {
            List<ClientBranchInfo> objClientBranchInfoList = new List<ClientBranchInfo>();

            objClientBranchInfoList = objClientBranchInfo.getClientBranchInfoByEmpID(txtClientBranchID);

            return objClientBranchInfoList;
        }

        public int InsertClientBranchInfo(string txtClientBranchCode, string txtClientBranchName, string txtClientBranchAddress1, string txtClientBranchAddress2, string txtClientBranchArea, string txtClientBranchCity, string txtClientBranchState, string txtClientBranchPIN, string txtClientBranchCountry, string txtClientBranchPhone, string txtClientBranchMailID, string txtClientBranchContactPerson, string txtClientBranchContactNumber, string txtClientBranchContactMail, string txtClientBranchWebSite, bool IsActive, bool IsDeleted, int txtClientID)
        {
            int affectedRows = 0;

            affectedRows = objClientBranchInfo.InsertClientBranchInfo(txtClientBranchCode, txtClientBranchName, txtClientBranchAddress1, txtClientBranchAddress2, txtClientBranchArea, txtClientBranchCity, txtClientBranchState, txtClientBranchPIN, txtClientBranchCountry, txtClientBranchPhone, txtClientBranchMailID, txtClientBranchContactPerson, txtClientBranchContactNumber, txtClientBranchContactMail, txtClientBranchWebSite, IsActive, IsDeleted, txtClientID);

            return affectedRows;
        }

        public int UpdateClientBranchInfo(int txtClientBranchID, string txtClientBranchCode, string txtClientBranchName, string txtClientBranchAddress1, string txtClientBranchAddress2, string txtClientBranchArea, string txtClientBranchCity, string txtClientBranchState, string txtClientBranchPIN, string txtClientBranchCountry, string txtClientBranchPhone, string txtClientBranchMailID, string txtClientBranchContactPerson, string txtClientBranchContactNumber, string txtClientBranchContactMail, string txtClientBranchWebSite, bool IsActive, bool IsDeleted, int txtClientID)
        {
            int affectedRows = 0;

            affectedRows = objClientBranchInfo.UpdateClientBranchInfo(txtClientBranchID, txtClientBranchCode, txtClientBranchName, txtClientBranchAddress1, txtClientBranchAddress2, txtClientBranchArea, txtClientBranchCity, txtClientBranchState, txtClientBranchPIN, txtClientBranchCountry, txtClientBranchPhone, txtClientBranchMailID, txtClientBranchContactPerson, txtClientBranchContactNumber, txtClientBranchContactMail, txtClientBranchWebSite, IsActive, IsDeleted, txtClientID);

            return affectedRows;
        }

        public int DeleteClientBranchInfo(int txtClientBranchID)
        {
            int affectedRows = 0;

            affectedRows = objClientBranchInfo.DeleteClientBranchInfo(txtClientBranchID);

            return affectedRows;
        }
    }
}
