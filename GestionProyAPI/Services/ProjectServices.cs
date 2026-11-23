using GestionProyAPI.DataSets.DataSetGestionProyTableAdapters;
using GestionProyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionProyAPI.Services
{
    public class ProjectServices
    {
        // Declare Adapter.
        private ProjectTableAdapter projectAdapter;
        private StateServices stateServices;

        public ProjectServices()
        {
            projectAdapter = new ProjectTableAdapter();
            stateServices = new StateServices();
        }

        public List<ProjectModel> getAllProjects()
        {
            var list = projectAdapter.GetData();

            if (list.Count == 0)
            {
                return null;
            }

            List<ProjectModel> projects = new List<ProjectModel>();

            foreach (var project in list)
            {
                projects.Add(new ProjectModel
                {
                    ID = project.pk_id,
                    Name = project.name,
                    Description = project.description,
                    DateStart = project.dateStart,
                    DateEnd = project.dateEnd,
                    ID_State = project.Isfk_id_stateNull() ? (int?)null : project.fk_id_state,
                    ID_Assigned = project.Isfk_id_assignedNull() ? (int?)null : project.fk_id_assigned
                });
            }

            return projects;
        }

        public List<ProjectModel> getProjectsByUser(int userID)
        {
            var list = projectAdapter.GetProjectsByUserID(userID);

            if (list.Count == 0)
            {
                return null;
            }

            List<ProjectModel> projects = new List<ProjectModel>();

            foreach (var project in list)
            {
                projects.Add(new ProjectModel
                {
                    ID = project.pk_id,
                    Name = project.name,
                    Description = project.description,
                    DateStart = project.dateStart,
                    DateEnd = project.dateEnd,
                    ID_State = project.Isfk_id_stateNull() ? (int?)null : project.fk_id_state,
                    ID_Assigned = project.Isfk_id_assignedNull() ? (int?)null : project.fk_id_assigned
                });
            }

            return projects;
        }

        public ProjectModel GetProjectByID(int id)
        {
            var projects = projectAdapter.GetDataByID(id);

            if (projects.Count == 0)
            {
                return null;
            }

            var project = projects[0];

            return new ProjectModel
            {
                ID = project.pk_id,
                Name = project.name,
                Description = project.description,
                DateStart = project.dateStart,
                DateEnd = project.dateEnd,
                ID_State = project.Isfk_id_stateNull() ? (int?)null : project.fk_id_state,
                ID_Assigned = project.Isfk_id_assignedNull() ? (int?)null : project.fk_id_assigned
            };
        }

        public bool InsertProject(ProjectModel project)
        {
            if (project.Name == null || project.DateStart == null || project.DateEnd == null)
            {
                return false;
            }

            project.CreatedIn = DateTime.UtcNow;

            projectAdapter.InsertProject(
                project.Name, 
                project.Description, 
                project.DateStart, 
                project.DateEnd, 
                project.ID_State, 
                project.ID_Assigned, 
                project.CreatedIn
            );

            return true;
        }

        public bool UpdateProject(ProjectModel project)
        {
            if (project.Name == null || project.DateStart == null || project.DateEnd == null)
            {
                return false;
            }

            projectAdapter.UpdateProject(
                project.Name,
                project.Description,
                project.DateStart,
                project.DateEnd,
                project.ID_State,
                project.ID_Assigned,
                project.ID
            );

            return true;
        }

        public bool CloseProject(int id)
        {
            var id_states = stateServices.GetIdByNameAndEntity("Cerrado", "Proyecto");

            if (id_states == null)
            {
                return false;
            }

            projectAdapter.UpdateClose(
                id_states,
                id
            );

            return true;
        }

        public bool DeleteProject(int id)
        {
            ProjectModel projectConsult = GetProjectByID(id);

            if (projectConsult == null)
            {
                return false;
            }

            projectAdapter.DeleteProject(id);

            return true;
        }
    }
}