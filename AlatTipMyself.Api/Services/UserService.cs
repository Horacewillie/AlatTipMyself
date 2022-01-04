using AlatTipMyself.Api.Data;
using AlatTipMyself.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlatTipMyself.Api.Services
{
    public class UserService : IUserService
    {
        private readonly TipMySelfContext _context;

        public UserService(TipMySelfContext context)
        {
            _context = context;
        }

        


        //public async Task<IEnumerable<UserDetail>> GetUserDetailsAsync()
        //{
        //    return await _context.UserDetails.OrderBy(c => c.AcctNumber).ToListAsync();
        //}

        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public async Task<UserDetail> UserLoginAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return null;

            }
            var user = await _context.UserDetails.SingleOrDefaultAsync(x => x.Email == email);

            if (user == null)
            {
                return null;
            }
            return user;
        }

       
    }
    
}
