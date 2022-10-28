using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketOfUser.Models.DTO
{
    public class TicketDTO
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Complaint { get; set; }
        public string AspNetUsersId { get; set; }
        public string AspNetUsers { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public int PriorityId { get; set; }
        public string Priority { get; set; }
        public string DeveloperId { get; set; }
    }
}
