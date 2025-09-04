using System;
using System.Collections.Generic;

namespace Vun.UnityUtils
{
    public partial class CollectionUtils
    {
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

#if UNITY_MATH_EXISTS
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