using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Models
{
    /// <summary>
    /// Represents a report parameter.
    /// </summary>
    public class ReportParameter
    {
        public string Name { get; set; }

        public object Value { get; set; }

        public string Description { get; set; }

        public ReportParameter()
        {
        }

        public ReportParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}
