using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnsureThat;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Pandell.Practicum.App.Data;
using Pandell.Practicum.App.Utility;

namespace Pandell.Practicum.App.Repository
{
    public abstract class AbstractRepository<TDomain, TId> : IRepository<TDomain, TId> where TDomain : class
    {
        protected AbstractRepository(IDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
        }
        
        public async Task<List<TDomain>> GetAllAsync()
        {
            return await ((ApplicationDbContext) ApplicationDbContext).Set<TDomain>()
                .ToListAsync()
                .ConfigureAwait(false);
        }
        
        protected async Task<TDomain> ExecuteAsync(TDomain domainToExecute, 
            Func<TDomain, EntityEntry<TDomain>> databaseExecutionFunction,
            string logMessage)
        {
            EnsureArg.IsNotNull(domainToExecute, nameof(domainToExecute));
            
            try
            {
                var entityResult = databaseExecutionFunction(domainToExecute);
                await ((ApplicationDbContext) ApplicationDbContext).SaveChangesAsync().ConfigureAwait(false);
                await ApplicationLogger.LogInformationAsync(logMessage);
                
                return entityResult.Entity;
            }
            catch (Exception exception)
            {
                await ApplicationLogger.LogExceptionAsync($"Error occured with executing the operation ${databaseExecutionFunction.Method.Name}", exception);
                return default;
            }
        }
        
        #region Abstract Methods
        
        public abstract Task<TDomain> AddAsync(TDomain domainToAdd);
        public abstract Task<TDomain> RemoveAsync(TDomain domainToRemove);
        public abstract Task<TDomain> UpdateAsync(TDomain domainToUpdate);
        public abstract Task<TDomain> GetByIdAsync(TId id);

        #endregion
        
        #region Class Members
        
        protected IDbContext ApplicationDbContext { get; }
        
        #endregion
    }
}