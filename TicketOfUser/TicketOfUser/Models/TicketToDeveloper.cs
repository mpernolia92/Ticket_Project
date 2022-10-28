using System;
using System.Collections.Generic;
using TicketOfUser.Data;

#nullable disable

namespace TicketOfUser.Models
{
    public partial class TicketToDeveloper
    {
        public string DeveloperId { get; set; }
        public int TicketId { get; set; }

        public virtual ApplicationUser Developer { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
