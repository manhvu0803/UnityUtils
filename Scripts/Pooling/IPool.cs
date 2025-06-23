using System.Collections.Generic;

namespace Vun.UnityUtils.Pooling
{
    public interface IPool<T>
    {
        /// <summary>
        /// The number of instances in the pool
        /// </summary>
        public int ReserveCount { get; }

        /// <summary>
        /// Take an instance from the pool
        /// </summary>
        public T Get();

        /// <summary>
        /// Return <c>item</c> to the pool
        /// </summary>
        public void Return(T item);

        /// <summary>
        /// Return <c>items</c> to the pool
        /// </summary>
        public void Return(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                Return(item);
            }
        }
    }
}