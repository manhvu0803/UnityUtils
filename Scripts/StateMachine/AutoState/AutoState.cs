namespace Vun.UnityUtils.GenericFSM
{
    /// <summary>
    /// An abstract implementation of <see cref="IAutoState"/>
    /// </summary>
    public abstract class AutoState<TContext, TState> : IAutoState<TContext, TState>
    {
        protected IAutoStateMachine<TContext, TState> StateMachine { get; private set; }

        /// <summary>The target (host) of this state</summary>
        protected TContext Context => StateMachine.Context;

        public virtual void Enter(IAutoStateMachine<TContext, TState> stateMachine)
        {
            StateMachine = stateMachine;
        }

        public virtual void Update(float deltaTime) { }

        protected void TransitionTo(TState state)
        {
            StateMachine.TransitionTo(state);
        }

        public virtual void Exit()
        {
            StateMachine = default;
        }
    }

    public class AutoState<TContext> : AutoState<TContext, IAutoState<TContext>>, IAutoState<TContext> { }
}