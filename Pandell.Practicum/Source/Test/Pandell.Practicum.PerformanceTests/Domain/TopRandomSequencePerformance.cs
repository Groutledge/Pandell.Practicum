using System.Diagnostics.CodeAnalysis;
using Pandell.Practicum.PerformanceTests.Enumerations;

namespace Pandell.Practicum.PerformanceTests.Domain
{
    [ExcludeFromCodeCoverage]
    public class TopRandomSequencePerformance
    {
        public TopRandomSequencePerformance(RandomSequenceMethodCodes randomSequenceMethodCode, long totalExecutionTime)
        {
            RandomSequenceMethodCode = randomSequenceMethodCode;
            TotalExecutionTime = totalExecutionTime;
        }
        
        #region Class Members
        
        public RandomSequenceMethodCodes RandomSequenceMethodCode { get; }
        public long TotalExecutionTime { get; }
        
        #endregion    
    }
}