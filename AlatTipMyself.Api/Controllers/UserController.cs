using AlatTipMyself.Api.DTO;
using AlatTipMyself.Api.Models;
using AlatTipMyself.Api.Parameters;
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

        [HttpPost("Login")]
        public async Task<ActionResult<UserDetailDto>> Login([FromBody] LoginParameter model)
        {
            var user = await _user.UserLoginAsync(model.Email, model.Password);
            var userDto = _mapper.Map<UserDetailDto>(user);
            return Ok(userDto);
        }   
        
        [HttpGet("UserDetails", Name="GetUserDetail")]

        public async Task<IActionResult> UserDetails (string acctNum)
        {
            var userDetails = await _user.GetUserDetail(acctNum);
            var userDto = _mapper.Map<UserDetailDto>(userDetails);
            return Ok(userDto);
        }

        [HttpPost("CreateAccount")]
        public async Task<ActionResult<UserDetailDto>> CreateAccountAsync( CreateAccountDto createUser)
        {
            if (createUser == null)
            {
                throw new ArgumentNullException(nameof(createUser));
            }
            var createdUser = _user.CreateAccountAsync(createUser);
            await _user.SaveAsync();
            var userDto = _mapper.Map<UserDetailDto>(createdUser);
            return CreatedAtRoute("GetUserDetail", new { AcctNum = userDto.AcctNumber }, userDto);
        }
    }
}
