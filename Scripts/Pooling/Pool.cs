using System;
using System.Collections.Generic;

namespace Vun.UnityUtils.Pooling
{
    /// <summary>
    /// A simple implementation of <see cref="IPool{T}"/>
    /// </summary>
    public class Pool<T> : IPool<T>
    {
        private readonly Queue<T> _items;

        public int ReserveCount => _items.Count;

        public Pool(IEnumerable<T> items)
        {
            _items = new Queue<T>(items);
        }

        public T Get()
        {
            if (_items.Count <= 0)
            {
                throw new Exception($"{this} is empty");
            }

            var item = _items.Dequeue();
            return item;
        }

        /// <summary>
        /// Return <c>item</c> to the pool
        /// </summary>
        /// <remarks>
        /// This method doesn't check if this item is originally from this pool or not,
        /// so it could be used to expand the pool
        /// </remarks>
        public void Return(T item)
        {
            _items.Enqueue(item);
        }

        // Re-implementation of default interface method for convenience
        public void Return(IEnumerable<T> items)
        {
            IPool<T> pool = this;
            pool.Return(items);
        }
    }
}