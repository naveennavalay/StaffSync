using System;
using System.Collections.Generic;

namespace WebStaffSync.Models
{
    public sealed class MyProfileViewModel
    {
        public string? EmployeeCode { get; set; }
        public string? EmployeeName { get; set; }
        public string? Status { get; set; }
        public string? PhotoDataUrl { get; set; }

        public string DataType { get; set; } = "string";
        public string TrueText { get; set; } = "Yes";
        public string FalseText { get; set; } = "No";

        public List<ProfileField> PersonalInfo { get; set; } = new();
        public List<ProfileField> ProfessionalInfo { get; set; } = new();
        public List<ProfileField> ContactInfo { get; set; } = new();
        public List<ProfileField> NominationInfo { get; set; } = new();
        public List<ProfileField> BankInfo { get; set; } = new();
        public List<ProfileField> EmployeeSpecificBankInfo { get; set; } = new();
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

        // Attribute-driven metadata for each independent grid.
        public List<ProfileGridColumn> FamilyMemberColumns { get; set; } = new();
        public List<ProfileGridColumn> DocumentColumns { get; set; } = new();
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

    /// <summary>Client-side metadata emitted by IntegrationService.</summary>
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
        public string TrueText { get; set; } = "Yes";
        public string FalseText { get; set; } = "No";
        public int Order { get; set; } = -1;
        public bool Sortable { get; set; } = true;
        public bool Filterable { get; set; } = true;
        public bool Groupable { get; set; }
        public bool Resizable { get; set; } = true;
        public bool Reorderable { get; set; } = true;
        public bool Locked { get; set; }
        public bool Lockable { get; set; } = true;
        public bool Exportable { get; set; } = true;
        public string? SortDirection { get; set; }
        public int SortOrder { get; set; } = -1;
        public int GroupOrder { get; set; } = -1;
        public string? KendoOptionsJson { get; set; }
    }
}
