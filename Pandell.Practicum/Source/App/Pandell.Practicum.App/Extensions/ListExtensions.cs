using System;
using System.Collections.Generic;
using System.Linq;
using Pandell.Practicum.App.Enumerations;

namespace Pandell.Practicum.App.Extensions
{
    public static class ListExtensions
    {
        public static bool IsNullOrEmpty(this List<int> listToCheck)
        {
            return listToCheck == null || !listToCheck.Any();
        }
        
        public static List<int> Shuffle(this List<int> enumerableToShuffle)
        {
            var random = new Random();
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

        public static List<string> Transform(this List<int> listToTransform)
        {
            var transformedRandomSequence = new List<string>();
            var numberOfBatches = listToTransform.NumberOfBatches((int) RandomSequenceCodes.MaxLineSequence);

            for (var i = 0; i < numberOfBatches; i++)
            {
                var generatedLine = listToTransform.Take((int) RandomSequenceCodes.MaxLineSequence);
                transformedRandomSequence.Add(generatedLine.ToFormattedBatch());
                listToTransform = RemoveRangeAlreadyAccountedFor(listToTransform, (int) RandomSequenceCodes.MaxLineSequence);
            }

            return transformedRandomSequence;
        }

        private static List<int> RemoveRangeAlreadyAccountedFor(List<int> listToTransform, int batchSize)
        {
            listToTransform.RemoveRange(0, 
                    listToTransform.Count < batchSize 
                        ? listToTransform.Count 
                        : batchSize);

            return listToTransform;
        }
    }
}