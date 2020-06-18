using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Pandell.Practicum.PerformanceTests.Enumerations;

namespace Pandell.Practicum.PerformanceTests.Domain
{
    [ExcludeFromCodeCoverage]
    public class RandomSequencePerformance
    {
        private Stopwatch stopWatch;

        public void Start(RandomSequenceMethodCodes randomSequenceMethod)
        {
            Initialize();
            
            RandomSequenceMethod = randomSequenceMethod;
            stopWatch.Start();
        }

        public void Stop()
        {
            stopWatch.Stop();
            ExecutionTime = stopWatch.Elapsed;
        }

        private void Initialize()
        {
            stopWatch = new Stopwatch();
            ExecutionTime = TimeSpan.Zero;
        }
        
        #region Class Members
        
        public RandomSequenceMethodCodes RandomSequenceMethod { get; private set; }
        public TimeSpan ExecutionTime { get; private set; }
        
        #endregion
    }
}