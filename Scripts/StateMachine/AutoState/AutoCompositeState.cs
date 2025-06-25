using System;

namespace Vun.UnityUtils.GenericFSM
{
    /// <summary>
    /// A hierarchical state machine implementation of <see cref="IAutoState{T,T}"/>
    /// </summary>
    public abstract class AutoCompositeState<TContext, TState, TInnerState> :
        AutoState<TContext, TState>,
        IAutoStateMachine<TContext, TInnerState>
        where TInnerState : IAutoState<TContext, TInnerState>
    {
        public new TContext Context => base.Context;

        public event Action<TInnerState> OnStateChanged;

        public event Action OnShutdown;

        public TInnerState CurrentState { get; private set; }

        protected abstract TInnerState InitialState { get; }

        public override void Enter(IAutoStateMachine<TContext, TState> stateMachine)
        {
            base.Enter(stateMachine);
            CurrentState = InitialState;
            CurrentState.Enter(this);
        }

        public override void Update(float deltaTime)
        {
            CurrentState.Update(deltaTime);
        }

        // Explicit implementation to avoid being accidentally called by subclasses
        void IStateMachine<TInnerState>.TransitionTo(TInnerState state)
        {
            CurrentState.Exit();
            CurrentState = state;
            CurrentState.Enter(this);
            OnStateChanged?.TryInvoke(state);
        }

        public virtual void Shutdown()
        {
            CurrentState.Exit();
            CurrentState = default;
        }

        public override string ToString()
        {
            return $"{GetType().Name}:{CurrentState.GetType().Name}";
        }
    }

    public abstract class AutoCompositeState<TContext, TInnerState> :
        AutoCompositeState<TContext,IAutoState<TContext>, TInnerState>
        where TInnerState : IAutoState<TContext, TInnerState>
    {

    }

    public abstract class AutoCompositeState<TContext> :
        AutoCompositeState<TContext, IAutoState<TContext>, IAutoState<TContext>>,
        IAutoState<TContext>
    {

    }
}