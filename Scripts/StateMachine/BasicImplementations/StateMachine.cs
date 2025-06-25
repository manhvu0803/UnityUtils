namespace Vun.UnityUtils.GenericFSM
{
    /// <summary>
    /// A simple implementation of <see cref="IUpdatable{TState}"/>
    /// </summary>
    public class StateMachine<T> : IStateMachine<IState<T>>
    {
        private readonly T _context;

        public IState<T> CurrentState { get; set; }

        public StateMachine(T context, IState<T> initialState)
        {
            _context = context;
            CurrentState = initialState;
            initialState.Enter(_context);
        }

        public void TransitionTo(IState<T> stateId)
        {
            CurrentState.Exit();
            CurrentState = stateId;
            CurrentState.Enter(_context);
        }

        public void Update(float deltaTime)
        {
            CurrentState.Update(deltaTime);
        }

        public void Shutdown()
        {
            CurrentState.Exit();
            CurrentState = default;
        }
    }
}