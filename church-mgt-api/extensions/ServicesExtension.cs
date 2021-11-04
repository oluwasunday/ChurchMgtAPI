using church_mgt_core.services.implementations;
using church_mgt_core.services.interfaces;
using church_mgt_core.Services.implementations;
using church_mgt_core.Services.interfaces;
using church_mgt_core.UnitOfWork.implementations;
using church_mgt_core.UnitOfWork.interfaces;
using church_mgt_dtos;
using church_mgt_dtos.AuthenticationDtos;
using church_mgt_dtos.DepartmentDtos;
using church_mgt_utilities.validations.AuthenticationValidators;
using church_mgt_utilities.validations.DepartmentValidators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace HotelMgt.API.Extensions
{
    public static class ServicesExtension
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {

            // Add Repository Injections Here 
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IDepartmentService, DepartmentService>();

            // Add Fluent Validator Injections Here
            services.AddTransient<IValidator<RegisterDto>, RegisterDtoValidator>();
            services.AddTransient<IValidator<LoginDto>, LoginDtoValidator>();
            services.AddTransient<IValidator<ResetPasswordDto>, ResetPasswordDtoValidator>();
            services.AddTransient<IValidator<AddDepartmentDto>, AddDepartmentDtoValidator>();

        }
    }
}
