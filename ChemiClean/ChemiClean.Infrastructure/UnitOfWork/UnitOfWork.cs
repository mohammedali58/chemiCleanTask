using ChemiClean.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace ChemiClean.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Defines the Context
        /// </summary>
        public ChemicleanContext _context;

        public IDbContextTransaction transactionScope;
        public IHttpContextAccessor HttpContext { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="ElectricityCorrespondenceContext"/></param>
        public UnitOfWork(ChemicleanContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            HttpContext = httpContext;
        }

        /// <summary>
        /// Commit changes
        /// </summary>
        public async Task<int> Commit()
        {
            try
            {
                int status = await _context.SaveChangesAsync();
                return status;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Commit changes and throw error if commit failed
        /// </summary>
        /// <param name="_culture">Culture to throw the exception message with.</param
        public async Task<int> Commit(string _culture)
        {
            try
            {
                int status = await _context.SaveChangesAsync();
                return status;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task Transaction()
        {
            transactionScope = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransaction()
        {
            try
            {
                await _context.SaveChangesAsync();
                await transactionScope.CommitAsync();
            }
            catch (Exception)
            {
                await transactionScope.RollbackAsync();
                throw;
            }
            finally
            {
                await transactionScope.DisposeAsync();
            }
        }
    }
}