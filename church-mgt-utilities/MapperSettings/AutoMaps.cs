using AutoMapper;
using church_mgt_dtos;
using church_mgt_dtos.AuthenticationDtos;
using church_mgt_models;
using System;

namespace church_mgt_utilities
{
    public class AutoMaps : Profile
    {
        public AutoMaps()
        {
            // Authentication
            CreateMap<AppUser, RegisterDto>().ReverseMap();
            CreateMap<AppUser, RegisterResponseDto>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.MiddleName} {src.LastName}"))
                .ForMember(dest => dest.Age,
                    opt => opt.MapFrom(src => src.DOB.GetCurrentAge())
                );
        }
    }
}
