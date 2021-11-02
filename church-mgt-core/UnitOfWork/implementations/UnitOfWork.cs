using church_mgt_core.repositories.abstractions;
using church_mgt_core.repositories.implementations;
using church_mgt_core.UnitOfWork.interfaces;
using church_mgt_database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_core.UnitOfWork.implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICommentRepository Comment { get; private set; }
        private readonly ChurchDbContext _context;

        public UnitOfWork(ChurchDbContext context)
        {
            _context = context;
            Comment = new CommentRepository(_context);
        }


        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
