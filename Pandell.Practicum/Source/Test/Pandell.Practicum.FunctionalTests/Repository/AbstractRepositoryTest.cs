using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FluentAssertions;
using Pandell.Practicum.App.Repository;
using Pandell.Practicum.FunctionalTests.Abstraction;
using Pandell.Practicum.FunctionalTests.TestCases;

namespace Pandell.Practicum.FunctionalTests.Repository
{
    [ExcludeFromCodeCoverage]
    public abstract class AbstractRepositoryTest<TDomain> : AbstractInjectorInstantiation
    {
        [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
        protected AbstractRepositoryTest()
        {
            HydrateRepositoryAndTestCase();;
        }
        
        protected async Task GetAllAsyncTestCase()
        {
            var domainsToInsert = GenerateListOfDomains();
            var expectedResults = AssertInsertMultipleDomains(domainsToInsert);

            var actualResults = await Repository.GetAllAsync().ConfigureAwait(false);
            RepositoryTestCase.AssertPositiveCaseOnListOfResults(expectedResults, actualResults);
            
            await AssertAllDomainsAreRemoved(expectedResults);
        }

        protected async Task CrudAsyncTestCase()
        {
            var expectedDomain = RepositoryTestCase.GenerateTestCaseMember();
            var insertedDomain = await AssertInsertedDomainAsync(expectedDomain).ConfigureAwait(false);
            var updatedDomain = await AssertUpdatedDomainAsync(insertedDomain).ConfigureAwait(false);

            await AssertRemoveDomainAsync(updatedDomain).ConfigureAwait(false);
        }

        private async Task<TDomain> AssertInsertedDomainAsync(TDomain expectedDomain)
        {
            var insertedDomain = await Repository.AddAsync(expectedDomain).ConfigureAwait(false);
            RepositoryTestCase.AssertExpectedVsActualValues(expectedDomain, insertedDomain);
            
            var foundDomain = await AssertFindDomainByIdAsync(insertedDomain);
            return foundDomain;
        }
        
        private async Task<TDomain> AssertFindDomainByIdAsync(TDomain domainToFind)
        {
            var foundDomain = await FindDomainByIdAsync(domainToFind).ConfigureAwait(false);
            RepositoryTestCase.AssertPositiveCaseAndExpectedVsActualValues(foundDomain, domainToFind);
            return foundDomain;
        }
        
        private async Task<TDomain> AssertUpdatedDomainAsync(TDomain insertedDomain)
        {
            var foundDomain = await FindDomainByIdAsync(insertedDomain).ConfigureAwait(false);
            RepositoryTestCase.AssertPositiveCaseOnResult(foundDomain);
            
            var expectedDomain = Clone(foundDomain);
            var updatePropertyOnDomain = UpdatePropertyOnDomain(foundDomain);
            var updatedDomain = await Repository.UpdateAsync(updatePropertyOnDomain).ConfigureAwait(false);
            RepositoryTestCase.AssertUpdatedExpectedVsActualValues(expectedDomain, updatedDomain);
            
            return updatedDomain;
        }
        
        private async Task AssertRemoveDomainAsync(TDomain domainToRemove)
        {
            var removedDomain = await Repository.RemoveAsync(domainToRemove).ConfigureAwait(false);
            RepositoryTestCase.AssertExpectedVsActualValues(domainToRemove, removedDomain);

            var foundCategory = await FindDomainByIdAsync(domainToRemove).ConfigureAwait(false);
            RepositoryTestCase.AssertNegativeCaseOnResult(foundCategory);
        }
        
        private List<TDomain> GenerateListOfDomains()
        {
            return new List<TDomain>
            {
                RepositoryTestCase.GenerateTestCaseMember(),
                RepositoryTestCase.GenerateTestCaseMember(),
                RepositoryTestCase.GenerateTestCaseMember()
            };
        }
        
        private List<TDomain> AssertInsertMultipleDomains(List<TDomain> domainsToInsert)
        {
            var expectedResults = new List<TDomain>();

            domainsToInsert.ForEach(expectedResult =>
            {
                var insertedDomain = AssertInsertedDomainAsync(expectedResult);
                Task.WaitAll(insertedDomain);

                expectedResults.Add(insertedDomain.Result);
            });
            
            return expectedResults;
        }

        private async Task AssertAllDomainsAreRemoved(List<TDomain> actualResults)
        {
            AssertRemoveMultipleDomains(actualResults);
            actualResults = await Repository.GetAllAsync().ConfigureAwait(false);
            actualResults.Should().BeEmpty();
        }
        
        private void AssertRemoveMultipleDomains(List<TDomain> domainsToRemove)
        {
            domainsToRemove.ForEach(domainToRemove =>
            {
                var removedDomainTask = Repository.RemoveAsync(domainToRemove);
                Task.WaitAll(removedDomainTask);
            });
        }
        
        #region Abstract Members

        protected abstract void HydrateRepositoryAndTestCase();
        protected abstract Task<TDomain> FindDomainByIdAsync(TDomain domainToFind);
        protected abstract TDomain UpdatePropertyOnDomain(TDomain domainToUpdate);
        protected abstract TDomain Clone(TDomain domainToClone);
        
        #endregion
        
        #region Class Members
        
        protected IRepository<TDomain, Guid> Repository { get; set; }
        protected ITestCase<TDomain> RepositoryTestCase { get; set; }

        #endregion    
    }
}