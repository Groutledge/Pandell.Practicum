using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using Pandell.Practicum.App.Enumerations;
using Pandell.Practicum.App.Services;
using Pandell.Practicum.UnitTests.Enumerations;
using Xunit;

namespace Pandell.Practicum.UnitTests.Services
{
    [ExcludeFromCodeCoverage]
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public class RandomSequenceGeneratorServiceTest
    {
        private readonly IRandomSequenceGeneratorService randomSequenceGeneratorService;

        public RandomSequenceGeneratorServiceTest()
        {
            randomSequenceGeneratorService = new RandomSequenceGeneratorService();
        }

        [Theory]
        [MemberData(nameof(RandomSequenceMethodTestCases))]
        public void GenerateRandomSequenceMethod_GeneratesARandomizedList_NoNumbersHaveBeenDuplicated(IEnumerable<int> randomSequences)
        {
            AssertOnlyUniqueItemsOnSequenceResults(randomSequences);
        }

        [Fact]
        public void FirstGenerateRandomSequenceMethod_OverANumberOfIterations_AlwaysReturnsAUniqueCollection()
        {
            ExecuteGenerateRandomSequenceMethodIterativeTestCase(() =>
                randomSequenceGeneratorService.FirstGenerateRandomSequenceMethod());
        }

        [Fact]
        public void SecondGenerateRandomSequenceMethod_OverANumberOfIterations_AlwaysReturnsAUniqueCollection()
        {
            ExecuteGenerateRandomSequenceMethodIterativeTestCase(() =>
                randomSequenceGeneratorService.SecondGenerateRandomSequenceMethod());
        }
        
        [Fact]
        public void ThirdGenerateRandomSequenceMethod_OverANumberOfIterations_AlwaysReturnsAUniqueCollection()
        {
            ExecuteGenerateRandomSequenceMethodIterativeTestCase(() =>
                randomSequenceGeneratorService.ThirdGenerateRandomSequenceMethod());
        }
        
        private void ExecuteGenerateRandomSequenceMethodIterativeTestCase(Func<IEnumerable<int>> randomSequenceMethod)
        {
            var randomMethodResults = IterativeRunRandomSequenceMethod(randomSequenceMethod);
            
            randomMethodResults.ForEach(randomMethodResult =>
            {
                AssertOnlyUniqueItemsOnSequenceResults(randomMethodResult);
                
                var numberOfSequenceMatches= CountAnyMatchingSequenceResultsWithinAllIterations(randomMethodResults, randomMethodResult);
                numberOfSequenceMatches.Should().BeLessOrEqualTo((int) TestParameterCodes.AllowableSequenceMatches);
            });
        }

        private List<IEnumerable<int>> IterativeRunRandomSequenceMethod(Func<IEnumerable<int>> randomSequenceMethod)
        {
            var randomMethodResults = new List<IEnumerable<int>>();
            
            for (var i = 0; i < (int) TestParameterCodes.IterationCount; i++)
            {
                var randomSequenceMethodResult = randomSequenceMethod();
                randomMethodResults.Add(randomSequenceMethodResult);
            }

            return randomMethodResults;
        }

        private void AssertOnlyUniqueItemsOnSequenceResults(IEnumerable<int> randomMethodResult)
        {
            randomMethodResult.Should()
                .NotBeEmpty()
                .And.OnlyHaveUniqueItems()
                .And.HaveCount((int) RandomSequenceCodes.MaxSequence)
                .And.NotContain(n => n > (int) RandomSequenceCodes.MaxSequence);
        }
        
        private int CountAnyMatchingSequenceResultsWithinAllIterations(List<IEnumerable<int>> randomMethodResults, 
            IEnumerable<int> randomMethodResult)
        {
            return randomMethodResults
                .Select(randomMethodResult.SequenceEqual)
                .Count(areSequencesEqual => areSequencesEqual);
        }
        
        #region Test Cases
        
        public static IEnumerable<object[]> RandomSequenceMethodTestCases => new List<object[]>
        {
            new object[] {RunFirstGenerateRandomSequenceMethod()},
            new object[] {RunSecondGenerateRandomSequenceMethod()},
            new object[] {RunThirdGenerateRandomSequenceMethod()}
        };
        
        private static IEnumerable<int> RunFirstGenerateRandomSequenceMethod()
        {
            return RunGenerateRandomSequenceMethod((randomService) =>
                randomService.FirstGenerateRandomSequenceMethod());
        }

        private static IEnumerable<int> RunSecondGenerateRandomSequenceMethod()
        {
            return RunGenerateRandomSequenceMethod((randomService) =>
                randomService.SecondGenerateRandomSequenceMethod());
        }
        
        private static IEnumerable<int> RunThirdGenerateRandomSequenceMethod()
        {
            return RunGenerateRandomSequenceMethod((randomService) =>
                randomService.ThirdGenerateRandomSequenceMethod());
        }

        private static IEnumerable<int> RunGenerateRandomSequenceMethod(Func<IRandomSequenceGeneratorService, 
            IEnumerable<int>> randomSequenceMethod)
        {
            var randomService = new RandomSequenceGeneratorService();
            return randomSequenceMethod(randomService);
        }
        
        #endregion
    }
}