using church_mgt_models;
using System.Threading.Tasks;

namespace church_mgt_core.Services.interfaces
{
    public interface ITokenGeneratorService
    {
        Task<string> GenerateToken(AppUser model);
    }
}