using ModelStaffSync;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaffSync
{
    public partial class frmAuditLogStatements : Form
    {
        //DALStaffSync.clsAssetsCategory objAssetCategory = new DALStaffSync.clsAssetsCategory();
        DALStaffSync.clsAssetsInfo objAssetsInfo = new DALStaffSync.clsAssetsInfo();
        DALStaffSync.clsAuditLog objAuditLog = new DALStaffSync.clsAuditLog();

        public frmAuditLogStatements()
        {
            InitializeComponent();
        }

        public frmAuditLogStatements(int SourceID, string EventGroup, string ModuleName, int ClientID)
        {
            InitializeComponent();
            lblSourceID.Text = SourceID.ToString();
            lblEventGroup.Text = EventGroup.ToString();
            lblFileName.Text = ModuleName.ToString();
            lblClientID.Text = ClientID.ToString();
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAuditLogStatements_Load(object sender, EventArgs e)
        {
            dtgAuditLogStatements.DataSource = objAuditLog.getAuditLogStatements(Convert.ToInt32(lblSourceID.Text.ToString()), lblEventGroup.Text.ToString(), Convert.ToInt32(lblClientID.Text.ToString()));
            FormatGrid();
        }

        private void FormatGrid()
        {
            dtgAuditLogStatements.Columns["UserAuditLogID"].ReadOnly = true;
            dtgAuditLogStatements.Columns["UserAuditLogID"].Width = 50;
            dtgAuditLogStatements.Columns["UserAuditLogID"].Visible = false;

            dtgAuditLogStatements.Columns["SourceID"].ReadOnly = true;
            dtgAuditLogStatements.Columns["SourceID"].Width = 50;
            dtgAuditLogStatements.Columns["SourceID"].Visible = false;

            dtgAuditLogStatements.Columns["EventDateTime"].ReadOnly = true;
            dtgAuditLogStatements.Columns["EventDateTime"].Width = 150;
            dtgAuditLogStatements.Columns["EventDateTime"].DefaultCellStyle.Format = "dd-MMM-yyyy hh:mm:ss tt";

            dtgAuditLogStatements.Columns["AuditLogStatement"].ReadOnly = true;
            dtgAuditLogStatements.Columns["AuditLogStatement"].Width = 500;

            dtgAuditLogStatements.Columns["ActionType"].ReadOnly = true;
            dtgAuditLogStatements.Columns["ActionType"].Width = 100;

            dtgAuditLogStatements.Columns["UserName"].ReadOnly = true;
            dtgAuditLogStatements.Columns["UserName"].Width = 200;

            dtgAuditLogStatements.Columns["EventGroup"].Visible = false;
            dtgAuditLogStatements.Columns["ClientID"].Visible = false;
        }

        private void btnCloseMe_Click_1(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txtSearch.Text))
                {
                    dtgAuditLogStatements.DataSource = objAuditLog.getAuditLogStatements(Convert.ToInt32(lblSourceID.Text.ToString()), lblEventGroup.Text.ToString(), Convert.ToInt32(lblClientID.Text.ToString()));
                }
                else
                {
                    dtgAuditLogStatements.DataSource = objAuditLog.getAuditLogStatements(Convert.ToInt32(lblSourceID.Text.ToString()), txtSearch.Text.ToString().Trim(), lblEventGroup.Text.ToString(), Convert.ToInt32(lblClientID.Text.ToString()));
                }
                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgDepartmentList_DoubleClick(object sender, EventArgs e)
        {
            //this.Close();
        }

        private void frmAuditLogStatements_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void picDownloadDocumentsList_Click(object sender, EventArgs e)
        {
            //string filePath = AppVariables.TempFolderPath + @"\" +  lblFileName + " - " + DateTime.Now.ToString() + ".csv";
            //bool ReportGenerated = Download.DownloadExcel(filePath, dtgAuditLogStatements);
            //if (ReportGenerated)
            //    Download.OpenCSV(filePath);
        }
    }
}
