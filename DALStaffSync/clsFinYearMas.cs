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

        public List<FinYearMas> GetSpecificFinYearInfo(int txtFinYearID)
        {
            List<FinYearMas> objFinYearMasInfo = new List<FinYearMas>();

            objFinYearMasInfo = objFinYearMas.GetSpecificFinYearInfo(txtFinYearID);

            return objFinYearMasInfo;
        }

        public List<FinYearMas> GetFinYearList()
        {
            List<FinYearMas> objFinYearMasList = new List<FinYearMas>();

            objFinYearMasList = objFinYearMas.GetFinYearList();

            return objFinYearMasList;
        }

        public List<FinYearMas> GetCompleteFinYearList()
        {
           List<FinYearMas> objFinYearMasList = new List<FinYearMas>();

           objFinYearMasList = objFinYearMas.GetCompleteFinYearList();

           return objFinYearMasList;
        }
    }
}
