using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionProyAPI.Models
{
    public class TaskModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? ID_Assigned { get; set; }
        public int ID_State { get; set; }
        public int ID_Project { get; set; }
        public int ID_Priority { get; set; }
        public DateTime CreatedIn { get; set; }
        public DateTime? ExpiredIn { get; set; }
    }
}