using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Pool;

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

        public static T[] ToArray<T>(this ICollection<T> collection, int arrayIndex = 0)
        {
            var array = new T[collection.Count];
            collection.CopyTo(array, arrayIndex);
            return array;
        }

        /// <summary>
        /// Add <c>default</c> value or remove items from the end of <c>list</c>,
        /// until <see cref="IList.Count"/> is equal to <c>length</c>
        /// </summary>
        /// <exception cref="ArgumentException">When <c>list</c> is read-only</exception>
        /// <exception cref="ArgumentOutOfRangeException">When <c>length</c> is less than 0</exception>
        public static void SetLength<T>(this IList<T> list, int length)
        {
#if DEBUG
            if (list.IsReadOnly)
            {
                throw new ArgumentException("Cannot set the length of a read-only list.");
            }

            if (length <= 0)
            {
                throw new ArgumentOutOfRangeException($"Invalid length: {length}");
            }
#endif

            while (list.Count < length)
            {
                list.Add(default);
            }

            while (list.Count > length)
            {
                list.RemoveAt(list.Count - 1);
            }
        }

        /// <summary>
        /// Move non-<c>default</c> (non-<c>null</c> for reference type) values to the start of <c>items</c>
        /// </summary>
        public static void ShiftValuesToTop<T>(this IList<T> items, Comparison<T> comparison)
        {
            var firstEmptyIndex = -1;

            for (var i = 0; i < items.Count; i++)
            {
                if (comparison(items[i], default) == 0)
                {
                    if (firstEmptyIndex == -1)
                    {
                        firstEmptyIndex = i;
                    }

                    continue;
                }

                if (firstEmptyIndex < 0)
                {
                    continue;
                }

                items.Swap(i, firstEmptyIndex);
                firstEmptyIndex++;
            }
        }

        /// <summary>
        /// Move non-<c>default</c> (non-<c>null</c> for reference type) values to the start of <c>items</c>
        /// </summary>
        public static void ShiftValuesToTop<T>(this IList<T> items) where T : IEquatable<T>
        {
            var firstEmptyIndex = -1;

            for (var i = 0; i < items.Count; i++)
            {
                if (items[i].Equals(default))
                {
                    if (firstEmptyIndex == -1)
                    {
                        firstEmptyIndex = i;
                    }

                    continue;
                }

                if (firstEmptyIndex < 0)
                {
                    continue;
                }

                items.Swap(i, firstEmptyIndex);
                firstEmptyIndex++;
            }
        }

        public static void Swap<T>(this IList<T> list, int indexA, int indexB)
        {
            (list[indexA], list[indexB]) = (list[indexB], list[indexA]);
        }

        #region Remove nulls

        /// <summary>
        /// Remove destroyed <see cref="UnityEngine.Object"/> from <c>collection</c>
        /// </summary>
        /// <returns>The number of objects removed</returns>
        public static int RemoveDestroyedObjects<T>(this ICollection<T> collection) where T : UnityEngine.Object
        {
            var buffer = ListPool<T>.Get();
            var removeCount = collection.RemoveDestroyedObjects(buffer);
            ListPool<T>.Release(buffer);
            return removeCount;
        }

        /// <summary>
        /// Remove destroyed <see cref="UnityEngine.Object"/> from <c>collection</c>
        /// </summary>
        /// <param name="buffer">Buffer for the removed objects</param>
        /// <returns>The number of objects removed</returns>
        public static int RemoveDestroyedObjects<T>(this ICollection<T> collection, ICollection<T> buffer) where T : UnityEngine.Object
        {
            buffer.Clear();

            foreach (var value in collection)
            {
                if (value == null)
                {
                    buffer.Add(value);
                }
            }

            foreach (var value in buffer)
            {
                collection.Remove(value);
            }

            return buffer.Count;
        }

        /// <summary>
        /// Remove null values from <c>dictionary</c>
        /// </summary>
        /// <returns>The number of items removed</returns>
        public static int RemoveNullValues<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            var buffer = ListPool<TKey>.Get();
            var removeCount = dictionary.RemoveNullValues(buffer);
            ListPool<TKey>.Release(buffer);
            return removeCount;
        }

        /// <summary>
        /// Remove null values from <c>dictionary</c>
        /// </summary>
        /// <param name="buffer">Buffer for the removed keys</param>
        /// <returns>The number of items removed</returns>
        public static int RemoveNullValues<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, ICollection<TKey> buffer)
        {
            buffer.Clear();

            foreach (var (key, value) in dictionary)
            {
                if (value == null)
                {
                    buffer.Add(key);
                }
            }

            foreach (var key in buffer)
            {
                dictionary.Remove(key);
            }

            return buffer.Count;
        }

        /// <summary>
        /// Remove destroyed <see cref="UnityEngine.Object"/> from <c>dictionary</c> <see cref="Dictionary.Keys"/>
        /// </summary>
        /// <returns>The number of items removed</returns>
        public static int RemoveDestroyedKeys<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) where TKey : UnityEngine.Object
        {
            var buffer = ListPool<TKey>.Get();
            var removeCount = dictionary.RemoveDestroyedKeys(buffer);
            ListPool<TKey>.Release(buffer);
            return removeCount;
        }

        /// <summary>
        /// Remove destroyed <see cref="UnityEngine.Object"/> from <c>dictionary</c> <see cref="Dictionary.Keys"/>
        /// </summary>
        /// <param name="buffer">Buffer for the removed objects</param>
        /// <returns>The number of items removed</returns>
        public static int RemoveDestroyedKeys<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, ICollection<TKey> buffer) where TKey : UnityEngine.Object
        {
            buffer.Clear();

            foreach (var key in dictionary.Keys)
            {
                if (key == null)
                {
                    buffer.Add(key);
                }
            }

            foreach (var key in buffer)
            {
                dictionary.Remove(key);
            }

            return buffer.Count;
        }

        #endregion
    }
}