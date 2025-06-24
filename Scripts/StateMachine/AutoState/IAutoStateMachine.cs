using System;

namespace Vun.UnityUtils.GenericFSM
{
    /// <summary>
    /// A derived of <see cref="IStateMachine{TState}"/> with <see cref="Context"/> and event callbacks,
    /// intended for using with <see cref="IAutoState{T}"/>, which can control the state machine from inside
    /// </summary>
    /// <remarks>
    /// This is less flexible than <see cref="IStateMachine{TState}"/>, but has more features
    /// </remarks>
    public interface IAutoStateMachine<out TContext, TState> : IStateMachine<TState>
    {
        /// <summary>The target (host) of this state machine's states</summary>
        public TContext Context { get; }

        /// <summary>The current state of this machine</summary>
        public TState CurrentState { get; }

        /// <summary>Invoked when this state machine state is changed</summary>
        public event Action<TState> OnStateChanged;

        /// <summary>Invoked when this state machine is shut down (either from outside or inside)</summary>
        public event Action OnShutdown;
    }
}