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
        public void Shutdown();
    }
}