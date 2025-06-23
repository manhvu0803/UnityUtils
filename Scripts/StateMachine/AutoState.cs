namespace Vun.UnityUtils.GenericFSM
{
    /// <summary>
    /// An abstract implementation of <see cref="IAutoState"/>
    /// </summary>
    public abstract class AutoState<T> : IAutoState<T>
    {
        protected IStateMachine<T, IAutoState<T>> StateMachine { get; private set; }

        /// <summary>The target (host) of this state</summary>
        protected T Context => StateMachine.Context;

        public virtual void OnEnter(IStateMachine<T, IAutoState<T>> stateMachine)
        {
            StateMachine = stateMachine;
        }

        public virtual void Update(float deltaTime) { }

        public void TransitionTo(IAutoState<T> state)
        {
            StateMachine.TransitionTo(state);
        }

        public virtual void OnExit()
        {
            StateMachine = default;
        }
    }
}