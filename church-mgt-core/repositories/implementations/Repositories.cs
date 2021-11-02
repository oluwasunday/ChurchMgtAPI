using church_mgt_core.repositories.abstractions;
using church_mgt_database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_core.repositories.implementations

{
    public class Repositories<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ChurchDbContext _dbContext;
        public Repositories(ChurchDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public virtual async Task<TEntity> GetAsync(string id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public virtual void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }
    }
}
