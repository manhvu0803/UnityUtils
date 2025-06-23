namespace Vun.UnityUtils.GenericFSM
{
    /// <summary>
    /// Simple state machine interface
    /// </summary>
    public interface IStateMachine<in TState>
    {
        public void TransitionTo(TState state);

        /// <summary>
        /// Exit the current state if the machine itself.
        /// The state machine should be considered invalid after this
        /// </summary>
        public void Exit();
    }

    /// <summary>
    /// <see cref="IStateMachine{TState}"/> with <see cref="Context"/>
    /// </summary>
    public interface IStateMachine<out TContext, in TState> : IStateMachine<TState>
    {
        /// <summary>The target (host) of this state machine's states</summary>
        public TContext Context { get; }
    }
}