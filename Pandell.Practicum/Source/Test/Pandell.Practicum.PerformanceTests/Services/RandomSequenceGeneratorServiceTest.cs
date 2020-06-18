using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Pandell.Practicum.App.Extensions;
using Pandell.Practicum.App.Services;
using Pandell.Practicum.PerformanceTests.Domain;
using Pandell.Practicum.PerformanceTests.Enumerations;
using Xunit;
using Xunit.Abstractions;

namespace Pandell.Practicum.PerformanceTests.Services
{
    [ExcludeFromCodeCoverage]
    public class RandomSequenceGeneratorServiceTest
    {
        private readonly ITestOutputHelper testOutputHelper;
        private readonly IRandomSequenceGeneratorService randomSequenceGeneratorService;

        public RandomSequenceGeneratorServiceTest(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
            randomSequenceGeneratorService = new RandomSequenceGeneratorService();
        }

        [Fact]
        public void GenerateRandomSequenceMethod_OnAllMethodsIdentified_DetermineWhichMethodIsFastest_OnLocalEnvironment()
        {
            var allMethodsSorted = AssertSortedExecuteAllGenerateRandomSequenceMethods();
            ConsoleOutputRandomSequenceMethodResults(allMethodsSorted);
        }

        [Fact]
        public void GenerateRandomSequenceMethod_OnAllMethodsIdentified_OverNumerousIterations_DetermineWhichMethodIsFastest_OnLocalEnvironment()
        {
            var topRandomSequencePerformances = GenerateTopRandomSequencePerformancesOverIterations();
            var topRatedRandomSequenceMethods = RateAllRandomSequenceMethodsOverMultipleIterations(topRandomSequencePerformances);
            ConsoleOutputTopRatedRandomSequenceMethodResults(topRatedRandomSequenceMethods);
        }

        [SuppressMessage("ReSharper", "CoVariantArrayConversion")]
        private List<Task<RandomSequencePerformance>> ExecuteAllGenerateRandomSequenceMethods()
        {
            var allGenerateRandomSequenceMethods = new List<Task<RandomSequencePerformance>>();
            allGenerateRandomSequenceMethods.Add(ExecuteFirstGenerateRandomSequenceMethodAsync());
            allGenerateRandomSequenceMethods.Add(ExecuteSecondGenerateRandomSequenceMethodAsync());
            allGenerateRandomSequenceMethods.Add(ExecuteThirdGenerateRandomSequenceMethodAsync());
            Task.WaitAll(allGenerateRandomSequenceMethods.ToArray());
            
            return allGenerateRandomSequenceMethods;
        }

        private async Task<RandomSequencePerformance> ExecuteFirstGenerateRandomSequenceMethodAsync()
        {
            return await ExecuteGenerateRandomSequenceMethodAsync(RandomSequenceMethodCodes.FirstRandomSequenceMethod,
                    () => randomSequenceGeneratorService.FirstGenerateRandomSequenceMethod())
                .ConfigureAwait(false);
        }

        private async Task<RandomSequencePerformance> ExecuteSecondGenerateRandomSequenceMethodAsync()
        {
            return await ExecuteGenerateRandomSequenceMethodAsync(RandomSequenceMethodCodes.SecondRandomSequenceMethod,
                    () => randomSequenceGeneratorService.SecondGenerateRandomSequenceMethod())
                .ConfigureAwait(false);
        }
        
        private async Task<RandomSequencePerformance> ExecuteThirdGenerateRandomSequenceMethodAsync()
        {
            return await ExecuteGenerateRandomSequenceMethodAsync(RandomSequenceMethodCodes.ThirdRandomSequenceMethod,
                    () => randomSequenceGeneratorService.ThirdGenerateRandomSequenceMethod())
                .ConfigureAwait(false);
        }
        
        private Task<RandomSequencePerformance> ExecuteGenerateRandomSequenceMethodAsync(
            RandomSequenceMethodCodes randomSequenceMethodCodes,
            Func<IEnumerable<int>> randomSequenceMethod)
        {
            return Task.Run(() =>
            {
                var randomSequencePerformance = new RandomSequencePerformance();
                randomSequencePerformance.Start(randomSequenceMethodCodes);

                randomSequenceMethod();
                randomSequencePerformance.Stop();

                return randomSequencePerformance;
            });
        }
        
        private List<RandomSequencePerformance> AssertSortedExecuteAllGenerateRandomSequenceMethods()
        {
            var allMethodsSorted = SortedExecuteAllGenerateRandomSequenceMethods();
            
            allMethodsSorted.Should()
                .NotBeEmpty()
                .And.NotContainNulls()
                .And.OnlyHaveUniqueItems(o => o.RandomSequenceMethod);
           
            return allMethodsSorted;
        }
        
