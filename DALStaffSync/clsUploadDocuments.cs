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
    public class clsUploadDocuments
    {
        dbStaffSync.clsUploadDocuments objUploadDocuments = new dbStaffSync.clsUploadDocuments();

        public clsUploadDocuments() { 

        }

        public int InsertDocumentUpload(string txtDocName, string txtDocType, DateTime txtDocUploadDate, string txtDocumentPath, bool DocUploadStatus)
        {
            int affectedRows = 0;

            affectedRows = objUploadDocuments.InsertDocumentUpload(txtDocName, txtDocType, txtDocUploadDate, txtDocumentPath, DocUploadStatus);

            return affectedRows;
        }

        public int InsertLinkUpdatedDocuments(int txtEmpID, int txtDocID)
        {
            int affectedRows = 0;

            affectedRows = objUploadDocuments.InsertLinkUpdatedDocuments(txtEmpID, txtDocID);

            return affectedRows;
        }

        public int UpdateLinkUpdatedDocuments(int txtEmpDocumentID, int txtEmpID, int txtDocID)
        {
            int affectedRows = 0;

            affectedRows = objUploadDocuments.UpdateLinkUpdatedDocuments(txtEmpDocumentID, txtEmpID, txtDocID);

            return affectedRows;
        }

        public List<EmployeeDocumentInfo> getEmployeeSpecificDocumentsList(int txtEmpID)
        {
            List<EmployeeDocumentInfo> employeeDocumentInfo = new List<EmployeeDocumentInfo>();

            employeeDocumentInfo = objUploadDocuments.getEmployeeSpecificDocumentsList(txtEmpID);

            return employeeDocumentInfo;
        }

        public EmployeeDocumentInfo getSpecificDocumentInfo(int txtDocumentID)
        {
            EmployeeDocumentInfo employeeDocumentInfo = new EmployeeDocumentInfo();

            employeeDocumentInfo = objUploadDocuments.getSpecificDocumentInfo(txtDocumentID);

            return employeeDocumentInfo;
        }


        public EmployeeDocumentInfo isDocumentReferenced(int txtEmpID, int txtDocumentID)
        {
            EmployeeDocumentInfo employeeDocumentInfo = new EmployeeDocumentInfo();

            employeeDocumentInfo = objUploadDocuments.isDocumentReferenced(txtEmpID, txtDocumentID);

            return employeeDocumentInfo;
        }
    }
}
