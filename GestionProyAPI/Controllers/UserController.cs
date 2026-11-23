using GestionProyAPI.Models;
using GestionProyAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GestionProyAPI.Controllers
{
    [Authorize(Roles = "1")]
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        private UserServices userService = new UserServices();

        [HttpGet]
        [Route("all")]
        public IHttpActionResult getUsers()
        {
            try
            {
                List<UserModel> users = userService.getAllUsers();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult getUser(int id)
        {
            try
            {
                UserModel user = userService.GetUserByID(id);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult addUser(UserModel user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("Sin usuario válido para registrar.");
                }

                bool result = userService.InsertUser(user);
                if (!result)
                {
                    return BadRequest("No se pudo registrar el usuario");
                }

                return Ok(200);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult updateUser(int id, UserModel user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("Sin usuario válido para actualizar.");
                }

                user.ID = id;
                bool result = userService.UpdateUser(user);
                if (!result)
                {
                    return BadRequest("No se pudo actualizar el usuario");
                }

                return Ok(200);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult deleteUser(int id)
        {
            try
            {
                bool result = userService.DeleteUser(id);
                if (!result)
                {
                    return BadRequest("Usuario para eliminar no existe.");
                }

                return Ok(200);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
