using GestionProyAPI.DataSets.DataSetGestionProyTableAdapters;
using GestionProyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionProyAPI.Services
{
    public class UserServices
    {
        // Declare Adapter.
        private UserTableAdapter userAdapter;

        public UserServices()
        {
            userAdapter = new UserTableAdapter();
        }

        public UserModel GetUserByEmail(string email) {
            var users = userAdapter.GetDataByEmail(email);

            if (users.Count == 0)
            {
                return null;
            }

            var user = users[0];

            return new UserModel
            {
                ID = user.pk_id,
                Name = user.name,
                Email = user.email,
                PassHash = user.passHash,
                ID_Role = user.fk_id_role,
                State = user.state
            };
        }
    }
}