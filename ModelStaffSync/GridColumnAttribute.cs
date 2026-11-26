using System;

namespace ModelStaffSync
{
    /// <summary>
    /// Defines generic Kendo Grid column behaviour for a model property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class GridColumnAttribute : Attribute
    {
        public bool Visible { get; set; } = true;
        public int Width { get; set; } = 150;
        public string Alignment { get; set; } = "left";
        public string Format { get; set; }

        public int Order { get; set; } = -1;
        public bool Sortable { get; set; } = true;
        public bool Filterable { get; set; } = true;
        public bool Groupable { get; set; } = false;
        public bool Resizable { get; set; } = true;
        public bool Reorderable { get; set; } = true;
        public bool Locked { get; set; } = false;
        public bool Lockable { get; set; } = true;
        public bool Exportable { get; set; } = true;
        public string SortDirection { get; set; }
        public int SortOrder { get; set; } = -1;
        public int GroupOrder { get; set; } = -1;
        public string KendoOptionsJson { get; set; }
    }
}
