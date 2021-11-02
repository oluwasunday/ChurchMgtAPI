using church_mgt_core.repositories.abstractions;
using church_mgt_database;
using church_mgt_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_core.repositories.implementations
{
    public class CommentRepository : Repositories<Comment>, ICommentRepository
    {
        private readonly ChurchDbContext _context;
        public CommentRepository(ChurchDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Comment> GetCommentByCustomerId(string memberId)
        {
            var member = _context.Comments.Where(x => x.Id == memberId).ToList();
            return member;
        }
    }
}
