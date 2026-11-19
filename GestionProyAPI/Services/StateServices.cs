using GestionProyAPI.DataSets.DataSetGestionProyTableAdapters;
using GestionProyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionProyAPI.Services
{
    public class StateServices
    {
        // Declare Adapter.
        private StateTableAdapter stateAdapter;

        public StateServices()
        {
            stateAdapter = new StateTableAdapter();
        }

        public List<StateModel> GetStates()
        {
            var data = stateAdapter.GetData();

            List<StateModel> States = new List<StateModel>();

            foreach (var state in data)
            {
                States.Add(new StateModel
                {
                    ID = state.pk_id,
                    Name = state.name,
                    Entity = state.entity,
                });
            }

            return States;
        }


    }
}