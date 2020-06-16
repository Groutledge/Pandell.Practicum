using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pandell.Practicum.App.Services
{
    public interface IService<TModel>
    {
        Task<List<TModel>> GetAllAsync();
        Task<TModel> AddAsync(TModel modelToAdd);
        Task<TModel> RemoveAsync(TModel modelToRemove);
        Task<TModel> UpdateAsync(TModel modelToUpdate);
        Task<TModel> GetByPrimaryKeyAsync(Guid id);
        Task<bool> DoesExistAsync(Guid id);
    }
}