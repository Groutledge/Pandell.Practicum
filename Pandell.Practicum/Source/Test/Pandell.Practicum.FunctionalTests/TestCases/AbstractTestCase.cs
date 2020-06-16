using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;

namespace Pandell.Practicum.FunctionalTests.TestCases
{
    [ExcludeFromCodeCoverage]
    public abstract class AbstractTestCase<TDomain> : ITestCase<TDomain>
    {
        public void AssertNegativeCaseOnResult(TDomain result)
        {
            result.Should().BeNull();
        }

        public void AssertPositiveCaseAndExpectedVsActualValues(TDomain actual, TDomain expected)
        {
            AssertPositiveCaseOnResult(actual);
            AssertExpectedVsActualValues(expected, actual);
        }
        
        public void AssertPositiveCaseOnResult(TDomain result)
        {
            result.Should()
                .NotBeNull()
                .And.BeAssignableTo<TDomain>();
        }
        
        #region Abstract Methods
        
        public abstract TDomain GenerateTestCaseMember();
        public abstract void AssertExpectedVsActualValues(TDomain expectedResult, TDomain actualResult);
        public abstract void AssertUpdatedExpectedVsActualValues(TDomain expectedResult, TDomain actualResult);
        public abstract void AssertPositiveCaseOnListOfResults(List<TDomain> expectedResults, List<TDomain> actualResults);
        
        #endregion
    }
}