using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using System.Security.Claims;
using System.Text;

[assembly: OwinStartup(typeof(GestionProyAPI.Startup))]
// Doc: https://learn.microsoft.com/en-us/aspnet/aspnet/overview/owin-and-katana/getting-started-with-owin-and-katana 
namespace GestionProyAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var secret = ConfigurationManager.AppSettings["JwtSecret"];

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secret)
            );

            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,

                    TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = ConfigurationManager.AppSettings["JwtIssuer"],

                        ValidateAudience = false,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,

                        ValidateLifetime = true,

                        RoleClaimType = ClaimTypes.Role
                    }
                }
            );
        }
    }
}
