using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketOfUser.Models;

namespace TicketOfUser.Data
{
    public class ApplicationRoleStore:RoleStore<ApplicationRole,db_TicktingProgContext>
    {
        public ApplicationRoleStore(db_TicktingProgContext context, IdentityErrorDescriber errorDescriber) : base(context, errorDescriber)
        {

        }
    }
}
