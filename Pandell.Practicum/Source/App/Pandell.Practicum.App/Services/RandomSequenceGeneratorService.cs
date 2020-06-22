using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using Pandell.Practicum.App.Enumerations;
using Pandell.Practicum.App.Extensions;

namespace Pandell.Practicum.App.Services
{
    public interface IRandomSequenceGeneratorService
    {
        List<int> FirstGenerateRandomSequenceMethod();
        List<int> SecondGenerateRandomSequenceMethod();
        List<int> ThirdGenerateRandomSequenceMethod();
    }
    
    public class RandomSequenceGeneratorService : IRandomSequenceGeneratorService
    {
        public List<int> FirstGenerateRandomSequenceMethod()
        {
            var random = new Random();
            var randomNumbers = new HashSet<int>();
            
            for (var i = 0; i < (int) RandomSequenceCodes.MaxSequence; i++)
                while (!randomNumbers.Add(random.Next((int) RandomSequenceCodes.MaxSequence)));

            return randomNumbers.ToList();
        }

        public List<int> SecondGenerateRandomSequenceMethod()
        {
            var randomNumbers = GenerateEnumerableIntegerRange();
            return MoreEnumerable.Shuffle(randomNumbers).ToList();
        }

        public List<int> ThirdGenerateRandomSequenceMethod()
        {
            var randomNumbers = GenerateEnumerableIntegerRange();
            return randomNumbers.Shuffle();
        }

        private List<int> GenerateEnumerableIntegerRange()
        {
            return Enumerable.Range(0, (int) RandomSequenceCodes.MaxSequence).ToList();
        }
    }
}