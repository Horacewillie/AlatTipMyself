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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _user.UserLoginAsync(model.Email);

            if (user == null)
            return BadRequest(new {StatusCode = 400,  Message = "Username or password is incorrect" });
            return Ok(user);
        }   
        
        [HttpGet("user-details")]

        public async Task<IActionResult> UserDetails (string acctNum)
        {
            var userDetails = await _user.GetUserDetail(acctNum);
            return Ok(userDetails);
        }
    }
}
