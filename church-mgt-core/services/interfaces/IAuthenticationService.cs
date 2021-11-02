using church_mgt_dtos;
using church_mgt_dtos.AuthenticationDtos;
using church_mgt_dtos.Dtos;
using System.Threading.Tasks;

namespace church_mgt_core.services.interfaces
{
    public interface IAuthenticationService
    {
        Task<Response<LoginResponseDto>> LoginUserAsync(LoginDto model);
        Task<Response<RegisterResponseDto>> Register(RegisterDto registerDto);
    }
}