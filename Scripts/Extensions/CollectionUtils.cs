using System;
using System.Collections.Generic;
using System.Text;
#if UNITY_COLLECTION_EXISTS
using Unity.Collections;
#endif

namespace Vun.UnityUtils
{
    public static partial class CollectionUtils
    {
        /// <summary>
        /// Join items in <c>collection</c> into a <c>string</c>
        /// </summary>
        public static string JoinToString<T>(this IEnumerable<T> collection, string separator = ", ", string start = "[", string end = "]")
        {
            var builder = new StringBuilder();
            builder.Append(start);
            using var enumerator = collection.GetEnumerator();

            if (enumerator.MoveNext())
            {
                builder.Append(enumerator.Current);

                while (enumerator.MoveNext())
                {
                    builder.Append(separator);
                    builder.Append(enumerator.Current);
                }
            }

            builder.Append(end);
            return builder.ToString();
        }

        /// <summary>
        /// Quickly remove item at <c>index</c> by move the last item in list to <c>index</c>
        /// </summary>
        public static void RemoveAtSwapBack<T>(this IList<T> list, int index)
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
            return list.RandomItem<T, IList<T>>();
        }

        /// <summary>
        /// Get a random element from <c>list</c>. A more complicated version of <see cref="RandomItem{T}(IList{T})"/> that avoids boxing
        /// </summary>
        /// <returns>A random element from <c>list</c>. <c>default</c> if <c>list</c> is <c>null</c> or empty </returns>
        public static T RandomItem<T, TList>(this TList list) where TList : IList<T>
        {
            return list is { Count: > 0 } ? list[UnityEngine.Random.Range(0, list.Count)] : default;
        }

        /// <summary>
        /// Get a random element from <c>list</c>
        /// </summary>
        /// <returns>A random element from <c>list</c>. <c>default</c> if <c>list</c> is <c>null</c> or empty </returns>
        public static T RandomItem<T>(this IList<T> list, Random random)
        {
            return list.RandomItem<T, IList<T>>(random);
        }

        /// <summary>
        /// Get a random element from <c>list</c>. A more complicated version of <see cref="RandomItem{T}"/> that avoids boxing
        /// </summary>
        /// <returns>A random element from <c>list</c>. <c>default</c> if <c>list</c> is <c>null</c> or empty </returns>
        public static T RandomItem<T, TList>(this TList list, Random random) where TList : IList<T>
        {
            return list is { Count: > 0 } ? list[random.Next(0, list.Count)] : default;
        }

#if UNITY_COLLECTION_EXISTS
        /// <summary>
        /// Get a random element from <c>list</c>.
        /// Will not change the <see cref="Unity.Mathematics.Random.state"/> of <c>random</c>
        /// </summary>
        /// <returns>A random element from <c>list</c>. <c>default</c> if <c>list</c> is <c>null</c> or empty </returns>
        public static T RandomItem<T>(this IList<T> list, Unity.Mathematics.Random random)
        {
            return list.RandomItem<T, IList<T>>(random);
        }

        /// <summary>
        /// Get a random element from <c>list</c>.
        /// Will change the <see cref="Unity.Mathematics.Random.state"/> of <c>random</c>
        /// </summary>
        /// <returns>A random element from <c>list</c>. <c>default</c> if <c>list</c> is <c>null</c> or empty </returns>
        public static T RandomItem<T>(this IList<T> list, ref Unity.Mathematics.Random random)
        {
            return list.RandomItem<T, IList<T>>(ref random);
        }

        /// <summary>
        /// Get a random element from <c>list</c>.
        /// Will not change the <see cref="Unity.Mathematics.Random.state"/> of <c>random</c>
        /// </summary>
        /// <returns>A random element from <c>list</c>. <c>default</c> if <c>list</c> is <c>null</c> or empty </returns>
        public static T RandomItem<T, TList>(this TList list, Unity.Mathematics.Random random) where TList : IList<T>
        {
            return list is { Count: > 0 } ? list[random.NextInt(0, list.Count)] : default;
        }

        /// <summary>
        /// Get a random element from <c>list</c>.
        /// Will change the <see cref="Unity.Mathematics.Random.state"/> of <c>random</c>
        /// </summary>
        /// <returns>A random element from <c>list</c>. <c>default</c> if <c>list</c> is <c>null</c> or empty </returns>
        public static T RandomItem<T, TList>(this TList list, ref Unity.Mathematics.Random random) where TList : IList<T>
        {
            return list is { Count: > 0 } ? list[random.NextInt(0, list.Count)] : default;
        }
#endif
    }
}