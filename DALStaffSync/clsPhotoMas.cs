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
    }
}
