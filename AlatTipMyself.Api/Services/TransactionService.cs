using AlatTipMyself.Api.Data;
using AlatTipMyself.Api.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlatTipMyself.Api.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly TipMySelfContext _context;


        public TransactionService(TipMySelfContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TransactionHistory>> GetAllTransactionsAsync(string AcctNumber)
        {
            return await Task.Run(() =>
            {
                IEnumerable<TransactionHistory> transactionHistories = _context.TransactionHistories.ToList();
                return transactionHistories;
            }
            );
        }

        public async Task<IEnumerable<WalletHistory>> GetAllWalletHistoriesAsync(string AcctNumber)
        {
            return await Task.Run(() =>
            {
                IEnumerable<WalletHistory> walletHistories = _context.WalletHistories.ToList();
                return walletHistories;
            }
            );
        }

        public async Task<UserDetail> SendMoneyAsync(string FromAccount, string ToAccount, decimal Amount)
        {
            if (FromAccount == ToAccount) throw new ApplicationException("Sender and receiver account can not be the same");
            UserDetail sourceAccount;
            UserDetail destinationAccount;
            TransactionHistory transactionHistory = new TransactionHistory();
            WalletHistory walletHistory = new WalletHistory();

            Wallet userWallet = _context.Wallets.Where(x => x.AcctNumber == FromAccount).SingleOrDefault();

            sourceAccount = _context.UserDetails.Where(x => x.AcctNumber == FromAccount).FirstOrDefault();
            destinationAccount = _context.UserDetails.Where(x => x.AcctNumber == ToAccount).FirstOrDefault();

            await Task.Run(() =>
            {
                if (sourceAccount == null || destinationAccount == null) throw new ArgumentNullException("Account does not exist");
                

                if (sourceAccount.AcctBalance >= Amount)
                {
                    sourceAccount.AcctBalance -= Amount;
                    destinationAccount.AcctBalance += Amount;
                    transactionHistory.TransactionStatus = TranStatus.Success;
                    transactionHistory.TransactionSourceAccount = FromAccount;
                    transactionHistory.TransactionDestinationAccount = ToAccount;
                    transactionHistory.TransactionAmount = Amount;
                    transactionHistory.TransactionDate = DateTime.UtcNow;
                    _context.TransactionHistories.Add(transactionHistory);
                }

                if (userWallet is null)
                {

                }
                else
                {
                    if (userWallet.TipStatus == true)
                    {
                        if (sourceAccount.AcctBalance >= (Convert.ToDecimal(userWallet.TipPercent)) / 100 * Amount)
                        {
                            sourceAccount.AcctBalance -= (Convert.ToDecimal(userWallet.TipPercent)) / 100 * Amount;
                            userWallet.WalletBalance += (Convert.ToDecimal(userWallet.TipPercent)) / 100 * Amount;
                        }
                        walletHistory.AcctNumber = FromAccount;
                        walletHistory.WalletId = userWallet.WalletId;
                        walletHistory.TransactionAmount = Amount;
                        walletHistory.TipPercent = Convert.ToInt32(userWallet.TipPercent);
                        walletHistory.TipAmount = (Convert.ToDecimal(userWallet.TipPercent)) / 100 * Amount;
                        walletHistory.Date = DateTime.UtcNow;
                        _context.WalletHistories.Add(walletHistory);
                    }
                }
            }
            );
            return sourceAccount;
        }
    }
}
