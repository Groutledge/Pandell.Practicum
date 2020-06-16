using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using Pandell.Practicum.App.Domain;
using Pandell.Practicum.App.Extensions;
using Pandell.Practicum.App.Utility;

namespace Pandell.Practicum.FunctionalTests.TestCases
{
    [ExcludeFromCodeCoverage]
    public class RandomSequenceRepositoryTestCase : AbstractTestCase<RandomSequence>
    {
        public override RandomSequence GenerateTestCaseMember()
        {
            return new RandomSequence
            {
                Id = Guid.NewGuid(),
                GeneratedSequence = RandomNumbers.ToJsonObject(),
                DateInserted = Clock.UtcNow(),
                LastModifiedBy = Environment.UserName
            };
        }

        public override void AssertExpectedVsActualValues(RandomSequence expectedResult, RandomSequence actualResult)
        {
            AssertGeneralExpectedVsActualValues(expectedResult, actualResult);
            actualResult.GeneratedSequence.Should().BeEquivalentTo(expectedResult.GeneratedSequence);
        }

        public override void AssertUpdatedExpectedVsActualValues(RandomSequence expectedResult, RandomSequence actualResult)
        {
            AssertGeneralExpectedVsActualValues(expectedResult, actualResult);
            actualResult.GeneratedSequence.Should().NotBeEquivalentTo(expectedResult.GeneratedSequence);
            actualResult.DateUpdated.Should().NotBeNull();
        }

        public override void AssertPositiveCaseOnListOfResults(List<RandomSequence> expectedResults, List<RandomSequence> actualResults)
        {
            actualResults.Should()
                .NotBeEmpty()
                .And.OnlyHaveUniqueItems(o => o.Id);

            actualResults.Select(s => s.Id)
                .Should()
                .BeEquivalentTo(expectedResults.Select(s => s.Id))
                .And.HaveCount(expectedResults.Count);
        }

        private void AssertGeneralExpectedVsActualValues(RandomSequence expectedResult, RandomSequence actualResult)
        {
            actualResult.Id.Should().Be(expectedResult.Id);
            actualResult.DateInserted.Should().Be(expectedResult.DateInserted);
            actualResult.LastModifiedBy.Should().Be(expectedResult.LastModifiedBy);
        }
        
        #region Class Members
        
        private List<int> RandomNumbers => new List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
        
        #endregion
    }
}