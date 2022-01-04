using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlatTipMyself.Api.Services
{
    public interface ITransactionService
    {
        public Task SendMoneyAsync(string FromAccount, string ToAccount, decimal Amount);
    }
}
