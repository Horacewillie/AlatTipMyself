using AlatTipMyself.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlatTipMyself.Api.Services
{
  public  interface IUserService
    {
        Task<IEnumerable<UserDetail>> GetUserDetails();

        Task<UserDetail> UserLogin(string email);

        void CreateWallet(Wallet wallet);

        Task<Wallet> UserWallet(string acctNum);

        Task<bool> WalletExists(string accNum);

        Task <bool> Save();

        //UserDetail GetByAccountNumber(string AccountNumber);
    }
}
