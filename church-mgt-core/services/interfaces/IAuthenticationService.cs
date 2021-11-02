using church_mgt_dtos;
using church_mgt_dtos.AuthenticationDtos;
using church_mgt_dtos.Dtos;
using System.Threading.Tasks;

namespace church_mgt_core.services.interfaces
{
    public interface IAuthenticationService
    {
        Task<Response<string>> ConfirmEmailAsync(string email, string token);
        Task<Response<string>> ForgotPasswordAsync(string email);
        Task<Response<LoginResponseDto>> LoginUserAsync(LoginDto model);
        Task<Response<RegisterResponseDto>> Register(RegisterDto registerDto);
        Task<Response<string>> ResetPasswordAsync(ResetPasswordDto model);
    }
}