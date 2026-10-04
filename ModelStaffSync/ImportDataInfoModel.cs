using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class ImportDataInfoModel
    {
        public int ImpDataInfoID { get; set; }

        [DisplayName("Import Data Code")]
        public string ImpDataInfoCode { get; set; }

        [DisplayName("Import Data Title")]
        public string ImpDataInfoTitle { get; set; }

        [DisplayName("Description")]
        public string ImpDataInfoDescription { get; set; }
        public string ImpDataInfoTemplateName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int OrderID { get; set; }
    }
}
