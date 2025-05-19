#if UNITY_COLLECTION_EXISTS
using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Vun.UnityUtils
{
    public static class NativeCollectionUtils
    {
        public static void DisposeIfCreated<T>(this NativeList<T> list) where T : unmanaged
        {
            if (list.IsCreated)
            {
                list.Dispose();
            }
        }

        public static void DisposeIfCreated<T>(this UnsafeList<T> list) where T : unmanaged
        {
            if (list.IsCreated)
            {
                list.Dispose();
            }
        }

        public static void DisposeIfCreated(this NativeStream stream)
        {
            if (stream.IsCreated)
            {
                stream.Dispose();
            }
        }

        public static void DisposeIfCreated<T>(this NativeArray<T> array) where T : unmanaged
        {
            if (array.IsCreated)
            {
                array.Dispose();
            }
        }

        public static void DisposeIfCreated<T>(this NativeQueue<T> queue) where T : unmanaged
        {
            if (queue.IsCreated)
            {
                queue.Dispose();
            }
        }

#if UNITY_COLLECTION_FROM_1_3
        public static void DisposeIfCreated<T>(this NativeParallelHashSet<T> hashSet) where T : unmanaged, IEquatable<T>
        {
            if (hashSet.IsCreated)
            {
                hashSet.Dispose();
            }
        }

        public static void DisposeIfCreated<T>(this UnsafeParallelHashSet<T> hashSet) where T : unmanaged, IEquatable<T>
        {
            if (hashSet.IsCreated)
            {
                hashSet.Dispose();
            }
        }

        public static void DisposeIfCreated<TKey, TValue>(this NativeParallelHashMap<TKey, TValue> hashMap)
            where TKey : unmanaged, IEquatable<TKey>
            where TValue : unmanaged
        {
            if (hashMap.IsCreated)
            {
                hashMap.Dispose();
            }
        }

        public static void DisposeIfCreated<TKey, TValue>(this UnsafeParallelHashMap<TKey, TValue> hashMap)
            where TKey : unmanaged, IEquatable<TKey>
            where TValue : unmanaged
        {
            if (hashMap.IsCreated)
            {
                hashMap.Dispose();
            }
        }

        public static void DisposeIfCreated<TKey, TValue>(this NativeParallelMultiHashMap<TKey, TValue> hashMap)
            where TKey : unmanaged, IEquatable<TKey>
            where TValue : unmanaged
        {
            if (hashMap.IsCreated)
            {
                hashMap.Dispose();
            }
        }

        public static void DisposeIfCreated<TKey, TValue>(this UnsafeParallelMultiHashMap<TKey, TValue> hashMap)
            where TKey : unmanaged, IEquatable<TKey>
            where TValue : unmanaged
        {
            if (hashMap.IsCreated)
            {
                hashMap.Dispose();
            }
        }
#else
        public static void DisposeIfCreated<T>(this NativeHashSet<T> hashSet) where T : unmanaged, IEquatable<T>
        {
            if (hashSet.IsCreated)
            {
                hashSet.Dispose();
            }
        }

        public static void DisposeIfCreated<T>(this UnsafeHashSet<T> hashSet) where T : unmanaged, IEquatable<T>
        {
            if (hashSet.IsCreated)
            {
                hashSet.Dispose();
            }
        }

        public static void DisposeIfCreated<TKey, TValue>(this NativeHashMap<TKey, TValue> hashMap)
            where TKey : unmanaged, IEquatable<TKey>
            where TValue : unmanaged
        {
            if (hashMap.IsCreated)
            {
                hashMap.Dispose();
            }
        }

        public static void DisposeIfCreated<TKey, TValue>(this UnsafeHashMap<TKey, TValue> hashMap)
            where TKey : unmanaged, IEquatable<TKey>
            where TValue : unmanaged
        {
            if (hashMap.IsCreated)
            {
                hashMap.Dispose();
            }
        }

        public static void DisposeIfCreated<TKey, TValue>(this NativeMultiHashMap<TKey, TValue> hashMap)
            where TKey : unmanaged, IEquatable<TKey>
            where TValue : unmanaged
        {
            if (hashMap.IsCreated)
            {
                hashMap.Dispose();
            }
        }

        public static void DisposeIfCreated<TKey, TValue>(this UnsafeMultiHashMap<TKey, TValue> hashMap)
            where TKey : unmanaged, IEquatable<TKey>
            where TValue : unmanaged
        {
            if (hashMap.IsCreated)
            {
                hashMap.Dispose();
            }
        }
#endif
    }
}
#endif