namespace Vun.UnityUtils.GenericFSM
{
    /// <summary>
    /// A hierarchical state machine implementation of <see cref="IAutoState{T,T}"/>
    /// </summary>
    public abstract class AutoCompositeState<TContext, TState, TInnerState> : AutoState<TContext, TState>
    {
        protected abstract IUpdatableAutoStateMachine<TContext, TInnerState> SubStateMachine { get; }

        public override void Update(float deltaTime)
        {
            SubStateMachine.Update(deltaTime);
        }

        public void TransitionSubMachineTo(TInnerState state)
        {
            SubStateMachine.TransitionTo(state);
        }

        public virtual void Shutdown()
        {
            SubStateMachine.Shutdown();
        }

        public override string ToString()
        {
            return $"{GetType().Name}:{SubStateMachine.CurrentState}";
        }
    }

    public abstract class AutoCompositeState<TContext, TInnerState> :
        AutoCompositeState<TContext, IAutoState<TContext>, TInnerState>
        where TInnerState : IAutoState<TContext, TInnerState>
    { }

    public abstract class AutoCompositeState<TContext> :
        AutoCompositeState<TContext, IAutoState<TContext>, IAutoState<TContext>>,
        IAutoState<TContext>
    { }
}