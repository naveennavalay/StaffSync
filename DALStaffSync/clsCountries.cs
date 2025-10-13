using dbStaffSync;
using ModelStaffSync;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class clsCountries
    {
        dbStaffSync.clsCountries objCountries = new dbStaffSync.clsCountries();

        public DataTable GetCountryList()
        {
            DataTable dt = new DataTable();

            dt = objCountries.GetCountryList();

            return dt;
        }

        public DataTable GetCountryList(string filterText)
        {
            DataTable dt = new DataTable();

            dt = objCountries.GetCountryList(filterText);

            return dt;
        }

        public string GetCountryByID(int CountryID)
        {
            string selectedCountryName = "";
            
            selectedCountryName = objCountries.GetCountryByID(CountryID);

            return selectedCountryName;
        }

        public int GetCountryByTitle(string CountryName)
        {
            int selectedStateID = 0;
            
            selectedStateID = objCountries.GetCountryByTitle(CountryName);

            return selectedStateID;
        }

        public int InsertCountry(string txtCountryCode, string txtCountryTitle, string txtCountryInitial, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;

            affectedRows = objCountries.InsertCountry(txtCountryCode, txtCountryTitle, txtCountryInitial, IsActive, IsDeleted);

            return affectedRows;
        }

        public int UpdateCountry(int txtCountryID, string txtCountryCode, string txtCountryTitle, string txtCountryInitial, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;

            affectedRows = objCountries.UpdateCountry(txtCountryID, txtCountryCode, txtCountryTitle, txtCountryInitial, IsActive, IsDeleted);

            return affectedRows;
        }

        public int DeleteCountry(int txtCountryID)
        {
            int affectedRows = 0;

            affectedRows = objCountries.DeleteCountry(txtCountryID);

            return affectedRows;
        }
    }
}
