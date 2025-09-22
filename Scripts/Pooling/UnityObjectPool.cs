using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;
#if UNITY_6000_0_OR_NEWER
using System.Threading.Tasks;
#endif

namespace Vun.UnityUtils.Pooling
{
    /// <summary>
    /// Container for static methods of <see cref="UnityObjectPool{T}"/>
    /// </summary>
    public static class UnityObjectPool
    {
        public static UnityObjectPool<T> CreatePool<T>(T prefab, int instanceCount, PoolEmptyBehaviour behaviour = PoolEmptyBehaviour.Recycle) where T : Object
        {
            var instances = new List<T>();

            for (var i = 0; i < instanceCount; i++)
            {
                instances.Add(Object.Instantiate(prefab));
            }

            return new UnityObjectPool<T>(prefab, instances, behaviour);
        }

#if UNITY_6000_0_OR_NEWER
        public static async ValueTask<UnityObjectPool<T>> CreatePoolAsync<T>(T prefab, int instanceCount, PoolEmptyBehaviour behaviour = PoolEmptyBehaviour.Recycle) where T : Object
        {
            var instances = await Object.InstantiateAsync(prefab);
            return new UnityObjectPool<T>(prefab, instances, behaviour);
        }
#endif
    }

    /// <summary>
    /// To quickly create a new instance from a prefab, use <see cref="UnityObjectPool.CreatePool"/> or <see cref="UnityObjectPool.CreatePoolAsync"/>
    /// </summary>
    public class UnityObjectPool<T> : IPool<T>, IDisposable where T : Object
    {
        private readonly IPool<T> _pool;

        public int ReserveCount => _pool.ReserveCount;

        public readonly PoolEmptyBehaviour Behaviour;

        public uint ExpandCount { get; set; } = 5;

        protected readonly T Prefab;

        public UnityObjectPool(T prefab, IEnumerable<T> items, PoolEmptyBehaviour behaviour = PoolEmptyBehaviour.Recycle)
        {
            Prefab = prefab;
            Behaviour = behaviour;

            if (behaviour == PoolEmptyBehaviour.Recycle)
            {
                _pool = new AutoReturnPool<T>(items);
            }
            else
            {
                _pool = new Pool<T>(items);
            }
        }

        public virtual T Get()
        {
            if (ReserveCount > 0 || Behaviour != PoolEmptyBehaviour.Expand)
            {
                return _pool.Get();
            }

            for (var i = 0; i < ExpandCount; i++)
            {
                var item = Object.Instantiate(Prefab);
                _pool.Return(item);
            }

            return _pool.Get();
        }

        public virtual void Return(T item)
        {
            _pool.Return(item);
        }

        /// <summary>
        /// Call <see cref="DestroyAllInstances"/> with <c>false</c>
        /// </summary>
        public void Dispose()
        {
            DestroyAllInstances(immediately: false);
        }

        /// <summary>
        /// Destroy all instances in reserve.
        /// Doesn't do anything with instances taken from the pool prior
        /// </summary>
        /// <param name="immediately">If <c>true</c>, force instances to be destroyed with <see cref="UnityEngine.Object.DestroyImmediate"/></param>
        public void DestroyAllInstances(bool immediately)
        {
            for (var i = _pool.ReserveCount - 1; i >= 0; i--)
            {
                var item = _pool.Get();

                if (immediately)
                {
                    Object.DestroyImmediate(item);
                }
                else
                {
                    item.DestroyUnconditionally();
                }
            }
        }

        // Re-implementation of default interface method for convenience
        public void Return(IEnumerable<T> items)
        {
            IPool<T> pool = this;
            pool.Return(items);
        }
    }
}