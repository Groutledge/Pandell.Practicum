using System;
using System.Collections.Generic;
using System.Linq;

namespace Pandell.Practicum.App.Extensions
{
    public static class ListExtensions
    {
        private static readonly Random random = new Random();

        public static IEnumerable<int> Shuffle(this IEnumerable<int> enumerableToShuffle)
        {
            var listToShuffle = enumerableToShuffle.ToList();
            var totalCount = listToShuffle.Count;
            
            while (totalCount > 1) 
            {  
                totalCount--;  
                var randomValue = random.Next(totalCount + 1);  
                var value = listToShuffle[randomValue];  
                listToShuffle[randomValue] = listToShuffle[totalCount];  
                listToShuffle[totalCount] = value;  
            }

            return listToShuffle;
        }
    }
}