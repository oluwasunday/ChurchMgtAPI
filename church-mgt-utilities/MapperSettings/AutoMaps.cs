using AutoMapper;
using church_mgt_dtos;
using church_mgt_dtos.AuthenticationDtos;
using church_mgt_dtos.CommentDto;
using church_mgt_dtos.DepartmentDtos;
using church_mgt_dtos.PrayerRequestDtos;
using church_mgt_models;
using System;
using System.Linq;

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

            // comments
            CreateMap<Comment, CommentResponseDto>().ReverseMap();

            // department
            CreateMap<Department, AddDepartmentDto>().ReverseMap();
            CreateMap<AddDepartmentResponseDto, Department>().ReverseMap();
            CreateMap<Department, MembersInDeptDto>()
                .ForMember(x => x.Department, y => y.MapFrom(u => u.Name))
                .ForMember(x => x.FullName, y => y.MapFrom(u => u.AppUsers.Select(v => v.Title + " " + v.FirstName + " " + v.LastName)));

            // prayer request
            CreateMap<PrayerRequest, AddPrayerRequestDto>().ReverseMap();

        }
    }
}
