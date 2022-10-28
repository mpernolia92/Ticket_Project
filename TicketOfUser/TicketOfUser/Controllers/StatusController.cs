using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketOfUser.Models;

namespace TicketOfUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly db_TicktingProgContext _context;
        public StatusController(db_TicktingProgContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetStatus()
        {
            return Ok(_context.Statuses.ToList());
        }
    }
}
