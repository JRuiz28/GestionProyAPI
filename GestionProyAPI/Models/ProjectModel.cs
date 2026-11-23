using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionProyAPI.Models
{
    public class ProjectModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int? ID_State { get; set; }
        public int? ID_Assigned { get; set; }
        public DateTime CreatedIn { get; set; }
    }
}