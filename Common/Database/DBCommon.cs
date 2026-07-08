using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public static class DecimalExtensions
    {
        public static decimal RoundUp(this decimal value)
        {
            return Math.Ceiling(value);
        }
    }
}
