using AlatTipMyself.Api.Data;
using AlatTipMyself.Api.DTO;
using AlatTipMyself.Api.Helpers;
using AlatTipMyself.Api.Models;
using AlatTipMyself.Api.Parameters;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlatTipMyself.Api.Services
{
    public class UserService : IUserService
    {
        private readonly TipMySelfContext _context;
        private readonly IMapper _mapper;

        public UserService(TipMySelfContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        
        

        public async Task<UserDetail> CreateAccountAsync(CreateAccountDto createUser)
        {
            if (string.IsNullOrEmpty(createUser.Email) || string.IsNullOrEmpty(createUser.FirstName) || string.IsNullOrEmpty(createUser.TransactionPin) || string.IsNullOrEmpty(createUser.Password) || string.IsNullOrEmpty(createUser.FirstName))
            {
                throw new ArgumentNullException(nameof(createUser));
            }

            if (await _context.UserDetails.AnyAsync(x => x.Email == createUser.Email)) throw new ApplicationException("An account already exists with this email");

            var user = _mapper.Map<UserDetail>(createUser);
            var Pin = createUser.TransactionPin;
            var Password = createUser.Password;

            byte[] pinHash, pinSalt;
            byte[] passwordHash, passwordSalt;

            HelperMethods.CreatePinHash(Pin, out pinHash, out pinSalt);
            HelperMethods.CreatePasswordHash(Password, out passwordHash, out passwordSalt);
            user.PinHash = pinHash;
            user.PinSalt = pinSalt;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _context.UserDetails.AddAsync(user);
            return user;
        }

       

        public async Task<UserDetail> GetUserDetail(string acctNum)
        {
            if(acctNum == null)
            {
                throw new ArgumentNullException(nameof(acctNum));
            }
            return await _context.UserDetails.Where(c => c.AcctNumber == acctNum).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public async Task<UserDetail> UserLoginAsync(string email, string Password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(Password)) throw new ArgumentNullException("Fields cannot be empty!");
           
            var user = await _context.UserDetails.SingleOrDefaultAsync(x => x.Email == email);
            if (user == null) throw new ApplicationException("Incorrect Email");
            if (!HelperMethods.VerifyPasswordHash(Password, user.PasswordHash, user.PasswordSalt).Result)throw new ApplicationException("Invalid Password");
            return user;
        }

    }
    
}
