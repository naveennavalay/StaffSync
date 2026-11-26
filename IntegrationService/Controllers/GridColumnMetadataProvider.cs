using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using ModelStaffSync;

namespace IntegrationService.Infrastructure
{
    /// <summary>
    /// Converts ModelStaffSync GridColumn attributes into metadata consumed by WebStaffSync.
    /// SourceName matches the humanized property key produced by WebStaffSync's Rows() mapper.
    /// </summary>
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
            public int Order { get; set; } = -1;
        }

        public static List<GridColumnMetadata> GetColumns(Type modelType)
        {
            if (modelType == null)
                throw new ArgumentNullException(nameof(modelType));

            var columns = modelType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.GetIndexParameters().Length == 0)
                .Select((property, index) => new
                {
                    Metadata = CreateColumn(property),
                    OriginalIndex = index
                })
                .ToList();

            // Explicitly ordered columns appear first in configured order.
            // Unconfigured columns retain their original reflection order.
            return columns
                .OrderBy(x => x.Metadata.Order < 0 ? int.MaxValue : x.Metadata.Order)
                .ThenBy(x => x.OriginalIndex)
                .Select(x => x.Metadata)
                .ToList();
        }

        private static GridColumnMetadata CreateColumn(PropertyInfo property)
        {
            var attribute = property.GetCustomAttributes(typeof(GridColumnAttribute), true)
                .Cast<GridColumnAttribute>()
                .FirstOrDefault();

            var displayName = property.GetCustomAttributes(typeof(DisplayNameAttribute), true)
                .Cast<DisplayNameAttribute>()
                .FirstOrDefault();

            return new GridColumnMetadata
            {
                Field = property.Name,
                SourceName = Humanize(property.Name),
                Title = displayName != null && !string.IsNullOrWhiteSpace(displayName.DisplayName)
                    ? displayName.DisplayName
                    : Humanize(property.Name),
                // No GridColumn attribute means visible by default.
                Visible = attribute == null || attribute.Visible,
                Width = attribute != null && attribute.Width > 0 ? attribute.Width : 150,
                Alignment = attribute != null && !string.IsNullOrWhiteSpace(attribute.Alignment)
                    ? attribute.Alignment
                    : "left",
                Format = attribute != null ? attribute.Format : null,
                Order = attribute != null ? attribute.Order : -1,
                DataType = GetDataType(property.PropertyType)
            };
        }

        private static string GetDataType(Type propertyType)
        {
            var type = Nullable.GetUnderlyingType(propertyType) ?? propertyType;

            if (type == typeof(bool))
                return "boolean";

            if (type == typeof(DateTime))
                return "date";

            if (type == typeof(byte) || type == typeof(short) || type == typeof(int) ||
                type == typeof(long) || type == typeof(float) || type == typeof(double) ||
                type == typeof(decimal))
                return "number";

            return "string";
        }

        private static string Humanize(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            var chars = new List<char>(value.Length + 8);
            for (int i = 0; i < value.Length; i++)
            {
                char current = value[i];

                if (i > 0 && char.IsUpper(current) &&
                    (char.IsLower(value[i - 1]) || char.IsDigit(value[i - 1])))
                    chars.Add(' ');

                chars.Add(current);
            }

            return new string(chars.ToArray());
        }
    }
}
