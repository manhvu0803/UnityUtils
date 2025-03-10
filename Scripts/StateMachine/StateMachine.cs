namespace Vun.UnityUtils.StateMachine
{
    public class StateMachine<T> : IStateMachine<T>
    {
        public T Owner { get; }

        private IState<T> _currentState;

        public StateMachine(T owner) : this(owner, new State<T>()) { }

        public StateMachine(T owner, IState<T> initialState)
        {
            Owner = owner;
            _currentState = initialState;
            initialState.OnEnter(this);
        }

        public void TransitionTo(IState<T> state)
        {
            _currentState.OnExit();
            _currentState = state;
            _currentState.OnEnter(this);
        }

        public void Update(float deltaTime)
        {
            _currentState.Update(deltaTime);
        }

        public void Exit()
        {
            _currentState.OnExit();
            _currentState = null;
        }
    }
}