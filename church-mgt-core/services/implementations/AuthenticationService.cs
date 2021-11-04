using AutoMapper;
using church_mgt_core.services.interfaces;
using church_mgt_core.Services.interfaces;
using church_mgt_dtos;
using church_mgt_dtos.AuthenticationDtos;
using church_mgt_dtos.Dtos;
using church_mgt_models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_core.services.implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly ITokenGeneratorService _tokenGenerator;

        public AuthenticationService(IMapper mapper, UserManager<AppUser> userManager, IEmailService emailService,
            IConfiguration configuration, ITokenGeneratorService tokenGenerator)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<Response<RegisterResponseDto>> Register(RegisterDto registerDto)
        {
            if (registerDto.Password != registerDto.ConfirmPassword)
                return Response<RegisterResponseDto>.Fail("Password and ConfirmPassword not match");

            AppUser user = _mapper.Map<AppUser>(registerDto);
            user.UserName = registerDto.Email;

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            

            var response = _mapper.Map<RegisterResponseDto>(user);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Member");

                var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var encodedEmailToken = Encoding.UTF8.GetBytes(emailToken);
                var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                string url = $"{_configuration["BaseUrl"]}api/Auth/confirm-email?email={user.Email}&token={validEmailToken}";
                var mailDto = new MailRequestDto
                {
                    ToEmail = user.Email,
                    Subject = "Confirm your email",
                    Body = $"<h1>Welcome to RCCG Solid Rock Parish</h1>\n<p>Pls confirm your email by <a href='{url}'>clicking here</a></p>",
                    Attachments = null
                };

                await _emailService.SendEmailAsync(mailDto);

                return Response<RegisterResponseDto>.Success("Succssfully created! Confirmation link successfully sent to specified email", response, StatusCodes.Status201Created);
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
            await _emailService.SendEmailAsync(new MailRequestDto { ToEmail = user.Email, Subject = "New login", Body = $"<h1>Hello, new login to your account noticed!</h1>\n<p>New login to your account on RCCG Solid Rock Parish</p> at {DateTime.UtcNow}", Attachments = null });

            return Response<LoginResponseDto>.Success("Login Successful!", new LoginResponseDto { Id = user.Id, Token = token });
        }

        public async Task<Response<string>> ConfirmEmailAsync(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return Response<string>.Fail("User not found", StatusCodes.Status404NotFound);

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            var normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);
            if (result.Succeeded)
                return Response<string>.Success("Email confirmation successful", user.Id, StatusCodes.Status200OK);

            return Response<string>.Fail(GetErrors(result), StatusCodes.Status400BadRequest);
        }

        private static string GetErrors(IdentityResult result)
        {
            return result.Errors.Aggregate(string.Empty, (current, err) => current + err.Description + "\n");
        }

        public async Task<Response<string>> ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return Response<string>.Fail("User not found", StatusCodes.Status404NotFound);
            
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            string url = $"{_configuration["BaseUrl"]}ResetPassword?email={email}&token={validToken}";

            await _emailService.SendEmailAsync(new MailRequestDto
            {
                ToEmail = email,
                Subject = "Reset Password",
                Body = $"<h1>Follow the instructions to reset your password</h1>\n<p>To reset your password, <a href='{url}'>click here</a></p>"
            });

            return Response<string>.Success("Successful", "Reset password link successfully sent to specified email", StatusCodes.Status200OK);
        }

        public async Task<Response<string>> ResetPasswordAsync(ResetPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Response<string>.Fail("User not found");

            if (model.NewPassword != model.ConfirmPassword)
                return Response<string>.Fail("Password and ConfirmPassword not match");

            var decodedToken = WebEncoders.Base64UrlDecode(model.Token);
            var normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ResetPasswordAsync(user, normalToken, model.NewPassword);
            if (result.Succeeded)
                return Response<string>.Success("Password successfully reset", "Successful");

            return new Response<string> { Message= "Something went wrong", StatusCode= StatusCodes.Status500InternalServerError, Errors = GetErrors(result)};
        }
    }
}
