using System.Collections.Generic;

namespace Vun.UnityUtils.Pooling
{
    /// <summary>
    /// An implementation of <see cref="IPool{T}"/> that tracks and automatically retrieve the oldest item when empty
    /// </summary>
    /// <remarks>This is slower and use more memory than <see cref="Pool{T}"/></remarks>
    public class AutoReturnPool<T> : IPool<T>
    {
        private readonly Queue<T> _items;

        // LinkedList is used instead of HashSet to preserve the order of removal
        private readonly LinkedList<T> _takenItems;

        public int ReserveCount => _items.Count;

        public AutoReturnPool(IEnumerable<T> items)
        {
            _items = new Queue<T>(items);
            _takenItems = new LinkedList<T>();
        }

        public T Get()
        {
            if (_items.Count <= 0)
            {
                return RecycleItem();
            }

            var item = _items.Dequeue();
            _takenItems.AddLast(item);
            return item;
        }

        private T RecycleItem()
        {
            var item = _takenItems.First.Value;
            _takenItems.AddLast(item);
            return item;
        }

        public void RetrieveAll()
        {
            foreach (var item in _takenItems)
            {
                _items.Enqueue(item);
            }

            _takenItems.Clear();
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
            _takenItems.Remove(item);
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