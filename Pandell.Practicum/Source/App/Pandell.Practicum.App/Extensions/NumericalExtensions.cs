using System;
using System.Collections.Generic;
using Castle.Core.Internal;

namespace Pandell.Practicum.App.Extensions
{
    public static class NumericalExtensions
    {
        public static int NumberOfBatches<T>(this List<T> listToGenerateBatchCountFrom, int batchSize)
        {
            return listToGenerateBatchCountFrom.IsNullOrEmpty()
                ? 0
                : (int) Math.Ceiling(listToGenerateBatchCountFrom.Count / (double) batchSize);
        }
    }
}