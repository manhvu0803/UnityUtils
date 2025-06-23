namespace Vun.UnityUtils.GenericFSM
{
    /// <summary>
    /// A simple implementation of <see cref="IStateMachine{T}"/> that need to be manually updated
    /// </summary>
    public class StateMachine<T> : IStateMachine<IState<T>>
    {
        private readonly T _context;

        public IState<T> CurrentState { get; set; }

        public StateMachine(T context, IState<T> initialState)
        {
            _context = context;
            CurrentState = initialState;
            initialState.OnEnter(_context);
        }

        public void TransitionTo(IState<T> state)
        {
            CurrentState.OnExit();
            CurrentState = state;
            CurrentState.OnEnter(_context);
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