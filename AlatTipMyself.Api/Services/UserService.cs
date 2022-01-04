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

        public void CreateWallet(Wallet wallet)
        {
            if(wallet == null)
            {
                throw new NullReferenceException("The model cannot be null");
            }
            var createWallet = _context.Wallets.Add(wallet);
        }


        public async Task<IEnumerable<UserDetail>> GetUserDetails()
        {
            return await _context.UserDetails.OrderBy(c => c.AcctNumber).ToListAsync();
        }

        public async Task<bool> Save()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public async Task<UserDetail> UserLogin(string email)
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

        public async Task<Wallet> UserWallet(string acctNum)
        {
            return await _context.Wallets.FirstOrDefaultAsync(c => c.AcctNumber == acctNum);
        }

        public async Task<bool> WalletExists(string accNum)
        {
            return await _context.Wallets.AnyAsync(c => c.AcctNumber == accNum);
        }
    }
    
}
