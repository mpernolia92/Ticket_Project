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
    public class PriorityController : ControllerBase
    {
        private readonly db_TicktingProgContext _context;
        public PriorityController(db_TicktingProgContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetStatus()
        {
            return Ok(_context.Priorities.ToList());
        }
    }
}
