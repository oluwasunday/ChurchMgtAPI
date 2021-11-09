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
    public class TestimonyRepository : Repositories<Testimony>, ITestimonyRepository
    {
        private readonly ChurchDbContext _context;
        private readonly DbSet<Testimony> _dbSet;
        public TestimonyRepository(ChurchDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Testimony>();
        }

        public override IEnumerable<Testimony> GetAll()
        {
            return _dbSet.OrderByDescending(x => x.CreatedAt);
        }
    }
}
