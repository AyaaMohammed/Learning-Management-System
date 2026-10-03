using System.Linq.Expressions;

namespace Application.Interfaces.Repositories
{
    /// <summary>
    /// Defines common asynchronous CRUD and query operations
    /// for an entity repository.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public interface IGenericRepositoryAsync<T> where T : class
    {
        /// <summary>
        /// Deletes multiple entities from the current DbContext.
        /// Changes are persisted when UnitOfWork.SaveChangesAsync() is called.
        /// </summary>
        Task DeleteRangeAsync(ICollection<T> entities);

        /// <summary>
        /// Retrieves an entity by its integer primary key.
        /// </summary>
        Task<T?> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new entity to the current DbContext.
        /// Changes are persisted when UnitOfWork.SaveChangesAsync() is called.
        /// </summary>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Retrieves the first entity that matches the specified predicate.
        /// Optional related entities can be loaded using includes.
        /// </summary>
        Task<T?> FirstOrDefaultAsync(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Retrieves all entities that match the specified predicate.
        /// If no predicate is provided, all entities are returned.
        /// Optional related entities can be loaded using includes.
        /// </summary>
        Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>>? predicate = null,
            params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Counts the number of entities that match the specified predicate.
        /// </summary>
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Adds multiple entities to the current DbContext.
        /// Changes are persisted when UnitOfWork.SaveChangesAsync() is called.
        /// </summary>
        Task AddRangeAsync(ICollection<T> entities);

        /// <summary>
        /// Updates an existing entity in the current DbContext.
        /// Changes are persisted when UnitOfWork.SaveChangesAsync() is called.
        /// </summary>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Updates multiple entities in the current DbContext.
        /// Changes are persisted when UnitOfWork.SaveChangesAsync() is called.
        /// </summary>
        Task UpdateRangeAsync(ICollection<T> entities);

        /// <summary>
        /// Deletes an entity from the current DbContext.
        /// Changes are persisted when UnitOfWork.SaveChangesAsync() is called.
        /// </summary>
        Task DeleteAsync(T entity);

        /// <summary>
        /// Returns the entity query as NoTracking.
        /// Suitable for read-only operations where change tracking is not required.
        /// </summary>
        IQueryable<T> GetTableNoTracking();

        /// <summary>
        /// Returns the entity query with change tracking enabled.
        /// Suitable when entities need to be modified and persisted.
        /// </summary>
        IQueryable<T> GetTableAsTracking();
    }
}