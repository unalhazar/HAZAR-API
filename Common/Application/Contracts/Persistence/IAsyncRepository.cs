using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : class
    {

        void LazyLoadingChangeState(bool State);

        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, List<Expression<Func<T, object>>> includes = null);
        Task<IReadOnlyList<T>> GetAllPagingAsync(Expression<Func<T, bool>> predicate, int number, int size);

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        string includeString = null,
                                        bool disableTracking = true);

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                       Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                       List<Expression<Func<T, object>>> includes = null,
                                       bool disableTracking = true);
        Task<IQueryable<T>> GetQuery();
        Task<T> GetByIdAsync(long id, List<Expression<Func<T, object>>> includes = null);
        Task<T> GetByQueryAsync(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>> includes = null);

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task RemoveAsync(T entity);

        Task DeleteAsync(T entity);

        Task BulkAddAsync(List<T> entities);

        Task BulkUpdateAsync(List<T> entities);

        Task BulkDeleteAsync(List<T> entities);

        Task BulkRemoveAsync(List<T> entities);
        Task<IDbContextTransaction> BeginTransaction();

        IQueryable<T> GetListByIncludes(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);
        T GetByIncludes(Expression<Func<T, bool>> filter = null, Func<IIncludable<T>, IIncludable> includes = null);

    }
}