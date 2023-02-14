using System.Linq.Expressions;

namespace Cancun.Booking.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        Task UpdateAsync(TEntity entity);

        Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

        Task<List<TEntity>> GetAllAsync();

        Task<List<TEntity>> GetAllByAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[]? includes);

        void Delete(TEntity entity);
    }
}
