#if UNITY_COLLECTION_EXISTS
using System;
using Unity.Collections;

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

        public static void DisposeIfCreated<T>(this NativeArray<T> list) where T : unmanaged
        {
            if (list.IsCreated)
            {
                list.Dispose();
            }
        }

        public static void DisposeIfCreated<T>(this NativeQueue<T> list) where T : unmanaged
        {
            if (list.IsCreated)
            {
                list.Dispose();
            }
        }

        public static void DisposeIfCreated<T>(this NativeParallelHashSet<T> list) where T : unmanaged, IEquatable<T>
        {
            if (list.IsCreated)
            {
                list.Dispose();
            }
        }

        public static void DisposeIfCreated<TKey, TValue>(this NativeParallelHashMap<TKey, TValue> list)
            where TKey : unmanaged, IEquatable<TKey>
            where TValue : struct
        {
            if (list.IsCreated)
            {
                list.Dispose();
            }
        }

        public static void DisposeIfCreated<TKey, TValue>(this NativeParallelMultiHashMap<TKey, TValue> list)
            where TKey : unmanaged, IEquatable<TKey>
            where TValue : struct
        {
            if (list.IsCreated)
            {
                list.Dispose();
            }
        }
    }
}
#endif