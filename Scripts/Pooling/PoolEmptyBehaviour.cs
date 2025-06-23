namespace Vun.UnityUtils.Pooling
{
    /// <summary>
    /// Behaviour of an <see cref="UnityEngine.Object"/> pool when its empty.
    /// Use with <see cref="UnityObjectPool{T}"/> or <see cref="GameObjectPool{T}"/>
    /// </summary>
    public enum PoolEmptyBehaviour
    {
        /// <summary>
        /// Throw an exception
        /// </summary>
        ThrowException,

        /// <summary>
        /// Automatically retrieve the oldest item
        /// </summary>
        Recycle,

        /// <summary>
        /// Automatically instantiate more objects to use
        /// </summary>
        Expand
    }
}