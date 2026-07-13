using ModelStaffSync;
using ModelStaffSync.Reports.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Helpers
{
    public static class ReportMetadataReader
    {
        public static List<ReportColumn> Create<T>()
        {
            return typeof(T)
                .GetProperties()
                .Select(property =>
                {
                    ReportColumnAttribute attribute = property.GetCustomAttribute<ReportColumnAttribute>();

                    if (attribute == null)
                        return null;

                    return new ReportColumn
                    {
                        Header = attribute.Header,
                        PropertyName = property.Name,
                        Width = attribute.Width,
                        MinimumWidth = attribute.MinimumWidth,
                        MaximumWidth = attribute.MaximumWidth,
                        AutoFit = attribute.AutoFit,
                        Alignment = attribute.Alignment,
                        Format = attribute.Format,
                        Visible = attribute.Visible,
                        ShowTotal = attribute.ShowTotal,
                        TotalBold = attribute.TotalBold,
                        Order = attribute.Order
                    };
                })
                .Where(c => c != null)
                .OrderBy(c => c.Order)
                .ToList();
        }
    }
}
