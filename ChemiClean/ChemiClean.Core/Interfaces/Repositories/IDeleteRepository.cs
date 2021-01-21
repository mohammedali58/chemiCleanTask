using System;
using System.Linq.Expressions;

namespace ChemiClean.Core.Interface
{
    public interface IDeleteRepository<T> where T : class //BaseEntity
    {
        #region Delete

        void BulkHardDelete(Expression<Func<T, bool>> filter = null);

        void HardDelete(T entity);

        #endregion Delete
    }
}