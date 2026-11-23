namespace WebStaffSync.Models
{
    public sealed class MyProfileViewModel
    {
        public string? EmployeeCode { get; set; }
        public string? EmployeeName { get; set; }
        public string? Status { get; set; }
        public string? PhotoDataUrl { get; set; }
        public List<ProfileField> PersonalInfo { get; set; } = new();
        public List<ProfileField> ProfessionalInfo { get; set; } = new();
        public List<ProfileField> ContactInfo { get; set; } = new();
        public List<ProfileField> NominationInfo { get; set; } = new();
        public List<ProfileField> BankInfo { get; set; } = new();
        public List<ProfileField> StatutoryInfo { get; set; } = new();
        public List<ProfileField> SalarySummary { get; set; } = new();
        public List<ProfileField> LeaveSummary { get; set; } = new();
        public List<ProfileRow> FamilyMembers { get; set; } = new();
        public List<ProfileRow> PreviousExperience { get; set; } = new();
        public List<string> Skills { get; set; } = new();
        public List<string> Education { get; set; } = new();
        public List<ProfileRow> Documents { get; set; } = new();
        public List<ProfileRow> LeaveBalances { get; set; } = new();
        public List<ProfileRow> SalaryComponents { get; set; } = new();
        public List<ProfileGridColumn> SalaryComponentColumns { get; set; } = new();
        public string? TaxScheme { get; set; }
        public string? EffectiveFrom { get; set; }
        public string? WeeklyOff { get; set; }
        public string? SameAsCurrentAddress { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public sealed class ProfileField
    {
        public string Label { get; set; } = "";
        public string Value { get; set; } = "";
    }

    public sealed class ProfileRow
    {
        public Dictionary<string, string> Values { get; set; } = new(StringComparer.OrdinalIgnoreCase);
    }
}


namespace WebStaffSync.Models
{
    /// <summary>
    /// Client-side Kendo Grid settings emitted by IntegrationService from
    /// ModelStaffSync GridColumn attributes.
    /// </summary>
    public sealed class ProfileGridColumn
    {
        public string Field { get; set; } = "";
        public string SourceName { get; set; } = "";
        public string Title { get; set; } = "";
        public bool Visible { get; set; } = true;
        public int Width { get; set; } = 150;
        public string Alignment { get; set; } = "left";
        public string? Format { get; set; }
        public string DataType { get; set; } = "string";
    }
}
