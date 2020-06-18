using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Pandell.Practicum.App.Services;
using Xunit;

namespace Pandell.Practicum.UnitTests.Services
{
    [ExcludeFromCodeCoverage]
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
            randomSequences.Should()
                .NotBeEmpty()
                .And.OnlyHaveUniqueItems();
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