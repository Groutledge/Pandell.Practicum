using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pandell.Practicum.App.Domain;
using Pandell.Practicum.App.Models;
using Pandell.Practicum.App.Repository;

namespace Pandell.Practicum.App.Services
{
    public class RandomSequenceService : AbstractService<RandomSequence, RandomSequenceModel>
    {
        public RandomSequenceService(IRepository<RandomSequence, Guid> repository, IIdService idService) : base(repository, idService)
        {
        }

        public override Task<List<RandomSequenceModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public override Task<RandomSequenceModel> AddAsync(RandomSequenceModel modelToAdd)
        {
            throw new NotImplementedException();
        }

        public override Task<RandomSequenceModel> RemoveAsync(RandomSequenceModel modelToRemove)
        {
            throw new NotImplementedException();
        }

        public override Task<RandomSequenceModel> UpdateAsync(RandomSequenceModel modelToUpdate)
        {
            throw new NotImplementedException();
        }

        public override Task<RandomSequenceModel> GetByPrimaryKeyAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> DoesExistAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}