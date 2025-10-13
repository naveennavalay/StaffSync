using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class clsStates
    {
        dbStaffSync.clsStates objStates = new dbStaffSync.clsStates();

        public DataTable GetStateList()
        {
            DataTable dt = new DataTable();

            dt = objStates.GetStateList();

            return dt;
        }

        public DataTable GetStateList(string filterText)
        {
            DataTable dt = new DataTable();

            dt = objStates.GetStateList(filterText);

            return dt;
        }

        public int GetStateByTitle(string StateName)
        {
            int selectedStateID = 0;

            selectedStateID = objStates.GetStateByTitle(StateName);

            return selectedStateID;
        }

        public string GetStateByID(int StateID)
        {
            string selectedState = "";
            
            selectedState = objStates.GetStateByID(StateID);

            return selectedState;
        }

        public int InsertState(string txtStateCode, string txtStateTitle, string txtStateInitial, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            
            affectedRows = objStates.InsertState(txtStateCode, txtStateTitle, txtStateInitial, IsActive, IsDeleted);

            return affectedRows;
        }

        public int UpdateState(int txtStateID, string txtStateCode, string txtStateTitle, string txtStateInitial, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;

            affectedRows = objStates.UpdateState(txtStateID, txtStateCode, txtStateTitle, txtStateInitial, IsActive, IsDeleted);

            return affectedRows;
        }

        public int DeleteState(int txtStateID)
        {
            int affectedRows = 0;

            affectedRows = objStates.DeleteState(txtStateID);

            return affectedRows;
        }
    }
}
