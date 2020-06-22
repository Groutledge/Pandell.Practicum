using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Pandell.Practicum.App.Enumerations;
using Pandell.Practicum.App.Models;
using Pandell.Practicum.App.Services;
using Pandell.Practicum.App.Utility;
using Pandell.Practicum.FunctionalTests.Abstraction;
using Xunit;

namespace Pandell.Practicum.FunctionalTests.Services
{
    [ExcludeFromCodeCoverage]
    public class RandomSequenceServiceTest : AbstractInjectorInstantiation
    {
        private readonly IRandomSequenceService randomSequenceService;
        
        public RandomSequenceServiceTest()
        {
            randomSequenceService = Injector.Resolve<IRandomSequenceService>();
        }

        [Fact]
        public async Task GenerateRandomSequence_ReturnsARandomSequence_WithAGuidId()
        {
            var randomSequence = await randomSequenceService.GenerateRandomSequence().ConfigureAwait(false);
            
            randomSequence.Id.Should().NotBe(Guid.Empty);
            randomSequence.RandomSequence.Should()
                .NotBeEmpty()
                .And.OnlyHaveUniqueItems()
                .And.HaveCount((int) RandomSequenceCodes.MaxSequence)
                .And.NotContain(n => n > (int) RandomSequenceCodes.MaxSequence);
        }

        [Fact]
        public async Task Add_Update_Remove_Async_AddsARandomSequence_UpdatesTheRandomSequence_RemovesTheRandomSequence_FromTheDatabase()
        {
            var insertedRandomSequence = await AssertAddRandomSequenceAsync().ConfigureAwait(false);
            var updatedRandomSequence = await AssertUpdateRandomSequenceAsync(insertedRandomSequence).ConfigureAwait(false);
            await AssertRemoveRandomSequenceAsync(updatedRandomSequence).ConfigureAwait(false);
        }

        private async Task<RandomSequenceModel> AssertAddRandomSequenceAsync()
        {
            var randomSequence = await randomSequenceService.GenerateRandomSequence().ConfigureAwait(false);
            var insertedRandomSequence = await randomSequenceService.AddAsync(randomSequence).ConfigureAwait(false);
            AssertExpectedVsActualRandomSequences(randomSequence, insertedRandomSequence);
            
            return insertedRandomSequence;
        }

        private async Task<RandomSequenceModel> AssertUpdateRandomSequenceAsync(RandomSequenceModel randomSequenceModel)
        {
            var randomSequenceForUpdate = await UpdateRandomSequenceAsync(randomSequenceModel).ConfigureAwait(false);
            var updatedRandomSequence = await randomSequenceService.UpdateAsync(randomSequenceForUpdate).ConfigureAwait(false);
            AssertExpectedVsActualRandomSequences(randomSequenceForUpdate, updatedRandomSequence);

            return updatedRandomSequence;
        }
        
        private async Task<RandomSequenceModel> UpdateRandomSequenceAsync(RandomSequenceModel randomSequenceModel)
        {
            var newRandomSequence = await randomSequenceService.GenerateRandomSequence().ConfigureAwait(false);
            newRandomSequence.RandomSequence.SequenceEqual(randomSequenceModel.RandomSequence).Should().BeFalse();
            
            randomSequenceModel.RandomSequence = newRandomSequence.RandomSequence;
            return randomSequenceModel;
        }
        
        private async Task AssertRemoveRandomSequenceAsync(RandomSequenceModel randomSequenceModel)
        {
            var removedRandomSequence = await randomSequenceService.RemoveAsync(randomSequenceModel).ConfigureAwait(false);
            var modelRemoved = await randomSequenceService.GetByPrimaryKeyAsync(removedRandomSequence.Id).ConfigureAwait(false);
            modelRemoved.Should().BeNull();
        }
        
        private void AssertExpectedVsActualRandomSequences(RandomSequenceModel expectedRandomSequence, RandomSequenceModel actualRandomSequence)
        {
            actualRandomSequence.Id.Should().Be(expectedRandomSequence.Id);
            actualRandomSequence.RandomSequence.SequenceEqual(expectedRandomSequence.RandomSequence).Should().BeTrue();
        }
    }
}