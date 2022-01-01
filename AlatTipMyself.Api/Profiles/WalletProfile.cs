using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlatTipMyself.Api.Profiles
{
    public class WalletProfile : Profile 
    {
        public WalletProfile()
        {
            CreateMap<DTO.TipWalletDTO, Models.Wallet>();
            CreateMap<Models.Wallet, DTO.WalletDTO>();
        }
    }
}
