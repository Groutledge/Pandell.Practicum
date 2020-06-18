using System.ComponentModel;

namespace Pandell.Practicum.PerformanceTests.Enumerations
{
    public enum TestOutputCodes
    {
        [Description("**************************************")] OutputHeaderFooter = 0,
        [Description("^^^^^^^^^^^^^^^^^^^^^^^^^^^^")] SummaryOutputHeaderFooter = 1,
        [Description("Testing Iterations")] TestingIterations = 100
    }
}