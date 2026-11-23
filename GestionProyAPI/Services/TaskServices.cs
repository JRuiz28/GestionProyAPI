using GestionProyAPI.DataSets.DataSetGestionProyTableAdapters;
using GestionProyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionProyAPI.Services
{
    public class TaskServices
    {
        // Declare Adapter.
        private TaskTableAdapter taskTableAdapter;

        public TaskServices()
        {
            taskTableAdapter = new TaskTableAdapter();
        }

        public List<TaskModel> getAllTasks()
        {
            var list = taskTableAdapter.GetData();

            if (list.Count == 0)
            {
                return null;
            }

            List<TaskModel> tasks = new List<TaskModel>();

            foreach (var task in list)
            {
                tasks.Add(new TaskModel
                {
                    ID = task.pk_id,
                    Title = task.title,
                    Description = task.description,
                    ID_Project = task.fk_id_project,
                    ID_Priority = task.fk_id_priority,
                    ID_State = task.fk_id_state,
                    ID_Assigned = task.Isfk_id_assignedNull() ? (int?)null : task.fk_id_assigned,
                    ExpiredIn = task.IsexpiredInNull() ? (DateTime?)null : task.expiredIn
                });
            }

            return tasks;
        }

        public List<TaskModel> getTasksByUser(int userID)
        {
            var list = taskTableAdapter.GetTasksByUserID(userID);

            if (list.Count == 0)
            {
                return null;
            }

            List<TaskModel> tasks = new List<TaskModel>();

            foreach (var task in list)
            {
                tasks.Add(new TaskModel
                {
                    ID = task.pk_id,
                    Title = task.title,
                    Description = task.description,
                    ID_Project = task.fk_id_project,
                    ID_Priority = task.fk_id_priority,
                    ID_State = task.fk_id_state,
                    ID_Assigned = task.Isfk_id_assignedNull() ? (int?)null : task.fk_id_assigned,
                    ExpiredIn = task.IsexpiredInNull() ? (DateTime?)null : task.expiredIn
                });
            }

            return tasks;
        }

        public TaskModel GetTaskByID(int id)
        {
            var tasks = taskTableAdapter.GetDataByID(id);

            if (tasks.Count == 0)
            {
                return null;
            }

            var task = tasks[0];

            return new TaskModel
            {
                ID = task.pk_id,
                Title = task.title,
                Description = task.description,
                ID_Project = task.fk_id_project,
                ID_Priority = task.fk_id_priority,
                ID_State = task.fk_id_state,
                ID_Assigned = task.Isfk_id_assignedNull() ? (int?)null : task.fk_id_assigned,
                ExpiredIn = task.IsexpiredInNull() ? (DateTime?)null : task.expiredIn
            };
        }

        public bool InsertTask(TaskModel task)
        {
            if (task.Title == null)
            {
                return false;
            }

            task.ID_State = 1;
            task.CreatedIn = DateTime.Now;
            task.ExpiredIn = null;

            taskTableAdapter.InsertTask(
                task.ID_Project,
                task.Title,
                task.Description,
                task.ID_Assigned,
                task.ID_State,
                task.ID_Priority,
                task.CreatedIn,
                task.ExpiredIn
            );

            return true;
        }

        public bool UpdateTask(TaskModel task)
        {
            if (task.Title == null)
            {
                return false;
            }

            taskTableAdapter.UpdateTask(
                task.Title,
                task.Description,
                task.ID_Project,
                task.ID_Assigned,
                task.ID_State,
                task.ID_Priority,
                task.ExpiredIn,
                task.ID
            );

            return true;
        }

        public bool DeleteTask(int id)
        {
            TaskModel taskConsult = GetTaskByID(id);

            if (taskConsult == null)
            {
                return false;
            }

            taskTableAdapter.DeleteTask(id);

            return true;
        }
    }
}