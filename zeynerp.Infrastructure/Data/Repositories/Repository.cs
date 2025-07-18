using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using zeynerp.Domain.Repositories;
using zeynerp.Infrastructure.Data.Contexts;

namespace zeynerp.Infrastructure.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly TenantDbContext _tenantDbContext;
        protected readonly DbSet<T> _dbSet;

        public Repository(TenantDbContext tenantDbContext)
        {
            _tenantDbContext = tenantDbContext;
            _dbSet = _tenantDbContext.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public virtual async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) => await _dbSet.Where(predicate).ToListAsync();

        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate) => await _dbSet.FirstOrDefaultAsync(predicate);

        public virtual async Task<T> AddAsync(T entity)
        {
            _dbSet.Add(entity);
            await _tenantDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _tenantDbContext.SaveChangesAsync();
            return entities;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _tenantDbContext.SaveChangesAsync();
            return entity;
        }        

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;

            _dbSet.Remove(entity);
            await _tenantDbContext.SaveChangesAsync();
            return true;
        }        
    }
}