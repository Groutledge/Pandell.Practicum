using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Pandell.Practicum.App.Enumerations;
using Pandell.Practicum.App.Extensions;
using Xunit;

namespace Pandell.Practicum.UnitTests.Extensions
{
    [ExcludeFromCodeCoverage]
    public class ListExtensionsTest
    {
        private readonly List<int> randomSequences;

        public ListExtensionsTest()
        {
            randomSequences = new List<int>();
            GenerateSequence();
        }
        
        [Fact]
        public void Transform_CreatesBatchesFromAList_ReturnsFormattedBatches()
        {
            var numberOfBatches = randomSequences.NumberOfBatches((int) RandomSequenceCodes.MaxLineSequence);
            
            var transformedRandomSequences = randomSequences.Transform();
            
            transformedRandomSequences.Should()
                .NotBeEmpty()
                .And.HaveCount(numberOfBatches)
                .And.OnlyHaveUniqueItems();
        }

        private void GenerateSequence()
        {
            for (var i = 0; i < (int) RandomSequenceCodes.MaxSequence; i++)
                randomSequences.Add(i);
        }
    }
}