        private List<RandomSequencePerformance> SortedExecuteAllGenerateRandomSequenceMethods()
        {
            var allGenerateRandomSequenceMethods = ExecuteAllGenerateRandomSequenceMethods();

            return allGenerateRandomSequenceMethods.Select(s => s.Result)
                .OrderBy(o => o.ExecutionTime.Ticks)
                .ToList();
        }
        
        private void ConsoleOutputRandomSequenceMethodResults(List<RandomSequencePerformance> allMethodsSorted)
        {
            testOutputHelper.WriteLine(TestOutputCodes.OutputHeaderFooter.ToDescription());
            allMethodsSorted.ForEach(method =>
                testOutputHelper.WriteLine($"** {method.RandomSequenceMethod.ToDescription()} - {method.ExecutionTime.Ticks} **"));
            testOutputHelper.WriteLine(TestOutputCodes.OutputHeaderFooter.ToDescription());
            
            GenerateSummaryOfRandomSequenceExecutions(allMethodsSorted);
        }

        private List<TopRandomSequencePerformance> GenerateTopRandomSequencePerformancesOverIterations()
        {
            var topRandomSequencePerformances = new List<TopRandomSequencePerformance>();

            for (var i = 0; i < (int) TestOutputCodes.TestingIterations; i++)
            {
                var sortedRandomSequenceMethods = AssertSortedExecuteAllGenerateRandomSequenceMethods();
                topRandomSequencePerformances.Add(
                    new TopRandomSequencePerformance(
                        sortedRandomSequenceMethods.First().RandomSequenceMethod, 
                        sortedRandomSequenceMethods.First().ExecutionTime.Ticks));
            }

            return topRandomSequencePerformances;
        }
        
        private List<TopRandomSequencePerformance> RateAllRandomSequenceMethodsOverMultipleIterations(List<TopRandomSequencePerformance> topRandomSequencePerformances)
        {
            var allMethodsEvaluated =
                (from randomSequenceMethod in EnumerationExtensions.ToEnumList<RandomSequenceMethodCodes>()
                    let totalExecutionTimeInTicks = topRandomSequencePerformances
                        .Where(w => w.RandomSequenceMethodCode == randomSequenceMethod)
                        .Sum(s => s.TotalExecutionTime)
                    select new TopRandomSequencePerformance(randomSequenceMethod, totalExecutionTimeInTicks)).ToList();

            return SortedTopRandomSequencePerformances(allMethodsEvaluated);
        }

        private static List<TopRandomSequencePerformance> SortedTopRandomSequencePerformances(List<TopRandomSequencePerformance> allMethodsEvaluated)
        {
            return allMethodsEvaluated
                .Where(w => w.TotalExecutionTime > 0)
                .OrderBy(o => o.TotalExecutionTime).ToList();
        }

        private void ConsoleOutputTopRatedRandomSequenceMethodResults(List<TopRandomSequencePerformance> topRatedRandomSequenceMethods)
        {
            testOutputHelper.WriteLine(TestOutputCodes.OutputHeaderFooter.ToDescription());
            topRatedRandomSequenceMethods.ForEach(method =>
                testOutputHelper.WriteLine($"** {method.RandomSequenceMethodCode.ToDescription()} - {method.TotalExecutionTime} **"));
            testOutputHelper.WriteLine(TestOutputCodes.OutputHeaderFooter.ToDescription());

            GenerateSummaryOfTopRatedRandomSequenceMethodResults(topRatedRandomSequenceMethods);
        }
        
        private void GenerateSummaryOfRandomSequenceExecutions(List<RandomSequencePerformance> allMethodsSorted)
        {
            testOutputHelper.WriteLine(TestOutputCodes.SummaryOutputHeaderFooter.ToDescription());
            testOutputHelper.WriteLine($"^^ The Winner of the Random Sequencing is: {allMethodsSorted.First().RandomSequenceMethod.ToDescription()} at {allMethodsSorted.First().ExecutionTime.Ticks} ^^");
            testOutputHelper.WriteLine(TestOutputCodes.SummaryOutputHeaderFooter.ToDescription());
        }

        private void GenerateSummaryOfTopRatedRandomSequenceMethodResults(List<TopRandomSequencePerformance> topRatedRandomSequenceMethods)
        {
            testOutputHelper.WriteLine(TestOutputCodes.SummaryOutputHeaderFooter.ToDescription());
            testOutputHelper.WriteLine($"^^ The Winner of the Iteration Execution of Random Sequencing is {topRatedRandomSequenceMethods.First().RandomSequenceMethodCode.ToDescription()} at a combined Execution Time of {topRatedRandomSequenceMethods.First().TotalExecutionTime} ^^");
            testOutputHelper.WriteLine(TestOutputCodes.SummaryOutputHeaderFooter.ToDescription());
        }
    }
}