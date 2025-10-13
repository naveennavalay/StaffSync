using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class SkillsInfo
    {
        public int EmpSkillID { get; set; }
        public int EmpID { get; set; }
        public int SkillID { get; set; }
        public int SkillExpertiseLevel { get; set; }
    }

    public class SkillModel
    {
        public int SkillID { get; set; }
        public string SkillCode { get; set; }

        public string SkillTitle { get; set; }
        public string SkillInitial { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
