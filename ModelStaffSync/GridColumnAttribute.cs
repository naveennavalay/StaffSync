using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelStaffSync
{
    [AttributeUsage(AttributeTargets.Property)]
    public class GridColumnAttribute : Attribute
    {
        public bool Visible { get; set; } = true;

        public int Width { get; set; } = 150;

        public string Alignment { get; set; } = "left";

        public string Format { get; set; }
    }
}
