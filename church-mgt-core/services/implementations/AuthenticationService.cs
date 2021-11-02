using AutoMapper;
using church_mgt_core.services.interfaces;
using church_mgt_core.Services.interfaces;
using church_mgt_dtos;
using church_mgt_dtos.AuthenticationDtos;
using church_mgt_dtos.Dtos;
using church_mgt_models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace church_mgt_core.services.implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ITokenGeneratorService _tokenGenerator;

        public AuthenticationService(IMapper mapper, UserManager<AppUser> userManager,
            IConfiguration configuration, ITokenGeneratorService tokenGenerator)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<Response<RegisterResponseDto>> Register(RegisterDto registerDto)
        {
            if (registerDto.Password != registerDto.ConfirmPassword)
                return Response<RegisterResponseDto>.Fail("Password and Conf irmPassword not match");

            AppUser user = _mapper.Map<AppUser>(registerDto);
            user.UserName = registerDto.Email;

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            

            var response = _mapper.Map<RegisterResponseDto>(user);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Member");
                return Response<RegisterResponseDto>.Success("Succssfully created!", response, StatusCodes.Status201Created);
            }

            string errors = "";
            foreach (var err in result.Errors)
                errors += err + Environment.NewLine;

            return Response<RegisterResponseDto>.Fail(errors);
        }

        public async Task<Response<LoginResponseDto>> LoginUserAsync(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Response<LoginResponseDto>.Fail("No user with the specified email address found");

            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!result)
                return Response<LoginResponseDto>.Fail("Invalid credentials");

            var token = await _tokenGenerator.GenerateToken(user);
            //await _mailService.SendEmailAsync(new MailRequestDto { ToEmail = user.Email, Subject = "New login", Body = $"<h1>Hello, new login to your account noticed!</h1>\n<p>New login to your account on Hotel Management</p> at {DateTime.UtcNow}", Attachments = null });

            return Response<LoginResponseDto>.Success("Login Successful!", new LoginResponseDto { Id = user.Id, Token = token });
        }
    }
}
