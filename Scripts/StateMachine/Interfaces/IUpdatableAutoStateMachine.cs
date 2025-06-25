namespace Vun.UnityUtils.GenericFSM
{
    public interface IUpdatableAutoStateMachine<out TContext, TState> : IUpdatable, IAutoStateMachine<TContext, TState> { }

    public interface IUpdatableAutoStateMachine<TContext> : IUpdatable, IAutoStateMachine<TContext> { }
}