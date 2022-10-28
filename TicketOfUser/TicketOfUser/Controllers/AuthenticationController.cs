using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketOfUser.Data;
using TicketOfUser.Repository.IRepository;
using TicketOfUser.ViewModel;

namespace TicketOfUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public AuthenticationController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost]
        public IActionResult Authenticates([FromBody] LoginViewModel loginviewModel)
        {
            var user =  _userRepository.Authenticate(loginviewModel);
            if (user == null)
                return BadRequest(new { message = "Wrong UserName or Password" });
            return Ok(user);
        }
    }
}
