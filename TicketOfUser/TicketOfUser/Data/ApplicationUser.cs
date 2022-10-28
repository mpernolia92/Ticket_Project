using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TicketOfUser.Models;

namespace TicketOfUser.Data
{
    public class ApplicationUser:IdentityUser
    {
        [NotMapped]
        public string Token { get; set; }
        [NotMapped]
        public string Role { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<TicketToDeveloper> TicketToDevelopers { get; set; }
    }
}
