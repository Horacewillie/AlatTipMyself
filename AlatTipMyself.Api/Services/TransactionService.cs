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
        public void SendMoney(string FromAccount, string ToAccount, decimal Amount)
        {
            UserDetail sourceAccount;
            UserDetail destinationAccount;
            WalletHistory walletHistory = new WalletHistory();
            var userWallet = _context.Wallets.Where(x => x.AcctNumber == FromAccount).SingleOrDefault();

            // var authUser = _accountService.Authenticate(FromAccount, TransactionPin);
            //if (authUser == null) throw new ApplicationException("Invalid credentials");
            

            try
            {
                sourceAccount = _context.UserDetails.Where(x => x.AcctNumber == FromAccount).FirstOrDefault();
                destinationAccount = _context.UserDetails.Where(x => x.AcctNumber == ToAccount).FirstOrDefault();

                if (sourceAccount == null || destinationAccount == null) throw new ApplicationException("Account does not exist");

                    if (sourceAccount.AcctBalance >= Amount)
                {
                    sourceAccount.AcctBalance -= Amount;
                    destinationAccount.AcctBalance += Amount;
                }

                
                if (userWallet == null) throw new ApplicationException("");



                if(userWallet.TipStatus == true)
                {
                    if(sourceAccount.AcctBalance >=  (Convert.ToDecimal(userWallet.TipPercent))/100 * Amount)
                    {
                        sourceAccount.AcctBalance -= (Convert.ToDecimal(userWallet.TipPercent)) / 100 * Amount;
                        userWallet.WalletBalance += (Convert.ToDecimal(userWallet.TipPercent)) / 100 * Amount;
                    }
                }


                //checking if transfer was successful
                //if ((_context.Entry(sourceAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified) && (_context.Entry(destinationAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified))
                //{

                //}
                //else
                //{


                //}

                walletHistory.AcctNumber = FromAccount;
                walletHistory.WalletId = userWallet.WalletId;
                walletHistory.TransactionAmount = Amount;
                walletHistory.TipPercent = Convert.ToInt32(userWallet.TipPercent);
                walletHistory.TipAmount = (Convert.ToDecimal(userWallet.TipPercent)) / 100 * Amount;
                walletHistory.Date = DateTime.UtcNow;



                _context.WalletHistories.Add(walletHistory);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                //_logger.LogError($"AN ERROR OCCURED...");
            } 
        }
    }
}
