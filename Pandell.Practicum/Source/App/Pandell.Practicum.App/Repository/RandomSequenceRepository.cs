using System;
using System.Threading.Tasks;
using Pandell.Practicum.App.Data;
using Pandell.Practicum.App.Domain;

namespace Pandell.Practicum.App.Repository
{
    public class RandomSequenceRepository : AbstractRepository<RandomSequence, Guid>
    {
        public RandomSequenceRepository(IDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public override async Task<RandomSequence> GetByIdAsync(Guid id)
        {
            return await ApplicationDbContext
                .RandomSequences
                .FindAsync(id)
                .ConfigureAwait(false);
        }

        public override async Task<RandomSequence> AddAsync(RandomSequence domainToAdd)
        {
            return await ExecuteAsync(domainToAdd, 
                    domain => ApplicationDbContext.RandomSequences.Add(domain),
                    $"Successfully added the Random Sequence {domainToAdd.Id}")
                .ConfigureAwait(false);
        }

        public override async Task<RandomSequence> RemoveAsync(RandomSequence domainToRemove)
        {
            return await ExecuteAsync(domainToRemove, 
                    domain => ApplicationDbContext.RandomSequences.Remove(domain),
                    $"Successfully removed the Random Sequence {domainToRemove.Id}")
                .ConfigureAwait(false);
        }

        public override async Task<RandomSequence> UpdateAsync(RandomSequence domainToUpdate)
        {
            return await ExecuteAsync(domainToUpdate, 
                    domain => ApplicationDbContext.RandomSequences.Update(domain),
                    $"Successfully updated the Random Sequence {domainToUpdate.Id}")
                .ConfigureAwait(false);
        }
    }
}