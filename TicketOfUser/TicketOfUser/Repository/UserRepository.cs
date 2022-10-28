using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketOfUser.Data;
using TicketOfUser.Models;
using TicketOfUser.Models.DTO;
using TicketOfUser.Repository.IRepository;
using TicketOfUser.ViewModel;

namespace TicketOfUser.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly db_TicktingProgContext _context;
        private readonly AppSetting _appSetting;
        public UserRepository(IOptions<AppSetting> appSetting, db_TicktingProgContext context)
        {
            _appSetting = appSetting.Value;
            _context = context;
        }
        public async Task<Tokens> Authenticate(LoginViewModel loginViewmodel)
        {
            // var userInfo = _context.Users.Where(x=>x.UserName == loginViewmodel.UserName && x.PasswordHash == loginViewmodel.Password);

            var userInfo = (from e in _context.Users
                            join ed in _context.UserRoles on e.Id equals ed.UserId
                            join r in _context.Roles on ed.RoleId equals r.Id
                            where e.UserName == loginViewmodel.UserName
                            select new
                            {
                                Id = e.Id,
                                UserName = e.UserName,
                                Role = r.Name

                            }).FirstOrDefault();
            if (userInfo == null)
                   return null;


                var tokenHandler = new JwtSecurityTokenHandler();
                var key = System.Text.Encoding.ASCII.GetBytes(_appSetting.Secret);

                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                  {
                        new Claim(ClaimTypes.Name,userInfo.Id),
                        new Claim(ClaimTypes.Role,userInfo.Role)
                  }),
                    Expires = DateTime.UtcNow.AddHours(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                  SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                Tokens user = new Tokens()
                {
                    Token = tokenHandler.WriteToken(token)
                };

                return user;
            
        }

    }
}
