using AlatTipMyself.Api.DTO;
using AlatTipMyself.Api.Models;
using AlatTipMyself.Api.Parameters;
using AlatTipMyself.Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AlatTipMyself.Api.Controllers
{
    [Route("api/Users")]
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
        [ProducesResponseType(typeof(UserDetailDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserDetailDto>> Login([FromBody] LoginParameter model)
        {
            var user = await _user.UserLoginAsync(model);
            var userDto = _mapper.Map<UserDetailDto>(user);
            return Ok(userDto);
        }   
        
        [HttpGet("UserDetails", Name="GetUserDetail")]
        [ProducesResponseType(typeof(UserDetailDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UserDetails (string acctNum)
        {
            var userDetails = await _user.GetUserDetailAsync(acctNum);
            var userDto = _mapper.Map<UserDetailDto>(userDetails);
            return Ok(userDto);
        }

        [HttpPost("CreateAccount")]
        [ProducesResponseType(typeof(UserDetailDto), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<UserDetailDto>> CreateNewAccount(UserDetailCreationDto createUser)
        {
            var userCreation = _mapper.Map<UserDetail>(createUser);
            await _user.CreateAccountAsync(userCreation);
            await _user.SaveAsync();
            var userToReturn = _mapper.Map<UserDetailDto>(userCreation);

            return CreatedAtRoute("GetUserDetail", new { acctNum = userToReturn.AcctNumber }, userToReturn);
        }

        [HttpGet("WalletDetail")]
        public async Task<ActionResult<WalletDto>> WalletDetail(string acctNumber)
        {
            var walletDetail = await _user.WalletDetailsAsync(acctNumber);
            return Ok(walletDetail);
        }
    }
}
