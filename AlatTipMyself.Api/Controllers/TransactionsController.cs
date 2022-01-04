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
        private ITransactionService _transactionService;
        IMapper _mapper;

        public TransactionsController(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("send_money")]
        public async Task<IActionResult> SendMoney(string FromAccount, string ToAccount, decimal Amount)
        {
            await _transactionService.SendMoneyAsync(FromAccount, ToAccount, Amount);
            return Ok();
        }
    }
}
