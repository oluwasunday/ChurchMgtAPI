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
    public class ContactUsRepository : Repositories<ContactUs>, IContactUsRepository
    {
        private readonly ChurchDbContext _context;
        private readonly DbSet<ContactUs> _dbSet;
        public ContactUsRepository(ChurchDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<ContactUs>();
        }

        public async Task<IEnumerable<ContactUs>> GetAllAsync()
        {
            var contacts = await _dbSet.OrderByDescending(x => x.CreatedAt).ToListAsync();
            return contacts;
        }
    }
}
