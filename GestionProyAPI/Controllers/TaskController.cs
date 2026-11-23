using GestionProyAPI.Models;
using GestionProyAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace GestionProyAPI.Controllers
{
    [Authorize(Roles = "1,2")]
    [RoutePrefix("api/tasks")]
    public class TaskController : ApiController
    {
        private TaskServices taskServices = new TaskServices();

        [Authorize(Roles = "1,2")]
        [HttpGet]
        [Route("all")]
        public IHttpActionResult getTasks()
        {
            try
            {
                if (User.IsInRole("1"))
                {
                    List<TaskModel> tasks = taskServices.getAllTasks();

                    if (tasks == null)
                    {
                        return NotFound();
                    }

                    return Ok(tasks);
                }
                else
                {
                    int userID = GetUserID();
                    List<TaskModel> tasks = taskServices.getTasksByUser(userID);

                    if (tasks == null)
                    {
                        return NotFound();
                    }

                    return Ok(tasks);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Authorize(Roles = "1,2")]
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult getTask(int id)
        {
            try
            {
                TaskModel task = taskServices.GetTaskByID(id);

                if (task == null)
                {
                    return NotFound();
                }

                return Ok(task);
            }
            catch
            {
                return InternalServerError();
            }
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        [Route("")]
        public IHttpActionResult addTask(TaskModel task)
        {
            try
            {
                if (task == null)
                {
                    return BadRequest("Sin tarea válido para registrar.");
                }

                bool result = taskServices.InsertTask(task);
                if (!result)
                {
                    return BadRequest("No se pudo registrar la tarea");
                }

                return Ok(200);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Authorize(Roles = "1")]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult updateTask(int id, TaskModel task)
        {
            try
            {
                if (task == null)
                {
                    return BadRequest("Sin tarea válido para actualizar.");
                }

                task.ID = id;
                bool result = taskServices.UpdateTask(task);
                if (!result)
                {
                    return BadRequest("No se pudo actualizar la tarea");
                }

                return Ok(200);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Authorize(Roles = "1")]
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult deleteTask(int id)
        {
            try
            {
                bool result = taskServices.DeleteTask(id);
                if (!result)
                {
                    return BadRequest("Tarea para eliminar no existe.");
                }

                return Ok(200);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        private int GetUserID()
        {
            var identity = User.Identity as ClaimsIdentity;

            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);

            return int.Parse(claim.Value);
        }
    }
}
