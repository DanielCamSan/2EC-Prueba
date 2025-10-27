using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TecWebFest.Api.Data;
using TecWebFest.Api.Repositories.Interfaces;

namespace TecWebFest.Api.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _ctx;
        protected readonly DbSet<T> _db;

        public GenericRepository(AppDbContext ctx)
        {
            _ctx = ctx;
            _db = _ctx.Set<T>();
        }

        public async Task<T?> GetByIdAsync(object id) => await _db.FindAsync(id);

        public async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = _db;
            if (predicate != null) query = query.Where(predicate);
            foreach (var includeProp in includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProp.Trim());
            if (orderBy != null) query = orderBy(query);
            return await query.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _db.AddAsync(entity);
        }

        public Task UpdateAsync(T entity)
        {
            _db.Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            _db.Remove(entity);
            return Task.CompletedTask;
        }

        public Task<int> SaveChangesAsync() => _ctx.SaveChangesAsync();
    }
}
