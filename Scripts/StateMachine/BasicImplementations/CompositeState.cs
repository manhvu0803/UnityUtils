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

        public override void Enter(T context)
        {
            base.Enter(context);
            _subState = InitialState;
            _subState.Enter(context);
            _context = context;
        }

        public override void Update(float deltaTime)
        {
            _subState.Update(deltaTime);
        }

        public virtual void TransitionTo(IState<T> state)
        {
            _subState.Exit();
            _subState = state;
            _subState.Enter(_context);
        }

        public virtual void Shutdown()
        {
            _subState.Exit();
            _subState = default;
        }
    }
}