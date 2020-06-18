using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using MoreLinq;
using Pandell.Practicum.App.Enumerations;
using Pandell.Practicum.App.Extensions;

namespace Pandell.Practicum.App.Services
{
    public interface IRandomSequenceGeneratorService
    {
        IEnumerable<int> FirstGenerateRandomSequenceMethod();
        IEnumerable<int> SecondGenerateRandomSequenceMethod();
        IEnumerable<int> ThirdGenerateRandomSequenceMethod();
    }
    
    public class RandomSequenceGeneratorService : IRandomSequenceGeneratorService
    {
        private readonly Random random;

        public RandomSequenceGeneratorService()
        {
            random = new Random();
        }
        
        [SuppressMessage("ReSharper", "EmptyEmbeddedStatement")]
        public IEnumerable<int> FirstGenerateRandomSequenceMethod()
        {
            var randomNumbers = new HashSet<int>();
            
            for (var i = 0; i < (int) RandomSequenceCodes.MaxSequence; i++)
                while (!randomNumbers.Add(random.Next()));

            return randomNumbers;
        }

        public IEnumerable<int> SecondGenerateRandomSequenceMethod()
        {
            var randomNumbers = GenerateEnumerableIntegerRange();
            return MoreEnumerable.Shuffle(randomNumbers);
        }

        public IEnumerable<int> ThirdGenerateRandomSequenceMethod()
        {
            var randomNumbers = GenerateEnumerableIntegerRange();
            return randomNumbers.Shuffle();
        }

        private IEnumerable<int> GenerateEnumerableIntegerRange()
        {
            return Enumerable.Range(0, (int) RandomSequenceCodes.MaxSequence);
        }
    }
}