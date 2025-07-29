namespace Vun.UnityUtils.GenericFSM
{
    /// <summary>
    /// A hierarchical state machine implementation of <see cref="IState"/>
    /// </summary>
    public abstract class CompositeState<TContext, TSubState> : State<TContext>, IStateMachine<TContext, TSubState>
        where TSubState : IState<TContext>
    {
        private TSubState _subState;

        TContext IStateMachine<TContext, TSubState>.Context => base.Context;

        protected abstract TSubState InitialState { get; }

        public override void Enter(TContext context)
        {
            base.Enter(context);
            _subState = InitialState;
            _subState.Enter(context);
        }

        public override void Update(float deltaTime)
        {
            _subState.Update(deltaTime);
        }

        public virtual void TransitionTo(TSubState state)
        {
            _subState.Exit();
            _subState = state;
            _subState.Enter(Context);
        }

        public virtual void Shutdown()
        {
            _subState.Exit();
            _subState = default;
        }
    }
}