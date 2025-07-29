using System;

namespace Vun.UnityUtils.GenericFSM
{
    public class AutoStateMachine<TContext, TState> : IUpdatableStateMachine<TContext, TState>
        where TState : IState<IUpdatableStateMachine<TContext, TState>>
    {
        private event Action<TState> StateTransitioned;

        private event Action AfterShutdown;

        public TContext Context { get; }

        public TState CurrentState { get; set; }

        public AutoStateMachine(TContext context, TState initialState)
        {
            Context = context;
            CurrentState = initialState;
            initialState.Enter(this);
        }

        public void TransitionTo(TState stateId)
        {
            CurrentState.Exit();
            CurrentState = stateId;
            CurrentState.Enter(this);
            StateTransitioned?.Invoke(stateId);
        }

        public void Update(float deltaTime)
        {
            CurrentState.Update(deltaTime);
        }

        public void Shutdown()
        {
            CurrentState.Exit();
            CurrentState = default;
            AfterShutdown?.Invoke();
        }
    }
}