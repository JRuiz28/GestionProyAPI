
using GestionProyAPI.Models;
using GestionProyAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Configuration;

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
            try
            {
                if (auth == null || string.IsNullOrWhiteSpace(auth.Email) || string.IsNullOrWhiteSpace(auth.Password))
                {
                    return BadRequest("Correo y contraseña son requeridos.");
                }

                var user = authService.Login(auth.Email, auth.Password);

                if (user == null || user.ID_Role == null)
                {
                    return Unauthorized();
                }

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.ID_Role.ToString())
                };

                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["JwtSecret"])
                );

                var credentials = new SigningCredentials(
                    key, SecurityAlgorithms.HmacSha256
                );

                var tokens = new JwtSecurityToken(
                    issuer: ConfigurationManager.AppSettings["JwtIssuer"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(60),
                    signingCredentials: credentials
                );

                var response = new JwtSecurityTokenHandler().WriteToken(tokens);

                return Ok(new
                {
                    Token = response,
                    User = new
                    {
                        user.ID,
                        user.Name,
                        user.Email,
                        user.ID_Role
                    }
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
