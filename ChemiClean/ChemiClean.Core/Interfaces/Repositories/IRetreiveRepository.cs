using ChemiClean.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static ChemiClean.SharedKernel.SharedKernelEnums;

namespace ChemiClean.Core.Interface
{
    public interface IRetreiveRepository<T> where T : class //BaseEntity
    {
        #region Retreive

        #region GetById

        Task<T> GetById(int Id);

        #endregion GetById

        #region GetList

        Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "");

        IQueryable<T> GetWhere(Expression<Func<T, bool>> filter = null, string includeProperties = "");

        Task<List<T>> GetWhereAsync<TKey>(Expression<Func<T, bool>> filter = null, string includeProperties = "", Expression<Func<T, TKey>> sortingExpression = null, SortDirection sortDir = SortDirection.Ascending);

        #endregion GetList

        #region Get Paged

        Task<List<T>> GetPageAsync<TKey>(int PageNumeber, int PageSize, Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> sortingExpression, SharedKernelEnums.SortDirection sortDir = SharedKernelEnums.SortDirection.Ascending, string includeProperties = "");

        #endregion Get Paged

        #region Get Individuals

        Task<int> GetCountAsync(Expression<Func<T, bool>> filter = null);

        Task<bool> GetAnyAsync(Expression<Func<T, bool>> filter = null);

        bool GetAny(Expression<Func<T, bool>> filter = null);

        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "");

        Task<T> GetFirstOrDefaultAsyncIgnoreFilters(Expression<Func<T, bool>> filter = null, string includeProperties = "");

        Task<T> GetLastOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "");

        Task<T> GetMax(Expression<Func<T, bool>> filter = null, string includeProperties = "");

        Task<T> GetMin(Expression<Func<T, bool>> filter = null, string includeProperties = "");

        #endregion Get Individuals

        #endregion Retreive
    }
}