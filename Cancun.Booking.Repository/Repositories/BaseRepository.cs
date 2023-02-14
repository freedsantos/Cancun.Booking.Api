using Cancun.Booking.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cancun.Booking.Repository.Repositories
{
    public class BaseRepository<TContext, TEntity> : IRepository<TEntity> where TContext : DbContext where TEntity : class
    {
        protected readonly DbSet<TEntity> _dbSet;
        protected readonly DbContext _context;

        public BaseRepository(TContext db)
        {
            _dbSet = db.Set<TEntity>();
            _context = db;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Entry(entity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTrackingWithIdentityResolution().ToListAsync();
        }

        public Task<List<TEntity>> GetAllByAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[]? includes)
        {
            var query = _dbSet.AsNoTrackingWithIdentityResolution().Where(predicate);

            if (includes is null)
                return query.ToListAsync();
            else
                return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).ToListAsync();
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
    }
}
