using System.Collections.Generic;

namespace GameCore
{
    public static class ICollectionExtensions
    {
        public static void Add<T>(this ICollection<T> collection, params T[] values)
        {
            foreach (var item in values)
            {
                collection.Add(item);
            }
        }

        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> values)
        {
            foreach (var item in values)
            {
                collection.Add(item);
            }
        }
    }
}
