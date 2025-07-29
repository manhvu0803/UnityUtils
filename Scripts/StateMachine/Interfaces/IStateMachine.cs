namespace Vun.UnityUtils.GenericFSM
{
    /// <summary>
    /// Simple state machine interface
    /// </summary>
    public interface IStateMachine<out TContext, in TState>
    {
        public TContext Context { get; }
        
        public void TransitionTo(TState state);

        /// <summary>
        /// Exit the current state of the machine itself.
        /// </summary>
        public void Shutdown();
    }
}