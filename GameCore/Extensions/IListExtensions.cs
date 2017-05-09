using GameCore.Collision;
using System;
using System.Collections.Generic;

namespace GameCore.Extensions
{
    public static class IListExtensions
    {
        public static void Disable(this IList<EntityTexture> items)
        {
            foreach (EntityTexture item in items)
            {
                item.Disabled = true;
            }
        }

        public static void Disable(this IList<GameCollider> items)
        {
            foreach (GameCollider item in items)
            {
                item.Disabled = true;
            }
        }

        public static void ForEachCombination<T>(
            this IList<T> items,
            Action<T, T> callback)
        {
            for (int i = 0; i < items.Count - 1; i++)
            {
                for (int j = items.Count - 1; j > i; j--)
                {
                    callback(items[i], items[j]);
                }
            }
        }

        public static void ForEachCombination<T>(
            this IList<T> a,
            IList<T> b,
            Action<T, T> callback)
        {
            for (int i = 0; i < a.Count; i++)
            {
                for (int j = 0; j < b.Count; j++)
                {
                    callback(a[i], b[j]);
                }
            }
        }
    }
}
