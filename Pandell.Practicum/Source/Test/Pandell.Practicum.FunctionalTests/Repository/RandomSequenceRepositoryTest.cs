using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Pandell.Practicum.App.Domain;
using Pandell.Practicum.App.Extensions;
using Pandell.Practicum.App.Repository;
using Pandell.Practicum.App.Utility;
using Pandell.Practicum.FunctionalTests.TestCases;
using Xunit;

namespace Pandell.Practicum.FunctionalTests.Repository
{
    [ExcludeFromCodeCoverage]
    public class RandomSequenceRepositoryTest : AbstractRepositoryTest<RandomSequence>
    {
        [Fact]
        public async Task GetAllAsync_ReturnsAllRandomSequences_FromTheDatabase()
        {
            await GetAllAsyncTestCase();
        }
        
        [Fact]
        public async Task Add_Update_Remove_Async_AddsARandomSequence_ToTheDatabase_UpdatesTheNumericalSequence_SavesToTheDatabase_ThenRemovesTheInsertedRandomSequence()
        {
            await CrudAsyncTestCase();
        }
        
        protected override void HydrateRepositoryAndTestCase()
        {
            Repository = Injector.Resolve<IRepository<RandomSequence, Guid>>();
            RepositoryTestCase = new RandomSequenceRepositoryTestCase();
        }

        protected override async Task<RandomSequence> FindDomainByIdAsync(RandomSequence domainToFind)
        {
            return await Repository.GetByIdAsync(domainToFind.Id).ConfigureAwait(false);
        }

        protected override RandomSequence UpdatePropertyOnDomain(RandomSequence domainToUpdate)
        {
            domainToUpdate.GeneratedSequence = UpdatedRandomSequence.ToJsonObject();
            domainToUpdate.DateUpdated = Clock.UtcNow();
            return domainToUpdate;
        }

        protected override RandomSequence Clone(RandomSequence domainToClone)
        {
            return (RandomSequence) domainToClone.Clone();
        }
        
        #region Class Members
        
        private List<int> UpdatedRandomSequence => new List<int> {11, 12, 13, 14, 15, 16, 17, 18, 19, 20};
        
        #endregion
    }
}