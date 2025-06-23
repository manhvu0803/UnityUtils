namespace Vun.UnityUtils.GenericFSM
{
    /// <summary>
    /// A hierarchical state machine implementation of <see cref="IAutoState"/>
    /// </summary>
    public abstract class CompositeAutoState<T> : AutoState<T>, IStateMachine<T, IAutoState<T>>
    {
        public new T Context => base.Context;

        private IAutoState<T> _subState;

        protected abstract IAutoState<T> InitialState { get; }

        public override void OnEnter(IStateMachine<T, IAutoState<T>> stateMachine)
        {
            base.OnEnter(stateMachine);
            _subState = InitialState;
            _subState.OnEnter(this);
        }

        public override void Update(float deltaTime)
        {
            _subState.Update(deltaTime);
        }

        // Explicit implementation to avoid being accidentally called by subclasses
        void IStateMachine<IAutoState<T>>.TransitionTo(IAutoState<T> state)
        {
            _subState.OnExit();
            _subState = state;
            _subState.OnEnter(this);
        }

        public virtual void Exit()
        {
            _subState.OnExit();
            _subState = default;
        }

        public override string ToString()
        {
            return $"{GetType().Name}:{_subState}";
        }
    }
}