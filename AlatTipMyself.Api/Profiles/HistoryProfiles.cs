using AlatTipMyself.Api.DTO;
using AlatTipMyself.Api.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlatTipMyself.Api.Profiles
{
    public class HistoryProfiles : Profile
    {
        public HistoryProfiles()
        {
            CreateMap<TransactionHistory, TransactionsDto>();
            CreateMap<WalletHistory, WalletHistoryDto>();
        }
    }
}
