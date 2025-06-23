namespace Vun.UnityUtils.GenericFSM
{
    /// <summary>
    /// A simple implementation of <see cref="IStateMachine{TContext,TState}"/> with <see cref="IAutoState{T}"/>,
    /// which need to be manually updated
    /// </summary>
    public class StateMachineWithContext<T> : IStateMachine<T, IAutoState<T>>
    {
        public T Context { get; }

        public IAutoState<T> CurrentState { get; set; }

        public StateMachineWithContext(T context, IAutoState<T> initialState)
        {
            Context = context;
            CurrentState = initialState;
            initialState.OnEnter(this);
        }

        public void TransitionTo(IAutoState<T> state)
        {
            CurrentState.OnExit();
            CurrentState = state;
            CurrentState.OnEnter(this);
        }

        public void Update(float deltaTime)
        {
            CurrentState.Update(deltaTime);
        }

        public void Exit()
        {
            CurrentState.OnExit();
            CurrentState = default;
        }
    }
}