using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class clsSkillsMas
    {
        dbStaffSync.clsSkillsMas objSkillsMas = new dbStaffSync.clsSkillsMas();

        public DataTable GetSkillList()
        {
            DataTable dt = new DataTable();
            
            dt = objSkillsMas.GetSkillList();

            return dt;
        }

        public DataTable GetSkillList(string filterText)
        {
            DataTable dt = new DataTable();

            dt = objSkillsMas.GetSkillList(filterText);

            return dt;
        }

        public int InsertSkill(string txtSkillCode, string txtSkillTitle, string txtSkillInitial, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;

            affectedRows = objSkillsMas.InsertSkill(txtSkillCode, txtSkillTitle, txtSkillInitial, IsActive, IsDeleted);

            return affectedRows;
        }

        public int UpdateSkill(int txtSkillID, string txtSkillCode, string txtSkillTitle, string txtSkillInitial, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;

            affectedRows = objSkillsMas.UpdateSkill(txtSkillID, txtSkillCode, txtSkillTitle, txtSkillInitial, IsActive, IsDeleted);

            return affectedRows;
        }

        public int DeleteSkill(int txtSkillID)
        {
            int affectedRows = 0;

            affectedRows = objSkillsMas.DeleteSkill(txtSkillID);

            return affectedRows;
        }
    }
}
