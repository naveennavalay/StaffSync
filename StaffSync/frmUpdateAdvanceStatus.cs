using Krypton.Toolkit;
using ModelStaffSync;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.Ocsp;
using StaffSync.StaffsyncDBDataSetTableAdapters;
using StaffSync.StaffsyncDBDTSetTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StaffSync
{
    public partial class frmUpdateAdvanceStatus : Form
    {
        public string SelectedStatus { get; private set; }
        private int _AdvanceRequestID;

        public frmUpdateAdvanceStatus()
        {
            InitializeComponent();
        }

        public frmUpdateAdvanceStatus(int AdvanceRequestID, string CurrentStatus)
        {
            InitializeComponent();
            _AdvanceRequestID = AdvanceRequestID;

            cmbStatus.Items.AddRange(new object[]
            {
                "Pending",
                "In Progress",
                "Approved",
                "Cancelled",
                "Rejected"
            });

            cmbStatus.SelectedItem = CurrentStatus;
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUpdateAdvanceStatus_Load(object sender, EventArgs e)
        {

        }

        private void btnCloseMe_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUpdateAdvanceStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedStatus = cmbStatus.Text;
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
