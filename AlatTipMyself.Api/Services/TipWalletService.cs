using AlatTipMyself.Api.Data;
using AlatTipMyself.Api.DTO;
using AlatTipMyself.Api.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlatTipMyself.Api.Services
{
    public class TipWalletService : ITipWalletService
    {
        private readonly IMapper _mapper;
        private readonly TipMySelfContext _context;
        public TipWalletService(IMapper mapper, TipMySelfContext context)
        {
            _mapper = mapper;
            _context = context;        }

        public async Task<Wallet> ActivateTipMyselfAsync(TipWalletDTO tipWallet, string acctNum)
        {
            if(tipWallet == null)
            {
                throw new ArgumentNullException(nameof(tipWallet));
            }
            var wallet = _mapper.Map<Models.Wallet>(tipWallet);
            var walletExists =  await _context.Wallets.AnyAsync(c => c.AcctNumber == acctNum);
            if (!walletExists)
            {
               await _context.Wallets.AddAsync(wallet);
                wallet.AcctNumber = acctNum;
                wallet.TipStatus = tipWallet.TipStatus;
                wallet.TipPercent = tipWallet.TipPercent;
                return wallet;
            }
            else
            {
                var userWallet = await _context.Wallets.FirstOrDefaultAsync(c => c.AcctNumber == acctNum);
                userWallet.AcctNumber = acctNum;
                userWallet.TipStatus = tipWallet.TipStatus;
                userWallet.TipPercent = tipWallet.TipPercent;
                return userWallet;
            }

        }

       
    }
}
