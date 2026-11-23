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
    [RoutePrefix("api/projects")]
    public class ProjectController : ApiController
    {
        private ProjectServices projectServices = new ProjectServices();

        [Authorize(Roles = "1,2")]
        [HttpGet]
        [Route("all")]
        public IHttpActionResult getProjects()
        {
            try
            {
                if (User.IsInRole("1"))
                {
                    List<ProjectModel> projects = projectServices.getAllProjects();

                    if (projects == null)
                    {
                        return NotFound();
                    }

                    return Ok(projects);
                }
                else
                {
                    int userID = GetUserID();
                    List<ProjectModel> projects = projectServices.getProjectsByUser(userID);

                    if (projects == null)
                    {
                        return NotFound();
                    }

                    return Ok(projects);
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
        public IHttpActionResult getProject(int id)
        {
            try
            {
                ProjectModel project = projectServices.GetProjectByID(id);

                if (project == null)
                {
                    return NotFound();
                }

                return Ok(project);
            }
            catch
            {
                return InternalServerError();
            }
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        [Route("")]
        public IHttpActionResult addProject(ProjectModel project)
        {
            try
            {
                if (project == null)
                {
                    return BadRequest("Sin proyecto válido para registrar.");
                }

                bool result = projectServices.InsertProject(project);
                if (!result)
                {
                    return BadRequest("No se pudo registrar el proyecto");
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
        public IHttpActionResult updateProject(int id, ProjectModel project)
        {
            try
            {
                if (project == null)
                {
                    return BadRequest("Sin projecto válido para actualizar.");
                }
                 
                project.ID = id;
                bool result = projectServices.UpdateProject(project);
                if (!result)
                {
                    return BadRequest("No se pudo actualizar el proyecto");
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
        [Route("{id:int}/close")]
        public IHttpActionResult updateCloseProject(int id)
        {
            try
            {
                ProjectModel project = projectServices.GetProjectByID(id);
                if (project == null)
                {
                    return BadRequest("Sin projecto válido para actualizar.");
                }

                bool result = projectServices.CloseProject(id);
                if (!result)
                {
                    return BadRequest("No se pudo actualizar el proyecto");
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
        public IHttpActionResult deleteProject(int id)
        {
            try
            {
                bool result = projectServices.DeleteProject(id);
                if (!result)
                {
                    return BadRequest("Projecto para eliminar no existe.");
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
