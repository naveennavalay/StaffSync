using System;
using System.ComponentModel;
using System.Linq;

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
        [GridColumn(Visible = true, Width = 150, Alignment = "left", DataType = "date", Format = "dd-MMM-yyyy")]
        public DateTime? FamMemDOB { get; set; }

        [DisplayName("Age")]
        [GridColumn(Visible = true, Width = 100, Alignment = "right", DataType = "number", Format = "0")]
        public int? FamMemAge { get; set; }

        [DisplayName("Relationship")]
        [GridColumn(Visible = true, Width = 150, Alignment = "left")]
        public string FamMemRelationship { get; set; }

        [DisplayName("Address 01")]
        [GridColumn(Visible = false, Width = 100)]
        public string FamMemAddr1 { get; set; }

        [DisplayName("Address 02")]
        [GridColumn(Visible = false, Width = 100)]
        public string FamMemAddr2 { get; set; }

        [DisplayName("Area")]
        [GridColumn(Visible = false, Width = 100)]
        public string FamMemArea { get; set; }

        [DisplayName("City")]
        [GridColumn(Visible = false, Width = 100)]
        public string FamMemCity { get; set; }

        [DisplayName("State")]
        [GridColumn(Visible = false, Width = 100)]
        public string FamMemState { get; set; }

        [DisplayName("PIN")]
        [GridColumn(Visible = false, Width = 100)]
        public string FamMemPIN { get; set; }

        [DisplayName("Country")]
        [GridColumn(Visible = false, Width = 100)]
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
        [GridColumn(Visible = true, Width = 110, Alignment = "center", DataType = "boolean", TrueText = "Yes", FalseText = "No")]
        public bool FamMemInsuranceEnrolled { get; set; }

        [DisplayName("Contact Person")]
        [GridColumn(Visible = false, Width = 120)]
        public bool FamMemIsContactPerson { get; set; }

        [DisplayName("Nominee")]
        [GridColumn(Visible = false, Width = 120)]
        public bool FamMemIsNomineePerson { get; set; }

        [GridColumn(Visible = false, Width = 100)]
        public int EmpID { get; set; }


        [DisplayName("Address")]
        [GridColumn(Visible = true, Width = 400, Alignment = "left")]
        public string Address
        {
            get
            {
                return string.Join(", ",
                    new[]
                    {
                        FamMemAddr1,
                        FamMemAddr2,
                        FamMemArea,
                        FamMemCity,
                        FamMemState,
                        FamMemPIN,
                        FamMemCountry
                    }
                    .Where(value => !string.IsNullOrWhiteSpace(value))
                    .Select(value => value.Trim()));
            }
        }
    }
}
