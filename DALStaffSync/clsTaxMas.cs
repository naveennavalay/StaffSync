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
    public class clsTaxMas
    {
        dbStaffSync.clsTaxMas objTaxInfo = new dbStaffSync.clsTaxMas();

        public List<TaxSchemeModel> GetTaxList()
        {

            List<TaxSchemeModel> objTaxSchemeModelList = new List<TaxSchemeModel>();

            objTaxSchemeModelList = objTaxInfo.GetTaxList();

            return objTaxSchemeModelList;
        }

        public EmpTaxSchemeModel getEmployeeSpecificTaxSchemeModel(int txtEmpID)
        {
            EmpTaxSchemeModel objSpecificTaxSchemeModel = new EmpTaxSchemeModel();

            objSpecificTaxSchemeModel = objTaxInfo.getEmployeeSpecificTaxSchemeModel(txtEmpID);

            return objSpecificTaxSchemeModel;
        }

        public int InsertEmployeeTaxSchemeModel(int txtEmpID, int txtTaxSchemeID, DateTime txtEffectiveDate)
        {
            int affectedRows = 0;
            
            affectedRows = objTaxInfo.InsertEmployeeTaxSchemeModel(txtEmpID, txtTaxSchemeID, txtEffectiveDate);

            return affectedRows;
        }

        public int UpdateEmployeeTaxSchemeModel(int txtEmpTaxSchemeID, int txtEmpID, int txtTaxSchemeID, DateTime txtEffectiveDate)
        {
            int affectedRows = 0;
            
            affectedRows = objTaxInfo.UpdateEmployeeTaxSchemeModel(txtEmpTaxSchemeID, txtEmpID, txtTaxSchemeID, txtEffectiveDate);

            return affectedRows;
        }

        public int DeleteEmployeeTaxSchemeModel(int txtEmpTaxSchemeID)
        {
            int affectedRows = 0;

            affectedRows = objTaxInfo.DeleteEmployeeTaxSchemeModel(txtEmpTaxSchemeID);

            return affectedRows;
        }
    }
}
