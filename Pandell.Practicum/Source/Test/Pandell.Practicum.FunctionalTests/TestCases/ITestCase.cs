using System.Collections.Generic;

namespace Pandell.Practicum.FunctionalTests.TestCases
{
    public interface ITestCase<TDomain>
    {
        TDomain GenerateTestCaseMember();
        void AssertPositiveCaseAndExpectedVsActualValues(TDomain actual, TDomain expected);
        void AssertNegativeCaseOnResult(TDomain result);
        void AssertExpectedVsActualValues(TDomain expectedResult, TDomain actualResult);
        void AssertUpdatedExpectedVsActualValues(TDomain expectedResult, TDomain actualResult);
        void AssertPositiveCaseOnListOfResults(List<TDomain> expectedResults, List<TDomain> actualResults);
        void AssertPositiveCaseOnResult(TDomain result);
    }
}