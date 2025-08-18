namespace Vun.UnityUtils.GenericFSM
{
    public class AutoState<TContext, TState> : State<IStateMachine<TContext, TState>>
    {
        protected new TContext Context => StateMachine.Context;

        protected IStateMachine<TContext, TState> StateMachine => base.Context;
    }
}