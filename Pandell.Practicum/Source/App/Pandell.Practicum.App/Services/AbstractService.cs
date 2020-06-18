using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AutoMapper;
using Pandell.Practicum.App.Repository;

namespace Pandell.Practicum.App.Services
{
    public abstract class AbstractService<TDomain, TModel> : IService<TModel>
    {
        [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
        protected AbstractService(IRepository<TDomain, Guid> repository, IIdService idService)
        {
            Repository = repository;
            IdService = idService;
            
            var mapperConfiguration = GenerateMapperConfiguration();
            Mapper = mapperConfiguration.CreateMapper();
        }
        
        protected async Task<TModel> ExecuteOperation(TDomain itemForExecuteOperation, 
            Func<TDomain, TDomain> itemPropertyUpdates,
            Func<TDomain, Task<TDomain>> daoOperation)
        {
            itemForExecuteOperation = itemPropertyUpdates(itemForExecuteOperation);
            var itemTransacted = await daoOperation(itemForExecuteOperation).ConfigureAwait(false);
            return await GetByDomainAsync(itemTransacted).ConfigureAwait(false);
        }
        
        #region Abstract Methods

        protected abstract MapperConfiguration GenerateMapperConfiguration();
        public abstract Task<List<TModel>> GetAllAsync();
        public abstract Task<TModel> AddAsync(TModel modelToAdd);
        public abstract Task<TModel> RemoveAsync(TModel modelToRemove);
        public abstract Task<TModel> UpdateAsync(TModel modelToUpdate);
        public abstract Task<TModel> GetByPrimaryKeyAsync(Guid id);
        public abstract Task<bool> DoesExistAsync(Guid id);
        protected abstract Task<TModel> GetByDomainAsync(TDomain itemToFind);
        
        #endregion
        
        #region Class Members
        
        protected IRepository<TDomain, Guid> Repository { get; }
        protected IIdService IdService { get; }
        protected IMapper Mapper { get; }
        
        #endregion    
    }
}