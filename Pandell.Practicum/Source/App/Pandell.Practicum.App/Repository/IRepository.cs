using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pandell.Practicum.App.Repository
{
    public interface IRepository<TDomain, in TId>
    {
        Task<List<TDomain>> GetAllAsync();
        Task<TDomain> GetByIdAsync(TId id);
        Task<TDomain> AddAsync(TDomain domainToAdd);
        Task<TDomain> RemoveAsync(TDomain domainToRemove);
        Task<TDomain> UpdateAsync(TDomain domainToUpdate);
    }
}