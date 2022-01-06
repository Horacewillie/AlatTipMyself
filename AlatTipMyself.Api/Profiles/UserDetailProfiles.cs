using AlatTipMyself.Api.DTO;
using AlatTipMyself.Api.Models;
using AlatTipMyself.Api.Parameters;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlatTipMyself.Api.Profiles
{
    public class UserDetailProfiles : Profile 
    {
        public UserDetailProfiles()
        {
            
            CreateMap<UserDetail, UserDetailDto>()
                .ForMember(dest => dest.AcctName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
            CreateMap<UserDetailCreationDto, UserDetail>();  
        }
    }
}
