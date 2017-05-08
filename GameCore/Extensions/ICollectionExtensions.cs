using System.Collections.Generic;

namespace GameCore.Extensions
{
    public static class ICollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> collection, params T[] values)
        {
            foreach (var item in values)
            {
                collection.Add(item);
            }
        }
    }
}
