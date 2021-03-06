using church_mgt_core.services.interfaces;
using church_mgt_dtos;
using church_mgt_dtos.AuthenticationDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
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
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _env;

        public AuthController(
            IAuthenticationService authenticationService, 
            IEmailService mailService, 
            IConfiguration configuration, 
            ILogger logger,
            IWebHostEnvironment env)
        {
            _authenticationService = authenticationService;
            _mailService = mailService;
            _configuration = configuration;
            _logger = logger;
            _env = env;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody ]RegisterDto registerDto)
        {
            _logger.Information($"Registration attempt for {registerDto.Email}");
            var result = await _authenticationService.Register(registerDto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            _logger.Information($"Login attempt for {loginDto.Email}");
            var result = await _authenticationService.LoginUserAsync(loginDto);
            return StatusCode(result.StatusCode, result);
        }


        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            try
            {
                string baseUrl = _env.IsProduction() ? _configuration["HerokuUrl"] : _configuration["BaseUrl"];
                var result = await _authenticationService.ConfirmEmailAsync(email, token);
                return Redirect($"{baseUrl}confirmemail.html");
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
                _logger.Information($"SendMail Attempt for {model.ToEmail}");
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
            _logger.Information($"Forgot password attempt for {email}");
            var result = await _authenticationService.ForgotPasswordAsync(email);
            return StatusCode(result.StatusCode, result);
        }

        // base-url/Auth/reset-password
        [HttpPost]
        [Route("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordDto model)
        {
            _logger.Information($"Reset password attempt for {model.Email}");
            var result = await _authenticationService.ResetPasswordAsync(model);
            return StatusCode(result.StatusCode, result);
        }

    }
}
