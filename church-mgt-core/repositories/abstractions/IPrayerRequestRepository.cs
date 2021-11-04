using church_mgt_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_core.repositories.abstractions
{
    public interface IPrayerRequestRepository : IRepository<PrayerRequest>
    {
        IEnumerable<PrayerRequest> GetPrayerRequestsByAMember(string memberId);
    }
}
