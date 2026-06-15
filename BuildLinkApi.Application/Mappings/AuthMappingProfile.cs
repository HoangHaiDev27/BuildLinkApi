using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BuildLinkApi.Application.DTOs;
using BuildLinkApi.Domain.Entities;

namespace BuildLinkApi.Application.Mappings
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<Account, CurrentAccountResponse>()
                .ForMember(dest => dest.AccountId,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AccountType,
                    opt => opt.MapFrom(src => src.AccountType.ToString()))
                .ForMember(dest => dest.Role,
                    opt => opt.MapFrom(src => src.Role.Name));
        }
    }
}