using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChemiClean.Core.Interface
{
    public interface IInsertRepository<T> where T : class //BaseEntity
    {
        #region Insert

        T Insert(T entity);

        Task<T> InsertAsync(T entity);

        void BulkInsert(List<T> entities);

        #endregion Insert
    }
}