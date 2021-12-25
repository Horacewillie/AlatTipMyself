using AlatTipMyself.Api.Models;
using AlatTipMyself.Api.Services;
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

        public UserController(IUserService user)
        {
            _user = user;
        }

        [HttpGet]
        public async Task<IActionResult> GetDetails()
        {
            var users = await _user.GetUserDetails();
            return Ok(users);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _user.UserLogin(model.Email);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);

        }

    }
}
