namespace Vun.UnityUtils.GenericFSM
{
    /// <summary>
    /// A stateful hierarchical state machine implementation of <see cref="IState"/>
    /// </summary>
    public class CompositeState<TContext, TSubState> : State<TContext>, IStateMachine<TContext, TSubState>
        where TSubState : IState<TContext>
    {
        private TSubState _subState;

        TContext IStateMachine<TContext, TSubState>.Context => base.Context;

        public CompositeState(TSubState initialState)
        {
            _subState = initialState;
        }

        protected override void Enter()
        {
            _subState.Enter(Context);
        }

        protected override void Update(float deltaTime)
        {
            _subState.Update(Context, deltaTime);
        }

        public virtual void TransitionTo(TSubState state)
        {
            _subState.Exit(Context);
            _subState = state;
            _subState.Enter(Context);
        }

        public virtual void Shutdown()
        {
            Exit();
            base.Exit(Context);
        }

        protected override void Exit()
        {
            _subState.Exit(Context);
            _subState = default;
        }
    }
}