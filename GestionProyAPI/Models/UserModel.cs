using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionProyAPI.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PassHash { get; set; }
        public int ID_Role { get; set; }
        public bool State { get; set; }
    }
}