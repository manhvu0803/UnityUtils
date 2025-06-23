using UnityEngine;

namespace Vun.UnityUtils
{
    /// <summary>
    /// An interface for a modular generator based on <see cref="ScriptableObject"/> for easy swapping
    /// </summary>
    public abstract class ScriptableCreator<T> : ScriptableObject
    {
        public abstract T CreateInstance();
    }
}