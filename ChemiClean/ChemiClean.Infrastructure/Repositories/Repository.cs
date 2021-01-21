using ChemiClean.Core.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static ChemiClean.SharedKernel.SharedKernelEnums;

namespace ChemiClean.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ChemicleanContext context;

        /// <summary>
        /// Defines the entities
        /// </summary>
        private DbSet<T> entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="Context"/></param>
        public Repository(ChemicleanContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        #region Insert

        public T Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            return entities.Add(entity).Entity;
        }

        public async Task<T> InsertAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            return (await entities.AddAsync(entity)).Entity;
        }

        public void BulkInsert(List<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");
            // entities.AddRange(entities);
            context.AddRangeAsync(entities);
        }

        #endregion Insert

        #region Update

        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            entities.Update(entity);
        }

        public void Update(List<T> entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            #region Check Related Data Before Deleting

            entities.UpdateRange(entity);

            #endregion Check Related Data Before Deleting
        }

        #endregion Update

        #region Delete

        public void BulkHardDelete(Expression<Func<T, bool>> filter = null)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");
            entities.RemoveRange(entities.Where(filter));
        }

        public void HardDelete(T entity)
        {
            entities.Remove(entity);
        }

        #endregion Delete

        #region Retreive

        #region GetById

        public async Task<T> GetById(int Id)
        {
            T record = await entities.FindAsync(Id);
            return record;
        }

        #endregion GetById

        #region GetList

        public async Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = entities;

            var isIncludeStringExist = !string.IsNullOrWhiteSpace(includeProperties);
            var isFilterExist = filter != null;

            switch (isIncludeStringExist, isFilterExist)
            {
                case (true, true):
                    query = query.Where(filter);
                    query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
                    return await query.AsNoTracking().ToListAsync();

                case (true, false):
                    query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
                    return await query.AsNoTracking().ToListAsync();

                case (false, true):
                    return await query.Where(filter).AsNoTracking().ToListAsync();

                default:
                    return await query.AsNoTracking().ToListAsync();
            }

        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = entities;
            if (filter != null)
            {
                query = query.Where(filter);
                query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
                return query.AsNoTracking();
            }
            else
            {
                return query.AsNoTracking();
            }
        }

        public async Task<List<T>> GetWhereAsync<TKey>(Expression<Func<T, bool>> filter = null, string includeProperties = "", Expression<Func<T, TKey>> sortingExpression = null, SortDirection sortDir = SortDirection.Ascending)
        {
            IQueryable<T> query = entities;
            if (filter != null)
                query = query.Where(filter);
            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            if (sortingExpression != null)
            {
                switch (sortDir)
                {
                    case SortDirection.Ascending:
                        query = query.OrderBy<T, TKey>(sortingExpression); break;
                    case SortDirection.Descending:
                        query = query.OrderByDescending<T, TKey>(sortingExpression); break;
                }
            }
            return await query.AsNoTracking().ToListAsync();
        }

        #endregion GetList

        #region Get Paged

        public async Task<List<T>> GetPageAsync<TKey>(int PageNumeber, int PageSize, Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> sortingExpression, SortDirection sortDir = SortDirection.Ascending, string includeProperties = "")
        {
            IQueryable<T> query = context.Set<T>();
            int skipCount = (PageNumeber - 1) * PageSize;

            if (filter != null)
                query = query.Where(filter);

            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            switch (sortDir)
            {
                case SortDirection.Ascending:
                    if (skipCount == 0)
                        query = query.OrderBy<T, TKey>(sortingExpression).Take(PageSize);
                    else
                        query = query.OrderBy<T, TKey>(sortingExpression).Skip(skipCount).Take(PageSize);
                    break;

                case SortDirection.Descending:
                    if (skipCount == 0)
                        query = query.OrderByDescending<T, TKey>(sortingExpression).Take(PageSize);
                    else
                        query = query.OrderByDescending<T, TKey>(sortingExpression).Skip(skipCount).Take(PageSize);
                    break;

                default:
                    break;
            }
            return await query.AsNoTracking().ToListAsync();
        }

        #endregion Get Paged

        #region Get Individuals

        public async Task<int> GetCountAsync(Expression<Func<T, bool>> filter = null)
        {
            return await entities.CountAsync(filter);
        }

        public async Task<bool> GetAnyAsync(Expression<Func<T, bool>> filter = null)
        {
            return await entities.AnyAsync(filter);
        }

        public bool GetAny(Expression<Func<T, bool>> filter = null)
        {
            return entities.Any(filter);
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = context.Set<T>();

            var isIncludeStringExist = !string.IsNullOrWhiteSpace(includeProperties);
            var isFilterExist = filter != null;

            switch (isIncludeStringExist, isFilterExist)
            {
                case (true, true):
                    query = query.Where(filter);
                    query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
                    await query.AsNoTracking().ToListAsync();
                    break;

                case (true, false):
                    query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
                    await query.AsNoTracking().ToListAsync();
                    break;

                case (false, true):
                    await query.Where(filter).AsNoTracking().ToListAsync();
                    break;

                default:
                    await query.AsNoTracking().ToListAsync();
                    break;
            }

            T record = await query.FirstOrDefaultAsync();
            if (record != default)
                context.Entry(record).State = EntityState.Detached;
            return record;
        }

        public async Task<T> GetFirstOrDefaultAsyncIgnoreFilters(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = context.Set<T>().IgnoreQueryFilters();
            if (filter != null)
                query = query.Where(filter).AsNoTracking();
            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty).IgnoreQueryFilters());

            T record = await query.FirstOrDefaultAsync();
            if (record != default)
                context.Entry(record).State = EntityState.Detached;
            return record;
        }

        public async Task<T> GetLastOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = context.Set<T>();
            if (filter != null)
                query = query.Where(filter).AsNoTracking();
            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            T record = await query.OrderByDescending(item => item).FirstOrDefaultAsync();
            if (record != default)
                context.Entry(record).State = EntityState.Detached;
            return record;
        }

        public async Task<T> GetMax(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = context.Set<T>();
            if (filter != null)
                query = query.Where(filter).AsNoTracking();
            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            T record = await query.MaxAsync();
            if (record != default)
                context.Entry(record).State = EntityState.Detached;
            return record;
        }

        public async Task<T> GetMin(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = context.Set<T>();
            if (filter != null)
                query = query.Where(filter).AsNoTracking();
            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            T record = await query.MinAsync();
            if (record != default)
                context.Entry(record).State = EntityState.Detached;
            return record;
        }

        #endregion Get Individuals

        #endregion Retreive
    }
}