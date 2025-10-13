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
    public class clsBirthdayList
    {
        dbStaffSync.clsBirthdayList objBankMas = new dbStaffSync.clsBirthdayList();


        public clsBirthdayList() { 

        }

        public List<BirthdayList> GetEmployeesBirthdayList()
        {
            List<BirthdayList> birthdayList = new List<BirthdayList>();

            birthdayList = objBankMas.GetEmployeesBirthdayList();

            return birthdayList;
        }
    }
}
