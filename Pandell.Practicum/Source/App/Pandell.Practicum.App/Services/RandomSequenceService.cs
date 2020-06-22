using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Pandell.Practicum.App.Domain;
using Pandell.Practicum.App.Extensions;
using Pandell.Practicum.App.Map;
using Pandell.Practicum.App.Models;
using Pandell.Practicum.App.Repository;
using Pandell.Practicum.App.Utility;
using static Pandell.Practicum.App.Utility.ApplicationLogger;

namespace Pandell.Practicum.App.Services
{
    public interface IRandomSequenceService : IService<RandomSequenceModel>
    {
        Task<RandomSequenceModel> GenerateRandomSequence();
    }
    
    public class RandomSequenceService : AbstractService<RandomSequence, RandomSequenceModel>, IRandomSequenceService
    {
        private readonly IRandomSequenceGeneratorService randomSequenceGeneratorService;
        
        public RandomSequenceService(IRepository<RandomSequence, Guid> repository, 
            IIdService idService,
            IRandomSequenceGeneratorService randomSequenceGeneratorService) : base(repository, idService)
        {
            this.randomSequenceGeneratorService = randomSequenceGeneratorService;
        }

        protected override MapperConfiguration GenerateMapperConfiguration()
        {
            return new RandomSequenceMapperConfiguration().Configure();
        }

        public override async Task<List<RandomSequenceModel>> GetAllAsync()
        {
            var allSequences = await Repository.GetAllAsync().ConfigureAwait(false);
            return Mapper.Map<List<RandomSequence>, List<RandomSequenceModel>>(allSequences);
        }

        public override async Task<RandomSequenceModel> AddAsync(RandomSequenceModel modelToAdd)
        {
            var domainToAdd = Mapper.Map<RandomSequenceModel, RandomSequence>(modelToAdd);
            
            return await ExecuteOperation(domainToAdd,
                    item =>
                    {
                        item.Id = item.Id.IsEmptyGuid() ? IdService.GenerateId() : item.Id;
                        item.DateInserted = Clock.UtcNow();
                        item.LastModifiedBy = Environment.UserName;
                        return item;
                    },
                    item => Repository.AddAsync(item))
                .ConfigureAwait(false);
        }

        public override async Task<RandomSequenceModel> RemoveAsync(RandomSequenceModel modelToRemove)
        {
            var foundItemToRemove = await Repository.GetByIdAsync(modelToRemove.Id).ConfigureAwait(false);
            await Repository.RemoveAsync(foundItemToRemove).ConfigureAwait(false);
            return modelToRemove;
        }

        public override async Task<RandomSequenceModel> UpdateAsync(RandomSequenceModel modelToUpdate)
        {
            var convertedToDomain = Mapper.Map<RandomSequenceModel, RandomSequence>(modelToUpdate);
            var foundDomain = await Repository.GetByIdAsync(modelToUpdate.Id).ConfigureAwait(false);
            foundDomain = Mapper.Map(convertedToDomain, foundDomain);
                
            return await ExecuteOperation(foundDomain,
                    item =>
                    {
                        item.DateUpdated = Clock.UtcNow();
                        item.LastModifiedBy = Environment.UserName;
                        return item;
                    }, item => Repository.UpdateAsync(item))
                .ConfigureAwait(false);
        }

        public override async Task<RandomSequenceModel> GetByPrimaryKeyAsync(Guid id)
        {
            var sequenceItem = await Repository.GetByIdAsync(id).ConfigureAwait(false);
            return Mapper.Map<RandomSequence, RandomSequenceModel>(sequenceItem);
        }

        public override async Task<bool> DoesExistAsync(Guid id)
        {
            var foundSequence = await Repository.GetByIdAsync(id).ConfigureAwait(false);
            return foundSequence != null;
        }

        public Task<RandomSequenceModel> GenerateRandomSequence()
        {
            return Task.Run(async () =>
            {
                try
                {
                    return new RandomSequenceModel
                    {
                        Id = IdService.GenerateId(),
                        RandomSequence = randomSequenceGeneratorService.ThirdGenerateRandomSequenceMethod(),
                    };
                }
                catch (Exception exception)
                {
                    await LogExceptionAsync(exception.Message, exception);
                    return default;
                }
            });
        }

        protected override async Task<RandomSequenceModel> GetByDomainAsync(RandomSequence itemToFind)
        {
            return await GetByPrimaryKeyAsync(itemToFind.Id).ConfigureAwait(false);
        }
    }
}