using church_mgt_core.services.interfaces;
using church_mgt_dtos;
using church_mgt_dtos.AuthenticationDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace church_mgt_api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IEmailService _mailService;

        public AuthController(IAuthenticationService authenticationService, IEmailService mailService)
        {
            _authenticationService = authenticationService;
            _mailService = mailService;
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

        // base-url/Auth/sendmail
        [HttpPost]
        [Route("send-mail")]
        [AllowAnonymous]
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
    }
}
