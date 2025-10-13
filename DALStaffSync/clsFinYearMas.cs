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
    public class clsFinYearMas
    {
        dbStaffSync.clsFinYearMas objFinYearMas = new dbStaffSync.clsFinYearMas();

        public List<FinYearMas> GetFinYearList()
        {
            List<FinYearMas> objFinYearMasList = new List<FinYearMas>();

            objFinYearMasList = objFinYearMas.GetFinYearList();

            return objFinYearMasList;
        }
    }
}
