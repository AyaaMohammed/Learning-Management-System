using System.Linq.Expressions;

namespace Application.Interfaces.Repositories
{
    public interface IGenericRepositoryAsync<T> where T : class
    {
        Task DeleteRangeAsync(ICollection<T> entities);

        Task<T?> GetByIdAsync(int id);

        Task<T> AddAsync(T entity);

        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate,params Expression<Func<T, object>>[] includes);

        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null,params Expression<Func<T, object>>[] includes);

        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        Task AddRangeAsync(ICollection<T> entities);

        Task UpdateAsync(T entity);

        Task UpdateRangeAsync(ICollection<T> entities);

        Task DeleteAsync(T entity);

        IQueryable<T> GetTableNoTracking();

        IQueryable<T> GetTableAsTracking();

        Task SaveChangesAsync();
    }
}