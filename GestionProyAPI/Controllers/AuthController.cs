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
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private AuthServices authService = new AuthServices();

        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(AuthModel auth)
        {
            if (auth == null || string.IsNullOrWhiteSpace(auth.Email) || string.IsNullOrWhiteSpace(auth.Password))
            {
                return BadRequest("Correo y contraseña son requeridos.");
            }

            var user = authService.Login(auth.Email, auth.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(user);
        }
    }
}
