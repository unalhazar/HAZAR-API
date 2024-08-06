using Application.Contracts.Extensions;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
namespace Infrastructure.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : class
    {
        protected readonly HazarDbContext _dbContext;
        public RepositoryBase(HazarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task DeleteAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public async Task RemoveAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IReadOnlyList<T>> GetAllPagingAsync(Expression<Func<T, bool>> predicate, int number, int size)
        {
            if (predicate != null)
            {
                return await _dbContext.Set<T>().Where(predicate).Skip(number).Take(size).ToListAsync();
            }
            else
            {
                return await _dbContext.Set<T>().Skip(number).Take(size).ToListAsync();
            }

        }
        public void LazyLoadingChangeState(bool State)
        {
            _dbContext.ChangeTracker.LazyLoadingEnabled = State;
        }
        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AsNoTracking().Where(predicate).ToListAsync();
        }
        public async Task<IQueryable<T>> GetQuery()
        {
            return _dbContext.Set<T>();
        }
        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }
        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                query = orderBy(query);
            return await query.ToListAsync();
        }
        public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>> includes = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query.AsNoTracking();
        }
        public async Task<T> GetByIdAsync(long id, List<Expression<Func<T, object>>> includes = null)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);

            if (includes != null && entity != null)
            {
                foreach (var include in includes)
                {
                    await _dbContext.Entry(entity).Reference(include).LoadAsync();
                }
            }

            return entity;
        }
        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            _dbContext.ChangeTracker.Clear();
        }
        public async Task BulkAddAsync(List<T> entities)
        {

            _dbContext.Set<T>().AddRange(entities);
            await _dbContext.SaveChangesAsync();
        }
        public async Task BulkUpdateAsync(List<T> entities)
        {
            await _dbContext.BulkUpdateAsync(entities);
        }
        public async Task BulkDeleteAsync(List<T> entities)
        {
            await _dbContext.BulkUpdateAsync(entities);
        }
        public async Task BulkRemoveAsync(List<T> entities)
        {
            await _dbContext.BulkDeleteAsync(entities);
        }
        public async Task<T> GetByQueryAsync(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>> includes = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query.FirstOrDefault();
        }
        public async Task<IDbContextTransaction> BeginTransaction()
        {
            return await _dbContext.Database.BeginTransactionAsync();
        }

        public IQueryable<T> GetListByIncludes(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                query = includes(query);
            }

            return query;
        }


        public T GetByIncludes(Expression<Func<T, bool>> filter = null, Func<IIncludable<T>, IIncludable> includes = null)
        {
            var query = _dbContext.Set<T>().AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            if (includes != null)
                query = query.IncludeMultiple(includes);

            return query.FirstOrDefault();
        }
    }
}