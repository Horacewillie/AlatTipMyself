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


        [HttpPost("tip-wallet")]
        public async Task<IActionResult> TipWallet([FromBody] TipWalletDTO model, string acctNum)
        {
            var wallet = _mapper.Map<Models.Wallet>(model);
            var walletExists = await _user.WalletExists(acctNum);

            if(!walletExists)
            {
                _user.CreateWallet(wallet);
                wallet.AcctNumber = acctNum;
                wallet.TipStatus = model.TipStatus;
                wallet.TipPercent = model.TipPercent;
                await _user.Save();
                var walletReturn = _mapper.Map<DTO.WalletDTO>(wallet);
                return CreatedAtRoute("GetWallet", new { id = walletReturn.WalletId }, walletReturn);

            }
            else
            {
                var userWallet = await _user.UserWallet(acctNum);
                userWallet.AcctNumber = acctNum;
                userWallet.TipStatus = model.TipStatus;
                userWallet.TipPercent = model.TipPercent;
                await _user.Save();
                var walletUpdate = _mapper.Map<DTO.WalletDTO>(userWallet);
                return Ok(walletUpdate);

            }

        }

        [HttpGet("walletid", Name = "GetWallet")]
        public async Task<IActionResult> UserWallet(string acctNum)
        {
            var userWallet = await _user.UserWallet(acctNum);

            if(userWallet == null)
            {
                return NotFound();
            }
            return Ok(userWallet);
        }

        //[HttpGet]
        //[Route("get_by_account_number")]
        //public IActionResult GetByAccountNumber(string accountNumber)
        //{
        //    var account = _user.GetByAccountNumber(accountNumber);
        //    var cleanedAccount = _mapper.Map<GetByAccountNumberDto>(account);
        //    return Ok(cleanedAccount);
        //}
    }
}
