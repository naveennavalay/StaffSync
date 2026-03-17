using ModelStaffSync;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class clsStates
    {
        dbStaffSync.clsStates objStates = new dbStaffSync.clsStates();

        public List<StateModel> GetStateList()
        {
            List<StateModel> objStatesList = new List<StateModel>();

            objStatesList = objStates.GetStateList();

            return objStatesList;
        }

        public List<StateModel> GetStateList(string filterText)
        {
            List<StateModel> objStatesList = new List<StateModel>();

            objStatesList = objStates.GetStateList(filterText);

            return objStatesList;
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
