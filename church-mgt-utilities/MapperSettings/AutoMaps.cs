using AutoMapper;
using church_mgt_dtos;
using church_mgt_dtos.AuthenticationDtos;
using church_mgt_dtos.CommentDto;
using church_mgt_dtos.DepartmentDtos;
using church_mgt_dtos.PaymentDtos;
using church_mgt_dtos.PaymentTypeDtos;
using church_mgt_dtos.PrayerRequestDtos;
using church_mgt_dtos.TestimonyDtos;
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

            // testimonies
            CreateMap<Testimony, AddTestimonyDto>().ReverseMap();


            // payment type
            CreateMap<PaymentType, AddPaymentTypeDto>().ReverseMap();
            CreateMap<AddPaymentTypeResponseDto, PaymentType>().ReverseMap();

            // payment
            CreateMap<Payment, PaymentResponseDto>()
                //.ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                //.ForMember(x => x.PaymentReference, y => y.MapFrom(z => z.PaymentReference))
                //.ForMember(x => x.Amount, y => y.MapFrom(z => z.Amount))
                //.ForMember(x => x.Status, y => y.MapFrom(z => z.Status))
                //.ForMember(x => x.CreatedAt, y => y.MapFrom(z => z.CreatedAt))
                .ForMember(x => x.PaymentType, y => y.MapFrom(u => u.PaymentType.Id))
                .ForMember(x => x.AppUser, y => y.MapFrom(u => u.AppUser.FirstName + " " + u.AppUser.LastName));
            CreateMap<Payment, MakePaymentDto>().ReverseMap();
        }
    }
}
