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

        Task<UserDetail> UserLoginAsync(string email, string Password);
        Task <bool> SaveAsync();

        Task<UserDetail> GetUserDetail(string acctNum);

        Task CreateAccountAsync(CreateUserParameters createUser);

        
    }
}
