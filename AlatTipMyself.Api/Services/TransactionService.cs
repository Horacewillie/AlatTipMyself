using AlatTipMyself.Api.Data;
using AlatTipMyself.Api.DTO;
using AlatTipMyself.Api.Helpers;
using AlatTipMyself.Api.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlatTipMyself.Api.Services
{
    public class TransactionService : ITransactionService, IDisposable
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
                IEnumerable<TransactionHistory> transactionHistories = _context.TransactionHistories.Where(x => x.TransactionSourceAccount == AcctNumber).OrderByDescending(x => x.Id).ToList();
                return transactionHistories;
            }
            );
        }

        public async Task<IEnumerable<WalletHistory>> GetAllWalletHistoriesAsync(string AcctNumber)
        {
            return await Task.Run(() =>
            {
                IEnumerable<WalletHistory> walletHistories = _context.WalletHistories.Where(x => x.AcctNumber == AcctNumber).OrderByDescending(x => x.WalletHistoryId).ToList();
                return walletHistories;
            }
            );
        }

        public async Task<WalletHistory> SendMoneyAsync(string FromAccount, string ToAccount, decimal Amount, string TransactionPin)
        {
            
            if (FromAccount == ToAccount) throw new ApplicationException("Sender and receiver account can not be the same");
            UserDetail sourceAccount;
            UserDetail destinationAccount;
            TransactionHistory transactionHistory = new TransactionHistory();
            WalletHistory walletHistory = new WalletHistory();

            Wallet userWallet = _context.Wallets.SingleOrDefault(x => x.AcctNumber == FromAccount);

            sourceAccount = _context.UserDetails.FirstOrDefault(x => x.AcctNumber == FromAccount);
            destinationAccount = _context.UserDetails.FirstOrDefault(x => x.AcctNumber == ToAccount);

            await Task.Run(() =>
            {

                 if(destinationAccount == null) throw new ApplicationException("Destination Account does not exist");
                if (sourceAccount.AcctBalance >= Amount)
                {
                    var isValid = HelperMethods.VerifyPin(TransactionPin, sourceAccount.TransactionPin);
                    if (!isValid) throw new ApplicationException("Incorrect Transfer Pin.");

                    if (Amount <= 0) throw new ApplicationException("Cannot send an amount less than or equal to zero");
                   
                    sourceAccount.AcctBalance -= Amount;
                    destinationAccount.AcctBalance += Amount;

                    transactionHistory.TransactionStatus = TranStatus.Success;
                    transactionHistory.TransactionSourceAccount = FromAccount;
                    transactionHistory.TransactionDestinationAccount = ToAccount;
                    transactionHistory.TransactionAmount = Amount;
                    transactionHistory.TransactionDate = DateTime.UtcNow;
                    _context.TransactionHistories.AddAsync(transactionHistory);

                    if (userWallet is null)
                    {
                        walletHistory.TipAmount = -2;
                    }
                    else
                    {
                        if (userWallet.TipStatus == true)
                        {
                            if (sourceAccount.AcctBalance >= (Convert.ToDecimal(userWallet.TipPercent)) / 100 * Amount)
                            {
                                sourceAccount.AcctBalance -= (Convert.ToDecimal(userWallet.TipPercent)) / 100 * Amount;
                                userWallet.WalletBalance += (Convert.ToDecimal(userWallet.TipPercent)) / 100 * Amount;

                                walletHistory.AcctNumber = FromAccount;
                                walletHistory.WalletId = userWallet.WalletId;
                                walletHistory.TransactionAmount = Amount;
                                walletHistory.TipPercent = Convert.ToInt32(userWallet.TipPercent);
                                walletHistory.TipAmount = (Convert.ToDecimal(userWallet.TipPercent)) / 100 * Amount;
                                walletHistory.Date = DateTime.UtcNow;

                                _context.WalletHistories.Add(walletHistory);
                            }
                            else
                            {
                                walletHistory.AcctNumber = FromAccount;
                                walletHistory.WalletId = userWallet.WalletId;
                                walletHistory.TransactionAmount = Amount;
                                walletHistory.TipPercent = Convert.ToInt32(userWallet.TipPercent);
                                walletHistory.TipAmount = -3;
                                walletHistory.Date = DateTime.UtcNow;                            
                            }                         
                        }
                        else
                        {
                            walletHistory.TipAmount = -1;
                        }
                    }
                }
                else
                {
                    throw new ApplicationException("Insufficient funds");
                }      
            }
            );
            return walletHistory;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose resources when needed
            }
        }
    }
}
