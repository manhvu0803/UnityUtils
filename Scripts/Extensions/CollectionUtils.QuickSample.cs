﻿using System;
using System.Collections.Generic;

namespace Vun.UnityUtils
{
    public static partial class CollectionUtils
    {
        #region ICollection buffer and UnityEngine.Random

        /// <summary>
        /// Quickly sample <c>sampleCount</c> items from <c>collection</c> and put them into <c>buffer</c>.
        /// This function have O(n) runtime and will not allocate extra memory
        /// </summary>
        /// <param name="collection">The sample collection</param>
        /// <param name="sampleCount">The number of sample item needed</param>
        /// <param name="buffer">Container for sample items.
        /// Do not use fixed size and read-only collections, <see cref="ICollection{T}.Clear"/> and <see cref="ICollection{T}.Add"/> will be called for this
        /// </param>
        public static void QuickSample<T>(this ICollection<T> collection, int sampleCount, ICollection<T> buffer)
        {
            collection.QuickSample<T, ICollection<T>, ICollection<T>>(sampleCount, buffer);
        }

        /// <param name="buffer">Container for sample items.
        /// Do not use fixed size and read-only collections, <see cref="ICollection{T}.Clear"/> and <see cref="ICollection{T}.Add"/> will be called for this
        /// </param>
        public static void QuickSample<T, TCollection, TBuffer>(this TCollection collection, int sampleCount, TBuffer buffer)
            where TCollection : ICollection<T>
            where TBuffer : ICollection<T>
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
        /// Optimized version of <c>QuickSample</c> for <c>List</c> that avoids <c>GetEnumerator</c>.
        /// To avoid boxing with <c>struct</c> use <see cref="QuickSample{T,TCollection,TBuffer}(TCollection,int,TBuffer)"/>
        /// </summary>
        /// <param name="buffer">Container for sample items.
        /// Do not use fixed size and read-only collections, <see cref="ICollection{T}.Clear"/> and <see cref="ICollection{T}.Add"/> will be called for this
        /// </param>
        public static void QuickSample<T>(this IList<T> list, int sampleCount, ICollection<T> buffer)
        {
            if (sampleCount > list.Count)
            {
                throw new ArgumentException($"The number of sample ({sampleCount}) is bigger than list size ({list.Count})");
            }

            buffer.Clear();

            for (var i = 0; i < list.Count; ++i)
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

        #endregion

        #region Array buffer and UnityEngine.Random
        
        /// <summary>
        /// Quickly sample <c>sampleCount</c> items from <c>collection</c> and put them into <c>buffer</c>.
        /// This function have O(n) runtime and will not allocate extra memory
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
        public static void QuickSample<T>(this IList<T> list, int sampleCount, T[] buffer, int writeIndex = 0)
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
        /// This function have O(n) runtime and will not allocate extra memory
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
    }
}