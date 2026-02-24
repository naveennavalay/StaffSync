//using C1.Framework;
using ModelStaffSync;
using Newtonsoft.Json;
//using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
//using static C1.Util.Win.Win32;

namespace DALStaffSync
{
    public class clsSalaryProfile
    {
        dbStaffSync.clsSalaryProfile objSalaryProfile = new dbStaffSync.clsSalaryProfile();

        public SpecificEmployeeSalaryInfo getSpecificEmployeeSalaryInfo(int txtEmpID)
        {
            SpecificEmployeeSalaryInfo specificEmployeeSalaryInfo = new SpecificEmployeeSalaryInfo();

            specificEmployeeSalaryInfo = objSalaryProfile.getSpecificEmployeeSalaryInfo(txtEmpID);

            return specificEmployeeSalaryInfo;
        }
        public int getEmployeeSalaryConfigStructure(int txtEmpID)
        {
            return objSalaryProfile.getEmployeeSalaryConfigStructure(txtEmpID);
        }

        public List<SalaryProfileInfo> getEmployeeSpecificSalaryConfigStructure(int txtEmpID, int txtEmpSalID)
        {
            return objSalaryProfile.getEmployeeSpecificSalaryConfigStructure(txtEmpID, txtEmpSalID);
        }

        public decimal getOriginalSalaryActualAmount(int txtEmpID, int txtSalHeaderID, string txtHeaderType)
        {
            return objSalaryProfile.getOriginalSalaryActualAmount(txtEmpID, txtSalHeaderID, txtHeaderType);
        }

        public SpecificEmployeeSalaryProfileInfo getEmployeeSpecificSalaryProfile(int txtEmpID)
        {
            SpecificEmployeeSalaryProfileInfo specificEmployeeSalaryProfileInfo = new SpecificEmployeeSalaryProfileInfo();
            
            specificEmployeeSalaryProfileInfo = objSalaryProfile.getEmployeeSpecificSalaryProfile(txtEmpID);

            return specificEmployeeSalaryProfileInfo;
        }

        public List<SalaryProfileTitleList> GetSalProfileTitleList()
        {
            DataTable dt = new DataTable();
            List<SalaryProfileTitleList> objSalaryProfileInfoList = new List<SalaryProfileTitleList>();

            objSalaryProfileInfoList = objSalaryProfile.GetSalProfileTitleList();

            return objSalaryProfileInfoList;
        }

        public List<SalaryProfileTitleList> GetSalProfileTitleList(string filterText)
        {
            DataTable dt = new DataTable();
            List<SalaryProfileTitleList> objSalaryProfileInfoList = new List<SalaryProfileTitleList>();

            objSalaryProfileInfoList = objSalaryProfile.GetSalProfileTitleList(filterText);

            return objSalaryProfileInfoList;
        }

        public DataTable GetSalaryInfoForBatchProcess(DateTime dtSalaryDate)
        {
            DataTable dt = new DataTable();
            dt = objSalaryProfile.GetSalaryInfoForBatchProcess(dtSalaryDate);
            return dt;
        }

        public List<SalaryProfileInfo> GetDefaultSalaryProfileInfo(int SalaryProfileID)
        {
            List<SalaryProfileInfo> objReturnSalaryProfileInfoList = new List<SalaryProfileInfo>();

            objReturnSalaryProfileInfoList = objSalaryProfile.GetDefaultSalaryProfileInfo(SalaryProfileID);

            return objReturnSalaryProfileInfoList;
        }

        public List<SalaryProfileInfo> GetEmployeeSpecificSalaryProfileInfo(int txtEmpID)
        {
            List<SalaryProfileInfo> objReturnSalaryProfileInfoList = new List<SalaryProfileInfo>();

            objReturnSalaryProfileInfoList = objSalaryProfile.GetEmployeeSpecificSalaryProfileInfo(txtEmpID);

            return objReturnSalaryProfileInfoList;
        }

        public int GetAllowenceProfileDetailID(int txtSalaryProfileID, int txtAllowenceID)
        {
            int SalProDetID = 0;
            SalProDetID = objSalaryProfile.GetAllowenceProfileDetailID(txtSalaryProfileID, txtAllowenceID);
            return SalProDetID;
        }

        public int GetDeductionProfileDetailID(int txtSalaryProfileID, int txtDeductionID)
        {
            int SalProDetID = 0;
            SalProDetID = objSalaryProfile.GetDeductionProfileDetailID(txtSalaryProfileID, txtDeductionID);
            return SalProDetID;
        }

        public int GetReimbursementProfileDetailID(int txtSalaryProfileID, int txtReimbursementID)
        {
            int SalProDetID = 0;
            SalProDetID = objSalaryProfile.GetReimbursementProfileDetailID(txtSalaryProfileID, txtReimbursementID);
            return SalProDetID;
        }

        public int IsSalaryAlreadyProcessed(string strMonthYear)
        {
            int intSalaryProcessedID = 0;
            intSalaryProcessedID = objSalaryProfile.IsSalaryAlreadyProcessed(strMonthYear);
            return intSalaryProcessedID;
        }

        public int InsertSalaryProfileInfo(string txtSalProfileCode, string txtSalProfileTitle, string txtSalProfileDescription, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;

            affectedRows = objSalaryProfile.InsertSalaryProfileInfo(txtSalProfileCode, txtSalProfileTitle, txtSalProfileDescription, IsActive, IsDeleted);

            return affectedRows;
        }

        public int InsertSalaryProfileDetailInfo(int txtSalProfileID, int txtAllID, int txtDedID, int txtReimbID, decimal txtSalProAmount)
        {
            int affectedRows = 0;

            affectedRows = objSalaryProfile.InsertSalaryProfileDetailInfo(txtSalProfileID, txtAllID, txtDedID, txtReimbID, txtSalProAmount);

            return affectedRows;
        }

        public int UpdateSalaryProfileInfo(int txtSalaryProfileID, string txtSalaryProfileCode, string txtSalaryProfileTitle, string txtSalaryProfileDescription, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;

            affectedRows = objSalaryProfile.UpdateSalaryProfileInfo( txtSalaryProfileID, txtSalaryProfileCode, txtSalaryProfileTitle, txtSalaryProfileDescription, IsActive, IsDeleted);

            return affectedRows;
        }

        public int UpdateSalaryProfileInfoAutomaticCalculationStatus(int txtSalaryProfileID, bool IsAutomaticCalculationRequired)
        {
            int affectedRows = 0;

            affectedRows = objSalaryProfile.UpdateSalaryProfileInfoAutomaticCalculationStatus(txtSalaryProfileID, IsAutomaticCalculationRequired);

            return affectedRows;
        }

        public int UpdateSalaryProfileDetailInfo(int SalProDetID, int txtSalProfileID, int txtAllID, int txtDedID, int txtReimbID, decimal txtSalProAmount)
        {
            int affectedRows = 0;

            affectedRows = objSalaryProfile.UpdateSalaryProfileDetailInfo(SalProDetID, txtSalProfileID, txtAllID, txtDedID, txtReimbID, txtSalProAmount);

            return affectedRows;
        }

        public int DeleteSalaryProfileInfo(int txtSalaryProfileID)
        {
            int affectedRows = 0;

            affectedRows = objSalaryProfile.DeleteSalaryProfileInfo(txtSalaryProfileID);

            return affectedRows;
        }
    }
}
