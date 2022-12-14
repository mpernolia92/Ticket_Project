using System;
using System.Collections.Generic;

#nullable disable

namespace TicketOfUser.Models
{
    public partial class Priority
    {
        public Priority()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
