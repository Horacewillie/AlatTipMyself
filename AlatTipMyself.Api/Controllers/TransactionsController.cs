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
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;


        public TransactionsController(ITransactionService transactionService, IMapper mapper, IUserService userService)
        {
            _transactionService = transactionService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("send-money")]
        public async Task<IActionResult> SendMoneyAsync(string FromAccount, [FromBody]SendMoneyParameter sendDetails)
        {
            if (sendDetails == null) return BadRequest(new { StatusCode = 400, Message = "Fields cannot be empty" });
            var ToAccount = sendDetails.ToAccount;
            var Amount = sendDetails.Amount;
            var sourceAccount = await _transactionService.SendMoneyAsync(FromAccount, ToAccount, Amount);
            await _userService.SaveAsync();

          //  return Content(sourceAccount.ToString());
            return Ok(sourceAccount);
            
        }

    /*   public Task SendMoneyAsync((string FromAccount, SendMoneyParameter sendMoneyParameter) p)
      {
            throw new NotImplementedException();
        }
  */ 
        [HttpGet]
        [Route("get-all-transactions")]
        public async Task<IActionResult> GetAllTransactionsAsync(string AcctNumber)
        {
            var transactionHistories = await _transactionService.GetAllTransactionsAsync(AcctNumber);
            var mappedTransactionHistories = _mapper.Map<IEnumerable<GetTransactionsDto>>(transactionHistories);
            await _userService.SaveAsync();
            return Ok(mappedTransactionHistories);
        }

        [HttpGet]
        [Route("get-all-wallet-histories")]
        public async Task<IActionResult> GetAllWalletHistoriesAsync(string AcctNumber)
        {
            var walletHistories = await _transactionService.GetAllWalletHistoriesAsync(AcctNumber);
            var mappedWalletHistories = _mapper.Map<IEnumerable<GetWalletHistoryDto>>(walletHistories);
            await _userService.SaveAsync();
            return Ok(mappedWalletHistories);
        }
    }
}
