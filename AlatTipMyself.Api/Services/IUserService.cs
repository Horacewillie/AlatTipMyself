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
    }
}
