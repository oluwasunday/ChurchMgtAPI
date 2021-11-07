using church_mgt_models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace church_mgt_core.repositories.abstractions
{
    public interface IContactUsRepository : IRepository<ContactUs>
    {
        Task<IEnumerable<ContactUs>> GetAllAsync();
    }
}