using ReportingEngine.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Core
{
    public static class ReportFactory
    {
        public static T Create<T>(ReportContext context, ReportSettings settings)
            where T : ReportBase
        {
            return (T)Activator.CreateInstance(typeof(T), context, settings);
        }
    }
}
