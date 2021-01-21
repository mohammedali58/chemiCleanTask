using System.Threading.Tasks;

namespace ChemiClean.Core.Interfaces
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Commit changes
        /// </summary>
        Task<int> Commit();

        /// <summary>
        /// Commit changes and throw error if commit failed
        /// </summary>
        /// <param name="_culture">Culture to throw the exception message with.</param>
        Task<int> Commit(string _culture);

        Task Transaction();

        Task CommitTransaction();
    }
}