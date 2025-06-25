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
    public abstract class ScriptableCreator<TOutput, TInput> : ScriptableObject, ICreator<TOutput, TInput>
    {
        public abstract TOutput Create(TInput input);
    }
}