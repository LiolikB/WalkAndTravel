﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkAndTravel.ClassLibrary;
using WalkAndTravel.ClassLibrary.Repositories;
using WalkAndTravel.ClassLibrary.Services;
using WalkAndTravel.DataAccess;

namespace WalkAndTravel.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RouteController : ControllerBase
    {

        public event EventHandler<ClassLibrary.Logging.LogEventArgs> Log;

        private IRouteServices _routeServices;

        public RouteController()
        {
            Log += ClassLibrary.Logging.Logger.Log;
            _routeServices = new RouteServices(new RouteRepository());
        }

        [HttpPost("SaveNewRoute")]
        public IActionResult SaveNewRoute([FromBody] RouteMinimal routes)
        {
            var route = _routeServices.SaveNewRoute(routes);
            if(route == -1)
            {
                return BadRequest();
            }
            Log(this, new ClassLibrary.Logging.LogEventArgs("Save route", "Custom", routes.Name));
            return Ok(route);
        }

        [HttpGet("RandomPOIRoute")]
        public Route GetRandomPOIRoute()
        {
            Log(this, new ClassLibrary.Logging.LogEventArgs("Generate route", "Sightseeing", "NoName"));
            return _routeServices.GetRandomPOIRoute();
        }

        [HttpGet("RandomRoute")]
        public Route GetRandomRoute()
        {
            Log(this, new ClassLibrary.Logging.LogEventArgs("Generate route", "City", "NoName"));
            return _routeServices.GetRandomRoute();
        }


        [HttpGet("Route")]
        public async Task<IEnumerable<Route>> Get()
        {
            return await _routeServices.GetRoutes();
        }
    }
}
