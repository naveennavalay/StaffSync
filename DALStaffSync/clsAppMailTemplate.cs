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
    public class clsAppMailTemplate
    {
        dbStaffSync.clsAppMailTemplate objAppMailTemplate = new dbStaffSync.clsAppMailTemplate();

        public List<AppMailTemplateModel> GetAppMailTemplateList()
        {
            List<AppMailTemplateModel> objAppMailTemplateList = new List<AppMailTemplateModel>();

            objAppMailTemplateList = objAppMailTemplate.GetAppMailTemplateList();

            return objAppMailTemplateList;
        }

        public AppMailTemplateModel GetSpecificAppMailTemplateByID(int appMailTempID)
        {
            AppMailTemplateModel objSelectedAppMailTemplate = new AppMailTemplateModel();

            objSelectedAppMailTemplate = objAppMailTemplate.GetSpecificAppMailTemplateByID(appMailTempID);

            return objSelectedAppMailTemplate;
        }

        public AppMailTemplateModel GetSpecificAppMailTemplateByName(string appMailTempName)
        {
            AppMailTemplateModel objSelectedAppMailTemplate = new AppMailTemplateModel();

            objSelectedAppMailTemplate = objAppMailTemplate.GetSpecificAppMailTemplateByName(appMailTempName);

            return objSelectedAppMailTemplate;
        }
    }
}
