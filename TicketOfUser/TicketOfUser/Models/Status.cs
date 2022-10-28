using System;
using System.Collections.Generic;

#nullable disable

namespace TicketOfUser.Models
{
    public partial class Status
    {
        public Status()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string Status1 { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
