using church_mgt_core.services.interfaces;
using church_mgt_dtos;
using church_mgt_dtos.AuthenticationDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace church_mgt_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IEmailService _mailService;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthenticationService authenticationService, IEmailService mailService, IConfiguration configuration)
        {
            _authenticationService = authenticationService;
            _mailService = mailService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody ]RegisterDto registerDto)
        {
            var result = await _authenticationService.Register(registerDto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _authenticationService.LoginUserAsync(loginDto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            try
            {
                var result = await _authenticationService.ConfirmEmailAsync(email, token);
                return Redirect($"{_configuration["BaseUrl"]}confirmemail.html");
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        // base-url/Auth/sendmail
        [HttpPost]
        [Route("send-mail")]
        public async Task<IActionResult> SendMail([FromForm] MailRequestDto model)
        {
            try
            {
                var result = await _mailService.SendEmailAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new[] { ex.Message, ex.StackTrace });
            }

        }

        // base-url/Auth/forgotpassword
        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var result = await _authenticationService.ForgotPasswordAsync(email);
            return StatusCode(result.StatusCode, result);
        }

        // base-url/Auth/reset-password
        [HttpPost]
        [Route("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordDto model)
        {
            var result = await _authenticationService.ResetPasswordAsync(model);
            return StatusCode(result.StatusCode, result);
        }
    }
}
