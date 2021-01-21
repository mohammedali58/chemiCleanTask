using ChemiClean.Core.Interfaces;

namespace ChemiClean.Core.Interface
{
    public interface IRepository<T> : IInsertRepository<T>, IUpdateRepository<T>, IDeleteRepository<T>, IRetreiveRepository<T> where T : class //BaseEntity
    {
    }
}