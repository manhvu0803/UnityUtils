using System.Collections.Generic;
using UnityEngine;
#if UNITY_6000_0_OR_NEWER
using System.Threading.Tasks;
#endif

namespace Vun.UnityUtils.Pooling
{
    /// <summary>
    /// Container for static methods of <see cref="GameObjectPool{T}"/>
    /// </summary>
    public static class GameObjectPool
    {
        public static GameObjectPool<T> CreatePool<T>(T prefab, int instanceCount, PoolEmptyBehaviour behaviour = PoolEmptyBehaviour.Recycle) where T : Component
        {
            var instances = new List<T>();

            for (var i = 0; i < instanceCount; i++)
            {
                instances.Add(Object.Instantiate(prefab));
            }

            return new GameObjectPool<T>(prefab, instances, behaviour);
        }

#if UNITY_6000_0_OR_NEWER
        public static async ValueTask<GameObjectPool<T>> CreatePoolAsync<T>(T prefab, int instanceCount, PoolEmptyBehaviour behaviour = PoolEmptyBehaviour.Recycle) where T : Component
        {
            var instances = await Object.InstantiateAsync(prefab);
            return new GameObjectPool<T>(prefab, instances, behaviour);
        }
#endif
    }

    /// <summary>
    /// To quickly create a new instance from a prefab, use <see cref="GameObjectPool.CreatePool"/> or <see cref="GameObjectPool.CreatePoolAsync"/>
    /// </summary>
    public class GameObjectPool<T> : UnityObjectPool<T> where T : Component
    {
        public GameObjectPool(T prefab, IEnumerable<T> items, PoolEmptyBehaviour behaviour = PoolEmptyBehaviour.Recycle) : base(prefab, items, behaviour) { }

        public override T Get()
        {
            var item = base.Get();
            item.gameObject.SetActive(true);
            return item;
        }

        public override void Return(T item)
        {
            item.gameObject.SetActive(false);
            base.Return(item);
        }
    }
}