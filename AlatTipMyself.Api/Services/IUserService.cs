using AlatTipMyself.Api.DTO;
using AlatTipMyself.Api.Models;
using AlatTipMyself.Api.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlatTipMyself.Api.Services
{
  public  interface IUserService
    {
        //Task<IEnumerable<UserDetail>> GetUserDetailsAsync();

        Task<UserDetail> UserLoginAsync(LoginParameter model);
        Task <bool> SaveAsync();

        Task<UserDetail> GetUserDetailAsync(string acctNum);

        Task CreateAccountAsync(UserDetail userDetail);

        Task<WalletDto> WalletDetailsAsync(string acctNumber);

        Task<IEnumerable<UserDetail>> GetUserDetailsAsync();

    }
}
