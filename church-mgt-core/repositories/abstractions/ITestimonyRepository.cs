using church_mgt_models;
using System.Collections.Generic;

namespace church_mgt_core.repositories.abstractions
{
    public interface ITestimonyRepository : IRepository<Testimony>
    {
        //IEnumerable<Testimony> GetPrayerTestimoniesByAMember(string memberId);
        new IEnumerable<Testimony> GetAll();
    }
}