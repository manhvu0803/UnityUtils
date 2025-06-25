namespace Vun.UnityUtils.GenericFSM
{
    /// <summary>
    /// A more generic version of <see cref="IAutoState{TContext}"/>
    /// </summary>
    public interface IAutoState<in TContext, TState> : IState<IAutoStateMachine<TContext, TState>> { }

    /// <summary>
    /// Interface for states with the capability to automatically transition to another state
    /// (without explicit support from their context)
    /// </summary>
    public interface IAutoState<TContext> : IAutoState<TContext, IAutoState<TContext>> { }
}