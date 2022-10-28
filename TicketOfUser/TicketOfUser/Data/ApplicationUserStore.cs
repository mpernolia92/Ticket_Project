using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketOfUser.Models;

namespace TicketOfUser.Data
{
    public class ApplicationUserStore:UserStore<ApplicationUser>
    {
        public ApplicationUserStore(db_TicktingProgContext context) : base(context)
        {

        }
    }
}
