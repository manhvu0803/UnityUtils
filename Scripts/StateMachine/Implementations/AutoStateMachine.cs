using System;

namespace Vun.UnityUtils.GenericFSM
{
    public class AutoStateMachine<TContext, TState> : IUpdatableStateMachine<TContext, TState>
        where TState : IState<IUpdatableStateMachine<TContext, TState>>
    {
        private event Action<TState> StateChanged;

        private event Action ShutdownCompleted;

        public TContext Context { get; }

        public TState CurrentState { get; set; }

        public AutoStateMachine(TContext context, TState initialState)
        {
            Context = context;
            CurrentState = initialState;
            initialState.Enter(this);
        }

        public void TransitionTo(TState state)
        {
            CurrentState.Exit(this);
            CurrentState = state;
            CurrentState.Enter(this);
            StateChanged?.Invoke(state);
        }

        public void Update(float deltaTime)
        {
            CurrentState.Update(this, deltaTime);
        }

        public void Shutdown()
        {
            CurrentState.Exit(this);
            CurrentState = default;
            ShutdownCompleted?.Invoke();
        }
    }
}