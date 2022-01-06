using AlatTipMyself.Api.DTO;
using AlatTipMyself.Api.Models;
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
            CreateMap<DTO.WalletCreationDto, Models.Wallet>();
            CreateMap<Models.Wallet, DTO.WalletDto>();
            
        }
    }
}
