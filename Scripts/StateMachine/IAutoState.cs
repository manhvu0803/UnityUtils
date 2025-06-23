namespace Vun.UnityUtils.GenericFSM
{
    /// <summary>
    /// Interface for states with the capability to automatically transition to another state
    /// (without explicit support from their context)
    /// </summary>
    public interface IAutoState<T> : IState<IStateMachine<T, IAutoState<T>>>
    {
        public void TransitionTo(IAutoState<T> state);
    }
}