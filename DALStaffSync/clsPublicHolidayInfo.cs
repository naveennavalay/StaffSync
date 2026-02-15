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
    public class clsPublicHolidayInfo
    {
        dbStaffSync.clsPublicHolidayInfo objPublicHolidayInfo = new dbStaffSync.clsPublicHolidayInfo();

        public clsPublicHolidayInfo() { 

        }

        public List<PublicHolidayType> getHolidayTypeList()
        {
            List<PublicHolidayType> lstPublicHolidayType = new List<PublicHolidayType>();
            
            lstPublicHolidayType = objPublicHolidayInfo.getHolidayTypeList();

            return lstPublicHolidayType;
        }

        public int InsertPublicHolidayMasterInfo(string txtPublicHolidayYear, bool IsDefault, int txtOrderID, int txtFinYearID)
        {
            int affectedRows = 0;

            affectedRows = objPublicHolidayInfo.InsertPublicHolidayMasterInfo(txtPublicHolidayYear, IsDefault, txtOrderID, txtFinYearID);

            return affectedRows;
        }

        public int InsertPublicHolidayDetailInfo(int txtPubHolMasID, string txtPublicHolidayTitle, DateTime txtPublicHolidayDate, int txtPubHolTypeID, int txtOrderID)
        {
            int affectedRows = 0;

            affectedRows = objPublicHolidayInfo.InsertPublicHolidayDetailInfo(txtPubHolMasID, txtPublicHolidayTitle, txtPublicHolidayDate, txtPubHolTypeID, txtOrderID);

            return affectedRows;
        }


        public int UpdatePublicHolidayDetailInfo(int txtPubHolDetID, int txtPubHolMasID, string txtPublicHolidayTitle, DateTime txtPublicHolidayDate, int txtPubHolTypeID, int txtOrderID)
        {
            int affectedRows = 0;

            affectedRows = objPublicHolidayInfo.UpdatePublicHolidayDetailInfo(txtPubHolDetID, txtPubHolMasID, txtPublicHolidayTitle, txtPublicHolidayDate, txtPubHolTypeID, txtOrderID);

            return affectedRows;
        }

        public int DeletePublicHolidayDetailInfo(int txtPubHolDetID)
        {
            int affectedRows = 0;

            affectedRows = objPublicHolidayInfo.DeletePublicHolidayDetailInfo(txtPubHolDetID);

            return affectedRows;
        }

        public List<PublicHolidayInfo> GetHolidayDetailsInfo(int txtYearID)
        {
            List<PublicHolidayInfo> lstPublicHolidayInfo = new List<PublicHolidayInfo>();

            lstPublicHolidayInfo = objPublicHolidayInfo.GetHolidayDetailsInfo(txtYearID);

            return lstPublicHolidayInfo;
        }
    }
}
