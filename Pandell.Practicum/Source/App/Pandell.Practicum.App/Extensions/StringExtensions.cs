using System.Collections.Generic;

namespace Pandell.Practicum.App.Extensions
{
    public static class StringExtensions
    {
        public static string ToFormattedBatch(this IEnumerable<int> batchToFormat)
        {
            return string.Join("-", batchToFormat);
        }
    }
}