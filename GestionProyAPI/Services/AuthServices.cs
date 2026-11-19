using GestionProyAPI.DataSets.DataSetGestionProyTableAdapters;
using GestionProyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace GestionProyAPI.Services
{
    public class AuthServices
    {
        private UserServices userService = new UserServices();

        public UserModel Login(string email, string password)
        {
            var user = userService.GetUserByEmail(email);

            if (user == null)
            {
                return null;
            }

            var hasher = new PasswordHasher();
            var verification = hasher.VerifyHashedPassword(user.PassHash, password);

            if (verification == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return user;
        }

    }
}