using System;
using System.Collections.Generic;
using System.Text;

namespace Vun.UnityUtils
{
    public static class CollectionUtils
    {
        #region ICollection buffer and UnityEngine.Random

        /// <summary>
        /// Quickly sample <c>sampleCount</c> items from <c>collection</c> and put them into <c>buffer</c>.
        /// This function have O(n) runtime and will not allocated extra memory
        /// </summary>
        /// <param name="collection">The sample collection</param>
        /// <param name="sampleCount">The number of sample item needed</param>
        /// <param name="buffer">Container for sample items. <c>Clear()</c> will be called for this</param>
        public static void QuickSample<T>(this ICollection<T> collection, int sampleCount, ICollection<T> buffer)
        {
            if (sampleCount > collection.Count)
            {
                throw new ArgumentException($"The number of sample ({sampleCount}) is bigger than collection size ({collection.Count})");
            }

            buffer.Clear();
            var i = 0;

            foreach (var item in collection)
            {
                var takeChance = (float)(sampleCount - buffer.Count) / (collection.Count - i);

                if (UnityEngine.Random.Range(0f, 1f) <= takeChance)
                {
                    buffer.Add(item);
                }

                if (buffer.Count >= sampleCount)
                {
                    break;
                }

                ++i;
            }
        }

        /// <summary>
        /// Optimized version of <c>QuickSample</c> for <c>List</c> (avoid <c>GetEnumerator</c>)
        /// </summary>
        public static void QuickSample<T>(this List<T> list, int sampleCount, ICollection<T> buffer)
        {
            if (sampleCount > list.Count)
            {
                throw new ArgumentException($"The number of sample ({sampleCount}) is bigger than list size ({list.Count})");
            }

            buffer.Clear();

            for (int i = 0; i < list.Count; ++i)
            {
                var takeChance = (float)(sampleCount - buffer.Count) / (list.Count - i);

                if (UnityEngine.Random.Range(0f, 1f) <= takeChance)
                {
                    buffer.Add(list[i]);
                }

                if (buffer.Count >= sampleCount)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Optimized version of <c>QuickSample</c> for arrays (avoid <c>GetEnumerator</c>)
        /// </summary>
        public static void QuickSample<T>(this T[] array, int sampleCount, ICollection<T> buffer)
        {
            if (sampleCount > array.Length)
            {
                throw new ArgumentException($"The number of sample ({sampleCount}) is bigger than array size ({array.Length})");
            }

            buffer.Clear();

            for (int i = 0; i < array.Length - sampleCount + buffer.Count; ++i)
            {
                var takeChance = (float)(sampleCount - buffer.Count) / (array.Length - i);

                if (UnityEngine.Random.Range(0f, 1f) <= takeChance)
                {
                    buffer.Add(array[i]);
                }

                if (buffer.Count >= sampleCount)
                {
                    break;
                }
            }
        }
        
        #endregion

        #region Array buffer and UnityEngine.Random
        
        /// <summary>
        /// Quickly sample <c>sampleCount</c> items from <c>collection</c> and put them into <c>buffer</c>.
        /// This function have O(n) runtime and will not allocated extra memory
        /// </summary>
        /// <param name="collection">The sample collection</param>
        /// <param name="sampleCount">The number of sample item needed</param>
        /// <param name="buffer">Container for sample items. The buffer will be overwritten starting from <c>writeIndex</c></param>
        /// <param name="writeIndex">The index to start writing in <c>buffer</c></param>
        public static void QuickSample<T>(this ICollection<T> collection, int sampleCount, T[] buffer, int writeIndex = 0)
        {
            if (sampleCount > collection.Count)
            {
                throw new ArgumentException($"The number of sample ({sampleCount}) is bigger than collection size ({collection.Count})");
            }

            if (writeIndex + sampleCount > buffer.Length)
            {
                throw new ArgumentException($"The buffer length ({buffer.Length}) is too short for {sampleCount} samples from index {writeIndex}");
            }

            var i = 0;
            var writeCount = 0;

            foreach (var item in collection)
            {
                var takeChance = (float)(sampleCount - writeCount) / (collection.Count - i);

                if (UnityEngine.Random.Range(0f, 1f) <= takeChance)
                {
                    buffer[writeIndex + writeCount] = item;
                    writeCount++;
                }

                if (writeCount >= sampleCount)
                {
                    break;
                }

                ++i;
            }
        }

        /// <summary>
        /// Optimized version of <c>QuickSample</c> for <c>List</c> (avoid <c>GetEnumerator</c>)
        /// </summary>
        public static void QuickSample<T>(this List<T> list, int sampleCount, T[] buffer, int writeIndex = 0)
        {
            if (sampleCount > list.Count)
            {
                throw new ArgumentException($"The number of sample ({sampleCount}) is bigger than collection size ({list.Count})");
            }

            if (writeIndex + sampleCount > buffer.Length)
            {
                throw new ArgumentException($"The buffer length ({buffer.Length}) is too short for {sampleCount} samples from index {writeIndex}");
            }

            var writeCount = 0;

            for (int i = 0; i < list.Count; i++)
            {
                var takeChance = (float)(sampleCount - writeCount) / (list.Count - i);

                if (UnityEngine.Random.Range(0f, 1f) <= takeChance)
                {
                    buffer[writeIndex + writeCount] = list[i];
                    writeCount++;
                }

                if (writeCount >= sampleCount)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Optimized version of <c>QuickSample</c> for <c>arrays</c> (avoid <c>GetEnumerator</c>)
        /// </summary>
        public static void QuickSample<T>(this T[] array, int sampleCount, T[] buffer, int writeIndex = 0)
        {
            if (sampleCount > array.Length)
            {
                throw new ArgumentException($"The number of sample ({sampleCount}) is bigger than collection size ({array.Length})");
            }

            if (writeIndex + sampleCount > buffer.Length)
            {
                throw new ArgumentException($"The buffer length ({buffer.Length}) is too short for {sampleCount} samples from index {writeIndex}");
            }

            var writeCount = 0;

            for (int i = 0; i < array.Length; i++)
            {
                var takeChance = (float)(sampleCount - writeCount) / (array.Length - i);

                if (UnityEngine.Random.Range(0f, 1f) <= takeChance)
                {
                    buffer[writeIndex + writeCount] = array[i];
                    writeCount++;
                }

                if (writeCount >= sampleCount)
                {
                    break;
                }
            }
        }
        
        #endregion

        #region ICollection buffer and System.Random
        
        /// <summary>
        /// Quickly sample <c>sampleCount</c> items from <c>collection</c> and put them into <c>buffer</c>.
        /// This function have O(n) runtime and will not allocated extra memory
        /// </summary>
        /// <param name="collection">The sample collection</param>
        /// <param name="sampleCount">The number of sample item needed</param>
        /// <param name="buffer">Container for sample items. <c>Clear()</c> will be called for this</param>
        /// <param name="random">The random number generator. If null, a temporary System.Random instance will be created</param>
        public static void QuickSample<T>(this ICollection<T> collection, int sampleCount, ICollection<T> buffer, Random random)
        {
            if (sampleCount > collection.Count)
            {
                throw new ArgumentException($"The number of sample ({sampleCount}) is higher than collection size ({collection.Count})");
            }

            random ??= new Random();
            buffer.Clear();
            var i = 0;

            foreach (var item in collection)
            {
                var takeChance = (float)(sampleCount - buffer.Count) / (collection.Count - i);

                if (random.NextDouble() <= takeChance)
                {
                    buffer.Add(item);
                }

                if (buffer.Count >= sampleCount)
                {
                    break;
                }

                ++i;
            }
        }
        
        #endregion

        /// <summary>
        /// Quickly sample <c>sampleCount</c> items from <c>collection</c> and put them into <c>buffer</c>.
        /// This function have O(n) runtime and will not allocated extra memory
        /// </summary>
        /// <param name="collection">The sample collection</param>
        /// <param name="sampleCount">The number of sample item needed</param>
        /// <param name="buffer">Container for sample items. <c>Clear()</c> will be called for this</param>
        /// <param name="testFunction">A function that recieves a float (between 0 to 1) represent the chance of an item be taken, and return whether that item should be taken</param>
        public static void QuickSample<T>(this ICollection<T> collection, int sampleCount, ICollection<T> buffer, Func<float, bool> testFunction)
        {
            if (sampleCount > collection.Count)
            {
                throw new ArgumentException($"The number of sample ({sampleCount}) is higher than collection size ({collection.Count})");
            }

            buffer.Clear();
            var i = 0;

            foreach (var item in collection)
            {
                var takeChance = (float)(sampleCount - buffer.Count) / (collection.Count - i);

                if (testFunction(takeChance))
                {
                    buffer.Add(item);
                }

                if (buffer.Count >= sampleCount)
                {
                    break;
                }

                ++i;
            }
        }

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
        public static void FastRemoveAt<T>(this List<T> list, int index)
        {
            (list[index], list[^1]) = (list[^1], list[index]);
            list.RemoveAt(list.Count - 1);
        }
    }
}