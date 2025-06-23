namespace Vun.UnityUtils.GenericFSM
{
    /// <summary>
    /// A hierarchical state machine implementation of <see cref="IState"/>
    /// </summary>
    public abstract class CompositeState<T> : State<T>, IStateMachine<IState<T>>
    {
        private IState<T> _subState;

        private T _context;

        protected abstract IState<T> InitialState { get; }

        public override void OnEnter(T context)
        {
            base.OnEnter(context);
            _subState = InitialState;
            _subState.OnEnter(context);
            _context = context;
        }

        public override void Update(float deltaTime)
        {
            _subState.Update(deltaTime);
        }

        public virtual void TransitionTo(IState<T> state)
        {
            _subState.OnExit();
            _subState = state;
            _subState.OnEnter(_context);
        }

        public virtual void Exit()
        {
            _subState.OnExit();
            _subState = default;
        }
    }
}