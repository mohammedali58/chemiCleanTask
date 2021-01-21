using System.Collections.Generic;

namespace ChemiClean.Core.Interfaces
{
    public interface IUpdateRepository<T> where T : class //BaseEntity
    {
        #region Update

        public void Update(T entity);

        public void Update(List<T> entity);

        #endregion Update
    }
}