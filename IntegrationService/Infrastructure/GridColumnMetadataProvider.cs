using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using ModelStaffSync;

namespace IntegrationService.Infrastructure
{
    /// <summary>Builds generic Kendo metadata from ModelStaffSync model attributes.</summary>
    public static class GridColumnMetadataProvider
    {
        public sealed class GridColumnMetadata
        {
            public string Field { get; set; }
            public string SourceName { get; set; }
            public string Title { get; set; }
            public bool Visible { get; set; }
            public int Width { get; set; }
            public string Alignment { get; set; }
            public string Format { get; set; }
            public string DataType { get; set; }
            public int Order { get; set; }
            public bool Sortable { get; set; }
            public bool Filterable { get; set; }
            public bool Groupable { get; set; }
            public bool Resizable { get; set; }
            public bool Reorderable { get; set; }
            public bool Locked { get; set; }
            public bool Lockable { get; set; }
            public bool Exportable { get; set; }
            public string SortDirection { get; set; }
            public int SortOrder { get; set; }
            public int GroupOrder { get; set; }
            public string KendoOptionsJson { get; set; }
        }

        public static List<GridColumnMetadata> GetColumns<T>() => GetColumns(typeof(T));

        public static List<GridColumnMetadata> GetColumns(Type modelType)
        {
            if (modelType == null) throw new ArgumentNullException(nameof(modelType));

            var columns = modelType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.GetIndexParameters().Length == 0)
                .Select((property, index) => new
                {
                    Metadata = CreateColumn(property),
                    OriginalIndex = index
                })
                .ToList();

            return columns
                .OrderBy(x => x.Metadata.Order < 0 ? int.MaxValue : x.Metadata.Order)
                .ThenBy(x => x.OriginalIndex)
                .Select(x => x.Metadata)
                .ToList();
        }

        private static GridColumnMetadata CreateColumn(PropertyInfo property)
        {
            var attribute = property.GetCustomAttributes(typeof(GridColumnAttribute), true)
                .Cast<GridColumnAttribute>().FirstOrDefault();
            var display = property.GetCustomAttributes(typeof(DisplayNameAttribute), true)
                .Cast<DisplayNameAttribute>().FirstOrDefault();

            return new GridColumnMetadata
            {
                Field = property.Name,
                SourceName = Humanize(property.Name),
                Title = display != null && !string.IsNullOrWhiteSpace(display.DisplayName)
                    ? display.DisplayName : Humanize(property.Name),
                Visible = attribute == null || attribute.Visible,
                Width = attribute != null && attribute.Width > 0 ? attribute.Width : 150,
                Alignment = attribute != null && !string.IsNullOrWhiteSpace(attribute.Alignment)
                    ? attribute.Alignment : "left",
                Format = attribute == null ? null : attribute.Format,
                DataType = GetDataType(property.PropertyType),
                Order = attribute == null ? -1 : attribute.Order,
                Sortable = attribute == null || attribute.Sortable,
                Filterable = attribute == null || attribute.Filterable,
                Groupable = attribute != null && attribute.Groupable,
                Resizable = attribute == null || attribute.Resizable,
                Reorderable = attribute == null || attribute.Reorderable,
                Locked = attribute != null && attribute.Locked,
                Lockable = attribute == null || attribute.Lockable,
                Exportable = attribute == null || attribute.Exportable,
                SortDirection = attribute == null ? null : NormalizeSortDirection(attribute.SortDirection),
                SortOrder = attribute == null ? -1 : attribute.SortOrder,
                GroupOrder = attribute == null ? -1 : attribute.GroupOrder,
                KendoOptionsJson = attribute == null ? null : attribute.KendoOptionsJson
            };
        }

        private static string NormalizeSortDirection(string value)
        {
            if (string.Equals(value, "asc", StringComparison.OrdinalIgnoreCase)) return "asc";
            if (string.Equals(value, "desc", StringComparison.OrdinalIgnoreCase)) return "desc";
            return null;
        }

        private static string GetDataType(Type propertyType)
        {
            var type = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
            if (type == typeof(bool)) return "boolean";
            if (type == typeof(DateTime)) return "date";
            if (type == typeof(byte) || type == typeof(short) || type == typeof(int) ||
                type == typeof(long) || type == typeof(float) || type == typeof(double) ||
                type == typeof(decimal)) return "number";
            return "string";
        }

        private static string Humanize(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;
            var chars = new List<char>(value.Length + 8);
            for (int i = 0; i < value.Length; i++)
            {
                char current = value[i];
                if (i > 0 && char.IsUpper(current) &&
                    (char.IsLower(value[i - 1]) || char.IsDigit(value[i - 1]))) chars.Add(' ');
                chars.Add(current);
            }
            return new string(chars.ToArray());
        }
    }
}
