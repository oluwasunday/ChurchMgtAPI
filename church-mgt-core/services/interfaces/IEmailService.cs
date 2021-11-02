using church_mgt_dtos.AuthenticationDtos;
using church_mgt_dtos.Dtos;
using System.Threading.Tasks;

namespace church_mgt_core.services.interfaces
{
    public interface IEmailService
    {
        Task<Response<string>> SendEmailAsync(MailRequestDto requestDto);
    }
}