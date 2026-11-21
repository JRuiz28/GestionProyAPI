using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(GestionProyAPI.Startup))]

// Doc: https://learn.microsoft.com/en-us/aspnet/aspnet/overview/owin-and-katana/getting-started-with-owin-and-katana 
namespace GestionProyAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
