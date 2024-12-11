using System;
using System.Collections.Generic;
using System.Text;

namespace Vun.UnityUtils
{
    public static partial class CollectionUtils
    {
        /// <summary>
        /// Join items in <c>collection</c> into a <c>string</c>
        /// </summary>
        public static string JoinToString<T>(this ICollection<T> collection, string separator = ", ", string start = "[", string end = "]")
        {
            var builder = new StringBuilder();
            builder.Append(start);
            var i = 0;
            
            foreach (var item in collection)
            {
                builder.Append(item);

                if (i < collection.Count - 1)
                {
                    builder.Append(separator);
                }

                i++;
            }

            builder.Append(end);
            return builder.ToString();
        }

        /// <summary>
        /// Quickly remove item at <c>index</c> by move the last item in list to <c>index</c>
        /// </summary>
        public static void FastRemoveAt<T>(this IList<T> list, int index)
        {
            (list[index], list[^1]) = (list[^1], list[index]);
            list.RemoveAt(list.Count - 1);
        }
        
        /// <summary>
        /// Get a random element from <c>list</c>
        /// </summary>
        /// <returns>A random element from <c>list</c>. <c>default</c> if <c>list</c> is <c>null</c> or empty </returns>
        public static T RandomItem<T>(this IList<T> list)
        {
            return list.Count <= 0 ? default : list[UnityEngine.Random.Range(0, list.Count)];
        }

        /// <summary>
        /// Get a random element from <c>list</c>. A more complicated version of <see cref="RandomItem{T}"/> that avoids boxing
        /// </summary>
        /// <returns>A random element from <c>list</c>. <c>default</c> if <c>list</c> is <c>null</c> or empty </returns>
        public static T RandomItem<T, TList>(this TList list) where TList : IList<T>
        {
            return list.Count <= 0 ? default : list[UnityEngine.Random.Range(0, list.Count)];
        }

        /// <summary>
        /// Get a random element from <c>list</c>
        /// </summary>
        /// <returns>A random element from <c>list</c>. <c>default</c> if <c>list</c> is <c>null</c> or empty </returns>
        public static T RandomItem<T>(this IList<T> list, Random random)
        {
            return list.Count <= 0 ? default : list[random.Next(0, list.Count)];
        }

        /// <summary>
        /// Get a random element from <c>list</c>. A more complicated version of <see cref="RandomItem{T}"/> that avoids boxing
        /// </summary>
        /// <returns>A random element from <c>list</c>. <c>default</c> if <c>list</c> is <c>null</c> or empty </returns>
        public static T RandomItem<T, TList>(this TList list, Random random) where TList : IList<T>
        {
            return list.Count <= 0 ? default : list[random.Next(0, list.Count)];
        }
    }
}