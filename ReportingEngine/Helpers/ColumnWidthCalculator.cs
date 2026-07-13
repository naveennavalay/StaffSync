using ModelStaffSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Helpers
{
    internal static class ColumnWidthCalculator
    {
        public static void Calculate(IList<ReportColumn> columns, IEnumerable<object> data, double printableWidth)
        {
            foreach (ReportColumn column in columns.Where(c => c.Visible))
            {
                if (!column.AutoFit)
                    continue;

                int longest = column.Header.Length;

                foreach (object row in data)
                {
                    PropertyInfo property =
                        row.GetType().GetProperty(column.PropertyName);

                    if (property == null)
                        continue;

                    object value = property.GetValue(row);

                    if (value == null)
                        continue;

                    int length = value.ToString().Length;

                    if (length > longest)
                        longest = length;
                }

                double width = EstimateWidth(longest);

                width = Math.Max(column.MinimumWidth, width);

                width = Math.Min(column.MaximumWidth, width);

                //column.Width = width;
            }

            Normalize(columns, printableWidth);
        }

        private static double EstimateWidth(int characters)
        {
            return (characters * 0.22) + 0.6;
        }

        private static void Normalize(IList<ReportColumn> columns, double printableWidth)
        {
            double totalWidth = columns
                .Where(c => c.Visible)
                .Sum(c => c.Width);

            if (totalWidth <= printableWidth)
                return;

            double ratio = printableWidth / totalWidth;

            if(totalWidth > printableWidth)
            {
                foreach (ReportColumn column in columns.Where(c => c.Visible))
                {
                    if (column.AutoFit)
                    {
                        column.Width *= ratio;
                    }
                }
            }
        }
    }
}
