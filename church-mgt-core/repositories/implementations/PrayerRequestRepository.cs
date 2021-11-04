using church_mgt_core.repositories.abstractions;
using church_mgt_database;
using church_mgt_models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_core.repositories.implementations
{
    public class PrayerRequestRepository : Repositories<PrayerRequest>, IPrayerRequestRepository
    {
        private readonly ChurchDbContext _context;
        private readonly DbSet<PrayerRequest> _dbSet;
        public PrayerRequestRepository(ChurchDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<PrayerRequest>();
        }

        public IEnumerable<PrayerRequest> GetPrayerRequestsByAMember(string memberId)
        {
            var request = _dbSet.Where(x => x.AppUserId == memberId).ToList();
            return request;
        }
    }
}
