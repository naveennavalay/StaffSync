using System;
using System.ComponentModel;

namespace ModelStaffSync
{
    public class EmpPersonalFamilyMemberInfo
    {
        [GridColumn(Visible = false, Width = 100)]
        public int EmpPerFamInfoID { get; set; }

        [GridColumn(Visible = false, Width = 100)]
        public int PersonalInfoID { get; set; }

        [DisplayName("Member Name")]
        [GridColumn(Visible = true, Width = 200, Alignment = "left")]
        public string FamMemName { get; set; }

        [DisplayName("Date Of Birth")]
        [GridColumn(Visible = true, Width = 150, Alignment = "left", Format = "dd-MMM-yyyy")]
        public DateTime? FamMemDOB { get; set; }

        [DisplayName("Age")]
        [GridColumn(Visible = true, Width = 100, Alignment = "right")]
        public int? FamMemAge { get; set; }

        [DisplayName("Relationship")]
        [GridColumn(Visible = true, Width = 150, Alignment = "left")]
        public string FamMemRelationship { get; set; }

        [DisplayName("Address 01")]
        [GridColumn(Visible = true, Width = 220, Alignment = "left")]
        public string FamMemAddr1 { get; set; }

        [DisplayName("Address 02")]
        [GridColumn(Visible = true, Width = 220, Alignment = "left")]
        public string FamMemAddr2 { get; set; }

        [DisplayName("Area")]
        [GridColumn(Visible = true, Width = 160, Alignment = "left")]
        public string FamMemArea { get; set; }

        [DisplayName("City")]
        [GridColumn(Visible = true, Width = 140, Alignment = "left")]
        public string FamMemCity { get; set; }

        [DisplayName("State")]
        [GridColumn(Visible = true, Width = 140, Alignment = "left")]
        public string FamMemState { get; set; }

        [DisplayName("PIN")]
        [GridColumn(Visible = true, Width = 100, Alignment = "left")]
        public string FamMemPIN { get; set; }

        [DisplayName("Country")]
        [GridColumn(Visible = true, Width = 140, Alignment = "left")]
        public string FamMemCountry { get; set; }

        [DisplayName("Contact Number")]
        [GridColumn(Visible = true, Width = 150, Alignment = "left")]
        public string FamMemContactNumber { get; set; }

        [DisplayName("Mail ID")]
        [GridColumn(Visible = true, Width = 200, Alignment = "left")]
        public string FamMemMailID { get; set; }

        [DisplayName("Blood Group")]
        [GridColumn(Visible = true, Width = 120, Alignment = "left")]
        public string FamMemBloodGroup { get; set; }

        [DisplayName("Is Insured?")]
        [GridColumn(Visible = true, Width = 110, Alignment = "center")]
        public bool FamMemInsuranceEnrolled { get; set; }

        [DisplayName("Contact Person")]
        [GridColumn(Visible = false, Width = 120)]
        public bool FamMemIsContactPerson { get; set; }

        [DisplayName("Nominee")]
        [GridColumn(Visible = false, Width = 120)]
        public bool FamMemIsNomineePerson { get; set; }

        [GridColumn(Visible = false, Width = 100)]
        public int EmpID { get; set; }
    }
}
