using AlatTipMyself.Api.DTO;
using AlatTipMyself.Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlatTipMyself.Api.Controllers
{
    [Route("api/TipWallet")]
    [ApiController]
    public class TipWalletController : ControllerBase
    {
        private readonly ITipWalletService _tipWallet;
        private readonly IUserService _user;
        private readonly IMapper _mapper;
        public TipWalletController(ITipWalletService tipWallet, IUserService user, IMapper mapper)
        {
            _tipWallet = tipWallet;
            _user = user;
            _mapper = mapper;
        }

        [HttpPost("enable-tip-wallet")]
        public async Task<ActionResult<WalletDTO>> TipWallet([FromBody] TipWalletDTO model, string acctNum)
        {
            if(model == null)
            {
                return BadRequest(new { StatusCode = 400, Message = "Fields cannot be empty" });
            }
            var enableTipWallet = await _tipWallet.ActivateTipMyselfAsync(model, acctNum);
            await _user.SaveAsync();
            var walletReturn = _mapper.Map<DTO.WalletDTO>(enableTipWallet);
            return Ok(walletReturn);

        }
    }
}
