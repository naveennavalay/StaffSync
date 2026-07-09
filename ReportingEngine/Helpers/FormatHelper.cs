using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Helpers
{
    public static class FormatHelper
    {
        public static string Date(DateTime? value) => value?.ToString("dd-MMM-yyyy") ?? "";
        public static string DateTimeFormat(DateTime? value) => value?.ToString("dd-MMM-yyyy hh:mm:ss tt") ?? "";
        public static string Currency(decimal value) => value.ToString("N2");
        public static string Number(double value) => value.ToString("N2");
    }
}
