using GestionProyAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GestionProyAPI.Controllers
{
    public class StatesController : ApiController
    {
        private StateServices stateService = new StateServices();

        [HttpGet]
        public IHttpActionResult GetStates()
        {
            var states = stateService.GetStates();

            return Ok(states);
        }
    }
}
