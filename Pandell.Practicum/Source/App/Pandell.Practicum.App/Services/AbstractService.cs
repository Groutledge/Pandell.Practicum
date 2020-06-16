using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pandell.Practicum.App.Repository;

namespace Pandell.Practicum.App.Services
{
    public abstract class AbstractService<TDomain, TModel> : IService<TModel>
    {
        protected AbstractService(IRepository<TDomain, Guid> repository, IIdService idService)
        {
            Repository = repository;
            IdService = idService;
        }
        
        #region Abstract Methods

        public abstract Task<List<TModel>> GetAllAsync();
        public abstract Task<TModel> AddAsync(TModel modelToAdd);
        public abstract Task<TModel> RemoveAsync(TModel modelToRemove);
        public abstract Task<TModel> UpdateAsync(TModel modelToUpdate);
        public abstract Task<TModel> GetByPrimaryKeyAsync(Guid id);
        public abstract Task<bool> DoesExistAsync(Guid id);

        #endregion
        
        #region Class Members
        
        protected IRepository<TDomain, Guid> Repository { get; }
        protected IIdService IdService { get; }
        
        #endregion    
    }
}