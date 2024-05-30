using Application.Utilities.Paging;
using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.ConcreteClasses
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext context;
        protected readonly DbSet<T> dbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            this.context = context;
            dbSet = this.context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task AddManyAsync(IEnumerable<T> entities)
        {
            await dbSet.AddRangeAsync(entities);
        }

        public async Task<T?> AddReturnEntityAsync(T entity)
        {
            EntityEntry<T> result = await dbSet.AddAsync(entity);
            return result.Entity;
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includedProperties = "", bool isTracking = false)
        {
            IQueryable<T> query = dbSet.AsQueryable();
            try
            {
                query = !isTracking ? query.AsNoTracking() : query.AsTracking();

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                query = includedProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

                if (orderBy != null)
                {
                    return await orderBy(query).ToListAsync();
                }
                else
                {
                    return await query.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when trying to query data for entity {nameof(T)}. " + ex.Message);
            }
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<PagedList<T>> GetPaginatedAsync(int pageNumber = 1, int pageSize = 20, Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includedProperties = "", bool isTracking = false)
        {
            IQueryable<T> query = dbSet.AsQueryable();
            try
            {
                query = !isTracking ? query.AsNoTracking() : query.AsTracking();

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                query = includedProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

                int totalCount = await query.CountAsync();

                if (orderBy != null)
                {
                    query = orderBy(query);
                }

                query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
                return new PagedList<T>(await query.ToListAsync(), totalCount, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when trying to query data with paging for entity {nameof(T)}. " + ex.Message);
            }
        }

        public async Task<bool> IsEntityExist(object id)
        {
            return await GetByIdAsync(id) != null;
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
    }
}
