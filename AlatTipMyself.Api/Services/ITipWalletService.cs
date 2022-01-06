using AlatTipMyself.Api.DTO;
using AlatTipMyself.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlatTipMyself.Api.Services
{
   public  interface ITipWalletService
    {
        Task<Wallet> ActivateTipMyselfAsync(WalletCreationDto tipWallet, string acctNum);

        

    }
}
