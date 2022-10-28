using System;
using System.Collections.Generic;
using TicketOfUser.Data;

#nullable disable

namespace TicketOfUser.Models
{
    public partial class Ticket
    {
        public Ticket()
        {
            Comments = new HashSet<Comment>();
            TicketToDevelopers = new HashSet<TicketToDeveloper>();
        }

        public int Id { get; set; }
        public string Subject { get; set; }
        public int? StatusId { get; set; }
        public int? ProrityId { get; set; }
        public string AspNetUsersId { get; set; }
        public string Complaint { get; set; }
        public byte[] AttachFile { get; set; }

        public virtual ApplicationUser AspNetUsers { get; set; }
        public virtual Priority Prority { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<TicketToDeveloper> TicketToDevelopers { get; set; }
    }
}
