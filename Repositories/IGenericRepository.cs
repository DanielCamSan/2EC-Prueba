using System.Linq.Expressions;

namespace TecWebFest.Api.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(object id);
        Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>>? predicate = null,
                                         Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                         string includeProperties = "");
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<int> SaveChangesAsync();
    }
}
