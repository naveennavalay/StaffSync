using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using ModelStaffSync;
using Quartz.Impl.AdoJobStore.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Quartz.Logging.OperationName;

namespace StaffSync
{
    public partial class frmPublicHolidayConfigPopup : Form
    {
        DALStaffSync.clsPublicHolidayInfo objPublicHolidayInfo = new DALStaffSync.clsPublicHolidayInfo();
        PublicHolidayInfo objOriginalValues = new PublicHolidayInfo();
        public PublicHolidayInfo objSaveTheseValues = new PublicHolidayInfo();

        DateTime dob, doj;
        string dateFormat = "dd-MM-yyyy";
        CultureInfo provider = CultureInfo.InvariantCulture;


        public frmPublicHolidayConfigPopup()
        {
            InitializeComponent();
        }

        public frmPublicHolidayConfigPopup(PublicHolidayInfo objPublicHolidayInfo)
        {
            InitializeComponent();

            objOriginalValues = objPublicHolidayInfo;
            lblPubHolDetID.Text = objPublicHolidayInfo.PubHolDetID.ToString();
            txtHolidayName.Text = objPublicHolidayInfo.PubHolidayTitle;
            txtHolidayDate.Text = objPublicHolidayInfo.PubHolDate?.ToString("dd-MM-yyyy");
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPublicHolidayConfigPopup_Load(object sender, EventArgs e)
        {

        }

        private void btnCloseMe_Click_1(object sender, EventArgs e)
        {
            objSaveTheseValues = objOriginalValues;
            this.Close();
        }

        private void frmPublicHolidayConfigPopup_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //if (!ValidateValuesOnUI())
            //{
            //    this.Cursor = Cursors.Default;
            //    return;
            //}

            objSaveTheseValues.PubHolDetID = objOriginalValues.PubHolDetID;
            objSaveTheseValues.PubHolMasID = objOriginalValues.PubHolMasID;
            objSaveTheseValues.PubHolidayTitle = txtHolidayName.Text.ToString().Trim();
            if(txtHolidayDate.Text.ToString().Trim().Replace(" ", "") != "--")
                objSaveTheseValues.PubHolDate = Convert.ToDateTime(txtHolidayDate.Text.ToString());

            objSaveTheseValues.OrderID = objOriginalValues.OrderID;

            if(objSaveTheseValues.PubHolDetID == 0)
                objSaveTheseValues.PubHolDetID = objPublicHolidayInfo.InsertPublicHolidayDetailInfo(objSaveTheseValues.PubHolMasID, txtHolidayName.Text, Convert.ToDateTime(txtHolidayDate.Text), Convert.ToInt16(objSaveTheseValues.OrderID));
            else
            {
                if(!string.IsNullOrEmpty(objSaveTheseValues.PubHolidayTitle.ToString().Trim()) && (txtHolidayDate.Text.ToString().Trim().Replace(" ", "") != "--"))
                {
                    objSaveTheseValues.PubHolDetID = objPublicHolidayInfo.UpdatePublicHolidayDetailInfo(objSaveTheseValues.PubHolDetID, objSaveTheseValues.PubHolMasID, txtHolidayName.Text, Convert.ToDateTime(txtHolidayDate.Text), Convert.ToInt16(objSaveTheseValues.OrderID));
                }
                else
                {
                    objPublicHolidayInfo.DeletePublicHolidayDetailInfo(objSaveTheseValues.PubHolDetID);
                }
            }

            this.Close();
        }

        public bool ValidateValuesOnUI()
        {
            bool validationStatus = true;
            errValidator.Clear();

            if (string.IsNullOrEmpty(txtHolidayName.Text.ToString().Trim()))
            {
                validationStatus = false;
                txtHolidayName.Focus();
                errValidator.SetError(this.txtHolidayName, txtHolidayName.Tag?.ToString() ?? "Holiday's Name is required.");
            }
            if (string.IsNullOrEmpty(txtHolidayDate.Text.ToString().Trim()))
            {
                validationStatus = false;
                txtHolidayDate.Focus();
                errValidator.SetError(this.txtHolidayDate, txtHolidayDate.Tag?.ToString() ?? "Holiday's Date is required.");
            }
            else if (!DateTime.TryParseExact(txtHolidayDate.Text, dateFormat, provider, DateTimeStyles.None, out dob))
            {
                validationStatus = false;
                txtHolidayDate.Focus();
                errValidator.SetError(this.txtHolidayDate, "Invalid Date of Birth format (dd-MM-yyyy).");
            }
            //else if (dob > DateTime.Now.Date)
            //{
            //    validationStatus = false;
            //    txtHolidayDate.Focus();
            //    errValidator.SetError(this.txtHolidayDate, "Date of Birth cannot be in the future.");
            //}

            return validationStatus;
        }


        public PublicHolidayInfo getLatestValues()
        {
            this.Close();
            return objSaveTheseValues;
        }
    }
}
