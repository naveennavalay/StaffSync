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
    public class clsEmployeePersonalIDInfo
    {
        dbStaffSync.clsEmployeePersonalIDInfo objEmployeePersonalIDInfo = new dbStaffSync.clsEmployeePersonalIDInfo();

        public clsEmployeePersonalIDInfo() { 

        }

        public int InsertEmployeePersonalIDInfo(int txtPersonalInfoID, string txtAadhaarCardNumber, string txtVoterCardNumber, string txtPANNumber, string txtPassportNumber, DateTime txtPassportIssueDate, DateTime txtPassportRenewalDate, string txtID1, string txtID2, string txtID3, string txtID4, string txtID5, bool PFApplicable, string PFAccNumber, DateTime PFJoiningDate, DateTime PFRelievingDate, bool PTApplicable, string PTAccNumber, bool ESIApplicable, string ESIAccNumber, string ESIDispensary, bool NPSApplicable, string NPSAccNumber)
        {
            int affectedRows = 0;

            affectedRows = objEmployeePersonalIDInfo.InsertEmployeePersonalIDInfo(txtPersonalInfoID, txtAadhaarCardNumber, txtVoterCardNumber, txtPANNumber, txtPassportNumber, txtPassportIssueDate, txtPassportRenewalDate, txtID1, txtID2, txtID3, txtID4, txtID5, PFApplicable, PFAccNumber, PFJoiningDate, PFRelievingDate, PTApplicable, PTAccNumber, ESIApplicable, ESIAccNumber, ESIDispensary, NPSApplicable, NPSAccNumber);

            return affectedRows;
        }

        public int UpdateEmployeePersonalIDInfo(int txtPersonalIDInfoID, int txtPersonalInfoID, string txtAadhaarCardNumber, string txtVoterCardNumber, string txtPANNumber, string txtPassportNumber, DateTime txtPassportIssueDate, DateTime txtPassportRenewalDate, string txtID1, string txtID2, string txtID3, string txtID4, string txtID5, bool PFApplicable, string PFAccNumber, DateTime PFJoiningDate, DateTime PFRelievingDate, bool PTApplicable, string PTAccNumber, bool ESIApplicable, string ESIAccNumber, string ESIDispensary, bool NPSApplicable, string NPSAccNumber)
        {
            int affectedRows = 0;

            affectedRows = objEmployeePersonalIDInfo.UpdateEmployeePersonalIDInfo(txtPersonalIDInfoID, txtPersonalInfoID, txtAadhaarCardNumber, txtVoterCardNumber, txtPANNumber, txtPassportNumber, txtPassportIssueDate, txtPassportRenewalDate, txtID1, txtID2, txtID3, txtID4, txtID5, PFApplicable, PFAccNumber, PFJoiningDate, PFRelievingDate, PTApplicable, PTAccNumber, ESIApplicable, ESIAccNumber, ESIDispensary, NPSApplicable, NPSAccNumber);

            return affectedRows;
        }

        public EmpPersonalIDInfo GetEmpPersonalIDInfo(int txtPersonalInfoID)
        {
            EmpPersonalIDInfo objEmpPersonalIDInfo = new EmpPersonalIDInfo();

            objEmpPersonalIDInfo = objEmployeePersonalIDInfo.GetEmpPersonalIDInfo(txtPersonalInfoID);

            return objEmpPersonalIDInfo;
        }
    }
}
