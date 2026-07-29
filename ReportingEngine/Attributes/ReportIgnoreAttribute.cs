using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ReportIgnoreAttribute : Attribute
    {

    }
}
