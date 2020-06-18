using System.ComponentModel;

namespace Pandell.Practicum.UnitTests.Enumerations
{
    public enum TestParameterCodes
    {
        [Description("Allowable Sequence Matches")] AllowableSequenceMatches = 1,
        [Description("Iteration Count")] IterationCount = 50,
        [Description("[1,2,3,4,5,6,7,8,9,10]")] IntegerJsonString = 2
    }
}