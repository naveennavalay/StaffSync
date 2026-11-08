using System;
using System.Drawing;
using System.Windows.Forms;

namespace StaffSync
{
    // UI prototype only. Database/DAL integration is intentionally pending.
    public class frmEmpTaskDelegates : Form
    {
        private GroupBox grpMoreOptions;
        private CheckBox chkAssetsInformation;
        private CheckBox chkReimbStatements;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private CheckBox checkBox3;
        private CheckBox checkBox4;
        private CheckBox chkQualSkillsInfo;
        private CheckBox chkInsuredMemberInfo;
        private CheckBox chkNomineeInfo;
        private CheckBox chkFamilyMembersInfo;

        public frmEmpTaskDelegates()
        {

        }

        private void InitializeComponent()
        {
            this.grpMoreOptions = new System.Windows.Forms.GroupBox();
            this.chkAssetsInformation = new System.Windows.Forms.CheckBox();
            this.chkReimbStatements = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.chkQualSkillsInfo = new System.Windows.Forms.CheckBox();
            this.chkInsuredMemberInfo = new System.Windows.Forms.CheckBox();
            this.chkNomineeInfo = new System.Windows.Forms.CheckBox();
            this.chkFamilyMembersInfo = new System.Windows.Forms.CheckBox();
            this.grpMoreOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMoreOptions
            // 
            this.grpMoreOptions.Controls.Add(this.chkAssetsInformation);
            this.grpMoreOptions.Controls.Add(this.chkReimbStatements);
            this.grpMoreOptions.Controls.Add(this.checkBox1);
            this.grpMoreOptions.Controls.Add(this.checkBox2);
            this.grpMoreOptions.Controls.Add(this.checkBox3);
            this.grpMoreOptions.Controls.Add(this.checkBox4);
            this.grpMoreOptions.Controls.Add(this.chkQualSkillsInfo);
            this.grpMoreOptions.Controls.Add(this.chkInsuredMemberInfo);
            this.grpMoreOptions.Controls.Add(this.chkNomineeInfo);
            this.grpMoreOptions.Controls.Add(this.chkFamilyMembersInfo);
            this.grpMoreOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpMoreOptions.Location = new System.Drawing.Point(14, 292);
            this.grpMoreOptions.Margin = new System.Windows.Forms.Padding(4);
            this.grpMoreOptions.Name = "grpMoreOptions";
            this.grpMoreOptions.Padding = new System.Windows.Forms.Padding(4);
            this.grpMoreOptions.Size = new System.Drawing.Size(1175, 126);
            this.grpMoreOptions.TabIndex = 68;
            this.grpMoreOptions.TabStop = false;
            this.grpMoreOptions.Text = "More Options";
            this.grpMoreOptions.Visible = false;
            // 
            // chkAssetsInformation
            // 
            this.chkAssetsInformation.AutoSize = true;
            this.chkAssetsInformation.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAssetsInformation.Checked = true;
            this.chkAssetsInformation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAssetsInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkAssetsInformation.Location = new System.Drawing.Point(670, 50);
            this.chkAssetsInformation.Name = "chkAssetsInformation";
            this.chkAssetsInformation.Size = new System.Drawing.Size(144, 19);
            this.chkAssetsInformation.TabIndex = 94;
            this.chkAssetsInformation.Tag = "Assets Information";
            this.chkAssetsInformation.Text = "Assets Information";
            this.chkAssetsInformation.UseVisualStyleBackColor = true;
            // 
            // chkReimbStatements
            // 
            this.chkReimbStatements.AutoSize = true;
            this.chkReimbStatements.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkReimbStatements.Checked = true;
            this.chkReimbStatements.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkReimbStatements.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkReimbStatements.Location = new System.Drawing.Point(618, 25);
            this.chkReimbStatements.Name = "chkReimbStatements";
            this.chkReimbStatements.Size = new System.Drawing.Size(196, 19);
            this.chkReimbStatements.TabIndex = 93;
            this.chkReimbStatements.Tag = "Reimbursment Statements";
            this.chkReimbStatements.Text = "Reimbursment Statements";
            this.chkReimbStatements.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.checkBox1.Location = new System.Drawing.Point(290, 100);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(227, 19);
            this.checkBox1.TabIndex = 92;
            this.checkBox1.Tag = "Qualification & Skills Information";
            this.checkBox1.Text = "Qualification & Skills Information";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.checkBox2.Location = new System.Drawing.Point(366, 75);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(151, 19);
            this.checkBox2.TabIndex = 91;
            this.checkBox2.Tag = "Holiday Information";
            this.checkBox2.Text = "Holiday Information";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.checkBox3.Location = new System.Drawing.Point(422, 50);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(95, 19);
            this.checkBox3.TabIndex = 90;
            this.checkBox3.Tag = "Salary Slip";
            this.checkBox3.Text = "Salary Slip";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox4.Checked = true;
            this.checkBox4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.checkBox4.Location = new System.Drawing.Point(343, 25);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(174, 19);
            this.checkBox4.TabIndex = 89;
            this.checkBox4.Tag = "Attendance Information";
            this.checkBox4.Text = "Attendance Information";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // chkQualSkillsInfo
            // 
            this.chkQualSkillsInfo.AutoSize = true;
            this.chkQualSkillsInfo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkQualSkillsInfo.Checked = true;
            this.chkQualSkillsInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkQualSkillsInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkQualSkillsInfo.Location = new System.Drawing.Point(13, 100);
            this.chkQualSkillsInfo.Name = "chkQualSkillsInfo";
            this.chkQualSkillsInfo.Size = new System.Drawing.Size(227, 19);
            this.chkQualSkillsInfo.TabIndex = 88;
            this.chkQualSkillsInfo.Tag = "Qualification & Skills Information";
            this.chkQualSkillsInfo.Text = "Qualification & Skills Information";
            this.chkQualSkillsInfo.UseVisualStyleBackColor = true;
            // 
            // chkInsuredMemberInfo
            // 
            this.chkInsuredMemberInfo.AutoSize = true;
            this.chkInsuredMemberInfo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkInsuredMemberInfo.Checked = true;
            this.chkInsuredMemberInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInsuredMemberInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkInsuredMemberInfo.Location = new System.Drawing.Point(60, 75);
            this.chkInsuredMemberInfo.Name = "chkInsuredMemberInfo";
            this.chkInsuredMemberInfo.Size = new System.Drawing.Size(180, 19);
            this.chkInsuredMemberInfo.TabIndex = 87;
            this.chkInsuredMemberInfo.Tag = "Insured Member Details";
            this.chkInsuredMemberInfo.Text = "Insured Member Details";
            this.chkInsuredMemberInfo.UseVisualStyleBackColor = true;
            // 
            // chkNomineeInfo
            // 
            this.chkNomineeInfo.AutoSize = true;
            this.chkNomineeInfo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkNomineeInfo.Checked = true;
            this.chkNomineeInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNomineeInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkNomineeInfo.Location = new System.Drawing.Point(79, 50);
            this.chkNomineeInfo.Name = "chkNomineeInfo";
            this.chkNomineeInfo.Size = new System.Drawing.Size(161, 19);
            this.chkNomineeInfo.TabIndex = 86;
            this.chkNomineeInfo.Tag = "Nominee Information";
            this.chkNomineeInfo.Text = "Nominee Information";
            this.chkNomineeInfo.UseVisualStyleBackColor = true;
            // 
            // chkFamilyMembersInfo
            // 
            this.chkFamilyMembersInfo.AutoSize = true;
            this.chkFamilyMembersInfo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFamilyMembersInfo.Checked = true;
            this.chkFamilyMembersInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFamilyMembersInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkFamilyMembersInfo.Location = new System.Drawing.Point(31, 25);
            this.chkFamilyMembersInfo.Name = "chkFamilyMembersInfo";
            this.chkFamilyMembersInfo.Size = new System.Drawing.Size(209, 19);
            this.chkFamilyMembersInfo.TabIndex = 85;
            this.chkFamilyMembersInfo.Tag = "Family Members Information";
            this.chkFamilyMembersInfo.Text = "Family Members Information";
            this.chkFamilyMembersInfo.UseVisualStyleBackColor = true;
            // 
            // frmEmpTaskDelegates
            // 
            this.ClientSize = new System.Drawing.Size(1202, 711);
            this.Controls.Add(this.grpMoreOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmEmpTaskDelegates";
            this.grpMoreOptions.ResumeLayout(false);
            this.grpMoreOptions.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
