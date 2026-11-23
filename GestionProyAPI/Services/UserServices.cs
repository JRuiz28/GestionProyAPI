using GestionProyAPI.DataSets.DataSetGestionProyTableAdapters;
using GestionProyAPI.Models;
using Microsoft.AspNet.Identity;
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

        public List<UserModel> getAllUsers()
        {
            var list = userAdapter.GetData();

            if(list.Count == 0)
            {
                return null;
            }
            
            List<UserModel> users = new List<UserModel>();

            foreach (var user in list)
            {
                users.Add(new UserModel
                {
                    ID = user.pk_id,
                    Name = user.name,
                    Email = user.email,
                    PassHash = user.passHash,
                    ID_Role = user.Isfk_id_roleNull() ? (int?)null : user.fk_id_role,
                    State = user.state
                });
            }

            return users;
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
                ID_Role = user.Isfk_id_roleNull() ? (int?)null : user.fk_id_role,
                State = user.state
            };
        }

        public UserModel GetUserByID (int id)
        {
            var users = userAdapter.GetDataByID(id);

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
                ID_Role = user.Isfk_id_roleNull() ? (int?)null : user.fk_id_role,
                State = user.state
            };
        }

        public bool InsertUser(UserModel user)
        {
            if (user.Name == null || user.Email == null || user.PassHash == null)
            {
                return false;
            }
            
            var hasher = new PasswordHasher();
            user.PassHash = hasher.HashPassword(user.PassHash);
            user.CreatedIn = DateTime.UtcNow;
            user.State = true;

            userAdapter.InsertUser(user.Name, user.Email, user.PassHash, user.ID_Role, user.CreatedIn, user.State);

            return true;
        }

        public bool UpdateUser(UserModel user)
        {
            if (user.Name == null || user.Email == null)
            {
                return false;
            }

            UserModel UserConsult = GetUserByID(user.ID);

            if (string.IsNullOrEmpty(user.PassHash))
            {
                user.PassHash = UserConsult.PassHash;
            }
            else
            {
                var hasher = new PasswordHasher();
                user.PassHash = hasher.HashPassword(user.PassHash);
            }

            userAdapter.UpdateUser(user.Name, user.Email, user.PassHash, user.ID_Role, user.State, user.ID);

            return true;
        }

        public bool DeleteUser(int id)
        {
            UserModel UserConsult = GetUserByID(id);

            if(UserConsult == null)
            {
                return false;
            }

            userAdapter.DeleteUser(id);

            return true;
        }
    }
}