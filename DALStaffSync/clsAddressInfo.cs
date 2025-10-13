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
    public class clsAddressInfo
    {
        dbStaffSync.clsAddressInfo objAddressInfo = new dbStaffSync.clsAddressInfo();

        public clsAddressInfo() { 

        }

        public DataTable GetAddressList(string ColumnName)
        {
            DataTable dt = new DataTable();

            dt = objAddressInfo.GetAddressList(ColumnName);

            return dt;
        }

        public int InsertAddressInfo(string txtAddress1, string txtAddress2, string txtArea, string txtCity, string txtPIN, string txtState, string txtCountry)
        {
            int affectedRows = 0;
            
            affectedRows = objAddressInfo.InsertAddressInfo(txtAddress1, txtAddress2, txtArea, txtCity, txtPIN, txtState, txtCountry);

            return affectedRows;
        }

        public int UpdateAddressInfo(int txtAddressInfoID, string txtAddress1, string txtAddress2, string txtArea, string txtCity, string txtPIN, string txtState, string txtCountry)
        {
            int affectedRows = 0;

            affectedRows = objAddressInfo.UpdateAddressInfo(txtAddressInfoID, txtAddress1, txtAddress2, txtArea, txtCity, txtPIN, txtState, txtCountry);

            return affectedRows;
        }

        public AddressInfo GetAddressInfo(int AddressID)
        {
            AddressInfo addressInfo = new AddressInfo();

            addressInfo = objAddressInfo.GetAddressInfo(AddressID);

            return addressInfo;
        }
    }
}
