using System;
using System.Collections.Generic;
using TicketOfUser.Data;

#nullable disable

namespace TicketOfUser.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public string Comment1 { get; set; }
        public string AspNetUsersId { get; set; }
        public int? TicketId { get; set; }

        public virtual ApplicationUser AspNetUsers { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
