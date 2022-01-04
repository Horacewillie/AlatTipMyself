using AlatTipMyself.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlatTipMyself.Api.Services
{
    public interface ITransactionService
    {
        public Task<UserDetail> SendMoneyAsync(string FromAccount, string ToAccount, decimal Amount);
        public Task<IEnumerable<TransactionHistory>> GetAllTransactionsAsync(string AcctNumber);
        public Task<IEnumerable<WalletHistory>> GetAllWalletHistoriesAsync(string AcctNumber);
    }
}
