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

        public async Task CreateAccountAsync(UserDetail userDetail)
        {
            if (string.IsNullOrEmpty(userDetail.FirstName) || string.IsNullOrEmpty(userDetail.LastName) || string.IsNullOrEmpty(userDetail.Email) || string.IsNullOrEmpty(userDetail.TransactionPin) || string.IsNullOrEmpty(userDetail.Password))
            {
                throw new ArgumentNullException("Field(s) cannot be empty");
            }
            if (!HelperMethods.VerifyEmail(userDetail.Email)) throw new ApplicationException("Incorrect Email");
            var user = await _context.UserDetails.SingleOrDefaultAsync(x => x.Email == userDetail.Email);
            if (user != null) throw new ApplicationException("Email already Exists");
            userDetail.Password = HelperMethods.HashPassword(userDetail.Password);
            userDetail.TransactionPin = HelperMethods.HashTransactionPin(userDetail.TransactionPin);
            await _context.UserDetails.AddAsync(userDetail);
        }



        public async Task<UserDetail> GetUserDetailAsync(string acctNum)
        {
            if(acctNum == null)
            {
                throw new ArgumentNullException("User doesn't exist");
            }
            return await _context.UserDetails.Where(c => c.AcctNumber == acctNum).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UserDetail>> GetUserDetailsAsync()
        {
            return await _context.UserDetails.ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public async Task<UserDetail> UserLoginAsync(LoginParameter model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password)) throw new ArgumentNullException(nameof(model));
            if (!HelperMethods.VerifyEmail(model.Email)) throw new ApplicationException("Incorrect Email Type");
            var user = await _context.UserDetails.SingleOrDefaultAsync(x => x.Email == model.Email);
            if (user == null) throw new ApplicationException("Incorrect Email");
            bool isValid = HelperMethods.VerifyPassword(model.Password, user.Password);
            if (!isValid) throw new ApplicationException("Incorrect Password!");
            return user;
        }

        public async Task<WalletDto> WalletDetailsAsync(string acctNumber)
        {
            var walletDetail = await _context.Wallets.FirstOrDefaultAsync(x => x.AcctNumber == acctNumber);
            if (walletDetail == null)
            {
                throw new ApplicationException("No wallet for user");
            }
            var walletDetailDto = _mapper.Map<WalletDto>(walletDetail);
            return walletDetailDto;
        }

        

    }
    
}
