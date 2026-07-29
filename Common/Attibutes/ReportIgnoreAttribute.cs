using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Attibutes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ReportIgnoreAttribute : Attribute
    {

    }
}
