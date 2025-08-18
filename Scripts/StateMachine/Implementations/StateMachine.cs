namespace Vun.UnityUtils.GenericFSM
{
    /// <summary>
    /// A simple implementation of <see cref="IUpdatableStateMachine{TState}"/>
    /// </summary>
    public class StateMachine<TContext, TState> : IUpdatableStateMachine<TContext, TState>
        where TState : IState<TContext>
    {
        public TContext Context { get; }

        public TState CurrentState { get; set; }

        public StateMachine(TContext context, TState initialState)
        {
            Context = context;
            CurrentState = initialState;
            initialState.Enter(Context);
        }

        public void TransitionTo(TState stateId)
        {
            CurrentState.Exit(Context);
            CurrentState = stateId;
            CurrentState.Enter(Context);
        }

        public void Update(float deltaTime)
        {
            CurrentState.Update(Context, deltaTime);
        }

        public void Shutdown()
        {
            CurrentState.Exit(Context);
            CurrentState = default;
        }
    }

    public class StateMachine<TContext> : StateMachine<TContext, IState<TContext>>
    {
        public StateMachine(TContext context, IState<TContext> initialState) : base(context, initialState) { }
    }
}