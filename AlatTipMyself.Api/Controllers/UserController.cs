using AlatTipMyself.Api.DTO;
using AlatTipMyself.Api.Models;
using AlatTipMyself.Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlatTipMyself.Api.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _user;
        private readonly IMapper _mapper;
       

        public UserController(IUserService user, IMapper mapper)
        {
            _user = user;
            _mapper = mapper;
            
        }
        #region
        //[HttpGet]
        //public async Task<IActionResult> GetDetails()
        //{
        //    var users = await _user.GetUserDetailsAsync();
        //    return Ok(users);
        //}
        #endregion


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _user.UserLoginAsync(model.Email);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            return Ok(user);
        }     
    }
}
