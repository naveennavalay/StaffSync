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
    public class clsPhotoMas
    {
        dbStaffSync.clsPhotoMas objPhotoMas = new dbStaffSync.clsPhotoMas();

        public clsPhotoMas() { 

        }

        public PhotoInfo getEmployeePhoto(int txtEmpID)
        {
            PhotoInfo photoInfo = new PhotoInfo();

            photoInfo = objPhotoMas.getEmployeePhoto(txtEmpID);

            return photoInfo;
        }

        public int InsertPhotoInfo(int txtEmpID, byte[] txtPhoto)
        {
            int affectedRows = 0;

            affectedRows = objPhotoMas.InsertPhotoInfo(txtEmpID, txtPhoto);

            return affectedRows;
        }

        public int UpdatePhotoInfo(int txtEmpID, byte[] txtPhoto)
        {
            int affectedRows = 0;

            affectedRows = objPhotoMas.UpdatePhotoInfo(txtEmpID, txtPhoto);

            return affectedRows;
        }

        public int UpdateCompanyLogoInfo(int txtCompanyID, byte[] txtPhoto)
        {
            int affectedRows = 0;

            affectedRows = objPhotoMas.UpdateCompanyLogoInfo(txtCompanyID, txtPhoto);

            return affectedRows;
        }

        public CompanyLogoInfo getCompanyLogo(int txtCompanyID)
        {
            CompanyLogoInfo photoInfo = new CompanyLogoInfo();

            photoInfo = objPhotoMas.getCompanyLogo(txtCompanyID);

            return photoInfo;
        }

        public int UpdateCompanyBranchLogoInfo(int txtCompanyBranchID, byte[] txtPhoto)
        {
            int affectedRows = 0;

            affectedRows = objPhotoMas.UpdateCompanyBranchLogoInfo(txtCompanyBranchID, txtPhoto);

            return affectedRows;
        }

        public CompanyBranchLogoInfo getCompanyBranchLogo(int txtCompanyBranchID)
        {
            CompanyBranchLogoInfo photoInfo = new CompanyBranchLogoInfo();

            photoInfo = objPhotoMas.getCompanyBranchLogo(txtCompanyBranchID);

            return photoInfo;
        }
    }
}
