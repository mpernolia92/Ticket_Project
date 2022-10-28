using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketOfUser.Data;
using TicketOfUser.Models.DTO;
using TicketOfUser.ViewModel;

namespace TicketOfUser.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<Tokens> Authenticate(LoginViewModel loginViewmodel);
    }
}
