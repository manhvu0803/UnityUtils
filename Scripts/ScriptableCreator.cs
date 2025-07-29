using UnityEngine;

namespace Vun.UnityUtils
{
    /// <summary>
    /// An interface for a modular generator based on <see cref="ScriptableObject"/> for easy swapping
    /// </summary>
    public abstract class ScriptableCreator<T> : ScriptableObject, ICreator<T>
    {
        public abstract T Create();
    }

    /// <summary>
    /// An interface for a modular generator based on <see cref="ScriptableObject"/> for easy swapping
    /// </summary>
    public abstract class ScriptableCreator<TInput, TOutput> : ScriptableObject, ICreator<TInput, TOutput>
    {
        public abstract TOutput Create(TInput input);
    }
}