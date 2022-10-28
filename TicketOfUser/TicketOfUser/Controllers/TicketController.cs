using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TicketOfUser.Models;
using TicketOfUser.Models.DTO;

namespace TicketOfUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly db_TicktingProgContext _context;
        public TicketController(db_TicktingProgContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetTicket()
        {
            var ticketInfo = _context.Tickets.ToList();
            return Ok(ticketInfo);
        }
        [HttpPost]
        public IActionResult PostTicket([FromBody] TicketDTO ticket)
        {
            if (ticket != null && ModelState.IsValid)
            {
                Ticket userTicket = new Ticket()
                {
                    Id = ticket.Id,
                    Subject = ticket.Subject,
                    Complaint = ticket.Complaint,
                    AspNetUsersId = ticket.AspNetUsersId
                };
                _context.Tickets.Add(userTicket);
                _context.SaveChanges();
            }
            //var file = Request.Form.Files[0];
            //var folderName = Path.Combine("resources", "images");
            //var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            //if (file.Length > 0)
            //{
            //    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            //    var fullPath = Path.Combine(pathToSave, fileName);
            //    var dbPath = Path.Combine(folderName, fileName);
            //    using (var stream = new FileStream(fullPath, FileMode.Create))
            //    {
            //        file.CopyTo(stream);
            //    }
            //    return Ok(new { dbPath });
            //}
            return Ok();

        }

        [HttpGet("alluser")]
        public IActionResult GetAllInfo()
        {
            var ticketDetail = (from e in _context.Tickets
                                join ed in _context.TicketToDevelopers on e.Id equals ed.TicketId
                                join u in _context.Users on e.AspNetUsersId equals u.Id 
                                join ud in _context.Users on ed.DeveloperId  equals ud.Id
                             
                                select new
                                {
                                    Id = e.Id,
                                    UserName = u.UserName,
                                    Complaint = e.Complaint,
                                    StatusId = e.StatusId,
                                    Status= e.Status,
                                    PriorityId = e.ProrityId,
                                    Priority=e.Prority,

                                   
                                    DeveloperId = ud.UserName
                                }).ToList();
            return Ok(ticketDetail);
        }

        [HttpGet("{Id}")]
        public IActionResult getUserById(string Id)
        {
            var userById = _context.Users.FirstOrDefault(x => x.Id == Id);
            return Ok(userById);
        }

        [HttpPut]
        public IActionResult ManagRequest([FromBody] TicketDTO adminDto)
        {
            var userDetails = _context.Tickets.Find(adminDto.Id);
            if (userDetails == null) return BadRequest(error: "Data not Found");

            if(adminDto!=null && ModelState.IsValid)
            {
                userDetails.Id = adminDto.Id; 
                userDetails.ProrityId = adminDto.PriorityId;
                userDetails.StatusId = adminDto.StatusId;
                 
                _context.Tickets.Update(userDetails);   
                _context.SaveChanges();

                TicketToDeveloper ticketDeveloper = new TicketToDeveloper()
                {
                    TicketId = adminDto.Id,
                    DeveloperId =adminDto.DeveloperId
                };
                _context.TicketToDevelopers.Add(ticketDeveloper);
                _context.SaveChanges();
            }
            return Ok();
        }
        
    }
}
