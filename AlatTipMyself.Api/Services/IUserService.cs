using AlatTipMyself.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlatTipMyself.Api.Services
{
  public  interface IUserService
    {
        //Task<IEnumerable<UserDetail>> GetUserDetailsAsync();

        Task<UserDetail> UserLoginAsync(string email);

       

        Task <bool> SaveAsync();

      
    }
}
