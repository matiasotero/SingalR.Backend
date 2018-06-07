using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SignalR_Backend.Controllers
{
    [Route("api/match")]
    [ApiController]
    public class MatchController : Controller
    {
        [HttpGet]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(typeof(IActionResult), 400)]
        public IActionResult GetMatch()
        {
            return Ok("Accion correcta");
        }
    }
}