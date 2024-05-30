using Application.Utilities.Paging;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(object id);
        Task AddAsync(T entity);
        Task<T?> AddReturnEntityAsync(T entity);
        Task AddManyAsync(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> IsEntityExist(object id);
        Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includedProperties = "",
            bool isTracking = false);
        Task<PagedList<T>> GetPaginatedAsync(
            int pageNumber = 1,
            int pageSize = 20,
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includedProperties = "",
            bool isTracking = false);
    }
}
